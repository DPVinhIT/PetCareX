USE PetCareX_DB;
GO

-- =============================================
-- YT1: Lấy lịch sử theo dõi sau phẫu thuật
-- =============================================
CREATE PROC sp_GetHistoryPostSurgeryMonitoring @InputPetID VARCHAR(20)
AS
BEGIN
    SELECT 
        psm.CheckTime AS "Thời Gian Kiểm Tra",
        psm.Status AS "Tình Trạng Sức Khỏe",
        psm.Note AS "Ghi Chú Của Y Tá",
        e.FullName AS "Y Tá Phụ Trách",
        srv.ServiceName AS "Ca Phẫu Thuật"
    FROM PostSurgeryMonitoring psm
    JOIN Surgery s ON psm.SurgeryID = s.SurgeryID
    JOIN Service srv ON s.SurgeryID = srv.ServiceID
    JOIN Nurse n ON psm.NurseID = n.NID
    JOIN Employee e ON n.NID = e.EmployeeID
    WHERE s.PetID = @InputPetID
    ORDER BY psm.CheckTime DESC;
END
GO

-- =============================================
-- YT2: Thêm log theo dõi sau phẫu thuật
-- =============================================
CREATE PROCEDURE sp_AddMonitoringLog
    @SurgeryID VARCHAR(20),
    @NurseID VARCHAR(20),
    @Status NVARCHAR(255),
    @Note NVARCHAR(255)
AS
BEGIN
    DECLARE @CurrentTime DATETIME;
    SET @CurrentTime = GETDATE();

    IF NOT EXISTS (SELECT 1 FROM Surgery WHERE SurgeryID = @SurgeryID)
    BEGIN
        RAISERROR(N'Lỗi: Ca phẫu thuật không tồn tại hoặc mã sai.', 16, 1);
        RETURN;
    END

    IF NOT EXISTS (SELECT 1 FROM Nurse WHERE Nurse.NID = @NurseID)
    BEGIN
        RAISERROR(N'Lỗi: Y tá không tồn tại', 16, 1);
        RETURN;
    END

    DECLARE @NewMonitorID VARCHAR(20);
    SET @NewMonitorID = 'MON' + REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR, @CurrentTime, 120), '-', ''), ':', ''), ' ', '');

    INSERT INTO PostSurgeryMonitoring (MonitorID, SurgeryID, NurseID, CheckTime, Status, Note)
    VALUES (@NewMonitorID, @SurgeryID, @NurseID, @CurrentTime, @Status, @Note);

    PRINT N'Đã cập nhật tình trạng thành công vào lúc: ' + CONVERT(NVARCHAR, @CurrentTime, 120);
END;
GO

-- =============================================
-- YT3: Phân công y tá cho ca phẫu thuật (sinh AssignmentID tự động)
-- LƯU Ý: Trigger trg_AutoGenerateTasks được định nghĩa trong trigger_YT.sql
-- =============================================
CREATE PROCEDURE sp_AssignNurseToSurgery
    @SurgeryID VARCHAR(20),
    @NurseID VARCHAR(20),
    @Note NVARCHAR(200)
AS
BEGIN
    DECLARE @NewAssignmentID VARCHAR(20);
    SET @NewAssignmentID = 'ASN' + FORMAT(GETDATE(), 'yyMMddHHmmss');
    
    INSERT INTO NurseAssignment (AssignmentID, SurgeryID, NurseID, Note)
    VALUES (@NewAssignmentID, @SurgeryID, @NurseID, @Note);
    
    PRINT N'Đã phân công và sinh checklist thành công!';
END;
GO

-- =============================================
-- YT4: Lấy danh sách công việc của y tá (bao gồm SurgeryID)
-- =============================================
IF OBJECT_ID('sp_GetMyTasks', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetMyTasks;
GO

CREATE PROCEDURE sp_GetMyTasks
    @NurseID VARCHAR(20)
AS
BEGIN
    SELECT 
        t.TaskID,
        s.SurgeryID,
        p.PetName AS "Thú Cưng",
        srv.ServiceName AS "Loại Phẫu Thuật",
        t.TaskName AS "Việc Cần Làm",
        a.Note AS "Lưu Ý"
    FROM NurseTask t
    JOIN NurseAssignment a ON t.AssignmentID = a.AssignmentID
    JOIN Surgery s ON a.SurgeryID = s.SurgeryID
    JOIN Pet p ON s.PetID = p.PetID
    JOIN Service srv ON s.SurgeryID = srv.ServiceID
    WHERE a.NurseID = @NurseID 
      AND t.IsCompleted = 0; -- Chỉ lấy việc chưa xong
END;
GO

-- =============================================
-- YT5: Đánh dấu hoàn thành công việc
-- Ghi chú sẽ được lưu qua sp_AddMonitoringLog
-- =============================================
IF OBJECT_ID('sp_CompleteTask', 'P') IS NOT NULL
    DROP PROCEDURE sp_CompleteTask;
GO

CREATE PROCEDURE sp_CompleteTask
    @TaskID VARCHAR(20)
AS
BEGIN
    UPDATE NurseTask
    SET IsCompleted = 1, 
        CompletedTime = GETDATE()
    WHERE TaskID = @TaskID;
    PRINT N'Đã hoàn thành công việc!';
END;
GO

-- =============================================
-- YT6: Lấy danh sách bệnh nhân đang nằm viện/theo dõi sau phẫu thuật
-- =============================================
CREATE PROCEDURE sp_GetInpatientsList
AS
BEGIN
    SELECT 
        s.SurgeryID AS "Mã PT",
        p.PetID AS "Mã Pet",
        p.PetName AS "Tên Thú Cưng",
        p.Species AS "Loài",
        c.FullName AS "Chủ Sở Hữu",
        srv.ServiceName AS "Loại Phẫu Thuật",
        s.SurgeryDate AS "Ngày Phẫu Thuật",
        s.SurgeryStatus AS "Tình Trạng"
    FROM Surgery s
    JOIN Pet p ON s.PetID = p.PetID
    JOIN Customer c ON p.CustomerID = c.CustomerID
    JOIN Service srv ON s.SurgeryID = srv.ServiceID
    WHERE s.SurgeryStatus != N'Xuất viện'
    ORDER BY s.SurgeryDate DESC;
END;
GO

-- =============================================
-- YT6: Lấy thông tin chi tiết thú cưng
-- =============================================
CREATE PROCEDURE sp_GetPetDetails
    @PetName NVARCHAR(50)
AS
BEGIN
    SELECT 
        p.PetID AS "Mã Pet",
        p.PetName AS "Tên Thú Cưng",
        p.Species AS "Loài",
        p.Breed AS "Giống",
        p.Gender AS "Giới Tính",
        p.Birthday AS "Ngày Sinh",
        p.HealthStatus AS "Tình Trạng Sức Khỏe",
        c.CustomerID AS "Mã Chủ",
        c.FullName AS "Tên Chủ Sở Hữu",
        c.PhoneNumber AS "SĐT Chủ",
        c.Email AS "Email Chủ"
    FROM Pet p
    JOIN Customer c ON p.CustomerID = c.CustomerID
    WHERE p.PetName = @PetName;
END;
GO
