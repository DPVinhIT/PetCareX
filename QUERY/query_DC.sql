USE PetCareX_DB
GO

-- ============================================================
-- DC: ĐĂNG NHẬP - ĐĂNG KÝ - QUÊN MẬT KHẨU
-- 3 chức năng:
--   1. Đăng nhập
--   2. Đăng ký tài khoản
--   3. Quên mật khẩu
-- ============================================================

-- =========================
-- 1. ĐĂNG NHẬP
-- =========================
CREATE PROC sp_Login
    @Username VARCHAR(20),
    @Password NVARCHAR(255),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1
        FROM AccountLogin
        WHERE Username = @Username
          AND Password = @Password
    )
        SET @result = 1; -- Đăng nhập thành công
    ELSE
        SET @result = 0; -- Sai username hoặc password
END
GO

-- =========================
-- 2. ĐĂNG KÝ TÀI KHOẢN
-- =========================
CREATE PROC sp_Register
    @Username VARCHAR(20),
    @Password NVARCHAR(255),
    @FullName NVARCHAR(100),
    @Phone VARCHAR(15),
    @Email VARCHAR(100),
    @CCCD VARCHAR(20),
    @Gender NVARCHAR(10),
    @Birthday DATE,
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- 1. Validate dữ liệu bắt buộc
        IF (
            @Username IS NULL OR LTRIM(RTRIM(@Username)) = '' OR
            @Password IS NULL OR LTRIM(RTRIM(@Password)) = '' OR
            @FullName IS NULL OR LTRIM(RTRIM(@FullName)) = '' OR
            @Email IS NULL OR LTRIM(RTRIM(@Email)) = '' OR
            @CCCD IS NULL OR LTRIM(RTRIM(@CCCD)) = ''
        )
        BEGIN
            SET @result = -2; -- Thiếu dữ liệu
            RETURN;
        END

        -- 2. Check trùng username
        IF EXISTS (SELECT 1 FROM AccountLogin WHERE Username = @Username)
        BEGIN
            SET @result = 0; -- Trùng username
            RETURN;
        END

        -- 3. Check trùng email
        IF EXISTS (SELECT 1 FROM Customer WHERE Email = @Email)
        BEGIN
            SET @result = -3; -- Trùng email
            RETURN;
        END

        -- 4. Check trùng CCCD
        IF EXISTS (SELECT 1 FROM Customer WHERE CCCD = @CCCD)
        BEGIN
            SET @result = -4; -- Trùng CCCD
            RETURN;
        END
        
        -- 5. Validate Gender hợp lệ (nếu có nhập)
        IF @Gender IS NOT NULL AND @Gender NOT IN (N'Nam', N'Nữ', N'Khác')
        BEGIN
            SET @result = -5; -- Giới tính không hợp lệ
            RETURN;
        END
        
        -- 6. Validate Birthday hợp lệ (phải < ngày hiện tại)
        IF @Birthday IS NOT NULL AND @Birthday >= CAST(GETDATE() AS DATE)
        BEGIN
            SET @result = -6; -- Ngày sinh không hợp lệ
            RETURN;
        END

        BEGIN TRAN;

        -- 7. Insert AccountLogin
        INSERT INTO AccountLogin (Username, Password)
        VALUES (@Username, @Password);

        -- 8. Tạo CustomerID (dùng MAX+1 để tránh trùng khi có xóa)
        DECLARE @MaxNum INT;
        DECLARE @CustomerID VARCHAR(20);

        SELECT @MaxNum = ISNULL(MAX(CAST(SUBSTRING(CustomerID, 4, 10) AS INT)), 0)
        FROM Customer
        WHERE CustomerID LIKE 'CUS[0-9]%';
        
        SET @CustomerID = 'CUS' + RIGHT('0000' + CAST(@MaxNum + 1 AS VARCHAR(4)), 4);

        -- 9. Insert Customer
        INSERT INTO Customer (
            CustomerID,
            FullName,
            PhoneNumber,
            Email,
            CCCD,
            Gender,
            Birthday,
            Username
        )
        VALUES (
            @CustomerID,
            @FullName,
            @Phone,
            @Email,
            @CCCD,
            @Gender,
            @Birthday,
            @Username
        );

        COMMIT;
        SET @result = 1; -- Đăng ký thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- =========================
-- 3. QUÊN MẬT KHẨU
-- =========================
CREATE PROC sp_ForgotPassword
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



