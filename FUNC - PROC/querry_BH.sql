USE PetCareX_DB;
GO


IF OBJECT_ID('CreateOrderAndAddItem_BH1') IS NOT NULL
    DROP PROCEDURE CreateOrderAndAddItem_BH1;
GO

-- =============================
-- CHỨC NĂNG: Tạo đơn hàng và thêm sản phẩm vào đơn hàng
-- PROCEDURE: CreateOrderAndAddItem_BH1
-- Tham số: @OrderID, @CustomerID, @SalesPersonID, @ProductID, @Quantity, @BranchID
-- Trả về: Thông báo thành công/thất bại
-- Ví dụ EXEC:
--   EXEC CreateOrderAndAddItem_BH1 @CustomerID = 'CUS0001', @SalesPersonID = 'E0001', @ProductID = 'PRD001', @Quantity = 2, @BranchID = 'BR001';
-- =============================

CREATE PROCEDURE CreateOrderAndAddItem_BH1
    @CustomerID VARCHAR(20),
    @SalesPersonID VARCHAR(20),          -- EmployeeID của nhân viên bán hàng
    @ProductID VARCHAR(20),
    @Quantity INT,
    @BranchID VARCHAR(20),
	@OrderID VARCHAR(20) OUT
AS
BEGIN
	BEGIN TRY
	BEGIN TRAN
    SET NOCOUNT ON; -- Thêm SET NOCOUNT ON để ngăn chặn các thông báo số lượng dòng bị ảnh hưởng

    -- Ràng buộc 1: Kiểm tra tính duy nhất của OrderID
    SET @OrderID = ISNULL('ORD' + RIGHT('000000' + CAST(CAST(RIGHT((SELECT MAX(OrderID) FROM Orders), 6)AS INT) + 1 AS VARCHAR(10)), 6), 'ORD000001')

    -- Ràng buộc 2: Kiểm tra số lượng mua (BH3: Số lượng cần mua >= 0)
    IF @Quantity <= 0 
    BEGIN
        PRINT N'Lỗi: Số lượng sản phẩm cần mua phải lớn hơn 0.' -- Đã sửa
        RETURN
    END

    -- Ràng buộc 3: Kiểm tra Khách hàng, Nhân viên, Chi nhánh, Sản phẩm tồn tại
    IF NOT EXISTS (SELECT 1 FROM Customer WHERE CustomerID = @CustomerID)
    BEGIN
        PRINT N'Lỗi: Mã khách hàng không tồn tại.' -- Đã sửa
        RETURN
    END
    IF NOT EXISTS (SELECT 1 FROM Employee WHERE EmployeeID = @SalesPersonID AND Role = N'SalePerson')
    BEGIN
        PRINT N'Lỗi: Mã nhân viên bán hàng không tồn tại hoặc không đúng vai trò.' -- Đã sửa
        RETURN
    END
    IF NOT EXISTS (SELECT 1 FROM Branch WHERE BranchID = @BranchID)
    BEGIN
        PRINT N'Lỗi: Mã chi nhánh không tồn tại.' -- Đã sửa
        RETURN
    END
    IF NOT EXISTS (SELECT 1 FROM Product WHERE ProductID = @ProductID)
    BEGIN
        PRINT N'Lỗi: Mã sản phẩm không tồn tại.' -- Đã sửa
        RETURN
    END

    -- Ràng buộc 4: Kiểm tra tồn kho và lấy giá bán
    DECLARE @StockQuantity INT
    DECLARE @SellingPrice DECIMAL(18, 2)
    
    SELECT @StockQuantity = StockQuantity
    FROM BranchProduct 
    WHERE BranchID = @BranchID AND ProductID = @ProductID

    SELECT @SellingPrice = SellingPrice
    FROM Product
    WHERE ProductID = @ProductID

    -- Ràng buộc 4a: Kiểm tra tồn kho (BH3: Số lượng tồn kho sản phẩm >= Số lượng cần mua)
    IF @StockQuantity IS NULL OR @StockQuantity < @Quantity
    BEGIN
        PRINT N'Lỗi: Số lượng tồn kho (' + CAST(ISNULL(@StockQuantity, 0) AS VARCHAR) + N') không đủ cho số lượng yêu cầu (' + CAST(@Quantity AS VARCHAR) + N').' -- Đã sửa
        RETURN
    END

    -- Bắt đầu tạo đơn hàng (Orders) và Chi tiết đơn hàng (OrderDetail)
    
    -- 5. Tạo Orders
    INSERT INTO Orders (OrderID, CustomerID, SalesPersonID, CreateDate, CreateTime, Status)
    VALUES (@OrderID, @CustomerID, @SalesPersonID, CONVERT(DATE, GETDATE()), CONVERT(TIME, GETDATE()), N'PENDING_PAYMENT')

    -- 6. Tạo OrderDetail: Giá bán tạm thời (TemporaryPrice) là giá niêm yết tại thời điểm này.
    INSERT INTO OrderDetail (OrderID, ProductID, Quantity, TemporaryPrice)
    VALUES (@OrderID, @ProductID, @Quantity, @SellingPrice)

    PRINT N'Tạo đơn hàng thành công. OrderID: ' + @OrderID + N'. Đã thêm sản phẩm ' + @ProductID + N' vào đơn hàng.'; -- Đã sửa
	COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0 ROLLBACK TRAN
		;THROW;
	END CATCH
END
GO


-- =============================
-- CHỨC NĂNG: Thêm sản phẩm vào đơn hàng có sẵn
-- PROCEDURE: AddItemToOrderDetail
-- Tham số: @OrderID, @ProductID, @Quantity, @BranchID
-- Trả về: Thông báo thành công/thất bại
-- Ví dụ EXEC:
--   EXEC UpdateOrderItemQuantity_BH3 @OrderID = 'ORD0001', @ProductID = 'PRD001', @Quantity = 3, @BranchID = 'BR001';
-- =============================

CREATE OR ALTER PROC AddItemToOrderDetail 
	@OrderID VARCHAR(20), 
	@ProductID VARCHAR(20), 
	@Quantity INT = 1, 
	@BranchID VARCHAR(20)
AS 
BEGIN
	SET NOCOUNT ON; -- Thêm SET NOCOUNT ON

    -- Ràng buộc 1: Kiểm tra đơn hàng và Sản phẩm trong đơn hàng tồn tại
    IF NOT EXISTS (SELECT 1 FROM Orders WHERE OrderID = @OrderID)
    BEGIN
        PRINT N'Lỗi: Mã đơn hàng không tồn tại.' -- Đã sửa
        RETURN
    END
    
    -- Ràng buộc 2: Kiểm tra Số lượng mới (BH3: Số lượng cần mua >= 0)
    IF @Quantity < 0 
    BEGIN
        PRINT N'Lỗi: Số lượng sản phẩm cần mua không thể âm.' -- Đã sửa
        RETURN
    END

    -- Ràng buộc 3: Kiểm tra Số lượng tồn kho sản phẩm (BH3)
    DECLARE @StockQuantity INT
    SELECT @StockQuantity = StockQuantity 
    FROM BranchProduct 
    WHERE BranchID = @BranchID AND ProductID = @ProductID

    IF @StockQuantity IS NULL OR @StockQuantity < @Quantity
    BEGIN
        PRINT N'Lỗi: Số lượng tồn kho (' + CAST(ISNULL(@StockQuantity, 0) AS VARCHAR) + N') không đủ cho số lượng cập nhật (' + CAST(@Quantity AS VARCHAR) + N').' -- Đã sửa
        RETURN
    END

	IF EXISTS (SELECT od.ProductID FROM OrderDetail od WHERE od.OrderID = @OrderID AND od.ProductID = @ProductID)
	BEGIN
		UPDATE OrderDetail
		SET	Quantity = Quantity + @Quantity
		WHERE OrderID = @OrderID AND ProductID = @ProductID
	END
	ELSE
	BEGIN
	DECLARE @Price DECIMAL(18, 2) = ISNULL(@Quantity * (SELECT TOP 1 p.SellingPrice FROM Product p WHERE p.ProductID = @ProductID), 0)
		INSERT INTO OrderDetail(OrderID, ProductID, Quantity, TemporaryPrice)
		VALUES (@OrderID, @ProductID, @Quantity, @Price)
	END
   
END



IF OBJECT_ID('UpdateOrderItemQuantity_BH3') IS NOT NULL
    DROP PROCEDURE UpdateOrderItemQuantity_BH3;
GO

-- =============================
-- CHỨC NĂNG: Cập nhật số lượng sản phẩm trong đơn hàng
-- PROCEDURE: UpdateOrderItemQuantity_BH3
-- Tham số: @OrderID, @ProductID, @NewQuantity, @BranchID
-- Trả về: Thông báo thành công/thất bại
-- Ví dụ EXEC:
--   EXEC UpdateOrderItemQuantity_BH3 @OrderID = 'ORD0001', @ProductID = 'PRD001', @NewQuantity = 3, @BranchID = 'BR001';
-- =============================

CREATE PROCEDURE UpdateOrderItemQuantity_BH3
    @OrderID VARCHAR(20),
    @ProductID VARCHAR(20),
    @NewQuantity INT,
    @BranchID VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON; -- Thêm SET NOCOUNT ON

    -- Ràng buộc 1: Kiểm tra đơn hàng và Sản phẩm trong đơn hàng tồn tại
    IF NOT EXISTS (SELECT 1 FROM Orders WHERE OrderID = @OrderID)
    BEGIN
        PRINT N'Lỗi: Mã đơn hàng không tồn tại.' -- Đã sửa
        RETURN
    END
    IF NOT EXISTS (SELECT 1 FROM OrderDetail WHERE OrderID = @OrderID AND ProductID = @ProductID)
    BEGIN
        PRINT N'Lỗi: Sản phẩm không tồn tại trong đơn hàng này.' -- Đã sửa
        RETURN
    END

    -- Ràng buộc 2: Kiểm tra Số lượng mới (BH3: Số lượng cần mua >= 0)
    IF @NewQuantity < 0 
    BEGIN
        PRINT N'Lỗi: Số lượng sản phẩm cần mua không thể âm.' -- Đã sửa
        RETURN
    END

    -- Ràng buộc 3: Kiểm tra Số lượng tồn kho sản phẩm (BH3)
    DECLARE @StockQuantity INT
    SELECT @StockQuantity = StockQuantity 
    FROM BranchProduct 
    WHERE BranchID = @BranchID AND ProductID = @ProductID

    IF @StockQuantity IS NULL OR @StockQuantity < @NewQuantity
    BEGIN
        PRINT N'Lỗi: Số lượng tồn kho (' + CAST(ISNULL(@StockQuantity, 0) AS VARCHAR) + N') không đủ cho số lượng cập nhật (' + CAST(@NewQuantity AS VARCHAR) + N').' -- Đã sửa
        RETURN
    END
    
    -- Nếu NewQuantity = 0, xóa sản phẩm khỏi đơn hàng
    IF @NewQuantity = 0
    BEGIN
        DELETE FROM OrderDetail
        WHERE OrderID = @OrderID AND ProductID = @ProductID
        PRINT N'Đã xóa sản phẩm ' + @ProductID + N' khỏi đơn hàng ' + @OrderID + N'.'; -- Đã sửa
        RETURN
    END

    -- Cập nhật số lượng
    UPDATE OrderDetail
    SET Quantity = @NewQuantity
    WHERE OrderID = @OrderID AND ProductID = @ProductID
    
    PRINT N'Cập nhật số lượng sản phẩm ' + @ProductID + N' thành công. Số lượng mới: ' + CAST(@NewQuantity AS VARCHAR); -- Đã sửa
END
GO

