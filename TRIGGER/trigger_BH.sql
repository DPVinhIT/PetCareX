USE PetCareX_DB;
GO
-- =============================================
-- TRIGGER 1:Kiểm tra Khách hàng & Nhân viên
-- =============================================
IF OBJECT_ID('trg_Orders_Validate_BH1') IS NOT NULL
    DROP TRIGGER trg_Orders_Validate_BH1;
GO
CREATE OR ALTER TRIGGER trg_Orders_Validate_BH1
ON dbo.Orders
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra Khách hàng tồn tại
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Customer c ON i.CustomerID = c.CustomerID
        WHERE c.CustomerID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi BH1: Mã khách hàng không tồn tại.', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END

    -- 2. Kiểm tra Nhân viên đúng vai trò SalePerson
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Employee e ON i.SalesPersonID = e.EmployeeID
        WHERE e.EmployeeID IS NULL OR e.Role <> N'SalePerson'
    )
    BEGIN
        RAISERROR (N'Lỗi BH1: Nhân viên bán hàng không hợp lệ (không tồn tại hoặc sai vai trò).', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END

    -- 3. Kiểm tra Chi nhánh tồn tại
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN Branch b ON i.BranchID = b.BranchID
        WHERE b.BranchID IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi BH1: Mã chi nhánh không tồn tại.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
-- =============================================
-- TRIGGER 2:
-- =============================================
CREATE OR ALTER TRIGGER trg_OrderDetail_StockCheck_BH1
ON dbo.OrderDetail
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra số lượng mua > 0
    IF EXISTS (SELECT 1 FROM inserted WHERE Quantity <= 0)
    BEGIN
        RAISERROR (N'Lỗi BH3: Số lượng sản phẩm phải lớn hơn 0.', 16, 1);
        ROLLBACK TRANSACTION; RETURN;
    END

    -- 2. Kiểm tra tồn kho tại ĐÚNG chi nhánh của đơn hàng
    IF EXISTS (
        SELECT 1 
        FROM inserted i
        JOIN Orders o ON i.OrderID = o.OrderID
        JOIN BranchProduct bp ON o.BranchID = bp.BranchID AND i.ProductID = bp.ProductID
        WHERE i.Quantity > bp.StockQuantity
    )
    BEGIN
        RAISERROR (N'Lỗi BH3: Số lượng tồn kho tại chi nhánh không đủ đáp ứng.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO
