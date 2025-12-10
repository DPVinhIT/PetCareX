/**
 * API Client - Kết nối Frontend với Backend
 * Gọi API để lấy dữ liệu từ database
 */

const API_BASE_URL = 'http://localhost:5000/api';

// ============================================
// API HELPER FUNCTIONS
// ============================================

async function apiCall(endpoint, options = {}) {
    try {
        const url = `${API_BASE_URL}${endpoint}`;
        const response = await fetch(url, {
            headers: {
                'Content-Type': 'application/json',
                ...options.headers
            },
            ...options
        });
        
        const data = await response.json();
        
        if (!response.ok) {
            throw new Error(data.error || 'Lỗi API');
        }
        
        return data;
    } catch (error) {
        console.error(`API Error [${endpoint}]:`, error.message);
        throw error;
    }
}

// ============================================
// BRANCHES API
// ============================================

async function fetchBranches() {
    try {
        const result = await apiCall('/branches');
        return result.data || [];
    } catch (error) {
        console.error('Lỗi lấy chi nhánh:', error);
        return [];
    }
}

async function fetchBranchById(branchId) {
    try {
        const result = await apiCall(`/branches/${branchId}`);
        return result.data;
    } catch (error) {
        console.error('Lỗi lấy chi tiết chi nhánh:', error);
        return null;
    }
}

// ============================================
// PRODUCTS API
// ============================================

async function fetchProducts(type = 'all', limit = null) {
    try {
        let endpoint = '/products';
        const params = new URLSearchParams();
        
        if (type && type !== 'all') {
            params.append('type', type);
        }
        if (limit) {
            params.append('limit', limit);
        }
        
        if (params.toString()) {
            endpoint += '?' + params.toString();
        }
        
        const result = await apiCall(endpoint);
        return result.data || [];
    } catch (error) {
        console.error('Lỗi lấy sản phẩm:', error);
        return [];
    }
}

async function fetchProductTypes() {
    try {
        const result = await apiCall('/products/types');
        return result.data || [];
    } catch (error) {
        console.error('Lỗi lấy loại sản phẩm:', error);
        return [];
    }
}

// ============================================
// SERVICES API
// ============================================

async function fetchServices() {
    try {
        const result = await apiCall('/services');
        return result.data || [];
    } catch (error) {
        console.error('Lỗi lấy dịch vụ:', error);
        return [];
    }
}

// ============================================
// MEMBERSHIP API
// ============================================

async function fetchMembershipLevels() {
    try {
        const result = await apiCall('/membership-levels');
        return result.data || [];
    } catch (error) {
        console.error('Lỗi lấy membership levels:', error);
        return [];
    }
}

// ============================================
// AUTH API
// ============================================

async function apiLogin(username, password) {
    try {
        const result = await apiCall('/auth/login', {
            method: 'POST',
            body: JSON.stringify({ username, password })
        });
        return result;
    } catch (error) {
        return { success: false, error: error.message };
    }
}

async function apiRegister(userData) {
    try {
        const result = await apiCall('/auth/register', {
            method: 'POST',
            body: JSON.stringify(userData)
        });
        return result;
    } catch (error) {
        return { success: false, error: error.message };
    }
}

// ============================================
// ORDERS API
// ============================================

async function createOrder(customerID, items) {
    try {
        const result = await apiCall('/orders', {
            method: 'POST',
            body: JSON.stringify({ customerID, items })
        });
        return result;
    } catch (error) {
        return { success: false, error: error.message };
    }
}

async function fetchOrders(customerId) {
    try {
        const result = await apiCall(`/orders/${customerId}`);
        return result.data || [];
    } catch (error) {
        console.error('Lỗi lấy đơn hàng:', error);
        return [];
    }
}

// ============================================
// LOYALTY API
// ============================================

async function addLoyaltyPointsAPI(customerID, points) {
    try {
        const result = await apiCall('/loyalty/add', {
            method: 'POST',
            body: JSON.stringify({ customerID, points })
        });
        return result;
    } catch (error) {
        return { success: false, error: error.message };
    }
}

// ============================================
// HEALTH CHECK
// ============================================

async function checkAPIHealth() {
    try {
        const result = await apiCall('/health');
        console.log('✅ API Server đang hoạt động:', result.message);
        return true;
    } catch (error) {
        console.error('❌ API Server không phản hồi');
        return false;
    }
}

// ============================================
// EXPORT
// ============================================

const API = {
    // Branches
    getBranches: fetchBranches,
    getBranchById: fetchBranchById,
    
    // Products
    getProducts: fetchProducts,
    getProductTypes: fetchProductTypes,
    
    // Services
    getServices: fetchServices,
    
    // Membership
    getMembershipLevels: fetchMembershipLevels,
    
    // Auth
    login: apiLogin,
    register: apiRegister,
    
    // Orders
    createOrder: createOrder,
    getOrders: fetchOrders,
    
    // Loyalty
    addLoyaltyPoints: addLoyaltyPointsAPI,
    
    // Health
    checkHealth: checkAPIHealth
};

// Check API khi load
document.addEventListener('DOMContentLoaded', async () => {
    const isAPIReady = await checkAPIHealth();
    if (isAPIReady) {
        console.log('🚀 API Client sẵn sàng!');
    } else {
        console.warn('⚠️ API Server chưa khởi động. Chạy: cd backend && npm start');
    }
});

console.log('📡 API Client đã được load!');
