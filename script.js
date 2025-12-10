/* ============================================
    dữ liệu động từ api
    branch: branchid, branchname, address, phonenumber, email, opentime, closetime
    tất cả chữ thường, viết liền
    ============================================ */

// Sử dụng API trên cloud (Render)
const API_BASE = 'https://petcarex-api.onrender.com/api';

// session user (kept in memory only). cookie-based auth is used; fetchAuthMe() populates this.
window.currentUser = null;

function getCurrentUser() {
    return window.currentUser;
}

async function fetchAuthMe() {
    try {
        const resp = await fetch(`${API_BASE}/auth/me`, { credentials: 'include' });
        const result = await resp.json();
        if (result && result.success) {
            window.currentUser = result.data;
        } else {
            window.currentUser = null;
        }
        updateNavbarAfterLogin();
        return window.currentUser;
    } catch (err) {
        window.currentUser = null;
        return null;
    }
}

// Biến lưu dữ liệu động từ API
let branchesData = [];
let productsData = [];
let servicesData = [];
let membershipLevelsData = [];

// Hàm gọi API
async function fetchAPI(endpoint) {
    try {
        const response = await fetch(`${API_BASE}${endpoint}`);
        const result = await response.json();
        if (result.success) {
            return result.data;
        }
        console.error('API Error:', result.error);
        return [];
    } catch (error) {
        console.error('Fetch Error:', error);
        return [];
    }
}

// Load dữ liệu khi khởi động
async function loadDataFromAPI() {
    console.log('🔄 Đang load dữ liệu từ API...');
    
    try {
        // Load song song tất cả dữ liệu
        const [branches, products, services, levels] = await Promise.all([
            fetchAPI('/branches'),
            fetchAPI('/products'),
            fetchAPI('/services'),
            fetchAPI('/membership-levels')
        ]);
        
        branchesData = branches || [];
        productsData = products || [];
        servicesData = services || [];
        membershipLevelsData = levels || [];
        
        console.log(`✅ Loaded: ${branchesData.length} branches, ${productsData.length} products, ${servicesData.length} services`);
        
        // Re-render nếu đang ở trang tương ứng
        renderBranches();
        
        return true;
    } catch (error) {
        console.error('❌ Lỗi load dữ liệu:', error);
        return false;
    }
}

// Đã loại bỏ dữ liệu mẫu fallback, mọi dữ liệu chi nhánh sẽ lấy từ API backend

// Function để render branches (theo cấu trúc database)
async function renderBranches() {
    const container = document.getElementById('branchesContainer');
    if (!container) return;
    
    // Load từ API nếu chưa có dữ liệu
    if (branchesData.length === 0) {
        await loadDataFromAPI();
    }
    
    const branches = getBranches();
    
    container.innerHTML = branches.map(branch => `
        <div class="branch-card">
            <div class="branch-header">
                <h3>${branch.branchname}</h3>
                <span class="badge">Mở cửa</span>
            </div>
            <div class="branch-info">
                <p><i class="fas fa-map-marker-alt"></i> ${branch.address}</p>
                <p><i class="fas fa-phone"></i> ${branch.phonenumber}</p>
                <p><i class="fas fa-clock"></i> ${branch.opentime}:00 - ${branch.closetime}:00</p>
            </div>
            <button class="btn btn-outline-sm" onclick="showBranchDetail('${branch.branchid}')">Chi Tiết</button>
        </div>
    `).join('');
}

// Show branch detail (theo cấu trúc database)
function showBranchDetail(branchId) {
    const branch = getBranches().find(b => b.branchid === branchId);
    if (!branch) return;
    
    const detailContent = document.getElementById('branchDetailContent');
    if (!detailContent) return;
    
    detailContent.innerHTML = `
        <div style="margin-bottom: 1.5rem;">
            <h3 style="color: var(--primary-color); margin-bottom: 1rem;">${branch.branchname}</h3>
            <div style="background: #f5f5f5; padding: 1rem; border-radius: 8px; margin-bottom: 1rem;">
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-map-marker-alt" style="color: #f44336; width: 20px;"></i> Địa chỉ:</strong> ${branch.address}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-phone" style="color: #4CAF50; width: 20px;"></i> Điện thoại:</strong> ${branch.phonenumber}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-envelope" style="color: #2196F3; width: 20px;"></i> Email:</strong> ${branch.email}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-clock" style="color: #FF9800; width: 20px;"></i> Giờ mở cửa:</strong> ${branch.opentime}:00 - ${branch.closetime}:00</p>
            </div>
        </div>
        
        <div style="margin-bottom: 1.5rem;">
            <h4 style="color: #2196F3; margin-bottom: 0.8rem;"><i class="fas fa-stethoscope"></i> Dịch Vụ Cung Cấp</h4>
            <div style="display: flex; flex-wrap: wrap; gap: 0.5rem;">
                <span style="background: #e3f2fd; padding: 0.4rem 0.8rem; border-radius: 20px; font-size: 0.9rem; color: #1976D2;">✓ Khám bệnh</span>
                <span style="background: #e3f2fd; padding: 0.4rem 0.8rem; border-radius: 20px; font-size: 0.9rem; color: #1976D2;">✓ Tiêm phòng</span>
                <span style="background: #e3f2fd; padding: 0.4rem 0.8rem; border-radius: 20px; font-size: 0.9rem; color: #1976D2;">✓ Phẫu thuật</span>
                <span style="background: #e3f2fd; padding: 0.4rem 0.8rem; border-radius: 20px; font-size: 0.9rem; color: #1976D2;">✓ Spa thú cưng</span>
            </div>
        </div>
        
        <div style="display: flex; gap: 1rem; margin-top: 1.5rem;">
            <button class="btn btn-primary" onclick="bookAtBranch('${branch.branchname}')">
                <i class="fas fa-calendar-plus"></i> Đặt Lịch Tại Đây
            </button>
            <button class="btn btn-outline" onclick="openMap('${branch.Address}')">
                <i class="fas fa-map"></i> Xem Bản Đồ
            </button>
        </div>
    `;
    
    openModal('branchDetailModal');
}

// Book at specific branch
function bookAtBranch(branchName) {
    // branchName may be a branchid or branchname; make selection robust
    closeModal('branchDetailModal');
    openModal('bookingModal');
    const branchSelect = document.getElementById('bookingBranch');
    if (!branchSelect) return;

    // Direct match by id
    const byId = Array.from(branchSelect.options).find(o => o.value === branchName);
    if (byId) {
        branchSelect.value = branchName;
        return;
    }
    // Match by visible text (branchname)
    const byName = Array.from(branchSelect.options).find(o => o.text === branchName || o.text.includes(branchName));
    if (byName) {
        branchSelect.value = byName.value;
    }
}

// Open map
function openMap(address) {
    const mapUrl = `https://www.google.com/maps/search/?api=1&query=${encodeURIComponent(address)}`;
    window.open(mapUrl, '_blank');
}

/* ============================================
   ACCOUNT MANAGEMENT
   ============================================ */

// Đã loại bỏ toàn bộ hàm lấy/lưu tài khoản localStorage, mọi thao tác tài khoản sẽ qua API

// Đã loại bỏ cộng điểm loyalty local, cần đồng bộ qua API nếu muốn

// Get membership tier based on points (theo database MembershipLevel)
// L1: Basic - 0.05 (5%), threshold: 60 points
// L2: Standard - 0.1 (10%), threshold: 240 points, retention: 60
// L3: Platinum - 0.15 (15%), threshold: 99999 points, retention: 160
function getMembershipTier(points) {
    if (points >= 240) {
        return { LevelID: 'L3', name: 'Platinum', discount: 15, color: '#E5E4E2' };
    } else if (points >= 60) {
        return { LevelID: 'L2', name: 'Standard', discount: 10, color: '#C0C0C0' };
    } else {
        return { LevelID: 'L1', name: 'Basic', discount: 5, color: '#CD7F32' };
    }
}

/* ============================================
   BOOKING & ORDER MANAGEMENT
   ============================================ */

// =============================
// API booking, orders, lịch sử
// =============================

// Lưu booking qua API backend
async function saveBooking(bookingData) {
    const user = getCurrentUser();
    if (!user) {
        showNotification('Vui lòng đăng nhập trước', 'info');
        return;
    }
    try {
        const resp = await fetch(`${API_BASE}/bookings`, {
            method: 'POST',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ...bookingData, customerid: user.customerid || user.id })
        });
        const result = await resp.json();
        if (result.success) {
            showNotification('Đặt lịch thành công!', 'success');
        } else {
            showNotification(result.error || 'Lỗi đặt lịch', 'error');
        }
    } catch (err) {
        showNotification('Lỗi kết nối server', 'error');
    }
}

// Lấy booking của user qua API backend
async function getUserBookings() {
    const user = getCurrentUser();
    if (!user) return [];
    try {
        const resp = await fetch(`${API_BASE}/bookings/${user.customerid || user.id}`, { credentials: 'include' });
        const result = await resp.json();
        if (result.success) return result.data;
        return [];
    } catch {
        return [];
    }
}

// Lấy orders của user qua API backend
async function getUserOrders() {
    const user = getCurrentUser();
    if (!user) return [];
    try {
        const resp = await fetch(`${API_BASE}/orders/${user.customerid || user.id}`, { credentials: 'include' });
        const result = await resp.json();
        if (result.success) return result.data;
        return [];
    } catch {
        return [];
    }
}



// Đã loại bỏ lưu vaccination local, cần gọi API backend để lưu vaccination

// Đã loại bỏ lấy booking local, cần gọi API backend để lấy booking

// Đã loại bỏ lấy vaccination local, cần gọi API backend để lấy vaccination

// Đã loại bỏ lấy orders local, cần gọi API backend để lấy orders

// View booking history
function viewBookingHistory() {
    const bookings = getUserBookings();
    const vaccinations = getUserVaccinations();
    const orders = getUserOrders();
    
    if (bookings.length === 0 && vaccinations.length === 0 && orders.length === 0) {
        showNotification('Chưa có lịch sử', 'info');
        return;
    }
    
    openModal('historyModal');
    displayBookingHistory(bookings, vaccinations, orders);
    // Xem lịch sử booking/orders qua API (gọi các API và truyền dữ liệu vào hàm hiển thị chi tiết)
    async function viewBookingHistory() {
        openModal('historyModal');
        const bookings = await getUserBookings();
        const orders = await getUserOrders();
        const vaccinations = await getUserVaccinations();
        // Gọi hàm hiển thị chi tiết (displayBookingHistory được giữ nguyên và nhận dữ liệu)
        displayBookingHistory(bookings || [], vaccinations || [], orders || []);
    }
}

// Display booking history (cập nhật theo cấu trúc database)
function displayBookingHistory(bookings, vaccinations, orders) {
    const historyContent = document.getElementById('historyContent');
    if (!historyContent) return;
    
    let content = '';
    
    // Orders section (group rows by orderid if backend returned flat rows)
    if (orders && orders.length > 0) {
        // if rows look like flat orderdetail rows (productid present), group them
        let ordersToRender = orders;
        if (orders[0] && (orders[0].productid || orders[0].productid === 0) && orders[0].orderid) {
            const map = {};
            for (const row of orders) {
                const oid = row.orderid || row.orderid || row.id || ('ord' + (row.orderid || Date.now()));
                if (!map[oid]) {
                    map[oid] = {
                        orderid: oid,
                        subtotal: row.subtotal || row.total || 0,
                        discount: row.discount || 0,
                        total: row.total || row.subtotal || 0,
                        membershipTier: row.membershiptier || row.membershipTier || '',
                        status: row.status || row.Status || '',
                        createdate: row.createdate || row.createdate || '',
                        createtime: row.createtime || row.createtime || '',
                        items: []
                    };
                }
                if (row.productid) {
                    map[oid].items.push({ productid: row.productid, productname: row.productname || row.productname, quantity: row.quantity || row.quantity, temporaryprice: row.temporaryprice || row.temporaryprice });
                }
            }
            ordersToRender = Object.values(map);
        }

        content += `
        <h3 style="color: #9C27B0; border-bottom: 2px solid #9C27B0; padding-bottom: 0.5rem; margin-bottom: 1rem;">
            <i class="fas fa-shopping-bag"></i> Đơn Hàng
        </h3>
        <div style="margin-bottom: 2rem;">
            ${ordersToRender.map(order => `
                <div style="background: #F3E5F5; padding: 1rem; margin: 0.5rem 0; border-radius: 8px; border-left: 4px solid #9C27B0;">
                    <p><strong>Mã đơn:</strong> #${order.orderid || order.id}</p>
                    <p><strong>Sản phẩm:</strong> ${(order.items || []).map(item => `${item.productname || item.name} (x${item.quantity || item.Quantity || 1})`).join(', ')}</p>
                    <p><strong>Tổng tiền:</strong> ${(order.subtotal || order.total || 0).toLocaleString('vi-VN')} VNĐ</p>
                    <p><strong>Giảm giá:</strong> -${(order.discount || 0).toLocaleString('vi-VN')} VNĐ (${order.membershipTier || ''})</p>
                    <p><strong>Thành tiền:</strong> <span style="color: #4CAF50; font-weight: bold;">${(order.total || order.subtotal || 0).toLocaleString('vi-VN')} VNĐ</span></p>
                    <p><strong>Trạng Thái:</strong> <span style="color: #4CAF50; font-weight: bold;">${order.status || ''}</span></p>
                    <p style="font-size: 0.85rem; color: #999;">Ngày: ${order.createdate || ''} ${order.createtime || ''}</p>
                </div>
            `).join('')}
        </div>
        `;
    }
    
    // Booking section
    if (bookings.length > 0) {
        content += `
        <h3 style="color: #2196F3; border-bottom: 2px solid #2196F3; padding-bottom: 0.5rem; margin-bottom: 1rem;">
            <i class="fas fa-calendar-check"></i> Lịch Đặt Khám
        </h3>
        <div style="margin-bottom: 2rem;">
            ${bookings.map(booking => `
                <div style="background: #F9F9F9; padding: 1rem; margin: 0.5rem 0; border-radius: 8px; border-left: 4px solid #2196F3;">
                    <p><strong>Dịch vụ:</strong> ${booking.service || 'N/A'}</p>
                    <p><strong>Thú Cưng:</strong> ${booking.petName || 'N/A'} (${booking.species || 'N/A'})</p>
                    <p><strong>Chi Nhánh:</strong> ${booking.branch || 'N/A'}</p>
                    <p><strong>Ngày:</strong> ${booking.date || 'N/A'} - <strong>Giờ:</strong> ${booking.time || 'N/A'}</p>
                    <p><strong>Triệu Chứng:</strong> ${booking.symptoms || 'Không có'}</p>
                    <p><strong>Giá tiền:</strong> <span style="color: #4CAF50; font-weight: bold;">${(booking.price || 0).toLocaleString('vi-VN')} VNĐ</span></p>
                    <p><strong>Trạng Thái:</strong> <span style="color: #4CAF50; font-weight: bold;">${booking.status}</span></p>
                    <p style="font-size: 0.85rem; color: #999;">Đặt lúc: ${booking.createdAt}</p>
                </div>
            `).join('')}
        </div>
        `;
    }
    
    // Vaccination section
    if (vaccinations.length > 0) {
        content += `
        <h3 style="color: #FF9800; border-bottom: 2px solid #FF9800; padding-bottom: 0.5rem; margin-bottom: 1rem;">
            <i class="fas fa-syringe"></i> Gói Tiêm Phòng
        </h3>
        <div>
            ${vaccinations.map(vac => `
                <div style="background: #FFF8F0; padding: 1rem; margin: 0.5rem 0; border-radius: 8px; border-left: 4px solid #FF9800;">
                    <p><strong>Gói:</strong> ${vac.packageName || 'N/A'}</p>
                    <p><strong>Giá tiền:</strong> <span style="color: #4CAF50; font-weight: bold;">${(vac.price || 0).toLocaleString('vi-VN')} VNĐ</span></p>
                    <p><strong>Điểm tích lũy:</strong> <span style="color: #2196F3; font-weight: bold;">+${Math.floor((vac.price || 0) / 50000)} điểm</span></p>
                    <p><strong>Trạng Thái:</strong> <span style="color: #4CAF50; font-weight: bold;">${vac.status}</span></p>
                    <p style="font-size: 0.85rem; color: #999;">Chọn lúc: ${vac.createdAt}</p>
                </div>
            `).join('')}
        </div>
        `;
    }
    
    historyContent.innerHTML = `<div style="max-height: 500px; overflow-y: auto;">${content}</div>`;
}

// Handle booking form submit
function handleBookingSubmit(event) {
    event.preventDefault();
    
    const serviceType = document.getElementById('serviceType');
    const selectedService = serviceType?.options[serviceType.selectedIndex];
    const price = parseInt(selectedService?.dataset?.price || serviceType?.value) || 0;

    if (!price) {
        showNotification('Vui lòng chọn dịch vụ', 'info');
        return;
    }

    const branchSelect = document.getElementById('bookingBranch');
    const branchId = branchSelect?.value || '';
    const branchName = branchSelect?.options[branchSelect.selectedIndex]?.text || '';

    const bookingData = {
        branchid: branchId,
        branch: branchName,
        petName: document.getElementById('petName').value,
        species: document.getElementById('petSpecies').value,
        symptoms: document.getElementById('symptoms').value,
        date: document.getElementById('bookingDate').value,
        time: document.getElementById('bookingTime').value,
        serviceid: selectedService?.value || '',
        service: selectedService?.text || '',
        price: price
    };
    
    saveBooking(bookingData);
    
    // Add loyalty points
    addLoyaltyPoints(price);
    
    setTimeout(() => {
        closeModal('bookingModal');
        event.target.reset();
        updateBookingPrice(); // Reset price display
    }, 1500);
}

// Update booking price display
function updateBookingPrice() {
    const serviceType = document.getElementById('serviceType');
    if (!serviceType) return;
    const selected = serviceType.options[serviceType.selectedIndex];
    const price = parseInt(selected?.dataset?.price || serviceType.value) || 0;
    const points = Math.floor(price / 50000);

    const totalEl = document.getElementById('bookingTotalPrice');
    const earnEl = document.getElementById('bookingLoyaltyEarn');
    if (totalEl) totalEl.textContent = price.toLocaleString('vi-VN') + ' VNĐ';
    if (earnEl) earnEl.textContent = `Tích lũy: ${points} điểm`;
}

// Select vaccination package
function selectVaccinationPackage(packageName, price) {
    const vaccinationData = {
        packageName: packageName,
        price: price
    };
    
    saveVaccination(vaccinationData);
    
    // Add loyalty points
    addLoyaltyPoints(price);
    
    setTimeout(() => {
        closeModal('vaccinationModal');
    }, 1500);
}

/* ============================================
   MODAL FUNCTIONS
   ============================================ */

function openModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.style.display = 'block';
        document.body.style.overflow = 'hidden';
        // If opening booking modal, populate selects from API
        if (modalId === 'bookingModal') {
            try { populateBookingModal(); } catch (e) { console.error(e); }
        }
    }
}

// Populate booking modal selects from API data
async function populateBookingModal() {
    try {
        if (branchesData.length === 0 || servicesData.length === 0) {
            await loadDataFromAPI();
        }

        const branchSelect = document.getElementById('bookingBranch');
        const serviceSelect = document.getElementById('serviceType');
        if (branchSelect) {
            branchSelect.innerHTML = '<option value="">Chọn chi nhánh</option>' +
                (branchesData || []).map(b => `<option value="${b.branchid}">${b.branchname}</option>`).join('');
        }
        if (serviceSelect) {
            serviceSelect.innerHTML = '<option value="">Chọn dịch vụ</option>' +
                (servicesData || []).map(s => {
                    const price = s.price || s.fee || s.sellingprice || s.serviceprice || 0;
                    const name = s.servicename || s.service || s.name || '';
                    return `<option value="${s.serviceid}" data-price="${price}">${name} - ${Number(price).toLocaleString('vi-VN')} VNĐ</option>`;
                }).join('');
        }
        updateBookingPrice();
    } catch (err) {
        console.error('populateBookingModal error', err);
    }
}

function closeModal(modalId) {
    console.log('closeModal called with id:', modalId);
    const modal = document.getElementById(modalId);
    console.log('modal element:', modal);
    
    if (modal) {
        console.log('closing modal, current display:', modal.style.display);
        modal.style.display = 'none';
        document.body.style.overflow = 'auto';
        console.log('modal closed, new display:', modal.style.display);
        
        // Clear error messages when closing auth modal
        if (modalId === 'authModal') {
            const modalLoginError = document.getElementById('modalLoginError');
            if (modalLoginError) {
                modalLoginError.classList.remove('show');
                modalLoginError.innerHTML = '';
            }
        }
    } else {
        console.log('modal not found with id:', modalId);
    }
}

// Close modal khi click bên ngoài
window.onclick = function(event) {
    if (event.target.classList.contains('modal')) {
        event.target.style.display = 'none';
        document.body.style.overflow = 'auto';
    }
}

/* ============================================
   SHOP FUNCTION
   ============================================ */

/* ============================================
    shop & cart management (theo cấu trúc database)
    product: productid, productname, producttype, sellingprice
    tất cả chữ thường, viết liền
    ============================================ */

// Fallback Product catalog - dùng khi API không hoạt động
const fallbackProducts = [
    // Thức ăn
    { productid: 'PRD0004', productname: 'Thức ăn thỏ hữu cơ', producttype: 'Thức ăn', sellingprice: 433048, icon: '🌿' },
    { productid: 'PRD0009', productname: 'Thức ăn mèo cao cấp', producttype: 'Thức ăn', sellingprice: 794584, icon: '🐟' },
    { productid: 'PRD0010', productname: 'Thức ăn Tây Ban Nha', producttype: 'Thức ăn', sellingprice: 584751, icon: '🍖' },
    { productid: 'PRD0015', productname: 'Thức ăn mèo cao cấp 2', producttype: 'Thức ăn', sellingprice: 142048, icon: '🐟' },
    
    // Dược phẩm
    { productid: 'PRD0013', productname: 'Siro ho cho chó', producttype: 'Dược phẩm', sellingprice: 887064, icon: '💊' },
    { productid: 'PRD0024', productname: 'Vitamin B12 tiêm', producttype: 'Dược phẩm', sellingprice: 177300, icon: '💉' },
    { productid: 'PRD0029', productname: 'Kem chữa ghẻ', producttype: 'Dược phẩm', sellingprice: 267671, icon: '🧴' },
    
    // Vitamin
    { productid: 'PRD0012', productname: 'Canxi cho chó già', producttype: 'Vitamin', sellingprice: 522901, icon: '💊' },
    { productid: 'PRD0017', productname: 'Dầu cá tốt cho lông', producttype: 'Vitamin', sellingprice: 160019, icon: '💊' },
    { productid: 'PRD0021', productname: 'Vitamin C dạng bột', producttype: 'Vitamin', sellingprice: 678478, icon: '💊' },
    
    // Thiết bị y tế
    { productid: 'PRD0002', productname: 'Ký sinh trùng detector', producttype: 'Thiết bị y tế', sellingprice: 242905, icon: '🧬' },
    { productid: 'PRD0003', productname: 'Bàn chải đánh răng', producttype: 'Thiết bị y tế', sellingprice: 996708, icon: '🪥' },
    { productid: 'PRD0005', productname: 'Ngoạm cắt móng', producttype: 'Thiết bị y tế', sellingprice: 943065, icon: '✂️' },
    
    // Phụ kiện
    { productid: 'PRD0006', productname: 'Cát lót thỏ', producttype: 'Phụ kiện', sellingprice: 622570, icon: '🪻' },
    { productid: 'PRD0027', productname: 'Giường nằm cho mèo', producttype: 'Phụ kiện', sellingprice: 244919, icon: '🛏️' },
    
    // Chăm sóc da
    { productid: 'PRD0007', productname: 'Xịt khử mùi', producttype: 'Chăm sóc da', sellingprice: 711660, icon: '🧿' },
    { productid: 'PRD0008', productname: 'Kem chống côn trùng', producttype: 'Chăm sóc da', sellingprice: 58432, icon: '🧴' },
    
    // Đồ chơi
    { productid: 'PRD0001', productname: 'Tunnel chơi thỏ', producttype: 'Đồ chơi', sellingprice: 751370, icon: '🚽' },
    { productid: 'PRD0011', productname: 'Chuông leng keng', producttype: 'Đồ chơi', sellingprice: 129578, icon: '🔔' },
    { productid: 'PRD0018', productname: 'Dây kéo vải', producttype: 'Đồ chơi', sellingprice: 28719, icon: '🧶' }
];

let cart = [];
let currentFilter = 'all';

// Getter cho products - dùng API data hoặc fallback
function getProducts() {
    return productsData.length > 0 ? productsData : fallbackProducts;
}

// Getter cho branches - dùng API data hoặc fallback
function getBranches() {
    return branchesData.length > 0 ? branchesData : fallbackBranchesData;
}

async function showShop() {
    openModal('shopModal');
    
    // Load dữ liệu từ API nếu chưa có
    if (productsData.length === 0) {
        await loadDataFromAPI();
    }
    
    displayProducts(getProducts());
    updateCartCount();
}

function filterProducts(category) {
    currentFilter = category;
    
    // Update active button
    document.querySelectorAll('.category-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
    
    // Filter products by producttype
    const allProducts = getProducts();
    const filtered = category === 'all' ? allProducts : allProducts.filter(p => (p.producttype || p.ProductType) === category);
    displayProducts(filtered);
}

function displayProducts(productList) {
    const grid = document.getElementById('productsGrid');
    if (!grid) return;
    
    grid.innerHTML = productList.map(product => {
        const name = product.productname || product.ProductName || '';
        const id = product.productid || product.ProductID || '';
        const type = product.producttype || product.ProductType || '';
        const price = product.sellingprice || product.SellingPrice || 0;
        const needsToggle = name.length > 25;
        return `
        <div class="product-card" data-category="${type}">
            <div class="product-image">${product.icon}</div>
            <div class="product-title-row">
                <div class="product-name" id="name-${id}">${name}</div>
                ${needsToggle ? `<button class="product-toggle" onclick="toggleProductName('${id}')">Xem</button>` : ''}
            </div>
            <div class="product-price">${price.toLocaleString('vi-VN')} VNĐ</div>
            <div class="product-stock">${type}</div>
            <button class="btn btn-primary btn-full" onclick="addToCart('${id}')">
                Thêm vào giỏ
            </button>
        </div>
    `}).join('');
}

function toggleProductName(productId) {
    const nameEl = document.getElementById(`name-${productId}`);
    const toggleBtn = event.target;
    
    if (nameEl.classList.contains('expanded')) {
        nameEl.classList.remove('expanded');
        toggleBtn.textContent = 'Xem';
    } else {
        nameEl.classList.add('expanded');
        toggleBtn.textContent = 'Ẩn';
    }
}

function addToCart(productId) {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) {
        showNotification('Vui lòng đăng nhập để mua hàng', 'info');
        return;
    }
    
    const product = getProducts().find(p => (p.productid || p.ProductID) === productId);
    if (!product) return;
    
    const existingItem = cart.find(item => (item.productid || item.ProductID) === productId);
    
    if (existingItem) {
        existingItem.quantity++;
        showNotification('Đã cập nhật số lượng', 'success');
    } else {
        // normalize to lowercase keys when pushing to cart
        cart.push({
            productid: product.productid || product.ProductID,
            productname: product.productname || product.ProductName,
            producttype: product.producttype || product.ProductType,
            sellingprice: product.sellingprice || product.SellingPrice || 0,
            icon: product.icon || '',
            quantity: 1
        });
        showNotification('Đã thêm vào giỏ hàng', 'success');
    }
    
    updateCartCount();
}

function updateCartCount() {
    const countEl = document.getElementById('cartCount');
    if (!countEl) return;
    
    const totalItems = cart.reduce((sum, item) => sum + item.quantity, 0);
    
    if (totalItems > 0) {
        countEl.textContent = totalItems;
        countEl.style.display = 'flex';
    } else {
        countEl.style.display = 'none';
    }
}

function openCart() {
    if (cart.length === 0) {
        showNotification('Giỏ hàng trống', 'info');
        return;
    }
    
    closeModal('shopModal');
    setTimeout(() => {
        openModal('cartModal');
        displayCart();
    }, 100);
}

function displayCart() {
    const cartContent = document.getElementById('cartContent');
    if (!cartContent) return;
    
    if (cart.length === 0) {
        cartContent.innerHTML = '<p style="text-align: center; color: #999; padding: 2rem;">Giỏ hàng trống</p>';
        return;
    }
    
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    const tierInfo = getMembershipTier(user?.loyaltyPoints || 0);
    
    const subtotal = cart.reduce((sum, item) => sum + ((item.sellingprice || item.SellingPrice || 0) * item.quantity), 0);
    const discount = Math.floor(subtotal * tierInfo.discount / 100);
    const finalTotal = subtotal - discount;
    const loyaltyPoints = Math.floor(finalTotal / 50000);
    
    cartContent.innerHTML = cart.map(item => `
        <div class="cart-item">
            <div class="cart-item-image">${item.icon}</div>
            <div class="cart-item-info">
                <div class="cart-item-name">${item.productname || item.ProductName}</div>
                <div class="cart-item-price">${(item.sellingprice || item.SellingPrice || 0).toLocaleString('vi-VN')} VNĐ</div>
                <div class="cart-item-controls">
                    <button class="cart-qty-btn" onclick="updateCartQuantity('${item.productid || item.ProductID}', -1)">-</button>
                    <span class="cart-qty">${item.quantity}</span>
                    <button class="cart-qty-btn" onclick="updateCartQuantity('${item.productid || item.ProductID}', 1)">+</button>
                    <button class="cart-remove-btn" onclick="removeFromCart('${item.productid || item.ProductID}')">Xóa</button>
                </div>
            </div>
        </div>
    `).join('');
    
    document.getElementById('cartTotal').textContent = subtotal.toLocaleString('vi-VN') + ' VNĐ';
    document.getElementById('cartDiscount').textContent = `-${discount.toLocaleString('vi-VN')} VNĐ (${tierInfo.discount}%)`;
    document.getElementById('cartFinalTotal').textContent = finalTotal.toLocaleString('vi-VN') + ' VNĐ';
    document.getElementById('cartLoyaltyEarn').textContent = `💎 Tích lũy: ${loyaltyPoints} điểm`;
}

function updateCartQuantity(productId, change) {
    const item = cart.find(i => (i.productid || i.ProductID) === productId);
    if (!item) return;
    
    const newQuantity = item.quantity + change;
    
    if (newQuantity <= 0) {
        removeFromCart(productId);
        return;
    }
    
    item.quantity = newQuantity;
    displayCart();
    updateCartCount();
}

function removeFromCart(productId) {
    cart = cart.filter(item => (item.productid || item.ProductID) !== productId);
    displayCart();
    updateCartCount();
    
    if (cart.length === 0) {
        closeModal('cartModal');
        showNotification('Giỏ hàng trống', 'info');
    }
}

function checkout() {
    const user = getCurrentUser();
    if (!user) {
        showNotification('Vui lòng đăng nhập', 'info');
        return;
    }
    
    if (cart.length === 0) return;
    
    const tierInfo = getMembershipTier(user.loyalpoint || user.loyaltyPoints || 0);
    const subtotal = cart.reduce((sum, item) => sum + ((item.sellingprice || item.SellingPrice || 0) * item.quantity), 0);
    const discount = Math.floor(subtotal * tierInfo.discount / 100);
    const finalTotal = subtotal - discount;
    
    // Save order via API
    const orderPayload = {
        orderid: 'ord' + Date.now(),
        customerid: user.customerid || user.id || user.CustomerID,
        items: cart.map(item => ({
            productid: item.productid || item.ProductID,
            productname: item.productname || item.ProductName,
            quantity: item.quantity,
            temporaryprice: item.sellingprice || item.SellingPrice
        })),
        subtotal: subtotal,
        discount: discount,
        total: finalTotal,
        membershipTier: tierInfo.name
    };

    fetch(`${API_BASE}/orders`, {
        method: 'POST',
        credentials: 'include',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(orderPayload)
    }).then(r => r.json()).then(result => {
        if (result.success) {
            // Add loyalty points
            addLoyaltyPoints(finalTotal);

            // Clear cart
            cart = [];
            updateCartCount();

            closeModal('cartModal');
            showNotification('Đặt hàng thành công! Sẽ giao hàng trong 1-2 ngày', 'success');
        } else {
            showNotification(result.error || 'Lỗi tạo đơn hàng', 'error');
        }
    }).catch(() => showNotification('Lỗi kết nối server', 'error'));
}

// Gọi API để thêm điểm loyalty cho user
async function addLoyaltyPoints(amount) {
    const user = getCurrentUser();
    if (!user) return 0;
    const points = Math.floor(amount / 50000);
    if (points <= 0) return 0;

    try {
        const resp = await fetch(`${API_BASE}/customers/${user.customerid || user.id}/add-loyalty`, {
            method: 'POST',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ points })
        });
        const result = await resp.json();
        if (result.success) {
            const total = result.data.points;
            // update in-memory currentUser
            window.currentUser = window.currentUser || {};
            window.currentUser.loyalpoint = total;
            return points;
        }
    } catch (err) {
        console.error('addLoyaltyPoints error', err);
    }
    return 0;
}

// Lưu vaccination via API
async function saveVaccination(vaccinationData) {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) {
        showNotification('Vui lòng đăng nhập trước', 'info');
        return;
    }
    try {
        const resp = await fetch(`${API_BASE}/vaccinations`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ ...vaccinationData, customerid: user.customerid || user.id })
        });
        const result = await resp.json();
        if (result.success) {
            showNotification('Chọn gói tiêm thành công', 'success');
        } else {
            showNotification(result.error || 'Lỗi chọn gói tiêm', 'error');
        }
    } catch (err) {
        showNotification('Lỗi kết nối server', 'error');
    }
}

// Lấy vaccination của user (tạm trả rỗng; có thể bổ sung endpoint GET khi cần)
async function getUserVaccinations() {
    return [];
}

/* ============================================
   HAMBURGER MENU
   ============================================ */

const hamburger = document.querySelector('.hamburger');
const navMenu = document.querySelector('.nav-menu');

if (hamburger) {
    hamburger.addEventListener('click', () => {
        navMenu.classList.toggle('active');
    });

    // Close menu khi click vào link
    const navLinks = document.querySelectorAll('.nav-menu li a');
    navLinks.forEach(link => {
        link.addEventListener('click', () => {
            navMenu.classList.remove('active');
        });
    });
}

// /* ============================================
//    FORM SUBMISSION
//    ============================================ */

// document.querySelectorAll('form').forEach(form => {
//     form.addEventListener('submit', function(e) {
//         e.preventDefault();
        
//         // Lấy dữ liệu form
//         const formData = new FormData(this);
        
//         // Hiển thị thông báo thành công
//         showNotification('Gửi thành công! Chúng tôi sẽ liên hệ với bạn sớm.', 'success');
        
//         // Reset form
//         this.reset();
        
//         // Đóng modal nếu có
//         // const modal = this.closest('.modal');
//         if (modal) {
//             setTimeout(() => {
//                 modal.style.display = 'none';
//                 document.body.style.overflow = 'auto';
//             }, 2000);
//         }
//     });
// });

/* ============================================
   NOTIFICATION SYSTEM
   ============================================ */

function showNotification(message, type = 'info') {
    // Tạo notification element
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <div class="notification-content">
            <i class="fas fa-${type === 'success' ? 'check-circle' : 'info-circle'}"></i>
            <span>${message}</span>
        </div>
        <button class="notification-close" onclick="this.parentElement.remove()">
            <i class="fas fa-times"></i>
        </button>
    `;
    
    // Thêm style cho notification
    const style = document.createElement('style');
    style.textContent = `
        .notification {
            position: fixed;
            top: 20px;
            right: 20px;
            background: white;
            padding: 1rem 1.5rem;
            border-radius: 8px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            z-index: 3000;
            display: flex;
            align-items: center;
            justify-content: space-between;
            gap: 1rem;
            animation: slideInRight 0.3s ease;
            min-width: 300px;
        }

        @keyframes slideInRight {
            from {
                transform: translateX(400px);
                opacity: 0;
            }
            to {
                transform: translateX(0);
                opacity: 1;
            }
        }

        .notification-success {
            border-left: 4px solid #4CAF50;
            background: #F1F8F6;
        }

        .notification-success .notification-content i {
            color: #4CAF50;
        }

        .notification-info {
            border-left: 4px solid #2196F3;
            background: #F1F5F8;
        }

        .notification-info .notification-content i {
            color: #2196F3;
        }

        .notification-content {
            display: flex;
            align-items: center;
            gap: 0.5rem;
            color: #333;
        }

        .notification-close {
            background: none;
            border: none;
            color: #999;
            cursor: pointer;
            font-size: 1.2rem;
            transition: color 0.3s;
        }

        .notification-close:hover {
            color: #333;
        }

        @media (max-width: 480px) {
            .notification {
                min-width: auto;
                right: 10px;
                left: 10px;
            }
        }
    `;
    document.head.appendChild(style);
    
    // Thêm notification vào body
    document.body.appendChild(notification);
    
    // Tự động xóa sau 4 giây
    setTimeout(() => {
        notification.style.animation = 'slideOutRight 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 4000);
}

/* ============================================
   SMOOTH SCROLL EFFECT
   ============================================ */

document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        const href = this.getAttribute('href');
        if (href !== '#') {
            const target = document.querySelector(href);
            if (target && this.classList.contains('nav-link')) {
                e.preventDefault();
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        }
    });
});

/* ============================================
   ANIMATION ON SCROLL
   ============================================ */

const observerOptions = {
    threshold: 0.1,
    rootMargin: '0px 0px -100px 0px'
};

const observer = new IntersectionObserver(function(entries) {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.style.animation = 'fadeInUp 0.6s ease forwards';
            observer.unobserve(entry.target);
        }
    });
}, observerOptions);

// Quan sát các card và section
document.querySelectorAll('.service-card, .branch-card, .membership-card, .contact-form').forEach(el => {
    el.style.opacity = '0';
    observer.observe(el);
});

// Thêm animation fadeInUp
const style = document.createElement('style');
style.textContent = `
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translateY(30px);
        }
        to {
            opacity: 1;
            transform: translateY(0);
        }
    }
`;
document.head.appendChild(style);

/* ============================================
   NAVBAR SCROLL EFFECT
   ============================================ */

let lastScroll = 0;
const navbar = document.querySelector('.navbar');

window.addEventListener('scroll', () => {
    const currentScroll = window.pageYOffset;
    
    // Thêm shadow khi scroll
    if (currentScroll > 0) {
        navbar.style.boxShadow = '0 4px 20px rgba(255, 107, 157, 0.15)';
    } else {
        navbar.style.boxShadow = '0 2px 10px rgba(255, 107, 157, 0.2)';
    }
    
    lastScroll = currentScroll;
});

/* ============================================
   COUNTER ANIMATION
   ============================================ */

function animateCounter(element, target, duration = 2000) {
    let current = 0;
    const increment = target / (duration / 16);
    
    const timer = setInterval(() => {
        current += increment;
        if (current >= target) {
            element.textContent = target;
            clearInterval(timer);
        } else {
            element.textContent = Math.floor(current);
        }
    }, 16);
}

/* ============================================
   READY EVENT
   ============================================ */

document.addEventListener('DOMContentLoaded', async function() {
    console.log('PetCareX website loaded successfully!');
    
    // Load dữ liệu từ API
    console.log('🔄 Đang tải dữ liệu từ API...');
    await loadDataFromAPI();
    
    // Initialize tooltips if needed
    initializeTooltips();
});

function initializeTooltips() {
    // Add tooltip functionality if needed
    const tooltips = document.querySelectorAll('[data-tooltip]');
    tooltips.forEach(el => {
        el.addEventListener('mouseenter', function() {
            const tooltip = document.createElement('div');
            tooltip.className = 'tooltip';
            tooltip.textContent = this.getAttribute('data-tooltip');
            this.appendChild(tooltip);
        });
        
        el.addEventListener('mouseleave', function() {
            const tooltip = this.querySelector('.tooltip');
            if (tooltip) tooltip.remove();
        });
    });
}

/* ============================================
   DEBOUNCE FUNCTION
   ============================================ */

function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

/* ============================================
   LOCAL STORAGE FOR PREFERENCES
   ============================================ */

// Lưu theme preference
function setTheme(theme) {
    localStorage.setItem('petcarex-theme', theme);
    document.documentElement.setAttribute('data-theme', theme);
}

// Lấy theme preference
function getTheme() {
    return localStorage.getItem('petcarex-theme') || 'light';
}

// Lưu customer info tạm thời
function saveCustomerInfo(info) {
    localStorage.setItem('petcarex-customer', JSON.stringify(info));
}

function getCustomerInfo() {
    const info = localStorage.getItem('petcarex-customer');
    return info ? JSON.parse(info) : null;
}

/* ============================================
   UTILITY FUNCTIONS
   ============================================ */

// Format currency
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

// Format date
function formatDate(date) {
    return new Intl.DateTimeFormat('vi-VN', {
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    }).format(new Date(date));
}

// Validate email
function validateEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

// Validate phone
function validatePhone(phone) {
    const re = /^(\+84|0)[0-9]{9,10}$/;
    return re.test(phone.replace(/\s/g, ''));
}

/* ============================================
   AUTH MODAL FUNCTIONS
   ============================================ */

function switchAuthTab(tab) {
    // Ẩn tất cả tab
    document.getElementById('loginTab').classList.remove('active');
    document.getElementById('signupTab').classList.remove('active');
    
    // Xóa error message
    const modalLoginError = document.getElementById('modalLoginError');
    if (modalLoginError) {
        modalLoginError.classList.remove('show');
        modalLoginError.innerHTML = '';
    }
    
    // Bỏ active từ tất cả button
    document.querySelectorAll('.auth-tab-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    // Hiện tab được chọn
    if (tab === 'login') {
        document.getElementById('loginTab').classList.add('active');
        document.querySelectorAll('.auth-tab-btn')[0].classList.add('active');
    } else {
        document.getElementById('signupTab').classList.add('active');
        document.querySelectorAll('.auth-tab-btn')[1].classList.add('active');
    }
}

async function handleLogin(event) {
    event.preventDefault();
    event.stopPropagation();
    
    console.log('handleLogin called');
    
    const form = event.target;
    const username = form.querySelector('input[type="text"]').value.trim();
    const password = form.querySelector('input[type="password"]').value;
    const remember = form.querySelector('#rememberMe')?.checked;
    const errorEl = document.getElementById('modalLoginError');
    
    console.log('username:', username, 'password:', password);
    
    // Clear previous error
    if (errorEl) {
        errorEl.classList.remove('show');
        errorEl.innerHTML = '';
    }
    
    // Validation
    if (!username || !password) {
        console.log('validation failed');
        if (errorEl) {
            errorEl.innerHTML = '<span>Vui lòng điền đầy đủ thông tin</span>';
            errorEl.classList.add('show');
        }
        return false;
    }
    
    // call backend login
    try {
        const resp = await fetch(`${API_BASE}/auth/login`, {
            method: 'POST',
            credentials: 'include',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username, password })
        });
        const result = await resp.json();
        if (result.success) {
            // refresh current user from cookie-backed session
            await fetchAuthMe();
            if (remember) {
                localStorage.setItem('petcarex-remember', JSON.stringify({ username, remember: true }));
            } else {
                localStorage.removeItem('petcarex-remember');
            }
            showNotification('Đăng nhập thành công!', 'success');
            setTimeout(() => {
                closeModal('authModal');
                updateNavbarAfterLogin();
            }, 800);
        } else {
            if (errorEl) {
                errorEl.innerHTML = '<span>Tên đăng nhập hoặc mật khẩu không chính xác</span>';
                errorEl.classList.add('show');
            }
        }
    } catch (err) {
        console.error('login error', err);
        if (errorEl) {
            errorEl.innerHTML = '<span>Lỗi kết nối server</span>';
            errorEl.classList.add('show');
        }
    }
    return false;
}

async function handleSignup(event) {
    event.preventDefault();
    event.stopPropagation();

    const form = event.target;
    const fullName = form.querySelector('input[name="fullname"]').value.trim();
    const username = form.querySelector('input[name="username"]').value.trim();
    const phone = form.querySelector('input[name="phone"]').value.trim();
    const password = form.querySelector('input[name="password"]').value;
    const confirmPassword = form.querySelector('input[name="confirmPassword"]').value;
    const agreeTerms = form.querySelector('input[name="agreeTerms"]').checked;

    // Clear previous error
    const errorEl = document.getElementById('modalSignupError');
    if (errorEl) {
        errorEl.classList.remove('show');
        errorEl.innerHTML = '';
    }

    // Validation (giữ nguyên như cũ)
    if (!fullName || !username || !phone || !password || !confirmPassword) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Vui lòng điền đầy đủ thông tin</span>';
            errorEl.classList.add('show');
        }
        return false;
    }
    if (username.length < 3) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Tên đăng nhập phải có tối thiểu 3 ký tự</span>';
            errorEl.classList.add('show');
        }
        return false;
    }
    if (!/^[0-9]{10,11}$/.test(phone)) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Số điện thoại không hợp lệ (10-11 số)</span>';
            errorEl.classList.add('show');
        }
        return false;
    }
    if (password.length < 6) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Mật khẩu phải có tối thiểu 6 ký tự</span>';
            errorEl.classList.add('show');
        }
        return false;
    }
    if (password !== confirmPassword) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Mật khẩu xác nhận không khớp</span>';
            errorEl.classList.add('show');
        }
        return false;
    }
    if (!agreeTerms) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Vui lòng đồng ý với điều khoản sử dụng</span>';
            errorEl.classList.add('show');
        }
        return false;
    }

    // Gửi request lên API backend
    try {
        const payload = {
            username,
            password,
            fullname: fullName,
            phone,
            email: null
        };
        const resp = await fetch(`${API_BASE}/auth/register`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });
        const result = await resp.json();
        if (result.success) {
            // Lưu thông tin user vào localStorage (tối thiểu)
            // chỉ dùng keys chữ thường để nhất quán với supabase
            localStorage.setItem('petcarex-user', JSON.stringify({
                id: result.data.customerid || null,
                customerid: result.data.customerid || null,
                username: result.data.username,
                fullname: fullName
            }));
            showNotification('Đăng ký thành công! Hãy đăng nhập', 'success');
            form.reset();
            setTimeout(() => {
                switchAuthTab('login');
            }, 1500);
        } else {
            if (errorEl) {
                errorEl.innerHTML = `<span>${result.error || 'Lỗi đăng ký'}</span>`;
                errorEl.classList.add('show');
            }
        }
    } catch (err) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Lỗi kết nối server</span>';
            errorEl.classList.add('show');
        }
    }
}

function socialLogin(provider) {
    showNotification(`Tính năng đăng nhập bằng ${provider} sẽ được cập nhật sớm!`, 'info');
}

function socialSignup(provider) {
    showNotification(`Tính năng đăng ký bằng ${provider} sẽ được cập nhật sớm!`, 'info');
}

function updateNavbarAfterLogin() {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (user) {
        // Hiển thị button Lịch Sử
        const historyLink = document.getElementById('historyLink');
        if (historyLink) {
            historyLink.style.display = 'block';
        }
        
        // Hiển thị button Đăng Xuất và ẩn Đăng Nhập
        const logoutBtn = document.querySelector('.btn-logout');
        if (logoutBtn) {
            logoutBtn.style.display = 'block';
        }
        
        const loginLink = document.querySelector('.btn-login');
        if (loginLink) {
            const displayName = user.fullname || user.username || 'Người dùng';
            loginLink.innerHTML = `<i class="fas fa-user"></i> ${displayName}`;
            loginLink.style.background = 'transparent';
            loginLink.style.color = 'white';
            loginLink.onclick = function(e) {
                e.preventDefault();
                openModal('memberModal');
                displayMemberInfo();
            };
        }
    }
}

function handleLogout() {
    // Xác nhận đăng xuất
    if (confirm('Bạn có chắc muốn đăng xuất?')) {
        // Xóa thông tin user
        localStorage.removeItem('petcarex-user');
        
        // Reset navbar
        const loginLink = document.querySelector('.btn-login');
        if (loginLink) {
            loginLink.innerHTML = 'Đăng Nhập';
            loginLink.style.background = '';
            loginLink.style.color = '';
            loginLink.onclick = function(e) {
                e.preventDefault();
                openModal('authModal');
            };
        }
        
        // Ẩn button Lịch Sử và Đăng Xuất
        const historyLink = document.getElementById('historyLink');
        if (historyLink) {
            historyLink.style.display = 'none';
        }
        
        const logoutBtn = document.querySelector('.btn-logout');
        if (logoutBtn) {
            logoutBtn.style.display = 'none';
        }
        
        // Reset giỏ hàng nếu có
        cart = [];
        updateCartCount();
        
        alert('Đã đăng xuất thành công!');
    }
}

function displayMemberInfo() {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) return;
    
    const loyaltyPoints = user.loyaltyPoints || 0;
    const tierInfo = getMembershipTier(loyaltyPoints);
    
    // Theo cấu trúc database MembershipLevel
    let benefits = [];
    
    if (tierInfo.LevelID === 'L3') { // Platinum >= 240 điểm
        benefits = [
            'Giảm 15% tổng hóa đơn',
            'Ưu tiên đặt lịch VIP',
            'Tư vấn miễn phí 24/7',
            'Duy trì với 160 điểm/năm'
        ];
    } else if (tierInfo.LevelID === 'L2') { // Standard >= 60 điểm
        benefits = [
            'Giảm 10% tổng hóa đơn',
            'Hỗ trợ ưu tiên',
            'Khuyến mãi độc quyền',
            'Duy trì với 60 điểm/năm'
        ];
    } else { // Basic (L1)
        benefits = [
            'Giảm 5% tổng hóa đơn',
            'Tích lũy điểm loyalty',
            'Ưu đãi chi nhánh',
            'Nâng cấp Standard khi đạt 60 điểm'
        ];
    }
    
    document.getElementById('memberName').textContent = user.FullName || user.fullname || user.username || 'Khách Hàng';
    document.getElementById('memberEmail').textContent = user.PhoneNumber || user.phone || 'Chưa có thông tin';
    document.getElementById('loyaltyPoints').textContent = loyaltyPoints;
    document.getElementById('memberTier').textContent = tierInfo.name + ` (Giảm ${tierInfo.discount}%)`;
    
    const benefitsList = document.getElementById('memberBenefits');
    benefitsList.innerHTML = benefits.map(b => `
        <li style="padding: 0.5rem 0; border-bottom: 1px solid #EEE;">
            <i class="fas fa-check" style="color: var(--accent-color); margin-right: 0.5rem;"></i>
            ${b}
        </li>
    `).join('');
}

function handleLogout() {
    localStorage.removeItem('petcarex-user');
    localStorage.removeItem('petcarex-remember');
    
    showNotification('Bạn đã đăng xuất', 'success');
    
    setTimeout(() => {
        location.reload();
    }, 1000);
}

// Load lại thông tin login khi trang load
window.addEventListener('DOMContentLoaded', () => {
    // Render branches từ mock data
    renderBranches();
    
    const user = localStorage.getItem('petcarex-user');
    if (user) {
        updateNavbarAfterLogin();
    }
    
    // Load remembered email
    const remembered = localStorage.getItem('petcarex-remember');
    if (remembered) {
        const data = JSON.parse(remembered);
        const loginEmailInput = document.querySelector('#loginTab input[type="text"]');
        if (loginEmailInput) {
            loginEmailInput.value = data.email;
        }
    }
});

console.log('PetCareX JavaScript loaded successfully!');
