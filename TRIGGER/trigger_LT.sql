USE PetCareX_DB;
go
-- =============================================
-- TRIGGER 1: Phone, Email, CCCD phải duy nhất(LT1)
-- =============================================
IF OBJECT_ID('trg_Customer_UniqueCheck_LT1') IS NOT NULL
    DROP TRIGGER trg_Customer_UniqueCheck_LT1;
GO
CREATE TRIGGER trg_Customer_UniqueCheck_LT1
ON dbo.Customer
FOR INSERT, UPDATE
AS
BEGIN
    -- Kiểm tra trùng Số điện thoại
    IF EXISTS (
        SELECT 1 FROM Customer c 
        JOIN inserted i ON c.PhoneNumber = i.PhoneNumber 
        WHERE c.CustomerID <> i.CustomerID
    )
    BEGIN
        RAISERROR (N'Lỗi: Số điện thoại đã tồn tại trên hệ thống (Trigger chặn).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Kiểm tra trùng CCCD
    IF EXISTS (
        SELECT 1 FROM Customer c 
        JOIN inserted i ON c.CCCD = i.CCCD 
        WHERE c.CustomerID <> i.CustomerID
    )
    BEGIN
        RAISERROR (N'Lỗi: CCCD đã tồn tại trên hệ thống (Trigger chặn).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
	 --Kiểm tra trùng Email
	IF EXISTS (
        SELECT 1 FROM Customer c 
        JOIN inserted i ON c.Email = i.Email 
        WHERE c.CustomerID <> i.CustomerID
    )
    BEGIN
        RAISERROR (N'Lỗi: Email đã tồn tại trên hệ thống (Trigger chặn).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- =============================================
-- TRIGGER 2: Username phải chưa tồn tại
-- =============================================
IF OBJECT_ID('trg_AccountLogin_Unique') IS NOT NULL
    DROP TRIGGER trg_AccountLogin_Unique;
GO
CREATE OR ALTER TRIGGER trg_AccountLogin_Unique
ON dbo.AccountLogin
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM AccountLogin a JOIN inserted i ON a.Username = i.Username 
        WHERE a.Username = i.Username AND EXISTS (SELECT 1 FROM deleted) -- Chỉ check khi Update
    )
    BEGIN
        -- Lưu ý: Khóa chính Primary Key đã tự động check duy nhất khi INSERT. 
        -- Trigger này giúp thông báo lỗi tiếng Việt rõ ràng hơn.
        RAISERROR (N'Lỗi: Tên đăng nhập (Username) đã tồn tại.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- =============================================
-- TRIGGER 3: CardID chưa tồn tại
-- =============================================
IF OBJECT_ID('trg_CardMembership_Unique') IS NOT NULL
    DROP TRIGGER trg_CardMembership_Unique;
GO
CREATE OR ALTER TRIGGER trg_CardMembership_Unique
ON dbo.CardMembership
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM CardMembership cm JOIN inserted i ON cm.CardID = i.CardID 
        WHERE cm.CardID <> i.CardID
    )
    BEGIN
        RAISERROR (N'Lỗi: Mã thẻ đã tồn tại trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- =============================================
-- TRIGGER 4: Kiểm tra ngày sinh hợp lệ(LT1)
-- =============================================
IF OBJECT_ID('trg_Customer_BirthdayCheck_LT1') IS NOT NULL
    DROP TRIGGER trg_Customer_BirthdayCheck_LT1;
GO
CREATE TRIGGER trg_Customer_BirthdayCheck_LT1
ON dbo.Customer
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM inserted WHERE Birthday >= CAST(GETDATE() AS DATE)
    )
    BEGIN
        RAISERROR (N'Lỗi: Ngày sinh khách hàng không hợp lệ (Phải trước ngày hiện tại).', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- =============================================
-- TRIGGER 5: Kiểm tra giới tính hợp lệ(LT1)
-- =============================================
IF OBJECT_ID('trg_CheckCustomerGender_LT1') IS NOT NULL
    DROP TRIGGER trg_CheckCustomerGender_LT1;
GO
CREATE TRIGGER trg_CheckCustomerGender_LT1
ON dbo.Customer
FOR INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM inserted 
        WHERE Gender NOT IN (N'Male', N'Female', N'Other')
    )
    BEGIN
        RAISERROR (N'Lỗi: Giới tính khách hàng không hợp lệ. Chỉ chấp nhận Male, Female hoặc Other.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- =============================================
-- TRIGGER 6: Khách hàng và thú cưng phải có hồ sơ trong hệ thống
-- =============================================
IF OBJECT_ID('trg_CheckCustomerPet_LT1') IS NOT NULL
    DROP TRIGGER trg_CheckCustomerPet_LT1;
GO
CREATE OR ALTER TRIGGER trg_CheckCustomerPet_LT1
ON dbo.OrderService
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra khách hàng phải có hồ sơ trong hệ thống
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Customer c ON i.CustomerID = c.CustomerID
        WHERE c.CustomerID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT1: Khách hàng chưa có hồ sơ trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. BỔ SUNG: Kiểm tra thú cưng PHẢI TỒN TẠI trong hệ thống (Check tồn tại trước)
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Pet p ON i.PetID = p.PetID
        WHERE i.PetID IS NOT NULL AND p.PetID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT1: Mã thú cưng không tồn tại trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 3. Kiểm tra thú cưng phải thuộc về khách hàng này (Check sở hữu sau)
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Pet p ON i.PetID = p.PetID AND i.CustomerID = p.CustomerID
        WHERE i.PetID IS NOT NULL AND p.PetID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT1: Hồ sơ thú cưng không thuộc quyền sở hữu của khách hàng này.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO
-- =============================================
-- TRIGGER 7: Khách hàng và thú cưng phải có hồ sơ trong hệ thống(LT1)
-- =============================================
IF OBJECT_ID('trg_CheckBranchService_LT1') IS NOT NULL
    DROP TRIGGER trg_CheckBranchService_LT1;
GO
CREATE OR ALTER TRIGGER trg_CheckBranchService_LT1
ON dbo.OrderSDetail
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra cặp BranchID và ServiceID phải tồn tại trong bảng BranchService
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN OrderService os ON i.OrderServiceID = os.OrderServiceID
        LEFT JOIN BranchService bs ON os.BranchID = bs.BranchID AND i.ServiceID = bs.ServiceID
        WHERE bs.ServiceID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT1: Chi nhánh này không cung cấp loại dịch vụ đã chọn.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- =============================================
-- TRIGGER 8: Dịch vụ được thêm vào đơn hàng đều phải tồn tại và phải được cung cấp bởi đúng chi nhánh của đơn hàng đó(LT1)
-- =============================================
IF OBJECT_ID('trg_CheckBranchService_LT1') IS NOT NULL
    DROP TRIGGER trg_CheckBranchService_LT1;
GO
CREATE OR ALTER TRIGGER trg_CheckBranchService_LT1
ON dbo.OrderSDetail
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra Dịch vụ phải tồn tại trong hệ thống (Ràng buộc 5)
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Service s ON i.ServiceID = s.ServiceID
        WHERE s.ServiceID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT1: Mã dịch vụ không tồn tại trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- 2. Kiểm tra Chi nhánh có cung cấp dịch vụ này không (Ràng buộc 4 & 6)
    -- Lấy BranchID từ bảng OrderService để đối soát
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN OrderService os ON i.OrderServiceID = os.OrderServiceID
        LEFT JOIN BranchService bs ON os.BranchID = bs.BranchID AND i.ServiceID = bs.ServiceID
        WHERE bs.ServiceID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT1: Chi nhánh của đơn hàng này không cung cấp loại dịch vụ đã chọn.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- =============================================
-- TRIGGER 9: Bác sĩ được chọn phải có lịch làm việc vào ngày và giờ đăng ký(LT2)
-- =============================================
IF OBJECT_ID('trg_CheckDoctorSchedule_LT2') IS NOT NULL
    DROP TRIGGER trg_CheckDoctorSchedule_LT2;
GO
CREATE OR ALTER TRIGGER trg_CheckDoctorSchedule_LT2
ON dbo.Appointment
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra nếu tồn tại lịch hẹn mà bác sĩ phụ trách không có lịch trực tương ứng
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        -- Tìm bác sĩ phụ trách dịch vụ đó (từ bảng Service)
        JOIN Service s ON i.ServiceID = s.ServiceID
        -- Kiểm tra lịch trực của bác sĩ đó tại đúng chi nhánh và thời gian
        LEFT JOIN WorkSchedule ws ON s.DID = ws.EmployeeID 
            AND i.Date = ws.WorkDate
            AND i.BranchID = ws.MID -- Giả định MID trong WorkSchedule lưu chi nhánh trực (hoặc JOIN qua bảng khác tùy thiết kế)
            AND DATEPART(HOUR, i.Time) >= ws.WorkTime
            AND DATEPART(HOUR, i.Time) < (ws.WorkTime + 4) -- Giả định 1 ca trực kéo dài 4 tiếng
        WHERE ws.EmployeeID IS NULL -- Nếu không tìm thấy dòng khớp trong WorkSchedule
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Bác sĩ phụ trách dịch vụ này không có lịch trực tại chi nhánh vào khung giờ đã chọn.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO



-- =============================================
-- TRIGGER 10: Kiểm tra giờ hẹn theo khung giờ chi nhánh (LT2)
-- =============================================
IF OBJECT_ID('trg_CheckBranchHours_LT2') IS NOT NULL
    DROP TRIGGER trg_CheckBranchHours_LT2;
GO
CREATE OR ALTER TRIGGER trg_CheckBranchHours_LT2
ON dbo.Appointment
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra nếu tồn tại lịch hẹn có giờ (Time) nằm ngoài khung (OpenTime - CloseTime)
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN Branch b ON i.BranchID = b.BranchID
        WHERE DATEPART(HOUR, i.Time) < b.OpenTime 
           OR DATEPART(HOUR, i.Time) >= b.CloseTime
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Giờ hẹn phải nằm trong khung giờ mở cửa của chi nhánh.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- =============================================
-- TRIGGER 11: Kiểm tra nhân viên phải là LỄ TÂNG
-- =============================================
IF OBJECT_ID('trg_Appointment_ReceptionistRole_LT2') IS NOT NULL
    DROP TRIGGER trg_Appointment_ReceptionistRole_LT2;
GO
CREATE OR ALTER TRIGGER trg_Appointment_ReceptionistRole_LT2
ON dbo.Appointment
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM inserted i
        JOIN Employee e ON i.RID = e.EmployeeID
        WHERE e.Role <> N'Receptionist'
    )
    BEGIN
        RAISERROR (N'Lỗi: Chỉ nhân viên Lễ tân mới được quyền ghi nhận lịch hẹn.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- =============================================
-- TRIGGER 12: Kiểm tra tồn tại / duy nhất
-- =============================================
IF OBJECT_ID('trg_Appointment_ValidateExistence_LT2') IS NOT NULL
    DROP TRIGGER trg_Appointment_ValidateExistence_LT2;
GO
CREATE OR ALTER TRIGGER trg_Appointment_ValidateExistence_LT2
ON dbo.Appointment
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra Chi nhánh tồn tại (Ràng buộc 1b)
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Branch b ON i.BranchID = b.BranchID
        WHERE b.BranchID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Chi nhánh không tồn tại trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END

    -- 2. Kiểm tra Khách hàng tồn tại (Ràng buộc 1c)
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Customer c ON i.CustomerID = c.CustomerID
        WHERE c.CustomerID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Khách hàng không tồn tại trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END

    -- 3. Kiểm tra Lễ tân tồn tại và đúng vai trò (Ràng buộc 1d)
    -- RID trong bảng Appointment của bạn tương ứng với mã Lễ tân
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Employee e ON i.RID = e.EmployeeID
        WHERE e.EmployeeID IS NULL OR e.Role <> N'Receptionist'
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Nhân viên Lễ tân không tồn tại hoặc không đúng vai trò.', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END

    -- 4. Kiểm tra Dịch vụ tồn tại (Ràng buộc 1e)
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Service s ON i.ServiceID = s.ServiceID
        WHERE s.ServiceID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Dịch vụ không tồn tại trong hệ thống.', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END
    
    -- Lưu ý: Ràng buộc 1a (Mã duy nhất) đã được Primary Key của bảng tự động xử lý.
END;
GO

-- =============================================
-- TRIGGER 13: Ngày hẹn phải sau ngày hiện tại
-- =============================================
IF OBJECT_ID('trg_Appointment_CheckDate_LT2') IS NOT NULL
    DROP TRIGGER trg_Appointment_CheckDate_LT2;
GO
CREATE OR ALTER TRIGGER trg_Appointment_CheckDate_LT2
ON dbo.Appointment
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Kiểm tra nếu tồn tại dòng dữ liệu có ngày hẹn nhỏ hơn ngày hiện tại
    IF EXISTS (
        SELECT 1 
        FROM inserted 
        WHERE Date < CAST(GETDATE() AS DATE)
    )
    BEGIN
        RAISERROR (N'Lỗi LT2: Ngày hẹn không được nhỏ hơn ngày hiện tại.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO


