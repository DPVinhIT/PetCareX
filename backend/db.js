/**
 * Database Connection Module
 * Kết nối SQL Server với Windows Authentication
 */

const sql = require('mssql/msnodesqlv8');
require('dotenv').config();

// Connection string cho Windows Authentication - ODBC Driver 17
const connectionString = 'Driver={ODBC Driver 17 for SQL Server};Server=localhost\\SQLEXPRESS;Database=PetCareX_DB;Trusted_Connection=yes;';

const config = {
    connectionString: connectionString,
    pool: {
        max: 10,
        min: 0,
        idleTimeoutMillis: 30000
    }
};

let pool = null;

// Kết nối database
async function connectDB() {
    try {
        if (pool) {
            return pool;
        }
        
        console.log('🔄 Đang kết nối SQL Server...');
        console.log(`   Server: localhost\\SQLEXPRESS`);
        console.log(`   Database: PetCareX_DB`);
        console.log(`   Auth: Windows Authentication`);
        
        pool = await sql.connect(config);
        
        console.log('✅ Kết nối SQL Server thành công!');
        return pool;
    } catch (error) {
        console.error('❌ Lỗi kết nối database:', error);
        throw error;
    }
}

// Đóng kết nối
async function closeDB() {
    try {
        if (pool) {
            await pool.close();
            pool = null;
            console.log('🔌 Đã đóng kết nối database');
        }
    } catch (error) {
        console.error('Lỗi đóng kết nối:', error.message);
    }
}

// Lấy pool hiện tại
function getPool() {
    return pool;
}

module.exports = {
    connectDB,
    closeDB,
    getPool,
    sql
};
