/**
 * PetCareX Backend API Server
 * Cung cấp REST API cho frontend
 */

const express = require('express');
const cors = require('cors');
const { connectDB, getPool, sql } = require('./db');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 5000;

// Middleware
app.use(cors());
app.use(express.json());

// ============================================
// API: HEALTH CHECK
// ============================================
app.get('/api/health', (req, res) => {
    res.json({ 
        status: 'OK', 
        message: 'PetCareX API đang hoạt động',
        timestamp: new Date().toISOString()
    });
});

// ============================================
// API: BRANCHES - Chi nhánh
// ============================================

// GET tất cả chi nhánh
app.get('/api/branches', async (req, res) => {
    try {
        const pool = getPool();
        const result = await pool.request().query(`
            SELECT 
                BranchID,
                BranchName,
                Address,
                PhoneNumber,
                Email,
                OpenTime,
                CloseTime
            FROM Branch
            ORDER BY BranchID
        `);
        
        res.json({
            success: true,
            data: result.recordset,
            count: result.recordset.length
        });
    } catch (error) {
        console.error('Lỗi lấy branches:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// GET chi nhánh theo ID
app.get('/api/branches/:id', async (req, res) => {
    try {
        const pool = getPool();
        const result = await pool.request()
            .input('branchId', sql.VarChar, req.params.id)
            .query(`
                SELECT * FROM Branch WHERE BranchID = @branchId
            `);
        
        if (result.recordset.length === 0) {
            return res.status(404).json({ success: false, error: 'Không tìm thấy chi nhánh' });
        }
        
        res.json({ success: true, data: result.recordset[0] });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// API: PRODUCTS - Sản phẩm
// ============================================

// GET tất cả sản phẩm
app.get('/api/products', async (req, res) => {
    try {
        const pool = getPool();
        const { type, limit } = req.query;
        
        let query = `
            SELECT 
                ProductID,
                ProductName,
                ProductType,
                SellingPrice
            FROM Product
        `;
        
        if (type && type !== 'all') {
            query += ` WHERE ProductType = @productType`;
        }
        
        query += ` ORDER BY ProductID`;
        
        if (limit) {
            query = `SELECT TOP ${parseInt(limit)} * FROM (${query}) AS sub`;
        }
        
        const request = pool.request();
        if (type && type !== 'all') {
            request.input('productType', sql.NVarChar, type);
        }
        
        const result = await request.query(query);
        
        res.json({
            success: true,
            data: result.recordset,
            count: result.recordset.length
        });
    } catch (error) {
        console.error('Lỗi lấy products:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// GET loại sản phẩm (distinct)
app.get('/api/products/types', async (req, res) => {
    try {
        const pool = getPool();
        const result = await pool.request().query(`
            SELECT DISTINCT ProductType FROM Product ORDER BY ProductType
        `);
        
        res.json({
            success: true,
            data: result.recordset.map(r => r.ProductType)
        });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// API: SERVICES - Dịch vụ
// ============================================

app.get('/api/services', async (req, res) => {
    try {
        const pool = getPool();
        const result = await pool.request().query(`
            SELECT 
                ServiceID,
                ServiceName,
                ServiceDescription,
                DID
            FROM Service
            ORDER BY ServiceID
        `);
        
        res.json({
            success: true,
            data: result.recordset,
            count: result.recordset.length
        });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// API: MEMBERSHIP LEVELS - Cấp độ thành viên
// ============================================

app.get('/api/membership-levels', async (req, res) => {
    try {
        const pool = getPool();
        const result = await pool.request().query(`
            SELECT * FROM MembershipLevel ORDER BY AnnualSpendingThreshold
        `);
        
        res.json({
            success: true,
            data: result.recordset
        });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// API: AUTHENTICATION - Đăng nhập/Đăng ký
// ============================================

// Đăng nhập
app.post('/api/auth/login', async (req, res) => {
    try {
        const { username, password } = req.body;
        
        if (!username || !password) {
            return res.status(400).json({ success: false, error: 'Thiếu username hoặc password' });
        }
        
        const pool = getPool();
        
        // Kiểm tra account
        const accountResult = await pool.request()
            .input('username', sql.VarChar, username)
            .input('password', sql.NVarChar, password)
            .query(`
                SELECT * FROM AccountLogin 
                WHERE Username = @username AND Password = @password
            `);
        
        if (accountResult.recordset.length === 0) {
            return res.status(401).json({ success: false, error: 'Sai tên đăng nhập hoặc mật khẩu' });
        }
        
        // Lấy thông tin customer
        const customerResult = await pool.request()
            .input('username', sql.VarChar, username)
            .query(`
                SELECT c.*, cm.CardID, cm.LoyalPoint, cm.LevelID
                FROM Customer c
                LEFT JOIN CardMembership cm ON c.CustomerID = cm.CustomerID
                WHERE c.Username = @username
            `);
        
        const customer = customerResult.recordset[0] || {};
        
        res.json({
            success: true,
            data: {
                username: username,
                CustomerID: customer.CustomerID,
                FullName: customer.FullName,
                PhoneNumber: customer.PhoneNumber,
                Email: customer.Email,
                LoyalPoint: customer.LoyalPoint || 0,
                LevelID: customer.LevelID || 'L1'
            }
        });
    } catch (error) {
        console.error('Lỗi đăng nhập:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// Đăng ký
app.post('/api/auth/register', async (req, res) => {
    try {
        const { username, password, fullName, phone, email } = req.body;
        
        if (!username || !password || !fullName) {
            return res.status(400).json({ success: false, error: 'Thiếu thông tin bắt buộc' });
        }
        
        const pool = getPool();
        
        // Kiểm tra username đã tồn tại chưa
        const existCheck = await pool.request()
            .input('username', sql.VarChar, username)
            .query(`SELECT * FROM AccountLogin WHERE Username = @username`);
        
        if (existCheck.recordset.length > 0) {
            return res.status(400).json({ success: false, error: 'Tên đăng nhập đã tồn tại' });
        }
        
        // Tạo ID mới
        const customerIdResult = await pool.request().query(`
            SELECT TOP 1 CustomerID FROM Customer ORDER BY CustomerID DESC
        `);
        
        let newCustomerId = 'CUS00001';
        if (customerIdResult.recordset.length > 0) {
            const lastId = customerIdResult.recordset[0].CustomerID;
            const num = parseInt(lastId.replace('CUS', '')) + 1;
            newCustomerId = 'CUS' + num.toString().padStart(5, '0');
        }
        
        // Tạo account
        await pool.request()
            .input('username', sql.VarChar, username)
            .input('password', sql.NVarChar, password)
            .query(`INSERT INTO AccountLogin (Username, Password) VALUES (@username, @password)`);
        
        // Tạo customer
        await pool.request()
            .input('customerId', sql.VarChar, newCustomerId)
            .input('fullName', sql.NVarChar, fullName)
            .input('phone', sql.VarChar, phone || '')
            .input('email', sql.VarChar, email || '')
            .input('username', sql.VarChar, username)
            .query(`
                INSERT INTO Customer (CustomerID, FullName, PhoneNumber, Email, Username)
                VALUES (@customerId, @fullName, @phone, @email, @username)
            `);
        
        // Tạo card membership
        const cardId = 'CARD' + Date.now();
        await pool.request()
            .input('cardId', sql.VarChar, cardId)
            .input('customerId', sql.VarChar, newCustomerId)
            .query(`
                INSERT INTO CardMembership (CardID, RegistrationDate, LoyalPoint, LevelID, CustomerID)
                VALUES (@cardId, GETDATE(), 0, 'L1', @customerId)
            `);
        
        res.json({
            success: true,
            message: 'Đăng ký thành công',
            data: { CustomerID: newCustomerId, username }
        });
    } catch (error) {
        console.error('Lỗi đăng ký:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// API: ORDERS - Đơn hàng
// ============================================

// Tạo đơn hàng mới
app.post('/api/orders', async (req, res) => {
    try {
        const { customerID, items } = req.body;
        
        if (!customerID || !items || items.length === 0) {
            return res.status(400).json({ success: false, error: 'Thiếu thông tin đơn hàng' });
        }
        
        const pool = getPool();
        
        // Tạo OrderID mới
        const orderId = 'ORD' + Date.now();
        
        // Tạo đơn hàng
        await pool.request()
            .input('orderId', sql.VarChar, orderId)
            .input('customerId', sql.VarChar, customerID)
            .query(`
                INSERT INTO Orders (OrderID, CustomerID, CreateDate, CreateTime, Status)
                VALUES (@orderId, @customerId, CAST(GETDATE() AS DATE), CAST(GETDATE() AS TIME), N'Đã đặt')
            `);
        
        // Thêm chi tiết đơn hàng
        for (const item of items) {
            await pool.request()
                .input('orderId', sql.VarChar, orderId)
                .input('productId', sql.VarChar, item.ProductID)
                .input('quantity', sql.Int, item.Quantity)
                .input('price', sql.Decimal(18, 2), item.TemporaryPrice)
                .query(`
                    INSERT INTO OrderDetail (OrderID, ProductID, Quantity, TemporaryPrice)
                    VALUES (@orderId, @productId, @quantity, @price)
                `);
        }
        
        res.json({
            success: true,
            message: 'Đặt hàng thành công',
            data: { OrderID: orderId }
        });
    } catch (error) {
        console.error('Lỗi tạo đơn hàng:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// Lấy đơn hàng của customer
app.get('/api/orders/:customerId', async (req, res) => {
    try {
        const pool = getPool();
        const result = await pool.request()
            .input('customerId', sql.VarChar, req.params.customerId)
            .query(`
                SELECT o.*, 
                    (SELECT SUM(od.Quantity * od.TemporaryPrice) 
                     FROM OrderDetail od WHERE od.OrderID = o.OrderID) as TotalAmount
                FROM Orders o
                WHERE o.CustomerID = @customerId
                ORDER BY o.CreateDate DESC, o.CreateTime DESC
            `);
        
        res.json({
            success: true,
            data: result.recordset
        });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// API: UPDATE LOYALTY POINTS
// ============================================

app.post('/api/loyalty/add', async (req, res) => {
    try {
        const { customerID, points } = req.body;
        
        const pool = getPool();
        await pool.request()
            .input('customerId', sql.VarChar, customerID)
            .input('points', sql.Int, points)
            .query(`
                UPDATE CardMembership 
                SET LoyalPoint = LoyalPoint + @points
                WHERE CustomerID = @customerId
            `);
        
        // Lấy điểm mới
        const result = await pool.request()
            .input('customerId', sql.VarChar, customerID)
            .query(`SELECT LoyalPoint FROM CardMembership WHERE CustomerID = @customerId`);
        
        res.json({
            success: true,
            data: { newPoints: result.recordset[0]?.LoyalPoint || 0 }
        });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// START SERVER
// ============================================

async function startServer() {
    try {
        // Kết nối database trước
        await connectDB();
        
        // Khởi động server
        app.listen(PORT, () => {
            console.log('');
            console.log('🚀 =====================================');
            console.log(`🚀 PetCareX API Server`);
            console.log(`🚀 Đang chạy tại: http://localhost:${PORT}`);
            console.log(`🚀 API endpoint: http://localhost:${PORT}/api`);
            console.log('🚀 =====================================');
            console.log('');
            console.log('📋 Các API có sẵn:');
            console.log('   GET  /api/health           - Kiểm tra server');
            console.log('   GET  /api/branches         - Lấy tất cả chi nhánh');
            console.log('   GET  /api/branches/:id     - Lấy chi nhánh theo ID');
            console.log('   GET  /api/products         - Lấy tất cả sản phẩm');
            console.log('   GET  /api/products/types   - Lấy loại sản phẩm');
            console.log('   GET  /api/services         - Lấy tất cả dịch vụ');
            console.log('   GET  /api/membership-levels- Lấy cấp độ thành viên');
            console.log('   POST /api/auth/login       - Đăng nhập');
            console.log('   POST /api/auth/register    - Đăng ký');
            console.log('   POST /api/orders           - Tạo đơn hàng');
            console.log('   GET  /api/orders/:customerId - Lấy đơn hàng');
            console.log('');
        });
    } catch (error) {
        console.error('❌ Không thể khởi động server:', error.message);
        process.exit(1);
    }
}

// Xử lý khi tắt server
process.on('SIGINT', async () => {
    console.log('\n🛑 Đang tắt server...');
    const { closeDB } = require('./db');
    await closeDB();
    process.exit(0);
});

// Khởi động
startServer();
