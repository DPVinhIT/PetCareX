USE PetCareX_DB;
GO

-- =============================================
-- XÓA CÁC TRIGGER CŨ (nếu có)
-- =============================================
IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_ValidateMonitoringTime')
    DROP TRIGGER trg_ValidateMonitoringTime;
GO

IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_AutoGenerateTasks')
    DROP TRIGGER trg_AutoGenerateTasks;
GO

IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_PreventDuplicateNurseAssignment')
    DROP TRIGGER trg_PreventDuplicateNurseAssignment;
GO

IF EXISTS (SELECT * FROM sys.triggers WHERE name = 'trg_PreventDuplicateAndGenerateTasks')
    DROP TRIGGER trg_PreventDuplicateAndGenerateTasks;
GO

-- =============================================
-- TRIGGER 1: Kiểm tra thời gian theo dõi sau phẫu thuật
-- =============================================
CREATE TRIGGER trg_ValidateMonitoringTime
ON PostSurgeryMonitoring
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN Surgery s ON i.SurgeryID = s.SurgeryID
        WHERE i.CheckTime < s.SurgeryDate
    )
    BEGIN
        RAISERROR(N'Lỗi: Thời gian kiểm tra không thể trước thời gian phẫu thuật!', 16, 1);
        RETURN;
    END
    
    INSERT INTO PostSurgeryMonitoring (MonitorID, SurgeryID, NurseID, CheckTime, Status, Note)
    SELECT MonitorID, SurgeryID, NurseID, CheckTime, Status, Note
    FROM inserted;
END;
GO

-- =============================================
-- TRIGGER 2: Ngăn phân công y tá trùng lặp + Tự động sinh danh sách công việc
-- LƯU Ý: Kết hợp 2 trigger vì INSTEAD OF INSERT sẽ chặn AFTER INSERT trigger
-- =============================================
CREATE TRIGGER trg_PreventDuplicateAndGenerateTasks
ON NurseAssignment
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra trùng lặp
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        INNER JOIN NurseAssignment na ON i.SurgeryID = na.SurgeryID AND i.NurseID = na.NurseID
    )
    BEGIN
        RAISERROR(N'Lỗi: Y tá này đã được phân công cho ca phẫu thuật này rồi!', 16, 1);
        RETURN;
    END
    
    -- Sinh AssignmentID tự động nếu chưa có
    DECLARE @BaseID VARCHAR(20);
    SET @BaseID = 'ASN' + FORMAT(GETDATE(), 'yyMMddHHmmss');
    
    -- Tạo bảng tạm lưu các AssignmentID mới
    DECLARE @NewAssignments TABLE (
        AssignmentID VARCHAR(20),
        SurgeryID VARCHAR(20),
        NurseID VARCHAR(20),
        AssignedDate DATE,
        Note NVARCHAR(200)
    );
    
    -- Insert vào bảng tạm với AssignmentID được sinh
    INSERT INTO @NewAssignments (AssignmentID, SurgeryID, NurseID, AssignedDate, Note)
    SELECT 
        CASE 
            WHEN i.AssignmentID IS NULL OR i.AssignmentID = '' 
            THEN @BaseID + RIGHT('000' + CAST(ROW_NUMBER() OVER (ORDER BY i.SurgeryID) AS VARCHAR), 3)
            ELSE i.AssignmentID
        END,
        i.SurgeryID, 
        i.NurseID, 
        ISNULL(i.AssignedDate, GETDATE()),
        i.Note
    FROM inserted i;
    
    -- Insert vào NurseAssignment
    INSERT INTO NurseAssignment (AssignmentID, SurgeryID, NurseID, AssignedDate, Note)
    SELECT AssignmentID, SurgeryID, NurseID, AssignedDate, Note
    FROM @NewAssignments;
    
    -- Tự động sinh danh sách công việc (thay thế cho trg_AutoGenerateTasks)
    INSERT INTO NurseTask (TaskID, AssignmentID, TaskName, IsCompleted)
    SELECT 
        'TSK' + FORMAT(GETDATE(), 'yyMMddHHmmss') + RIGHT('000' + CAST(ROW_NUMBER() OVER (ORDER BY t.ChecklistID) AS VARCHAR), 3),
        na.AssignmentID,
        t.CheckItem,
        0 
    FROM @NewAssignments na
    JOIN Surgery s ON na.SurgeryID = s.SurgeryID
    JOIN SurgeryChecklistTemplate t ON s.SurgeryID = t.ServiceID;
END;
GO