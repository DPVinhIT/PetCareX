use PetCareX_DB
GO 

--0. AD1: Đăng ký tài khoản và quên mật khẩu
CREATE OR ALTER PROC dbo.spDangKyTaiKhoan
(
	@Username VARCHAR(20),
    @Password NVARCHAR(255),
	@Fullname NVARCHAR(100),
	@Email VARCHAR(100),
    @CCCD VARCHAR(20),
    @Phone VARCHAR(15),
    @Gender NVARCHAR(10),
	@Birthday DATE
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Validate dữ liệu
        IF (
            @Username IS NULL OR LTRIM(RTRIM(@Username)) = '' OR
            @Email IS NULL OR LTRIM(RTRIM(@Email)) = '' OR
            @CCCD IS NULL OR LTRIM(RTRIM(@CCCD)) = '' OR
            @Phone IS NULL OR LTRIM(RTRIM(@Phone)) = '' OR
            @Password IS NULL OR LTRIM(RTRIM(@Password)) = '' OR
			@Gender IS NULL OR @Birthday IS NULL
        )
        BEGIN
            ;THROW 51006, N'Thông tin nhập vào không hợp lệ', 1;
        END
		--Check đã tồn tại username tương tự chưa
		IF EXISTS (SELECT al.Username FROM AccountLogin al WHERE al.Username = @Username)
		BEGIN
			;THROW 51007, N'Username này đã tồn tại', 1;
		END
		--Kiểm tra xem CCCD có bị trùng không
		IF EXISTS (SELECT c.CustomerID FROM Customer c WHERE c.CCCD = @CCCD)
		BEGIN
			;THROW 51001, N'Đã có người sử dụng số căn cước công dân này để đăng ký tài khoản, xin vui lòng kiểm tra lại.',1;		 
		END
		--Tạo ID cho khách hàng mới 
		DECLARE @maxID VARCHAR(20)  
		DECLARE @nextID VARCHAR(20)
		SELECT @maxID = MAX(PetID) FROM Pet 
		--Nếu chưa có ID khách hàng nào được tạo
		IF @maxID IS NULL
		BEGIN
			SET @nextID = 'CUS00001'
		END
		--Nếu đã tồn tại mã khách hàng từ trước trONg hệ thống
		ELSE 
		BEGIN 
			--Tạo mã khách hàng tăng dần
			DECLARE @num INT = CAST(RIGHT(@maxID, 5) AS INT) + 1;
			SET @nextID = 'CUS' + RIGHT('00000' + CAST(@num AS VARCHAR(10)), 5)
		END
		--INSERT thông tin khách hàng vào bảng
		INSERT INTO AccountLogin(Username, Password)
		VALUES (@Username, @Password)
		INSERT INTO Customer(CustomerID, FullName, PhoneNumber, Email, CCCD, Gender, Birthday, Username)
		VALUES (@nextID, @Fullname, @Phone, @Email, @CCCD, @Gender, @Birthday, @Username)
		COMMIT TRAN
	END TRY
	BEGIN CATCH

	END CATCH
END
GO
CREATE PROC sp_ForgotPasswordE
    @Username VARCHAR(20),
    @Email VARCHAR(100),
    @CCCD VARCHAR(20),
    @Phone VARCHAR(15),
    @NewPassword NVARCHAR(255),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- 1. Validate dữ liệu
        IF (
            @Username IS NULL OR LTRIM(RTRIM(@Username)) = '' OR
            @Email IS NULL OR LTRIM(RTRIM(@Email)) = '' OR
            @CCCD IS NULL OR LTRIM(RTRIM(@CCCD)) = '' OR
            @Phone IS NULL OR LTRIM(RTRIM(@Phone)) = '' OR
            @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = ''
        )
        BEGIN
            SET @result = -2; -- Thiếu dữ liệu
            RETURN;
        END

        -- 2. Check Username tồn tại trong AccountLogin
        IF NOT EXISTS (SELECT 1 FROM AccountLogin WHERE Username = @Username)
        BEGIN
            SET @result = -3; -- Username không tồn tại
            RETURN;
        END
        
        -- 3. Check thông tin có khớp cùng 1 khách hàng không
        IF NOT EXISTS (
            SELECT 1
            FROM Customer
            WHERE Username = @Username
              AND Email = @Email
              AND CCCD = @CCCD
              AND PhoneNumber = @Phone
        )
        BEGIN
            SET @result = 0; -- Thông tin không khớp
            RETURN;
        END

        BEGIN TRAN;

        -- 4. Reset mật khẩu
        UPDATE AccountLogin
        SET Password = @NewPassword
        WHERE Username = @Username;

        COMMIT;
        SET @result = 1; -- Reset thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- =============================
-- CHỨC NĂNG: Quên mật khẩu
-- PROCEDURE: sp_ForgotPassword
-- Tham số: @Username, @Email, @CCCD, @Phone, @NewPassword, @result OUTPUT
-- Trả về: 1 nếu thành công, 0 nếu thông tin không khớp, -2 thiếu dữ liệu, -3 username không tồn tại
-- Ví dụ EXEC:
--   DECLARE @result INT;
--   EXEC sp_ForgotPassword @Username = 'user1', @Email = 'a@email.com', @CCCD = '123456789', @Phone = '0123456789', @NewPassword = 'newpass', @result = @result OUTPUT;
--   SELECT @result AS Result;
-- =============================
--1. AD2: Quản lý hồ sơ cá nhân - Xem hồ sơ
CREATE OR ALTER FUNCTION dbo.udf_XemHoSo 
(
	@CusID VARCHAR(20)
)
RETURNS TABLE
AS
RETURN (
	SELECT *
	FROM Customer c
	WHERE c.CustomerID = @CusID
)

GO
--2. AD2: Quản lý hồ sơ cá nhân - Thay đổi hồ sơ
CREATE OR ALTER PROC dbo.udf_ThayDoiHoSo
(
	@CusID		  VARCHAR(20),
    @FullName     NVARCHAR(100) = NULL,
    @PhoneNumber  VARCHAR(15) = NULL,
    @Email        VARCHAR(100) = NULL,
    @CCCD         VARCHAR(20) = NULL,
    @Gender       NVARCHAR(10) = NULL,
    @Birthday     DATE
)
AS 
BEGIN 
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem Khách hàng này có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		--Kiểm tra xem CCCD có bị trùng không
		IF EXISTS (SELECT c.CustomerID FROM Customer c WHERE c.CCCD = @CCCD AND c.CustomerID <> @CusID)
		BEGIN
			;THROW 51001, N'Đã có người sử dụng số căn cước công dân này để đăng ký tài khoản, xin vui lòng kiểm tra lại.',1;		 
		END
		--Cập nhật các thông tin đã thay đổi
		update Customer
		SET FullName = ISNULL(@FullName, FullName),
			PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber),
			Email = ISNULL(@Email, Email),
			CCCD = ISNULL(@CCCD, CCCD),
			Gender = ISNULL(@Gender, Gender),
			Birthday = ISNULL(@Birthday, Birthday)
		WHERE CustomerID = @CusID
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END;

GO

--3. AD3: Quản lý hồ sơ thú cưng - Xem danh sách hồ sơ thú cưng
CREATE OR ALTER FUNCTION dbo.udf_XemDanhSachHoSoThuCung
(
	@CusID VARCHAR(20)
)
RETURNS TABLE
AS 
RETURN 
(
	SELECT PetID, PetName, Species, Breed, Birthday, Gender, HealthStatus 
	FROM Pet p
	WHERE p.CustomerID = @CusID
)

GO

CREATE OR ALTER PROC dbo.sp_XemDanhSachHoSoThuCung
(
	@CusID VARCHAR(20)
)
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		SELECT * FROM dbo.udf_XemDanhSachHoSoThuCung(@CusID)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--4. AD3: Quản lý hồ sơ thú cưng - Cập nhật hồ sơ thú cưng
CREATE OR ALTER PROC dbo.sp_CapNhatHoSoThuCung
(
	@PetID        VARCHAR(20) = NULL,
	@CusID		  VARCHAR(20) = NULL,
    @PetName      NVARCHAR(50) = NULL,
    @Species      NVARCHAR(50) = NULL,
    @Breed        NVARCHAR(50) = NULL,
    @Birthday     DATE = NULL,
    @Gender       NVARCHAR(10) = NULL,
    @HealthStatus NVARCHAR(255) = NULL
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem thú cưng có tồn tại trONg hệ thống hay không
		IF NOT EXISTS (SELECT p.PetID FROM Pet p WHERE p.PetID = @PetID)
		BEGIN
			;THROW 51002, N'Hệ thống không tồn tại mã thú cưng này, xin vui lòng kiểm tra lại.', 1;
		END
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		update Pet
		SET PetName = ISNULL(@PetName, PetName),
			Species = ISNULL(@Species, Species),
			Breed = ISNULL(@Breed, Breed),
			Birthday = ISNULL(@Birthday, Birthday),
			Gender = ISNULL(@Gender, Gender),
			HealthStatus = ISNULL(@HealthStatus, HealthStatus)
		WHERE PetID = @PetID AND CustomerID = @CusID 
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--5. AD3: Quản lý hồ sơ thú cưng - Thêm mới hồ sơ thú cưng
CREATE OR ALTER PROC dbo.sp_ThemMoiHoSoThuCung
(
    @CusID		  VARCHAR(20),
    @PetName      NVARCHAR(50),
    @Species      NVARCHAR(50),
    @Breed        NVARCHAR(50),
    @Birthday     DATE,
    @Gender       NVARCHAR(10),
    @HealthStatus NVARCHAR(255)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		--Tạo mã thú cưng mới 
		DECLARE @maxID VARCHAR(20)  
		DECLARE @nextID VARCHAR(20)
		SELECT @maxID = MAX(PetID) FROM Pet 
		--Nếu chưa có mã thú cưng nào được tạo
		IF @maxID IS NULL
		BEGIN
			SET @nextID = 'PET00001'
		END
		--Nếu đã tồn tại mã thú cưng từ trước trONg hệ thống
		ELSE 
		BEGIN 
			--Tạo mã thú cưng tăng dần
			DECLARE @num INT = CAST(RIGHT(@maxID, 5) AS INT) + 1;
			SET @nextID = 'PET' + RIGHT('00000' + CAST(@num AS VARCHAR(10)), 5)
		END
		INSERT INTO Pet(PetID, CustomerID, PetName, Species, Breed, Birthday, Gender, HealthStatus)
		VALUES (@nextID, @CusID, @PetName, @Species, @Breed, @Birthday, @Gender, @HealthStatus)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--6. AD4: Xem hạng thành viên, điểm tích lũy
CREATE OR ALTER FUNCTION dbo.udf_XemHangThanhVien
(
	@CusID VARCHAR(20)
)
RETURNS TABLE
AS
RETURN 
(
	SELECT cms.CustomerID, cms.LoyalPoINT, msl.LevelName, msl.DIScountRate 
	FROM CardMembership cms
	JOIN MembershipLevel msl ON cms.LevelID = msl.LevelID
	WHERE cms.CustomerID = @CusID AND year(cms.RegIStratiONDate) = year(getdate())
)

GO

CREATE OR ALTER PROC dbo.sp_XemHangThanhVien
(
	@CusID VARCHAR(20)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		--Sao chép dữ liệu từ hàm XemHangThanhVien vào bảng tạm
		SELECT * INTO #res FROM dbo.udf_XemHangThanhVien(@CusID)
		--Kiểm tra xem khách hàng đã được đăng ký thẻ từ trước hay chưa
		IF ((SELECT * FROM #res) IS NULL)
		BEGIN
			;THROW 51003, N'Khách hàng này chưa được tạo thẻ thành viên trONg năm nay', 1;
		END
		ELSE
		BEGIN
			SELECT * FROM #res
		END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--7. AD5: Đăng ký gói tiêm phòng - Xem danh sách các gói tiêm phòng
CREATE OR ALTER FUNCTION dbo.udf_XemDanhSachCacGOiTiemPhONg
()
RETURNS TABLE
AS
RETURN 
(
	SELECT * 
	FROM VaccinatiONPackage
)
GO
CREATE OR ALTER PROC dbo.sp_XemDanhSachCacGOiTiemPhONg
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra hiện tại hệ thống có đang có gói tiêm phòng nào khả dụng không
		--Gán dữ liệu từ hàm XemDanhSachCacGOiTiemPhONg() vào bảng tạm #res
		SELECT *
		INTO #res
		FROM udf_XemDanhSachCacGOiTiemPhONg()
		IF (SELECT * FROM #res) IS NULL
		BEGIN 
			;THROW 51004, N'Hiện tại hệ thống không có gói tiêm phòng nào khả dụng', 1;
		END 
		ELSE 
		BEGIN
			SELECT * FROM #res
		END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--8. AD5: Đăng ký gói tiêm phòng - Lựa chọn đăng ký gói tiêm phòng cho thú cưng (Cần tạo thêm TABLE ORderService(ORderID, CREATEDate, CREATETime, CustomerID) và DetailORderService(ORderID, ServiceID))
CREATE OR ALTER PROC dbo.sp_DangKyGOiTiemPhONg
(
	@CusID VARCHAR(20),
	@VPID VARCHAR(20),
	@ORderID VARCHAR(20) = NULL
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		--Kiểm tra gói tiêm phòng có tồn tại hay không
		IF NOT EXISTS(SELECT vp.VPID FROM VaccinatiONPackage vp WHERE vp.VPID = @VPID)
		BEGIN 
			;THROW 51006, N'Hệ thống không tồn tại mã gói tiêm phòng này, xin vui lòng kiểm tra lại', 1;
		END
		--Kiểm tra xem phiên mua hàng này của khách hàng đã có ORder nào được tạo chưa
		IF @ORderID IS NULL
		BEGIN
			--Tạo ID mới nếu khách hàng chưa có ORder
			DECLARE @maxID VARCHAR(20) = (SELECT MAX(OrderServiceID) FROM OrderService)
			--Nếu chưa có ORderID nào từng được tạo (ORderID hiện tại là cái đầu tiên của hệ thống)
			IF @maxID IS NULL
			BEGIN
				SET @ORderID = 'ORD000001'
			END
			--Nếu đã tồn tại ORderID từ trước trONg hệ thống (các khách hàng trước đã tạo)
			ELSE 
			BEGIN 
				--Tạo ORderID tăng dần
				DECLARE @num INT = CAST(RIGHT(@maxID, 6) AS INT) + 1;
				SET @ORderID = 'ORD' + RIGHT('000000' + CAST(@num AS VARCHAR(10)), 6)
			END
			--INSERT ORder vào hệ thống
			INSERT INTO ORderService(OrderServiceID, CreateDate, CreateTime, CustomerID)
			VALUES (@ORderID, FORMAT(GETDATE(), 'dd/mm/yy'), FORMAT(GETDATE(), 'HH:mm:ss'), @CusID)
		END
		--INSERT vào bảng DetailORderService trONg hệ thống
		INSERT INTO OrderSDetail(OrderServiceID, ServiceID)
		VALUES (@ORderID, @VPID)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--9. AD6: Xem lịch sử sử dụng dịch vụ 
CREATE OR ALTER FUNCTION dbo.udf_XemLichSuSuDungDichVu
(
	@CusID VARCHAR(20)
)
RETURNS TABLE
AS 
RETURN 
(
	SELECT dIStinct i.CREATEdDate, i.CREATEdTime, s.ServiceName, s.DID, i.InvoiceID
	FROM Invoice i
	JOIN Orders o ON i.OrderID = o.OrderID
	JOIN OrderService os ON i.OrderServiceID = os.OrderServiceID
	JOIN OrderSDetail od ON od.OrderServiceID = os.OrderServiceID
	JOIN Service s ON od.ServiceID = s.ServiceID
	WHERE i.CustomerID = @CusID
)
GO

CREATE OR ALTER PROC dbo.sp_XemLichSuSuDungDichVu
(
	@CusID VARCHAR(20)
)
AS 
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		--Kiểm tra khách hàng có từng sử dụng dịch vụ chưa
		--Gán dữ liệu từ hàm udf_XemLichSuSuDungDichVu() vào bảng tạm #res
		SELECT *
		INTO #res
		FROM udf_XemLichSuSuDungDichVu(@CusID)
		IF (SELECT * FROM #res) IS NULL
		BEGIN 
			;THROW 51005, N'Khách hàng chưa từng sử dụng dịch vụ ở hệ thống', 1;
		END 
		ELSE 
		BEGIN
			SELECT * FROM #res
		END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
--10. AD7: Đánh giá dịch vụ
CREATE OR ALTER PROC dbo.sp_DanhGiaDichVu
(
	@ReviewID              VARCHAR(20),
    @CusID				   VARCHAR(20),
    @InvoiceID             VARCHAR(20),
    @ServiceQuantityScORe  INT,
    @StaffAttitudeScORe    INT,
    @OverallSatISfactiON   INT,
    @Comment               NVARCHAR(MAX)
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRAN
		--Kiểm tra xem khách hàng có tồn tại hay không
		IF NOT EXISTS(SELECT c.CustomerID FROM Customer c WHERE c.CustomerID = @CusID)
		BEGIN 
			;THROW 51000, N'Hệ thống không tồn tại mã khách hàng này, xin vui lòng kiểm tra lại', 1;
		END
		--Tạo ReviewID mới
		DECLARE @maxID VARCHAR(20)  
		DECLARE @nextID VARCHAR(20)
		SELECT @maxID = MAX(ReviewID) FROM Review
		--Nếu chưa có ReviewID nào được tạo
		IF @maxID IS NULL
		BEGIN
			SET @nextID = 'RVW000001'
		END
		--Nếu đã tồn tại ReviewID từ trước trONg hệ thống
		ELSE 
		BEGIN 
			--Tạo ReviewID tăng dần
			DECLARE @num INT = CAST(RIGHT(@maxID, 6) AS INT) + 1;
			SET @nextID = 'RVW' + RIGHT('000000' + CAST(@num AS VARCHAR(10)), 6)
		END
		INSERT INTO Review(ReviewID, CustomerID, InvoiceID, ServiceQuantityScORe, StaffAttitudeScORe, OverallSatISfactiON, Comment)
		VALUES (@nextID, @CusID, @InvoiceID, @ServiceQuantityScORe, @StaffAttitudeScORe, @OverallSatISfactiON, @Comment)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		-- Nếu có lỗi xảy ra ở bất kỳ đâu
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        
        -- Ném lại chính xác lỗi đã xảy ra để ứng dụng biết
        ;THROW;
	END CATCH
END
GO
