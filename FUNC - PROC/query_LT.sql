
USE PetCareX_DB
GO
CREATE PROCEDURE CreateNewCustomer
    @FullName NVARCHAR(100),
    @PhoneNumber VARCHAR(15),
    @Email VARCHAR(100),
    @CCCD VARCHAR(20),
    @Gender NVARCHAR(10),
    @Birthday DATE
AS
BEGIN
	-- Ràng buộc: CustomerID là duy nhất
    DECLARE @CusID VARCHAR(20) = ISNULL('CUS' + RIGHT('00000' + CAST(CAST(RIGHT((SELECT MAX(CustomerID) FROM Customer), 5)AS INT) + 1 AS VARCHAR(10)), 5), 'CUS00001')

    -- Ràng buộc: Số điện thoại, Email, CCCD phải duy nhất (AD1)
    IF EXISTS (SELECT 1 FROM Customer WHERE PhoneNumber = @PhoneNumber)
    BEGIN
        print N'Lỗi: Số điện thoại đã tồn tại trong hệ thống.' 
        RETURN
    END
    IF EXISTS (SELECT 1 FROM Customer WHERE Email = @Email)
    BEGIN
       print N'Lỗi: Email đã tồn tại trong hệ thống.' 
        RETURN
    END
    IF EXISTS (SELECT 1 FROM Customer WHERE CCCD = @CCCD)
    BEGIN
        print N'Lỗi: CCCD đã tồn tại trong hệ thống.' 
        RETURN
    END

    -- Ràng buộc: Ngày sinh khách hàng hợp lệ (Phải trước ngày hiện tại)
    IF @Birthday >= GETDATE()
    BEGIN
        print N'Lỗi: Ngày sinh không hợp lệ.' 
        RETURN
    END

	-- Ràng buộc: Giới tính hợp lệ
    IF @Gender NOT IN (N'Male', N'Female')
    BEGIN
        print N'Lỗi: Giới tính không hợp lệ. Phải là Male, Female.'
        RETURN
    END

    -- 2. Tạo hồ sơ Customer
    INSERT INTO Customer (CustomerID, FullName, PhoneNumber, Email, CCCD, Gender, Birthday)
    VALUES (@CusID, @FullName, @PhoneNumber, @Email, @CCCD, @Gender, @Birthday)

	DECLARE @CardID VARCHAR(20) = ISNULL('CARD' + RIGHT('00000' + CAST(CAST(RIGHT((SELECT MAX(CustomerID) FROM Customer), 5)AS INT) + 1 AS VARCHAR(10)), 5), 'CARD00001')
    -- 3. Tạo CardMembership (Mặc định là cấp độ BASIC, LoyalPoint = 0)
    INSERT INTO CardMembership (CardID, RegistrationDate, LoyalPoint, LevelID, CustomerID)
    VALUES (@CardID, GETDATE(), 0, 'L1', @CusID)
    
    PRINT N'Tạo khách hàng mới thành công. CustomerID: ' + @CusID; 

END
GO
EXEC CreateNewCustomer @FullName = N'Nguyễn Văn A', @PhoneNumber = '0123456789', @Email = 'a@email.com', @CCCD = '123456789', @Gender = N'Male', @Birthday = '2000-01-01'
GO
-- =============================
-- CHỨC NĂNG: Tạo khách hàng mới
-- PROCEDURE: CreateNewCustomer
-- Tham số: @CustomerID, @CardID, @FullName, @PhoneNumber, @Email, @CCCD, @Gender, @Birthday, @Username, @Password
-- Trả về: Thông báo thành công/thất bại
-- Ví dụ EXEC:
--   EXEC CreateNewCustomer @CustomerID = 'CUS0001', @FullName = N'Nguyễn Văn A', @PhoneNumber = '0123456789', @Email = 'a@email.com', @CCCD = '123456789', @Gender = N'Male', @Birthday = '2000-01-01', @Username = 'user1';
-- =============================





IF OBJECT_ID('RegisterAppointment') IS NOT NULL
    DROP PROCEDURE RegisterAppointment;
GO

CREATE PROCEDURE RegisterAppointment      
    @CustomerID VARCHAR(20),
    @BranchID VARCHAR(20),
    @ServiceID VARCHAR(20),            
    @ReceptionistID VARCHAR(20),       
    @AppointmentDate DATE,
    @AppointmentTime TIME,
    @Room NVARCHAR(50) = NULL
AS
BEGIN

    -- Ràng buộc 1a: Kiểm tra AppointmentID duy nhất
    DECLARE @AppointmentID VARCHAR(20) = ISNULL('APP' + RIGHT('000000' + CAST(CAST(RIGHT((SELECT MAX(AppointmentID) FROM Appointment), 6)AS INT) + 1 AS VARCHAR(10)), 6), 'APP000001')
    
    -- Ràng buộc 1b: Kiểm tra Chi nhánh tồn tại
    IF NOT EXISTS (SELECT 1 FROM Branch WHERE BranchID = @BranchID)
    BEGIN
        PRINT N'Lỗi: Chi nhánh với mã ' + @BranchID + N' không tồn tại.' 
        RETURN
    END

    -- Ràng buộc 1c: Kiểm tra Khách hàng tồn tại
    IF NOT EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @CustomerID)
    BEGIN
        PRINT N'Lỗi: Khách hàng với mã ' + @CustomerID + N' không tồn tại.' 
        RETURN
    END

    -- Ràng buộc 1d: Kiểm tra Lễ tân tồn tại (Employee)
    IF NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @ReceptionistID AND Role = N'Receptionist')
    BEGIN
        PRINT N'Lỗi: Nhân viên Lễ tân không tồn tại hoặc không đúng vai trò.' 
        RETURN
    END

    -- Ràng buộc 1e: Kiểm tra Dịch vụ (Service) tồn tại
    IF NOT EXISTS (SELECT 1 FROM Service WHERE ServiceID = @ServiceID)
    BEGIN
        PRINT N'Lỗi: Dịch vụ với mã ' + @ServiceID + N' không tồn tại.' 
        RETURN
    END

	-- Ràng buộc 2: Ngày hẹn phải sau ngày hiện tại
    IF @AppointmentDate < CONVERT(DATE, GETDATE())
    BEGIN
        PRINT N'Lỗi: Ngày hẹn phải sau ngày hiện tại.' 
        RETURN
    END
    
    -- Ràng buộc 3: Giờ hẹn nằm trong khung giờ mở cửa của chi nhánh
    DECLARE @OpenTime INT, @CloseTime INT
    SELECT @OpenTime = OpenTime, @CloseTime = CloseTime 
    FROM Branch 
    WHERE BranchID = @BranchID

    DECLARE @ApptTimeInt INT = DATEPART(HOUR, @AppointmentTime)

    IF @OpenTime IS NULL OR @ApptTimeInt < @OpenTime OR @ApptTimeInt > @CloseTime
    BEGIN
        PRINT N'Lỗi: Giờ hẹn phải nằm trong khung giờ mở cửa của của chi nhánh.' 
        RETURN
    END

    -- Ghi nhận lịch hẹn
    INSERT INTO Appointment (AppointmentID, CreateDate, CreateTime, Room, Date, Time, BranchID, ServiceID, CustomerID, RID)
    VALUES (
        @AppointmentID,
        CONVERT(DATE, GETDATE()),
        CONVERT(TIME, GETDATE()),
        @Room,
        @AppointmentDate,
        @AppointmentTime,
        @BranchID,
        @ServiceID,
        @CustomerID,
        @ReceptionistID
    );

    PRINT N'Đăng ký lịch hẹn thành công. Mã lịch hẹn: ' + @AppointmentID; 
END
GO

-- =============================
-- CHỨC NĂNG: Đăng ký lịch hẹn
-- PROCEDURE: RegisterAppointment
-- Tham số: @AppointmentID, @CustomerID, @BranchID, @ServiceID, @ReceptionistID, @AppointmentDate, @AppointmentTime, @Room (tùy chọn)
-- Trả về: Thông báo thành công/thất bại
-- Ví dụ EXEC:
--   EXEC RegisterAppointment @AppointmentID = 'APPT0001', @CustomerID = 'CUS0001', @BranchID = 'BR001', @ServiceID = 'SRV001', @ReceptionistID = 'E0001', @AppointmentDate = '2025-01-01', @AppointmentTime = '09:00', @Room = N'Phòng 1';
-- =============================



IF OBJECT_ID('GetCustomerInfoByContact') IS NOT NULL
    DROP FUNCTION GetCustomerInfoByContact;
GO

CREATE FUNCTION GetCustomerInfoByContact
(
    @SearchTerm NVARCHAR(100)
)
RETURNS TABLE
AS
RETURN
(
    SELECT
        C.CustomerID,
        C.FullName,
        C.PhoneNumber,
        C.Email,
        C.CCCD,
        C.Gender,
        C.Birthday,
        Card.LevelID AS MembershipLevel,
        Card.LoyalPoint,
        Card.RegistrationDate AS CardRegistrationDate
    FROM
        Customer C
    LEFT JOIN
        CardMembership Card ON C.CustomerID = Card.CustomerID
    WHERE
        C.PhoneNumber = @SearchTerm OR C.Email = @SearchTerm OR C.CCCD = @SearchTerm OR C.CustomerID = @SearchTerm
);
GO

-- =============================
-- CHỨC NĂNG: Tra cứu thông tin khách hàng theo số điện thoại, email, CCCD hoặc mã khách hàng
-- FUNCTION: GetCustomerInfoByContact
-- Tham số: @SearchTerm (Số điện thoại, email, CCCD hoặc mã khách hàng)
-- Trả về: Thông tin khách hàng
-- Ví dụ EXEC:
--   SELECT * FROM GetCustomerInfoByContact('0123456789');
-- =============================




IF OBJECT_ID('AddNewPet') IS NOT NULL
    DROP PROCEDURE AddNewPet;
GO

CREATE PROCEDURE AddNewPet
    @PetID VARCHAR(20),             -- ID từ bên ngoài
    @CustomerID VARCHAR(20),
    @PetName NVARCHAR(50),
    @Species NVARCHAR(50),
    @Breed NVARCHAR(50),
    @Birthday DATE,
    @Gender NVARCHAR(10),
    @HealthStatus NVARCHAR(255)
AS
BEGIN

    -- Ràng buộc 1: Kiểm tra PetID duy nhất
    IF EXISTS (SELECT 1 FROM Pet WHERE PetID = @PetID)
    BEGIN
        PRINT N'Lỗi: Mã thú cưng đã tồn tại.' 
        RETURN
    END

    -- Ràng buộc 2: Khách hàng phải tồn tại
    IF NOT EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @CustomerID)
    BEGIN
        PRINT N'Lỗi: Mã khách hàng không tồn tại.' 
        RETURN
    END

    IF @Birthday >= GETDATE()
    BEGIN
        PRINT N'Lỗi: Ngày sinh thú cưng không hợp lệ.' 
        RETURN
    END
    
   -- Ràng buộc 4: Giới tính hợp lệ
    IF @Gender NOT IN (N'Male', N'Female')
    BEGIN
        PRINT N'Lỗi: Giới tính thú cưng không hợp lệ.' 
        RETURN
    END


    INSERT INTO Pet (PetID, CustomerID, PetName, Species, Breed, Birthday, Gender, HealthStatus)
    VALUES (@PetID, @CustomerID, @PetName, @Species, @Breed, @Birthday, @Gender, @HealthStatus)

    PRINT N'Thêm hồ sơ thú cưng thành công. PetID: ' + @PetID; 
END
GO

-- =============================
-- CHỨC NĂNG: Thêm mới hồ sơ thú cưng
-- PROCEDURE: AddNewPet
-- Tham số: @PetID, @CustomerID, @PetName, @Species, @Breed, @Birthday, @Gender, @HealthStatus
-- Trả về: Thông báo thành công/thất bại
-- Ví dụ EXEC:
--   EXEC AddNewPet @PetID = 'PET0001', @CustomerID = 'CUS0001', @PetName = N'Mèo Mun', @Species = N'Mèo', @Breed = N'Anh lông ngắn', @Birthday = '2020-01-01', @Gender = N'Male', @HealthStatus = N'Khỏe mạnh';
-- =============================



