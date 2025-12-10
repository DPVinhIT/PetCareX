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
    ssl: { rejectUnauthorized: false }
});

// For password hashing
const bcrypt = require('bcrypt');
const SALT_ROUNDS = 10;

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
        const result = await pool.query('SELECT * FROM branch ORDER BY "BranchID"');
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get branch by ID
app.get('/api/branches/:id', async (req, res) => {
    try {
        const { id } = req.params;
        const result = await pool.query('SELECT * FROM branch WHERE "BranchID" = $1', [id]);
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
        let query = 'SELECT * FROM product';
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
        const result = await pool.query('SELECT DISTINCT "ProductType" FROM product ORDER BY "ProductType"');
        res.json({ success: true, data: result.rows.map(r => r.ProductType) });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get all services
app.get('/api/services', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM service ORDER BY "ServiceID"');
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get membership levels
app.get('/api/membership-levels', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM membershiplevel ORDER BY "LevelID"');
        res.json({ success: true, data: result.rows });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Login
app.post('/api/auth/login', async (req, res) => {
    try {
        const { username, password } = req.body;
        if (!username || !password) {
            return res.status(400).json({ success: false, error: 'Missing username or password' });
        }

        // Get stored hash for username
        const accRes = await pool.query('SELECT password FROM accountlogin WHERE username = $1', [username]);
        if (accRes.rows.length === 0) {
            return res.status(401).json({ success: false, error: 'Sai tên đăng nhập hoặc mật khẩu' });
        }

        const hash = accRes.rows[0].password;
        const match = await bcrypt.compare(password, hash);
        if (!match) {
            return res.status(401).json({ success: false, error: 'Sai tên đăng nhập hoặc mật khẩu' });
        }

        // Password OK — fetch customer profile
        const result = await pool.query(
            `SELECT c.*, cm.loyalpoint, cm.levelid 
             FROM customer c 
             LEFT JOIN cardmembership cm ON c.customerid = cm.customerid
             WHERE c.username = $1`,
            [username]
        );

        const user = result.rows[0] || null;
        res.json({ success: true, data: user });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Register (secure)
app.post('/api/auth/register', async (req, res) => {
    try {
        const { username, password, fullname, email, phone } = req.body;

        if (!username || !password) {
            return res.status(400).json({ success: false, error: 'Missing username or password' });
        }

        // Check if username exists
        const exists = await pool.query('SELECT username FROM accountlogin WHERE username = $1', [username]);
        if (exists.rows.length > 0) {
            return res.status(409).json({ success: false, error: 'Tên đăng nhập đã tồn tại' });
        }

        // Hash password
        const hashed = await bcrypt.hash(password, SALT_ROUNDS);

        // Insert into accountlogin
        await pool.query('INSERT INTO accountlogin(username, password) VALUES($1, $2)', [username, hashed]);

        // Create a simple CustomerID
        const customerId = 'C' + Date.now();

        // Insert into customer (safe to provide NULLs for optional fields)
        await pool.query(
            'INSERT INTO customer(customerid, fullname, phonenumber, email, username) VALUES($1, $2, $3, $4, $5)',
            [customerId, fullname || null, phone || null, email || null, username]
        );

        res.json({ success: true, data: { username, customerId } });
    } catch (error) {
        console.error('Register error:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get orders by customer
app.get('/api/orders/:customerId', async (req, res) => {
    try {
        const { customerId } = req.params;
        const result = await pool.query(
            `SELECT o.*, od.productid, od.quantity, od.temporaryprice, p.productname
             FROM orders o
             LEFT JOIN orderdetail od ON o.orderid = od.orderid
             LEFT JOIN product p ON od.productid = p.productid
             WHERE o.customerid = $1
             ORDER BY o.createdate DESC`,
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
