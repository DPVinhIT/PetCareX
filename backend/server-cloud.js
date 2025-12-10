/**
 * PetCareX API Server - Cloud Version (PostgreSQL)
 */

const express = require('express');
const cors = require('cors');
require('dotenv').config();
const jwt = require('jsonwebtoken');

const app = express();
const PORT = process.env.PORT || 5000;

// Middleware
app.use(cors());
app.use(express.json());

// Lightweight cookie parser middleware (replaces cookie-parser dependency)
app.use((req, res, next) => {
    req.cookies = {};
    const raw = req.headers?.cookie;
    if (!raw) return next();
    raw.split(';').forEach(pair => {
        const idx = pair.indexOf('=');
        if (idx < 0) return;
        const key = pair.slice(0, idx).trim();
        const val = pair.slice(idx + 1).trim();
        try { req.cookies[key] = decodeURIComponent(val); } catch (e) { req.cookies[key] = val; }
    });
    next();
});

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
        const result = await pool.query('SELECT * FROM branch ORDER BY branchid');
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get branch by ID
app.get('/api/branches/:id', async (req, res) => {
    try {
        const { id } = req.params;
        const result = await pool.query('SELECT * FROM branch WHERE branchid = $1', [id]);
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
            query += ` WHERE producttype = $1`;
        }
        
        query += ' ORDER BY productid';
        
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
        const result = await pool.query('SELECT DISTINCT producttype FROM product ORDER BY producttype');
        res.json({ success: true, data: result.rows.map(r => r.producttype) });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get all services
app.get('/api/services', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM service ORDER BY serviceid');
        res.json({ success: true, data: result.rows, count: result.rows.length });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get membership levels
app.get('/api/membership-levels', async (req, res) => {
    try {
        const result = await pool.query('SELECT * FROM membershiplevel ORDER BY levelid');
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
        // create jwt token (contains customerid and username)
        const payload = { username, customerid: user?.customerid || null };
        const token = jwt.sign(payload, process.env.JWT_SECRET || 'dev_secret', { expiresIn: '7d' });
        // set HttpOnly cookie
        res.cookie('pcx_token', token, { httpOnly: true, sameSite: 'lax', secure: false, maxAge: 7 * 24 * 60 * 60 * 1000 });
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

        // Create a simple customerid (lowercase)
        const customerId = 'c' + Date.now();

        // Insert into customer (safe to provide NULLs for optional fields)
        await pool.query(
            'INSERT INTO customer(customerid, fullname, phonenumber, email, username) VALUES($1, $2, $3, $4, $5)',
            [customerId, fullname || null, phone || null, email || null, username]
        );

        // create jwt token and set cookie
        const payload = { username, customerid: customerId };
        const token = jwt.sign(payload, process.env.JWT_SECRET || 'dev_secret', { expiresIn: '7d' });
        res.cookie('pcx_token', token, { httpOnly: true, sameSite: 'lax', secure: false, maxAge: 7 * 24 * 60 * 60 * 1000 });

        res.json({ success: true, data: { username, customerid: customerId } });
    } catch (error) {
        console.error('Register error:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get current authenticated user from cookie
app.get('/api/auth/me', async (req, res) => {
    try {
        const token = req.cookies?.pcx_token;
        if (!token) return res.json({ success: true, data: null });
        let payload;
        try {
            payload = jwt.verify(token, process.env.JWT_SECRET || 'dev_secret');
        } catch (e) {
            return res.json({ success: true, data: null });
        }
        const username = payload.username;
        if (!username) return res.json({ success: true, data: null });
        const result = await pool.query(`SELECT c.*, cm.loyalpoint, cm.levelid 
             FROM customer c LEFT JOIN cardmembership cm ON c.customerid = cm.customerid WHERE c.username = $1`, [username]);
        const user = result.rows[0] || null;
        res.json({ success: true, data: user });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Logout
app.post('/api/auth/logout', (req, res) => {
    res.clearCookie('pcx_token');
    res.json({ success: true });
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

// Create booking (appointment)
app.post('/api/bookings', async (req, res) => {
    try {
        const { branch, petName, species, symptoms, date, time, service, price } = req.body;
        const customerId = req.body.customerid || req.body.customerId || req.body.customerId || null;

        const appointmentId = 'ap' + Date.now();
        const createDate = new Date().toISOString().split('T')[0];
        const createTime = new Date().toTimeString().split(' ')[0];

        // Try to find branch id by name (all-lowercase columns in supabase)
        let branchId = null;
        if (branch) {
            const b = await pool.query('SELECT branchid FROM branch WHERE branchname = $1 LIMIT 1', [branch]);
            if (b.rows.length > 0) branchId = b.rows[0].branchid;
        }

        // Try to find service id by name
        let serviceId = null;
        if (service) {
            const s = await pool.query('SELECT serviceid FROM service WHERE servicename = $1 LIMIT 1', [service]);
            if (s.rows.length > 0) serviceId = s.rows[0].serviceid;
        }

        await pool.query(
            `INSERT INTO appointment(appointmentid, createdate, createtime, date, time, branchid, serviceid, customerid, room)
             VALUES($1,$2,$3,$4,$5,$6,$7,$8,$9)`,
            [appointmentId, createDate, createTime, date, time, branchId, serviceId, customerId, null]
        );

        res.json({ success: true, data: { appointmentId } });
    } catch (error) {
        console.error('Booking error:', error);
        res.status(500).json({ success: false, error: error.message });
    }
});

// Get bookings for customer
app.get('/api/bookings/:customerId', async (req, res) => {
    try {
        const { customerId } = req.params;
        const result = await pool.query('SELECT * FROM appointment WHERE customerid = $1 ORDER BY createdate DESC, createtime DESC', [customerId]);
        res.json({ success: true, data: result.rows });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Create order (and details)
app.post('/api/orders', async (req, res) => {
    const client = await pool.connect();
    try {
        // accept both camelCase and lowercase payload keys
        const body = req.body || {};
        const orderId = body.OrderID || body.orderid || ('o' + Date.now());
        const customerId = body.CustomerID || body.customerid || body.customerId;
        const items = body.items || [];

        await client.query('BEGIN');

        await client.query(
            'INSERT INTO orders(orderid, customerid, salespersonid, createdate, createtime, status) VALUES($1,$2,$3,$4,$5,$6)',
            [orderId, customerId, null, new Date().toISOString().split('T')[0], new Date().toTimeString().split(' ')[0], 'Đã đặt']
        );

        for (const it of items) {
            const productId = it.ProductID || it.productid || it.productId || it.id;
            const quantity = it.Quantity || it.quantity || 1;
            const temporaryPrice = it.TemporaryPrice || it.temporaryprice || it.price || 0;
            await client.query(
                'INSERT INTO orderdetail(orderid, productid, quantity, temporaryprice) VALUES($1,$2,$3,$4)',
                [orderId, productId, quantity, temporaryPrice]
            );
        }

        await client.query('COMMIT');
        res.json({ success: true, data: { orderid: orderId } });
    } catch (error) {
        await client.query('ROLLBACK');
        console.error('Create order error:', error);
        res.status(500).json({ success: false, error: error.message });
    } finally {
        client.release();
    }
});

// Add loyalty points to customer's cardmembership
app.post('/api/customers/:customerId/add-loyalty', async (req, res) => {
    try {
        const { customerId } = req.params;
        const { points } = req.body;

        // Find existing cardmembership
        const cm = await pool.query('SELECT * FROM cardmembership WHERE customerid = $1 LIMIT 1', [customerId]);
        if (cm.rows.length > 0) {
            await pool.query('UPDATE cardmembership SET loyalpoint = COALESCE(loyalpoint,0) + $1 WHERE customerid = $2', [points, customerId]);
        } else {
            const cardId = 'cm' + Date.now();
            await pool.query('INSERT INTO cardmembership(cardid, registrationdate, loyalpoint, levelid, customerid) VALUES($1, NOW(), $2, $3)', [cardId, points, customerId]);
        }

        // Return updated points
        const updated = await pool.query('SELECT loyalpoint FROM cardmembership WHERE customerid = $1 LIMIT 1', [customerId]);
        const total = updated.rows[0]?.loyalpoint || 0;
        res.json({ success: true, data: { points: total } });
    } catch (error) {
        res.status(500).json({ success: false, error: error.message });
    }
});

// Save vaccination record
app.post('/api/vaccinations', async (req, res) => {
    try {
        const body = req.body || {};
        const id = 'vac' + Date.now();
        const vaccineId = body.VaccineID || body.vaccineid || null;
        const vaccinationDate = body.VaccinationDate || body.vaccinationdate || new Date().toISOString();
        const dosage = body.Dosage || body.dosage || null;
        await pool.query('INSERT INTO vaccination(vid, vaccineid, vaccinationdate, dosage) VALUES($1,$2,$3,$4)', [id, vaccineId, vaccinationDate, dosage]);
        // Optionally link to customer via appointment/invoice; skipping linking for now
        res.json({ success: true, data: { id } });
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
        console.error('❌ Lỗi kết nối database:', error);
    }
});

// Diagnostic endpoint to check DB connectivity (useful for debugging deploys)
app.get('/api/db-status', async (req, res) => {
    try {
        const r = await pool.query('SELECT NOW()');
        res.json({ success: true, dbNow: r.rows[0] });
    } catch (err) {
        console.error('/api/db-status error', err);
        res.status(500).json({ success: false, error: err.message || String(err) });
    }
});

module.exports = app;
