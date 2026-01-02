-- Thu ngân chỉ cần tìm những đơn CHƯA THANH TOÁN (Pending)
USE PetCareX_DB;
GO

CREATE OR ALTER PROC get_orderNotYetPaid
	@id_customer VARCHAR(20)
AS
BEGIN
SELECT 
    o.OrderID,
    c.FullName AS "Tên Khách",
    e.FullName AS "NV Bán Hàng",
    o.CreateDate
FROM Orders o
JOIN Customer c ON o.CustomerID = c.CustomerID
JOIN Employee e ON o.SalesPersonID = e.EmployeeID
WHERE o.Status = 'Created'
AND o.CustomerID = @id_customer
END
go


CREATE OR ALTER PROCEDURE sp_GetOrderDetail
    @OrderID VARCHAR(20) -- Đầu vào là Mã Đơn Hàng thu ngân vừa chọn
AS
BEGIN
    SELECT 
        p.ProductID AS "Mã SP",
        p.ProductName AS "Tên Sản Phẩm",
        od.Quantity AS "Số Lượng",
        
        -- Lấy giá niêm yết hiện tại để tham chiếu
        p.SellingPrice AS "Đơn Giá Niêm Yết",
        
        -- Lấy giá thực tế tại thời điểm bán (lưu trong OrderDetail)
        -- (Vì giá niêm yết có thể đổi, nhưng giá lúc khách mua thì phải giữ nguyên)
        od.TemporaryPrice AS "Thành Tiền (Tạm tính)"
        
    FROM OrderDetail od
    JOIN Product p ON od.ProductID = p.ProductID
    WHERE od.OrderID = @OrderID;
END;
GO


GO

CREATE OR ALTER PROCEDURE sp_CreateInvoice
    @OrderID VARCHAR(20),        
    @CashierID VARCHAR(20),       
    @PaymentMethodID VARCHAR(20),
    @PaymentMoney DECIMAL(18, 2),
    @promotion Float = 0,         
    @discountID VARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @CustomerID VARCHAR(20);
        DECLARE @CardID VARCHAR(20); 
        SELECT @CustomerID = CustomerID 
        FROM Orders 
        WHERE OrderID = @OrderID AND Status = 'Created';

        -- 1. KIỂM TRA ĐƠN HÀNG
        IF @CustomerID IS NULL 
        BEGIN
            RAISERROR(N'Lỗi: Đơn hàng không tồn tại hoặc trạng thái không phải Created.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        SELECT TOP 1 @CardID = CardID
        FROM CardMembership
        WHERE CustomerID = @CustomerID
        ORDER BY RegistrationDate DESC; 
        -- =================================================

        IF EXISTS (SELECT 1 FROM OrderDetail WHERE OrderID = @OrderID AND TemporaryPrice <= 0)
        BEGIN
            RAISERROR(N'Lỗi: Có sản phẩm giá <= 0.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 2. TÍNH TOÁN TIỀN
        DECLARE @ProductTotal DECIMAL(18, 2);
        SELECT @ProductTotal = SUM(TemporaryPrice) FROM OrderDetail WHERE OrderID = @OrderID;

        -- Lấy chiết khấu thẻ thành viên (Dùng @CardID vừa tìm được)
        DECLARE @MemberDiscountRate FLOAT = 0;
        IF @CardID IS NOT NULL
        BEGIN
            SELECT @MemberDiscountRate = l.DiscountRate
            FROM CardMembership c
            JOIN MembershipLevel l ON c.LevelID = l.LevelID
            WHERE c.CardID = @CardID;
        END

        -- Lấy chiết khấu mã giảm giá (Discount ID) - Validate theo MinLevelID
        DECLARE @CouponDiscountRate FLOAT = 0;
        DECLARE @IsDiscountValid BIT = 0;
        DECLARE @CustomerLevelID VARCHAR(10) = NULL;
        
        -- Lấy Level của khách hàng
        IF @CardID IS NOT NULL
        BEGIN
            SELECT @CustomerLevelID = LevelID 
            FROM CardMembership 
            WHERE CardID = @CardID;
        END
        
        IF @discountID IS NOT NULL
        BEGIN
            -- Kiểm tra discount còn hiệu lực VÀ khách hàng đủ level
            SELECT @CouponDiscountRate = d.Percentage, @IsDiscountValid = 1
            FROM Discount d
            WHERE d.DiscountID = @discountID 
              AND GETDATE() BETWEEN d.StartDate AND d.EndDate
              AND (
                  d.MinLevelID IS NULL  -- NULL = áp dụng tất cả
                  OR (
                      @CustomerLevelID IS NOT NULL 
                      AND @CustomerLevelID >= d.MinLevelID  -- L1 < L2 < L3
                  )
              );
            
            IF @@ROWCOUNT = 0 
            BEGIN
                SET @CouponDiscountRate = 0;
                -- Không raise error, chỉ bỏ qua discount không hợp lệ
            END
        END

        -- TÍNH TỔNG THỰC TẾ
        DECLARE @FinalTotal DECIMAL(18, 2);
        SET @FinalTotal = @ProductTotal * (1 - ISNULL(@MemberDiscountRate, 0)) 
                                        * (1 - ISNULL(@CouponDiscountRate, 0)) 
                                        * (1 - ISNULL(@promotion, 0));
        
        SET @FinalTotal = ROUND(@FinalTotal, 0); 

        -- VALIDATION TIỀN
        IF @FinalTotal < 0
        BEGIN
            RAISERROR(N'Lỗi: Tổng tiền hóa đơn bị âm.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        IF @PaymentMoney < @FinalTotal
        BEGIN
            DECLARE @ErrStr NVARCHAR(200) = N'Lỗi: Khách đưa thiếu tiền. Cần: ' + FORMAT(@FinalTotal, 'N0');
            RAISERROR(@ErrStr, 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- 3. TẠO HÓA ĐƠN
        DECLARE @NewInvoiceID VARCHAR(20);
        SET @NewInvoiceID = 'INV' + REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR, GETDATE(), 120), '-', ''), ':', ''), ' ', '');

        -- (Đã có @CustomerID ở bước 0 rồi, không cần SELECT lại nữa)

        INSERT INTO Invoice (
            InvoiceID, OrderID, CustomerID, CID, CardID, 
            CreatedDate, CreatedTime, 
            TotalPrice, PaymentMoney, PaymentTypeID
        )
        VALUES (
            @NewInvoiceID, @OrderID, @CustomerID, @CashierID, @CardID,
            GETDATE(), CONVERT(TIME, GETDATE()),
            @FinalTotal, @PaymentMoney, @PaymentMethodID
        );

        IF @IsDiscountValid = 1
        BEGIN
            INSERT INTO ApplyDiscount(InvoiceID, DiscountID, AppliedDate)
            VALUES (@NewInvoiceID, @discountID, GETDATE());
        END

        -- OrderID đã được thêm trực tiếp vào Invoice ở trên

        UPDATE Orders SET Status = 'Completed' WHERE OrderID = @OrderID;

        -- 4. CẬP NHẬT ĐIỂM LOYALTY & CẤP ĐỘ THÀNH VIÊN
        IF @CardID IS NOT NULL
        BEGIN
            DECLARE @PointsEarned INT;
            SET @PointsEarned = CAST((@FinalTotal / 50000) AS INT);

            IF @PointsEarned > 0
            BEGIN
                -- Cộng điểm
                UPDATE CardMembership
                SET LoyalPoint = ISNULL(LoyalPoint, 0) + @PointsEarned
                WHERE CardID = @CardID;

                -- Lấy tổng điểm mới
                DECLARE @CurrentPoints INT;
                SELECT @CurrentPoints = LoyalPoint 
                FROM CardMembership 
                WHERE CardID = @CardID;

                -- Xét hạng
                DECLARE @NewLevelID VARCHAR(10);
                IF @CurrentPoints >= 240
                    SET @NewLevelID = 'L3'; 
                ELSE IF @CurrentPoints >= 100
                    SET @NewLevelID = 'L2'; 
                ELSE
                    SET @NewLevelID = 'L1'; 

                -- Cập nhật Level
                UPDATE CardMembership
                SET LevelID = @NewLevelID
                WHERE CardID = @CardID;
                
                PRINT N'Tích điểm thành công cho thẻ: ' + @CardID;
            END
        END

        COMMIT TRANSACTION;
        
        SELECT * FROM Invoice WHERE InvoiceID = @NewInvoiceID;
        PRINT N'Thanh toán thành công! Mã hóa đơn: ' + @NewInvoiceID;

    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;
GO

-- =============================================
-- Lấy danh sách hóa đơn đã thanh toán trong ngày
-- =============================================
CREATE OR ALTER PROCEDURE sp_GetTodayInvoices
    @CashierID VARCHAR(20) = NULL  -- Tùy chọn: lọc theo thu ngân
AS
BEGIN
    SELECT 
        i.InvoiceID AS "Mã HĐ",
        c.FullName AS "Khách hàng",
        c.PhoneNumber AS "SĐT",
        i.OrderID AS "Mã đơn",
        i.TotalPrice AS "Tổng tiền",
        pt.MethodName AS "PT Thanh toán",
        e.FullName AS "Thu ngân",
        CAST(i.CreatedDate AS DATETIME) + CAST(i.CreatedTime AS DATETIME) AS "Thời gian"
    FROM Invoice i
    JOIN Orders o ON i.OrderID = o.OrderID
    JOIN Customer c ON o.CustomerID = c.CustomerID
    JOIN PaymentMethod pt ON i.PaymentTypeID = pt.PaymentTypeID
    JOIN Employee e ON i.CID = e.EmployeeID
    WHERE CAST(i.CreatedDate AS DATE) = CAST(GETDATE() AS DATE)
      AND (@CashierID IS NULL OR i.CID = @CashierID)
    ORDER BY i.CreatedDate DESC, i.CreatedTime DESC;
END;
GO
