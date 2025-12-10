/**
 * PetCareX API Server - Cloud Version (PostgreSQL)
 */

const express = require('express');
const cors = require('cors');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 5000;

// Middleware
app.use(cors());
app.use(express.json());

// Database connection
const { Pool } = require('pg');
const pool = new Pool({
    connectionString: process.env.DATABASE_URL,
    ssl: process.env.NODE_ENV === 'production' ? { rejectUnauthorized: false } : false
});

// ============================================
// API ROUTES
// ============================================

// Health check
app.get('/api/health', (req, res) => {
    res.json({ success: true, message: 'PetCareX API đang hoạt động!', timestamp: new Date() });
});

// Get all branches
app.get('/api/branches', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM "Branch" ORDER BY "BranchID"');
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get branch by ID
app.get('/api/branches/:id', async (req, res) => {
    try {
        const { id } = req.params;
        const result = await pool.query('SELECT * FROM "Branch" WHERE "BranchID" = $1', [id]);
        if (result.rows.length === 0) {
            return res.status(404).json({ success: false, error: 'Không tìm thấy chi nhánh' });
        }
        res.json({ success: true, data: result.rows[0] });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get all products
app.get('/api/products', async (req, res) => {
    try {
        const { type, limit } = req.query;
        let query = 'SELECT * FROM "Product"';
        const params = [];
        
        if (type) {
            params.push(type);
            query += ` WHERE "ProductType" = $1`;
        }
        
        query += ' ORDER BY "ProductID"';
        
        if (limit) {
            params.push(parseInt(limit));
            query += ` LIMIT $${params.length}`;
        }
        
        const result = await pool.query(query, params);
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get product types
app.get('/api/products/types', async (req, res) => {
    try {
        const result = await pool.query('SELECT DISTINCT "ProductType" FROM "Product" ORDER BY "ProductType"');
        res.json({ success: true, data: result.rows.map(r => r.ProductType) });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get all services
app.get('/api/services', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM "Service" ORDER BY "ServiceID"');
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get membership levels
app.get('/api/membership-levels', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM "MembershipLevel" ORDER BY "LevelID"');
        res.json({ success: true, data: result.rows });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Login
app.post('/api/auth/login', async (req, res) => {
    try {
        const { username, password } = req.body;
        
        const result = await pool.query(
            `SELECT c.*, cm."LoyalPoint", cm."LevelID" 
             FROM "Customer" c 
             LEFT JOIN "CardMembership" cm ON c."CustomerID" = cm."CustomerID"
             LEFT JOIN "AccountLogin" a ON c."Username" = a."Username"
             WHERE a."Username" = $1 AND a."Password" = $2`,
            [username, password]
        );
        
        if (result.rows.length === 0) {
            return res.status(401).json({ success: false, error: 'Sai tên đăng nhập hoặc mật khẩu' });
        }
        
        res.json({ success: true, data: result.rows[0] });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get orders by customer
app.get('/api/orders/:customerId', async (req, res) => {
    try {
        const { customerId } = req.params;
        const result = await pool.query(
            `SELECT o.*, od."ProductID", od."Quantity", od."TemporaryPrice", p."ProductName"
             FROM "Orders" o
             LEFT JOIN "OrderDetail" od ON o."OrderID" = od."OrderID"
             LEFT JOIN "Product" p ON od."ProductID" = p."ProductID"
             WHERE o."CustomerID" = $1
             ORDER BY o."CreateDate" DESC`,
            [customerId]
        );
        res.json({ success: true, data: result.rows });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// ============================================
// START SERVER
// ============================================

app.listen(PORT, async () => {
    console.log(`🚀 PetCareX API Server đang chạy tại port ${PORT}`);
    
    // Test database connection
    try {
        await pool.query('SELECT NOW()');
        console.log('✅ Kết nối PostgreSQL thành công!');
    } catch (error) {
        console.error('❌ Lỗi kết nối database:', error.message);
    }
});

module.exports = app;
