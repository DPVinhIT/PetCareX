USE PetCareX_DB
GO
CREATE OR ALTER PROC sp_Login
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
    BEGIN
        SET @result = 1;
        SELECT 
            EmployeeID,
            FullName,
            Role
		FROM Employee
        WHERE Username = @Username
    END
    ELSE
    BEGIN
        SET @result = 0;
        -- trả empty để khỏi lỗi đọc
        SELECT CAST(NULL AS VARCHAR(20)) AS EmployeeID,
               CAST(NULL AS NVARCHAR(100)) AS FullName,
               CAST(NULL AS NVARCHAR(50)) AS Role
        WHERE 1=0;
    END
END
GO

CREATE PROC sp_VerifyForgotPassword
    @Username   VARCHAR(20),
    @PhoneNumber VARCHAR(15),
    @EmployeeID VARCHAR(20),
    @ManagerID  VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Employee
        WHERE Username   = @Username
          AND EmployeeID = @EmployeeID
          AND PhoneNumber = @PhoneNumber
          AND MID        = @ManagerID
    )
        SET @result = 1;
    ELSE
        SET @result = 0;
END
GO

CREATE PROC sp_ResetPassword
    @Username VARCHAR(20),
    @NewPassword NVARCHAR(255),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM AccountLogin WHERE Username = @Username)
    BEGIN
        SET @result = 0;
        RETURN;
    END

    UPDATE AccountLogin
    SET Password = @NewPassword  -- (nếu bạn hash thì thay bằng hash)
    WHERE Username = @Username;

    SET @result = 1;
END
GO

CREATE PROC sp_ChangePasswordE
    @Username        VARCHAR(20),
    @OldPassword     NVARCHAR(255),
    @NewPassword     NVARCHAR(255),
    @ConfirmPassword NVARCHAR(255),
    @result          INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        -- 1) Validate
        IF (
            @Username IS NULL OR LTRIM(RTRIM(@Username)) = '' OR
            @OldPassword IS NULL OR LTRIM(RTRIM(@OldPassword)) = '' OR
            @NewPassword IS NULL OR LTRIM(RTRIM(@NewPassword)) = '' OR
            @ConfirmPassword IS NULL OR LTRIM(RTRIM(@ConfirmPassword)) = ''
        )
        BEGIN
            SET @result = -2; -- Thiếu dữ liệu
            RETURN;
        END

        -- 2) Username tồn tại?
        IF NOT EXISTS (SELECT 1 FROM AccountLogin WHERE Username = @Username)
        BEGIN
            SET @result = -3; -- Username không tồn tại
            RETURN;
        END

        -- 3) OldPassword đúng không?
        IF NOT EXISTS (
            SELECT 1
            FROM AccountLogin
            WHERE Username = @Username
              AND Password = @OldPassword
        )
        BEGIN
            SET @result = 0; -- OldPassword không khớp
            RETURN;
        END

        -- 4) New = Confirm?
        IF (@NewPassword <> @ConfirmPassword)
        BEGIN
            SET @result = -4; -- NewPassword và ConfirmPassword không khớp
            RETURN;
        END

        BEGIN TRAN;

        -- 5) Update
        UPDATE AccountLogin
        SET Password = @NewPassword
        WHERE Username = @Username;

        COMMIT;

        SET @result = 1; -- Thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO


