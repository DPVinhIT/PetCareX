USE PetCareX_DB
GO

CREATE PROC sp_SearchEmployees
    @EmployeeID VARCHAR(20) = NULL,
    @Name		NVARCHAR(100) = NULL,    
    @Gender     NVARCHAR(10) = NULL   -- N'Nam' / N'Nữ' / NULL (None)
AS
BEGIN
    SET NOCOUNT ON;

    -- Chuẩn hoá input
    SET @EmployeeID = NULLIF(LTRIM(RTRIM(@EmployeeID)), '');
    SET @Gender     = NULLIF(LTRIM(RTRIM(@Gender)), '');

    SELECT
        e.EmployeeID,     
        e.FullName,  
		e.Birthday,
        e.Gender,          
        e.PhoneNumber,    -- nếu CCCD của bạn nằm cột khác thì đổi lại
        e.StartDate,        -- nếu chưa có Address thì bỏ dòng này hoặc đổi đúng tên cột
        e.BaseSalary,
        e.Role,
        e.MID,
        m.FullName        AS ManagerName
    FROM Employee e
    LEFT JOIN Employee m ON m.EmployeeID = e.MID
    WHERE
        (@EmployeeID IS NULL OR e.EmployeeID = @EmployeeID)
        AND (@Gender IS NULL OR e.Gender = @Gender)
    ORDER BY e.EmployeeID;
END
GO

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
        IF @Role IS NOT NULL AND @Role NOT IN (N'Manager', N'Doctor', N'Nurse', N'Receptionist', N'Cashier', N'SalePerson')
        BEGIN
            SET @result = -8; -- Role không hợp lệ (Manager/Doctor/Nurse/Receptionist/Cashier/SalePerson)
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

CREATE PROC sp_GetLeaveRequests
    @RequestDate DATE = NULL,
    @Status      VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        lr.EmployeeID,
        e.FullName,  
        lr.StartDate,
        lr.EndDate,   
        lr.Reason,    
        lr.Status 
    FROM LeaveRequest lr
    JOIN Employee e ON lr.EmployeeID = e.EmployeeID
    WHERE
        (@Status IS NULL OR lr.Status = @Status)
        AND (@RequestDate IS NULL OR lr.RequestDate = @RequestDate)
    ORDER BY lr.EmployeeID ASC;
END
GO


CREATE PROC sp_UpdateLeaveRequestStatus
    @EmployeeID VARCHAR(20),
    @StartDate DATE,
    @EndDate DATE,
    @Status NVARCHAR(50), -- 'Approved' hoặc 'Rejected'
    @MID VARCHAR(20) = NULL,     -- Manager phê duyệt
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

CREATE OR ALTER PROC sp_GetWorkSchedule
    @EmployeeID VARCHAR(20)   = NULL,
    @WorkDate   DATE          = NULL,
    @Shift      NVARCHAR(50)  = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Chuẩn hoá chuỗi rỗng => NULL
    SET @EmployeeID = NULLIF(LTRIM(RTRIM(@EmployeeID)), '');
    SET @Shift      = NULLIF(LTRIM(RTRIM(@Shift)), '');

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
    WHERE
        (@EmployeeID IS NULL OR ws.EmployeeID = @EmployeeID)
        AND (@WorkDate IS NULL OR ws.WorkDate = @WorkDate)
        AND (@Shift IS NULL OR ws.Shift = @Shift)
    ORDER BY ws.WorkTime;
END
GO

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

CREATE PROC sp_SearchDiscounts
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
        e.FullName AS ManagerName
    FROM Discount d
    LEFT JOIN Employee e ON d.MID = e.EmployeeID
    ORDER BY d.StartDate DESC;
END
GO

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

CREATE PROC sp_Stat_RevenueByBranch
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ToDateNext DATE = DATEADD(DAY, 1, @ToDate);

    ;WITH ProductPart AS (
        SELECT
            o.BranchID,
            ProductRevenue  = SUM(i.TotalPrice),
            ProductInvoices = COUNT(*),
            ServiceRevenue  = CAST(0 AS DECIMAL(18,2)),
            ServiceInvoices = 0
        FROM Invoice i
        JOIN Orders o ON o.OrderID = i.OrderID
        WHERE i.OrderID IS NOT NULL
          AND i.CreatedDate >= @FromDate
          AND i.CreatedDate <  @ToDateNext
        GROUP BY o.BranchID
    ),
    ServicePart AS (
        SELECT
            os.BranchID,
            ProductRevenue  = CAST(0 AS DECIMAL(18,2)),
            ProductInvoices = 0,
            ServiceRevenue  = SUM(i.TotalPrice),
            ServiceInvoices = COUNT(*)
        FROM Invoice i
        JOIN OrderService os ON os.OrderServiceID = i.OrderServiceID
        WHERE i.OrderServiceID IS NOT NULL
          AND i.CreatedDate >= @FromDate
          AND i.CreatedDate <  @ToDateNext
        GROUP BY os.BranchID
    ),
    Combined AS (
        SELECT * FROM ProductPart
        UNION ALL
        SELECT * FROM ServicePart
    )
    SELECT
        b.BranchID,
        b.BranchName,
        ProductRevenue   = SUM(c.ProductRevenue),
        ServiceRevenue   = SUM(c.ServiceRevenue),
        TotalRevenue     = SUM(c.ProductRevenue) + SUM(c.ServiceRevenue),
        ProductInvoices  = SUM(c.ProductInvoices),
        ServiceInvoices  = SUM(c.ServiceInvoices)
    FROM Combined c
    JOIN Branch b ON b.BranchID = c.BranchID
    GROUP BY b.BranchID, b.BranchName
    ORDER BY BranchID ASC, TotalRevenue DESC
    OPTION (RECOMPILE);
END
GO

CREATE PROC sp_Stat_RevenueByDoctor
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ToDateNext DATE = DATEADD(DAY, 1, @ToDate);

    ;WITH SvcCount AS (
        SELECT OrderServiceID, Cnt = COUNT(*)
        FROM OrderSDetail
        GROUP BY OrderServiceID
    ),
    Lines AS (
        SELECT
            i.InvoiceID,
            s.DID,
            LineRevenue = CAST(i.TotalPrice AS DECIMAL(18,2)) / NULLIF(sc.Cnt, 0)
        FROM Invoice i
        JOIN SvcCount sc      ON sc.OrderServiceID = i.OrderServiceID
        JOIN OrderSDetail osd ON osd.OrderServiceID = i.OrderServiceID
        JOIN Service s        ON s.ServiceID = osd.ServiceID
        WHERE i.OrderServiceID IS NOT NULL
          AND i.CreatedDate >= @FromDate
          AND i.CreatedDate <  @ToDateNext
    )
    SELECT
        d.DID,
        DoctorName = e.FullName,
        Revenue = SUM(l.LineRevenue),
        ServiceLineCount = COUNT(*),
        InvoiceCount = COUNT(DISTINCT l.InvoiceID)
    FROM Lines l
    JOIN Doctor d   ON d.DID = l.DID
    JOIN Employee e ON e.EmployeeID = d.DID
    GROUP BY d.DID, e.FullName
    ORDER BY Revenue DESC
    OPTION (RECOMPILE);
END
GO

CREATE OR ALTER PROC sp_Stat_ServiceVolume
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ToDateNext DATE = DATEADD(DAY, 1, @ToDate);

    ;WITH X AS (
        SELECT
            PeriodKey = CONVERT(DATE, ExaminationDate),
            Cnt = COUNT(*)
        FROM Examination
        WHERE ExaminationDate >= @FromDate
          AND ExaminationDate <  @ToDateNext
        GROUP BY CONVERT(DATE, ExaminationDate)
    ),
    V AS (
        SELECT
            PeriodKey = CONVERT(DATE, VaccinationDate),
            Cnt = COUNT(*)
        FROM Vaccination
        WHERE VaccinationDate >= @FromDate
          AND VaccinationDate <  @ToDateNext
        GROUP BY CONVERT(DATE, VaccinationDate)
    ),
    S AS (
        SELECT
            PeriodKey = CONVERT(DATE, SurgeryDate),
            Cnt = COUNT(*)
        FROM Surgery
        WHERE SurgeryDate >= @FromDate
          AND SurgeryDate <  @ToDateNext
        GROUP BY CONVERT(DATE, SurgeryDate)
    ),
    P AS (
        SELECT PeriodKey FROM X
        UNION
        SELECT PeriodKey FROM V
        UNION
        SELECT PeriodKey FROM S
    )
    SELECT
        p.PeriodKey,
        ExaminationCount = ISNULL(x.Cnt, 0),
        VaccinationCount = ISNULL(v.Cnt, 0),
        SurgeryCount     = ISNULL(s.Cnt, 0)
    FROM P p
    LEFT JOIN X x ON x.PeriodKey = p.PeriodKey
    LEFT JOIN V v ON v.PeriodKey = p.PeriodKey
    LEFT JOIN S s ON s.PeriodKey = p.PeriodKey
    ORDER BY p.PeriodKey
    OPTION (RECOMPILE);
END
GO

CREATE PROC sp_Stat_ProductSalesRevenue
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ToDateNext DATE = DATEADD(DAY, 1, @ToDate);

    SELECT
        p.ProductID,
        p.ProductName,
        TotalQty = SUM(od.Quantity),
        Revenue  = SUM(CAST(od.Quantity AS DECIMAL(18,2)) * CAST(od.TemporaryPrice AS DECIMAL(18,2)))
    FROM Invoice i
    JOIN Orders o        ON o.OrderID = i.OrderID
    JOIN OrderDetail od  ON od.OrderID = o.OrderID
    JOIN Product p       ON p.ProductID = od.ProductID
    WHERE i.OrderID IS NOT NULL
      AND i.CreatedDate >= @FromDate
      AND i.CreatedDate <  @ToDateNext
    GROUP BY p.ProductID, p.ProductName
    ORDER BY Revenue DESC
    OPTION (RECOMPILE);
END
GO

CREATE PROC sp_Stat_TotalRevenue
    @FromDate DATE,
    @ToDate   DATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ToDateNext DATE = DATEADD(DAY, 1, @ToDate);

    SELECT
        TotalRevenue   = SUM(i.TotalPrice),
        InvoiceCount   = COUNT(*),
        ProductRevenue = SUM(CASE WHEN i.OrderID IS NOT NULL THEN i.TotalPrice ELSE 0 END),
        ServiceRevenue = SUM(CASE WHEN i.OrderServiceID IS NOT NULL THEN i.TotalPrice ELSE 0 END),
        AvgInvoice     = AVG(CAST(i.TotalPrice AS DECIMAL(18,2)))
    FROM Invoice i
    WHERE i.CreatedDate >= @FromDate
      AND i.CreatedDate <  @ToDateNext
    OPTION (RECOMPILE);
END
GO

CREATE OR ALTER PROC dbo.sp_ChangePassword
    @Username        NVARCHAR(50),
    @OldPassword     NVARCHAR(255),
    @NewPassword     NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Check tài khoản + mật khẩu cũ đúng
    IF NOT EXISTS (
        SELECT 1
        FROM AccountLogin
        WHERE Username = @Username AND Password = @OldPassword
    )
    BEGIN
        SELECT 0 AS Result, N'Sai mật khẩu cũ hoặc không tồn tại tài khoản.' AS Message;
        RETURN;
    END

    -- Update mật khẩu mới
    UPDATE AccountLogin
    SET Password = @NewPassword
    WHERE Username = @Username;

    SELECT 1 AS Result, N'Đổi mật khẩu thành công.' AS Message;
END
GO
