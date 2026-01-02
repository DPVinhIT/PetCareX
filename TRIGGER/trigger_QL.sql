USE PetCareX_DB
GO

/* ============================================================
   TRIGGER (QL3): trg_WorkSchedule_Validate
   Bảng: WorkSchedule
   Mục tiêu:
   - Chặn phân công lịch làm việc vào những ngày nhân viên đã được duyệt nghỉ phép
   Ghi chú:
   - DB hiện tại không có Employee.Status => không check Active/Locked/Resigned được.
   ============================================================ */
CREATE OR ALTER TRIGGER trg_WorkSchedule_Validate
ON WorkSchedule
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Nếu không có bảng LeaveRequest thì bỏ qua rule nghỉ phép
    IF OBJECT_ID('dbo.LeaveRequest', 'U') IS NULL
        RETURN;

    /* ------------------------------------------------------------
       Rule:
       - LeaveRequest.Status = 'Approved'
       - WorkSchedule.WorkDate nằm trong [StartDate, EndDate]
       ------------------------------------------------------------ */
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN LeaveRequest lr
          ON lr.EmployeeID = i.EmployeeID
         AND lr.Status = 'Approved'
         AND i.WorkDate BETWEEN lr.StartDate AND lr.EndDate
    )
    BEGIN
        RAISERROR (N'Không thể phân công lịch: nhân viên đã được duyệt nghỉ vào ngày này.', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO


/* ============================================================
   TRIGGER (QL5): trg_Discount_Validate
   Bảng: Discount
   Mục tiêu:
   - Validate dữ liệu chương trình khuyến mãi theo policy hệ thống
   Rule:
   (1) StartDate < EndDate (không NULL)
   (2) Percentage nằm trong [0, 15]
   ============================================================ */
CREATE OR ALTER TRIGGER trg_Discount_Validate
ON Discount
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    /* ------------------------------------------------------------
       (1) Kiểm tra thời gian hợp lệ
       ------------------------------------------------------------ */
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE i.StartDate IS NULL
           OR i.EndDate IS NULL
           OR i.StartDate >= i.EndDate
    )
    BEGIN
        RAISERROR (N'Khuyến mãi không hợp lệ: StartDate phải nhỏ hơn EndDate (và không được NULL).', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    /* ------------------------------------------------------------
       (2) Kiểm tra phần trăm giảm hợp lệ: [0, 15]
       ------------------------------------------------------------ */
    IF EXISTS (
        SELECT 1
        FROM inserted i
        WHERE i.Percentage IS NULL
           OR i.Percentage < 0
           OR i.Percentage > 15
    )
    BEGIN
        RAISERROR (N'Khuyến mãi không hợp lệ: Percentage chỉ được phép trong khoảng [0, 15].', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END
GO
