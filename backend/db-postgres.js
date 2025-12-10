/**
 * PostgreSQL Database Connection (cho Cloud - Supabase)
 */

const { Pool } = require('pg');
require('dotenv').config();

const pool = new Pool({
    connectionString: process.env.DATABASE_URL,
    ssl: {
        rejectUnauthorized: false
    }
});

async function connectDB() {
    try {
        const client = await pool.connect();
        console.log('✅ Kết nối PostgreSQL thành công!');
        client.release();
        return pool;
    } catch (error) {
        console.error('❌ Lỗi kết nối PostgreSQL:', error.message);
        throw error;
    }
}

async function query(text, params) {
    const result = await pool.query(text, params);
    return result.rows;
}

module.exports = { connectDB, query, pool };
