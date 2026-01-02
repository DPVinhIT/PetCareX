USE PetCareX_DB;
GO

-- =============================================
-- TRIGGER 1: Tự động tính TemporaryPrice khi thêm OrderDetail
-- =============================================
CREATE TRIGGER trg_AutoCalculateTemporaryPrice
ON OrderDetail
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO OrderDetail (OrderID, ProductID, Quantity, TemporaryPrice)
    SELECT 
        i.OrderID,
        i.ProductID,
        i.Quantity,
        CASE 
            WHEN i.TemporaryPrice IS NULL OR i.TemporaryPrice = 0 
            THEN i.Quantity * p.SellingPrice
            ELSE i.TemporaryPrice
        END
    FROM inserted i
    JOIN Product p ON i.ProductID = p.ProductID;
END;
GO

-- =============================================
-- TRIGGER 2: Kiểm tra số lượng tồn kho trước khi thêm OrderDetail
-- (StockQuantity nằm ở bảng BranchProduct, cần JOIN qua Orders để lấy BranchID)
-- =============================================
CREATE TRIGGER trg_ValidateStockBeforeOrder
ON OrderDetail
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN Orders o ON i.OrderID = o.OrderID
        JOIN BranchProduct bp ON i.ProductID = bp.ProductID AND o.BranchID = bp.BranchID
        WHERE i.Quantity > bp.StockQuantity
    )
    BEGIN
        RAISERROR(N'Lỗi: Số lượng đặt hàng vượt quá số lượng tồn kho tại chi nhánh!', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;
GO

-- =============================================
-- TRIGGER 3: Cập nhật tồn kho khi hoàn thành đơn hàng
-- (Trừ StockQuantity trong bảng BranchProduct)
-- =============================================
CREATE TRIGGER trg_UpdateStockOnOrderComplete
ON Orders
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Chỉ xử lý khi Status thay đổi từ 'Pending' sang 'Completed'
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN deleted d ON i.OrderID = d.OrderID
        WHERE i.Status = 'Completed' AND d.Status = 'Pending'
    )
    BEGIN
        UPDATE BranchProduct
        SET StockQuantity = StockQuantity - od.Quantity
        FROM BranchProduct bp
        JOIN OrderDetail od ON bp.ProductID = od.ProductID
        JOIN inserted i ON od.OrderID = i.OrderID AND bp.BranchID = i.BranchID
        JOIN deleted d ON i.OrderID = d.OrderID
        WHERE i.Status = 'Completed' AND d.Status = 'Pending';
    END
END;
GO

-- =============================================
-- TRIGGER 4: Ngăn xóa hóa đơn đã thanh toán
-- =============================================
CREATE TRIGGER trg_PreventInvoiceDelete
ON Invoice
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;
    
    RAISERROR(N'Lỗi: Không được phép xóa hóa đơn đã thanh toán! Vui lòng liên hệ quản trị viên.', 16, 1);
    RETURN;
END;
GO

-- =============================================
-- TRIGGER 5: Kiểm tra mã giảm giá còn hiệu lực trước khi áp dụng
-- =============================================
CREATE TRIGGER trg_ValidateDiscountBeforeApply
ON ApplyDiscount
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra mã giảm giá có hết hạn không
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN Discount d ON i.DiscountID = d.DiscountID
        WHERE GETDATE() NOT BETWEEN d.StartDate AND d.EndDate
    )
    BEGIN
        RAISERROR(N'Lỗi: Mã giảm giá đã hết hạn hoặc chưa đến thời gian sử dụng!', 16, 1);
        RETURN;
    END
    
    INSERT INTO ApplyDiscount (InvoiceID, DiscountID, AppliedDate)
    SELECT InvoiceID, DiscountID, ISNULL(AppliedDate, GETDATE())
    FROM inserted;
END;
GO
