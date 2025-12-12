USE PetCareX_DB
GO

-- ============================================================
-- QL1: QUẢN LÝ THÔNG TIN NHÂN VIÊN
-- 3 chức năng:
--   1. Xem tất cả nhân viên
--   2. Xem theo mã ID
--   3. Tìm kiếm theo ID để chỉnh sửa
-- ============================================================

-- =========================
-- XEM TẤT CẢ NHÂN VIÊN
-- =========================
CREATE PROC sp_GetAllEmployees
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        e.EmployeeID,
        e.FullName,
        e.Birthday,
        e.Gender,
        e.PhoneNumber,
        e.StartDate,
        e.BaseSalary,
        e.Role,
        m.FullName AS ManagerName
    FROM Employee e
    LEFT JOIN Employee m ON e.MID = m.EmployeeID
    ORDER BY e.EmployeeID;
END
GO

-- =========================
-- XEM THEO MÃ ID
-- =========================
CREATE PROC sp_GetEmployeeById
    @EmployeeID VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra ID có tồn tại không
    IF NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @EmployeeID)
    BEGIN
        SET @result = 0; -- Không tìm thấy
        RETURN;
    END
    
    SET @result = 1; -- Tìm thấy
    
    SELECT 
        e.EmployeeID,
        e.FullName,
        e.Birthday,
        e.Gender,
        e.PhoneNumber,
        e.StartDate,
        e.BaseSalary,
        e.Role,
        e.MID,
        m.FullName AS ManagerName
    FROM Employee e
    LEFT JOIN Employee m ON e.MID = m.EmployeeID
    WHERE e.EmployeeID = @EmployeeID;
END
GO

-- =========================
-- CHỈNH SỬA THÔNG TIN THEO ID
-- =========================
CREATE PROC sp_UpdateEmployeeById
    @EmployeeID VARCHAR(20),
    @FullName NVARCHAR(100),
    @Birthday DATE,
    @Gender NVARCHAR(10),
    @PhoneNumber VARCHAR(15),
    @StartDate DATE,
    @BaseSalary DECIMAL(18,2),
    @Role NVARCHAR(50),
    @MID VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- 1. Kiểm tra nhân viên tồn tại
        IF NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @EmployeeID)
        BEGIN
            SET @result = 0; -- Không tìm thấy nhân viên
            RETURN;
        END
        
        -- 2. Kiểm tra MID có hợp lệ không (nếu có nhập)
        IF @MID IS NOT NULL AND @MID <> '' 
           AND NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @MID)
        BEGIN
            SET @result = -2; -- Manager không tồn tại
            RETURN;
        END
        
        -- 3. Kiểm tra MID không được trùng với EmployeeID (không tự quản lý mình)
        IF @MID = @EmployeeID
        BEGIN
            SET @result = -3; -- Không thể tự làm quản lý của chính mình
            RETURN;
        END
        
        -- 4. Kiểm tra FullName không rỗng
        IF @FullName IS NULL OR LTRIM(RTRIM(@FullName)) = ''
        BEGIN
            SET @result = -4; -- Họ tên không được để trống
            RETURN;
        END
        
        -- 5. Kiểm tra Gender hợp lệ
        IF @Gender IS NOT NULL AND @Gender NOT IN (N'Nam', N'Nữ', N'Khác')
        BEGIN
            SET @result = -5; -- Giới tính không hợp lệ
            RETURN;
        END
        
        -- 6. Kiểm tra BaseSalary >= 0
        IF @BaseSalary IS NOT NULL AND @BaseSalary < 0
        BEGIN
            SET @result = -6; -- Lương không được âm
            RETURN;
        END
        
        -- 7. Kiểm tra Birthday hợp lệ (phải < ngày hiện tại)
        IF @Birthday IS NOT NULL AND @Birthday >= CAST(GETDATE() AS DATE)
        BEGIN
            SET @result = -7; -- Ngày sinh không hợp lệ
            RETURN;
        END
        
        -- 8. Kiểm tra Role hợp lệ
        IF @Role IS NOT NULL AND @Role NOT IN (N'Admin', N'Manager', N'Doctor', N'Nurse', N'Staff', N'Receptionist', N'Cashier')
        BEGIN
            SET @result = -8; -- Role không hợp lệ (Admin/Manager/Doctor/Nurse/Staff/Receptionist/Cashier)
            RETURN;
        END
        
        -- 9. Kiểm tra StartDate hợp lệ (phải <= ngày hiện tại và > Birthday)
        IF @StartDate IS NOT NULL
        BEGIN
            IF @StartDate > CAST(GETDATE() AS DATE)
            BEGIN
                SET @result = -9; -- Ngày bắt đầu không thể trong tương lai
                RETURN;
            END
            IF @Birthday IS NOT NULL AND @StartDate <= @Birthday
            BEGIN
                SET @result = -10; -- Ngày bắt đầu phải sau ngày sinh
                RETURN;
            END
        END
        
        BEGIN TRAN;
        
        -- 10. Cập nhật thông tin
        UPDATE Employee
        SET 
            FullName = @FullName,
            Birthday = @Birthday,
            Gender = @Gender,
            PhoneNumber = @PhoneNumber,
            StartDate = @StartDate,
            BaseSalary = @BaseSalary,
            Role = @Role,
            MID = NULLIF(@MID, '')
        WHERE EmployeeID = @EmployeeID;
        
        COMMIT;
        SET @result = 1; -- Cập nhật thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- ============================================================
-- QL2: XEM VÀ PHÊ DUYỆT ĐƠN NGHỈ PHÉP
-- 2 chức năng:
--   1. Xem tất cả đơn nghỉ phép (mọi trạng thái)
--   2. Phê duyệt đơn (chỉ xem đơn Pending, chỉnh sửa Status)
-- ============================================================

-- =========================
-- XEM TẤT CẢ ĐƠN NGHỈ PHÉP
-- =========================
CREATE PROC sp_GetAllLeaveRequests
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        lr.EmployeeID,
        e.FullName AS EmployeeName,
        lr.StartDate,
        lr.EndDate,
        DATEDIFF(DAY, lr.StartDate, lr.EndDate) + 1 AS TotalDays,
        lr.Reason,
        lr.Status,
        lr.MID,
        m.FullName AS ManagerName
    FROM LeaveRequest lr
    JOIN Employee e ON lr.EmployeeID = e.EmployeeID
    LEFT JOIN Employee m ON lr.MID = m.EmployeeID
    ORDER BY lr.StartDate DESC;
END
GO

-- =========================
-- XEM ĐƠN CHỜ DUYỆT (PENDING)
-- =========================
CREATE PROC sp_GetPendingLeaveRequests
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        lr.EmployeeID,
        e.FullName AS EmployeeName,
        lr.StartDate,
        lr.EndDate,
        DATEDIFF(DAY, lr.StartDate, lr.EndDate) + 1 AS TotalDays,
        lr.Reason,
        lr.Status
    FROM LeaveRequest lr
    JOIN Employee e ON lr.EmployeeID = e.EmployeeID
    WHERE lr.Status = 'Pending'
    ORDER BY lr.StartDate ASC;
END
GO

-- =========================
-- CẬP NHẬT TRẠNG THÁI ĐƠN
-- =========================
CREATE PROC sp_UpdateLeaveRequestStatus
    @EmployeeID VARCHAR(20),
    @StartDate DATE,
    @EndDate DATE,
    @Status NVARCHAR(50), -- 'Approved' hoặc 'Rejected'
    @MID VARCHAR(20),     -- Manager phê duyệt
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- 1. Kiểm tra EndDate >= StartDate
        IF @EndDate < @StartDate
        BEGIN
            SET @result = -5; -- Ngày kết thúc phải >= ngày bắt đầu
            RETURN;
        END
        
        -- 2. Kiểm tra đơn nghỉ phép tồn tại và đang Pending
        IF NOT EXISTS (
            SELECT 1 FROM LeaveRequest 
            WHERE EmployeeID = @EmployeeID 
              AND StartDate = @StartDate 
              AND EndDate = @EndDate
              AND Status = 'Pending'
        )
        BEGIN
            SET @result = 0; -- Không tìm thấy đơn hoặc đơn đã được xử lý
            RETURN;
        END
        
        -- 3. Kiểm tra Status hợp lệ (chỉ cho phép Approved hoặc Rejected)
        IF @Status NOT IN ('Approved', 'Rejected')
        BEGIN
            SET @result = -2; -- Status không hợp lệ
            RETURN;
        END
        
        -- 4. Kiểm tra Manager tồn tại
        IF @MID IS NULL OR NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @MID)
        BEGIN
            SET @result = -3; -- Manager không tồn tại
            RETURN;
        END
        
        -- 5. Kiểm tra Manager không phải là người nộp đơn
        IF @MID = @EmployeeID
        BEGIN
            SET @result = -4; -- Không thể tự phê duyệt đơn của mình
            RETURN;
        END
        
        BEGIN TRAN;
        
        -- 6. Cập nhật Status và MID
        UPDATE LeaveRequest
        SET 
            Status = @Status,
            MID = @MID
        WHERE EmployeeID = @EmployeeID 
          AND StartDate = @StartDate 
          AND EndDate = @EndDate;
        
        COMMIT;
        SET @result = 1; -- Cập nhật thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- ============================================================
-- QL3: XEM LỊCH LÀM VIỆC CỦA NHÂN VIÊN
-- 1 chức năng: Xem lịch làm việc theo ngày
-- ============================================================

-- =========================
-- XEM LỊCH THEO NGÀY
-- =========================
CREATE PROC sp_GetWorkScheduleByDate
    @WorkDate DATE,
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra có lịch làm việc trong ngày không
    IF NOT EXISTS (SELECT 1 FROM WorkSchedule WHERE WorkDate = @WorkDate)
    BEGIN
        SET @result = 0; -- Không có lịch trong ngày này
        RETURN;
    END
    
    SET @result = 1; -- Tìm thấy
    
    SELECT 
        ws.EmployeeID,
        e.FullName AS EmployeeName,
        ws.WorkDate,
        ws.WorkTime,
        ws.Shift,
        ws.MID,
        m.FullName AS ManagerName
    FROM WorkSchedule ws
    JOIN Employee e ON ws.EmployeeID = e.EmployeeID
    LEFT JOIN Employee m ON ws.MID = m.EmployeeID
    WHERE ws.WorkDate = @WorkDate
    ORDER BY ws.WorkTime;
END
GO

-- ============================================================
-- QL4: PHÂN CÔNG LỊCH LÀM VIỆC CHO NHÂN VIÊN
-- 1 chức năng: Thêm lịch làm việc cho nhân viên
-- ============================================================

-- =========================
-- PHÂN CÔNG LỊCH LÀM VIỆC
-- =========================
CREATE PROC sp_AssignWorkSchedule
    @EmployeeID VARCHAR(20),
    @WorkDate DATE,
    @WorkTime INT,
    @Shift NVARCHAR(50),
    @MID VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- 1. Kiểm tra nhân viên tồn tại
        IF NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @EmployeeID)
        BEGIN
            SET @result = 0; -- Nhân viên không tồn tại
            RETURN;
        END
        
        -- 2. Kiểm tra Manager tồn tại (nếu có)
        IF @MID IS NOT NULL AND @MID <> '' 
           AND NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @MID)
        BEGIN
            SET @result = -2; -- Manager không tồn tại
            RETURN;
        END
        
        -- 3. Kiểm tra ngày làm việc phải >= ngày hiện tại
        IF @WorkDate < CAST(GETDATE() AS DATE)
        BEGIN
            SET @result = -3; -- Không thể phân công ngày trong quá khứ
            RETURN;
        END
        
        -- 4. Kiểm tra WorkTime hợp lệ (0-23 giờ)
        IF @WorkTime < 0 OR @WorkTime > 23
        BEGIN
            SET @result = -4; -- Giờ làm việc không hợp lệ (0-23)
            RETURN;
        END
        
        -- 5. Kiểm tra Shift không rỗng
        IF @Shift IS NULL OR LTRIM(RTRIM(@Shift)) = ''
        BEGIN
            SET @result = -5; -- Ca làm việc không được để trống
            RETURN;
        END
        
        -- 6. Kiểm tra nhân viên đã có lịch trùng chưa (cùng ngày, cùng giờ)
        IF EXISTS (
            SELECT 1 FROM WorkSchedule 
            WHERE EmployeeID = @EmployeeID 
              AND WorkDate = @WorkDate 
              AND WorkTime = @WorkTime
        )
        BEGIN
            SET @result = -6; -- Nhân viên đã có lịch làm việc vào thời điểm này
            RETURN;
        END
        
        -- 7. Kiểm tra nhân viên có đơn nghỉ phép được duyệt trong ngày này không
        IF EXISTS (
            SELECT 1 FROM LeaveRequest 
            WHERE EmployeeID = @EmployeeID 
              AND Status = 'Approved'
              AND @WorkDate BETWEEN StartDate AND EndDate
        )
        BEGIN
            SET @result = -7; -- Nhân viên đang nghỉ phép trong ngày này
            RETURN;
        END
        
        BEGIN TRAN;
        
        -- 8. Thêm lịch làm việc
        INSERT INTO WorkSchedule (EmployeeID, WorkDate, WorkTime, Shift, MID)
        VALUES (@EmployeeID, @WorkDate, @WorkTime, @Shift, NULLIF(@MID, ''));
        
        COMMIT;
        SET @result = 1; -- Phân công thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- ============================================================
-- QL5: QUẢN LÝ CHƯƠNG TRÌNH KHUYẾN MÃI
-- 4 chức năng:
--   1. Xem tất cả chương trình khuyến mãi
--   2. Thêm chương trình khuyến mãi
--   3. Sửa chương trình khuyến mãi
--   4. Xóa chương trình khuyến mãi
-- ============================================================

-- =========================
-- XEM TẤT CẢ CHƯƠNG TRÌNH KHUYẾN MÃI
-- =========================
CREATE PROC sp_GetAllDiscounts
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        d.DiscountID,
        d.DiscountName,
        d.StartDate,
        d.EndDate,
        d.TargetUser,
        d.Percentage,
        d.MID,
        e.FullName AS ManagerName,
        CASE 
            WHEN GETDATE() < d.StartDate THEN N'Chưa bắt đầu'
            WHEN GETDATE() BETWEEN d.StartDate AND d.EndDate THEN N'Đang diễn ra'
            ELSE N'Đã kết thúc'
        END AS Status
    FROM Discount d
    LEFT JOIN Employee e ON d.MID = e.EmployeeID
    ORDER BY d.StartDate DESC;
END
GO

-- =========================
-- THÊM CHƯƠNG TRÌNH KHUYẾN MÃI
-- =========================
CREATE PROC sp_AddDiscount
    @DiscountName NVARCHAR(100),
    @StartDate DATE,
    @EndDate DATE,
    @TargetUser NVARCHAR(50),
    @Percentage FLOAT,
    @MID VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- 1. Validate dữ liệu bắt buộc
        IF @DiscountName IS NULL OR LTRIM(RTRIM(@DiscountName)) = ''
        BEGIN
            SET @result = -2; -- Thiếu tên chương trình
            RETURN;
        END
        
        -- 2. Kiểm tra ngày hợp lệ
        IF @StartDate IS NULL OR @EndDate IS NULL
        BEGIN
            SET @result = -3; -- Thiếu ngày bắt đầu hoặc kết thúc
            RETURN;
        END
        
        IF @EndDate < @StartDate
        BEGIN
            SET @result = -4; -- Ngày kết thúc phải >= ngày bắt đầu
            RETURN;
        END
        
        -- 3. Kiểm tra Percentage hợp lệ (0-100)
        IF @Percentage IS NULL OR @Percentage <= 0 OR @Percentage > 100
        BEGIN
            SET @result = -5; -- Phần trăm khuyến mãi không hợp lệ
            RETURN;
        END
        
        -- 4. Kiểm tra Manager tồn tại
        IF @MID IS NOT NULL AND @MID <> '' 
           AND NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @MID)
        BEGIN
            SET @result = -6; -- Manager không tồn tại
            RETURN;
        END
        
        BEGIN TRAN;
        
        -- 5. Tạo DiscountID (DST + số thứ tự 4 chữ số, dùng MAX để tránh trùng khi xóa)
        DECLARE @MaxNum INT;
        DECLARE @DiscountID VARCHAR(20);
        
        SELECT @MaxNum = ISNULL(MAX(CAST(SUBSTRING(DiscountID, 4, 4) AS INT)), 0)
        FROM Discount
        WHERE DiscountID LIKE 'DST[0-9][0-9][0-9][0-9]';
        
        SET @DiscountID = 'DST' + RIGHT('0000' + CAST(@MaxNum + 1 AS VARCHAR(4)), 4);
        
        -- 6. Thêm chương trình
        INSERT INTO Discount (DiscountID, DiscountName, StartDate, EndDate, TargetUser, Percentage, MID)
        VALUES (@DiscountID, @DiscountName, @StartDate, @EndDate, @TargetUser, @Percentage, NULLIF(@MID, ''));
        
        COMMIT;
        SET @result = 1; -- Thêm thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- =========================
-- SỬA CHƯƠNG TRÌNH KHUYẾN MÃI
-- =========================
CREATE PROC sp_UpdateDiscount
    @DiscountID VARCHAR(20),
    @DiscountName NVARCHAR(100),
    @StartDate DATE,
    @EndDate DATE,
    @TargetUser NVARCHAR(50),
    @Percentage FLOAT,
    @MID VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- 1. Kiểm tra chương trình tồn tại
        IF NOT EXISTS (SELECT 1 FROM Discount WHERE DiscountID = @DiscountID)
        BEGIN
            SET @result = 0; -- Không tìm thấy chương trình
            RETURN;
        END
        
        -- 2. Kiểm tra tên chương trình không rỗng
        IF @DiscountName IS NULL OR LTRIM(RTRIM(@DiscountName)) = ''
        BEGIN
            SET @result = -2; -- Tên chương trình không được để trống
            RETURN;
        END
        
        -- 3. Kiểm tra ngày hợp lệ
        IF @StartDate IS NULL OR @EndDate IS NULL
        BEGIN
            SET @result = -3; -- Thiếu ngày bắt đầu hoặc kết thúc
            RETURN;
        END
        
        IF @EndDate < @StartDate
        BEGIN
            SET @result = -4; -- Ngày kết thúc phải >= ngày bắt đầu
            RETURN;
        END
        
        -- 4. Kiểm tra Percentage hợp lệ (0-100)
        IF @Percentage IS NULL OR @Percentage <= 0 OR @Percentage > 100
        BEGIN
            SET @result = -5; -- Phần trăm khuyến mãi không hợp lệ
            RETURN;
        END
        
        -- 5. Kiểm tra Manager tồn tại (nếu có)
        IF @MID IS NOT NULL AND @MID <> '' 
           AND NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @MID)
        BEGIN
            SET @result = -6; -- Manager không tồn tại
            RETURN;
        END
        
        BEGIN TRAN;
        
        -- 6. Cập nhật thông tin
        UPDATE Discount
        SET 
            DiscountName = @DiscountName,
            StartDate = @StartDate,
            EndDate = @EndDate,
            TargetUser = @TargetUser,
            Percentage = @Percentage,
            MID = NULLIF(@MID, '')
        WHERE DiscountID = @DiscountID;
        
        COMMIT;
        SET @result = 1; -- Cập nhật thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO

-- =========================
-- XÓA CHƯƠNG TRÌNH KHUYẾN MÃI
-- =========================
CREATE PROC sp_DeleteDiscount
    @DiscountID VARCHAR(20),
    @result INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- 1. Kiểm tra chương trình tồn tại
        IF NOT EXISTS (SELECT 1 FROM Discount WHERE DiscountID = @DiscountID)
        BEGIN
            SET @result = 0; -- Không tìm thấy chương trình
            RETURN;
        END
        
        -- 2. Kiểm tra chương trình đã được áp dụng vào hóa đơn chưa
        IF EXISTS (SELECT 1 FROM ApplyDiscount WHERE DiscountID = @DiscountID)
        BEGIN
            SET @result = -2; -- Không thể xóa, đã có hóa đơn áp dụng
            RETURN;
        END
        
        BEGIN TRAN;
        
        -- 3. Xóa chương trình
        DELETE FROM Discount WHERE DiscountID = @DiscountID;
        
        COMMIT;
        SET @result = 1; -- Xóa thành công
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        SET @result = -1; -- Lỗi hệ thống
    END CATCH
END
GO
