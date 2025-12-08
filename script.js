/* ============================================
   MOCK DATA - CHI NHÁNH
   ============================================ */

const branchesData = [
    {
        id: 1,
        name: 'Hà Nội - Chi Nhánh 1',
        address: '123 Đường Lý Thái Tổ, Hà Nội',
        phone: '(024) 1234-5678',
        hours: '7:00 - 21:00 (Thứ 2 - CN)',
        status: 'Mở cửa',
        email: 'hanoi1@petcarex.vn',
        manager: 'BS. Nguyễn Văn A',
        services: ['Khám bệnh', 'Tiêm phòng', 'Phẫu thuật', 'Siêu âm', 'X-quang', 'Spa thú cưng'],
        parking: 'Có bãi đỗ xe rộng rãi',
        facilities: ['Phòng khám hiện đại', '2 phòng mổ', 'Khu điều trị riêng', 'Phòng chờ VIP']
    },
    {
        id: 2,
        name: 'Hà Nội - Chi Nhánh 2',
        address: '456 Phố Cổ, Hà Nội',
        phone: '(024) 9876-5432',
        hours: '7:00 - 21:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 3,
        name: 'TP. Hồ Chí Minh - Chi Nhánh 3',
        address: '789 Nguyễn Hué, TP. HCM',
        phone: '(028) 5555-6666',
        hours: '7:00 - 21:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 4,
        name: 'Đà Nẵng - Chi Nhánh 4',
        address: '321 Đường Tôn Đức Thắng, Đà Nẵng',
        phone: '(0236) 3333-4444',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 5,
        name: 'Hải Phòng - Chi Nhánh 5',
        address: '654 Đường Lạch Tray, Hải Phòng',
        phone: '(0225) 2222-3333',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 6,
        name: 'Cần Thơ - Chi Nhánh 6',
        address: '987 Đường Hòa Bình, Cần Thơ',
        phone: '(0292) 1111-2222',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 7,
        name: 'Nha Trang - Chi Nhánh 7',
        address: '135 Đường Trần Phú, Nha Trang',
        phone: '(0258) 7777-8888',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 8,
        name: 'Huế - Chi Nhánh 8',
        address: '246 Đường Nguyễn Huệ, Huế',
        phone: '(0234) 6666-7777',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 9,
        name: 'Biên Hòa - Chi Nhánh 9',
        address: '357 Đường Võ Văn Kiệt, Biên Hòa',
        phone: '(0251) 5555-6666',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    },
    {
        id: 10,
        name: 'Vũng Tàu - Chi Nhánh 10',
        address: '468 Đường Thùy Vân, Vũng Tàu',
        phone: '(0254) 4444-5555',
        hours: '7:00 - 20:00 (Thứ 2 - CN)',
        status: 'Mở cửa'
    }
];

// Function để render branches
function renderBranches() {
    const container = document.getElementById('branchesContainer');
    if (!container) return;
    
    container.innerHTML = branchesData.map(branch => `
        <div class="branch-card">
            <div class="branch-header">
                <h3>${branch.name}</h3>
                <span class="badge">${branch.status || 'Mở cửa'}</span>
            </div>
            <div class="branch-info">
                <p><i class="fas fa-map-marker-alt"></i> ${branch.address}</p>
                <p><i class="fas fa-phone"></i> ${branch.phone}</p>
                <p><i class="fas fa-clock"></i> ${branch.hours}</p>
            </div>
            <button class="btn btn-outline-sm" onclick="showBranchDetail(${branch.id})">Chi Tiết</button>
        </div>
    `).join('');
}

// Show branch detail
function showBranchDetail(branchId) {
    const branch = branchesData.find(b => b.id === branchId);
    if (!branch) return;
    
    const detailContent = document.getElementById('branchDetailContent');
    if (!detailContent) return;
    
    detailContent.innerHTML = `
        <div style="margin-bottom: 1.5rem;">
            <h3 style="color: var(--primary-color); margin-bottom: 1rem;">${branch.name}</h3>
            <div style="background: #f5f5f5; padding: 1rem; border-radius: 8px; margin-bottom: 1rem;">
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-map-marker-alt" style="color: #f44336; width: 20px;"></i> Địa chỉ:</strong> ${branch.address}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-phone" style="color: #4CAF50; width: 20px;"></i> Điện thoại:</strong> ${branch.phone}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-envelope" style="color: #2196F3; width: 20px;"></i> Email:</strong> ${branch.email || 'Đang cập nhật'}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-clock" style="color: #FF9800; width: 20px;"></i> Giờ mở cửa:</strong> ${branch.hours}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-user-md" style="color: #9C27B0; width: 20px;"></i> Quản lý:</strong> ${branch.manager || 'Đang cập nhật'}</p>
                <p style="margin: 0.5rem 0;"><strong><i class="fas fa-car" style="color: #607D8B; width: 20px;"></i> Bãi đỗ xe:</strong> ${branch.parking || 'Có'}</p>
            </div>
        </div>
        
        ${branch.services ? `
        <div style="margin-bottom: 1.5rem;">
            <h4 style="color: #2196F3; margin-bottom: 0.8rem;"><i class="fas fa-stethoscope"></i> Dịch Vụ Cung Cấp</h4>
            <div style="display: flex; flex-wrap: wrap; gap: 0.5rem;">
                ${branch.services.map(service => `
                    <span style="background: #e3f2fd; padding: 0.4rem 0.8rem; border-radius: 20px; font-size: 0.9rem; color: #1976D2;">
                        ✓ ${service}
                    </span>
                `).join('')}
            </div>
        </div>
        ` : ''}
        
        ${branch.facilities ? `
        <div style="margin-bottom: 1.5rem;">
            <h4 style="color: #4CAF50; margin-bottom: 0.8rem;"><i class="fas fa-building"></i> Cơ Sở Vật Chất</h4>
            <ul style="list-style: none; padding: 0; margin: 0;">
                ${branch.facilities.map(facility => `
                    <li style="padding: 0.4rem 0; color: #666;">
                        <i class="fas fa-check-circle" style="color: #4CAF50; margin-right: 0.5rem;"></i>${facility}
                    </li>
                `).join('')}
            </ul>
        </div>
        ` : ''}
        
        <div style="display: flex; gap: 1rem; margin-top: 1.5rem;">
            <button class="btn btn-primary" onclick="bookAtBranch('${branch.name}')">
                <i class="fas fa-calendar-plus"></i> Đặt Lịch Tại Đây
            </button>
            <button class="btn btn-outline" onclick="openMap('${branch.address}')">
                <i class="fas fa-map"></i> Xem Bản Đồ
            </button>
        </div>
    `;
    
    openModal('branchDetailModal');
}

// Book at specific branch
function bookAtBranch(branchName) {
    closeModal('branchDetailModal');
    openModal('bookingModal');
    const branchSelect = document.getElementById('bookingBranch');
    if (branchSelect) {
        branchSelect.value = branchName;
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

// Get all registered accounts from localStorage
function getAllAccounts() {
    const accounts = localStorage.getItem('petcarex-accounts');
    return accounts ? JSON.parse(accounts) : [];
}

// Save accounts to localStorage
function saveAccounts(accounts) {
    localStorage.setItem('petcarex-accounts', JSON.stringify(accounts));
}

// Check if username already exists
function usernameExists(username) {
    const accounts = getAllAccounts();
    return accounts.some(acc => acc.username === username);
}

// Add loyalty points to user (1 point = 50,000 VND)
function addLoyaltyPoints(amount) {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) return;
    
    // Calculate points: 1 point per 50,000 VND
    const pointsToAdd = Math.floor(amount / 50000);
    
    // Update user loyalty points
    user.loyaltyPoints = (user.loyaltyPoints || 0) + pointsToAdd;
    localStorage.setItem('petcarex-user', JSON.stringify(user));
    
    // Update in accounts list
    const accounts = getAllAccounts();
    const accountIndex = accounts.findIndex(acc => acc.id === user.id);
    if (accountIndex !== -1) {
        accounts[accountIndex].loyaltyPoints = user.loyaltyPoints;
        saveAccounts(accounts);
    }
    
    // Show notification with new tier if changed
    const tier = getMembershipTier(user.loyaltyPoints);
    showNotification(`Đã cộng ${pointsToAdd} điểm loyalty! Cấp độ: ${tier.name}`, 'success');
    
    return pointsToAdd;
}

// Get membership tier based on points
function getMembershipTier(points) {
    if (points >= 240) {
        return { name: 'VIP', discount: 15, color: '#FFD700' };
    } else if (points >= 100) {
        return { name: 'Thân Thiết', discount: 10, color: '#C0C0C0' };
    } else {
        return { name: 'Cơ Bản', discount: 5, color: '#CD7F32' };
    }
}

/* ============================================
   BOOKING & ORDER MANAGEMENT
   ============================================ */

// Save booking appointment
function saveBooking(bookingData) {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) {
        showNotification('Vui lòng đăng nhập trước', 'info');
        return;
    }
    
    let bookings = JSON.parse(localStorage.getItem('petcarex-bookings')) || [];
    const booking = {
        id: Date.now(),
        userId: user.id,
        ...bookingData,
        createdAt: new Date().toLocaleString('vi-VN'),
        status: 'Đã đặt'
    };
    bookings.push(booking);
    localStorage.setItem('petcarex-bookings', JSON.stringify(bookings));
}

// Save vaccination package
function saveVaccination(vaccinationData) {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) {
        showNotification('Vui lòng đăng nhập trước', 'info');
        return;
    }
    
    let vaccinations = JSON.parse(localStorage.getItem('petcarex-vaccinations')) || [];
    const vaccination = {
        id: Date.now(),
        userId: user.id,
        ...vaccinationData,
        createdAt: new Date().toLocaleString('vi-VN'),
        status: 'Đã chọn'
    };
    vaccinations.push(vaccination);
    localStorage.setItem('petcarex-vaccinations', JSON.stringify(vaccinations));
}

// Get user's bookings
function getUserBookings() {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) return [];
    
    const bookings = JSON.parse(localStorage.getItem('petcarex-bookings')) || [];
    return bookings.filter(b => b.userId === user.id);
}

// Get user's vaccinations
function getUserVaccinations() {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) return [];
    
    const vaccinations = JSON.parse(localStorage.getItem('petcarex-vaccinations')) || [];
    return vaccinations.filter(v => v.userId === user.id);
}

// Get user's orders
function getUserOrders() {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) return [];
    
    const orders = JSON.parse(localStorage.getItem('petcarex-orders')) || [];
    return orders.filter(o => o.userId === user.id);
}

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
}

// Display booking history
function displayBookingHistory(bookings, vaccinations, orders) {
    const historyContent = document.getElementById('historyContent');
    if (!historyContent) return;
    
    let content = '';
    
    // Orders section
    if (orders && orders.length > 0) {
        content += `
        <h3 style="color: #9C27B0; border-bottom: 2px solid #9C27B0; padding-bottom: 0.5rem; margin-bottom: 1rem;">
            <i class="fas fa-shopping-bag"></i> Đơn Hàng
        </h3>
        <div style="margin-bottom: 2rem;">
            ${orders.map(order => `
                <div style="background: #F3E5F5; padding: 1rem; margin: 0.5rem 0; border-radius: 8px; border-left: 4px solid #9C27B0;">
                    <p><strong>Mã đơn:</strong> #${order.id}</p>
                    <p><strong>Sản phẩm:</strong> ${order.items.map(item => `${item.name} (x${item.quantity})`).join(', ')}</p>
                    <p><strong>Tổng tiền:</strong> ${order.subtotal.toLocaleString('vi-VN')} VNĐ</p>
                    <p><strong>Giảm giá:</strong> -${order.discount.toLocaleString('vi-VN')} VNĐ (${order.membershipTier})</p>
                    <p><strong>Thành tiền:</strong> <span style="color: #4CAF50; font-weight: bold;">${order.total.toLocaleString('vi-VN')} VNĐ</span></p>
                    <p><strong>Trạng Thái:</strong> <span style="color: #4CAF50; font-weight: bold;">${order.status}</span></p>
                    <p style="font-size: 0.85rem; color: #999;">Đặt lúc: ${order.createdAt}</p>
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
    const price = parseInt(serviceType.value);
    
    if (!price) {
        showNotification('Vui lòng chọn dịch vụ', 'info');
        return;
    }
    
    const bookingData = {
        branch: document.getElementById('bookingBranch').value,
        petName: document.getElementById('petName').value,
        species: document.getElementById('petSpecies').value,
        symptoms: document.getElementById('symptoms').value,
        date: document.getElementById('bookingDate').value,
        time: document.getElementById('bookingTime').value,
        service: serviceType.options[serviceType.selectedIndex].text,
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
    const price = parseInt(serviceType.value) || 0;
    const points = Math.floor(price / 50000);
    
    document.getElementById('bookingTotalPrice').textContent = price.toLocaleString('vi-VN') + ' VNĐ';
    document.getElementById('bookingLoyaltyEarn').textContent = `Tích lũy: ${points} điểm`;
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
   SHOP & CART MANAGEMENT
   ============================================ */

// Product catalog
const products = [
    // Thức ăn
    { id: 1, name: 'Thức ăn hạt cho chó trưởng thành', category: 'food', price: 350000, stock: 50, icon: '🍖' },
    { id: 2, name: 'Thức ăn hạt cho mèo', category: 'food', price: 280000, stock: 45, icon: '🐟' },
    { id: 3, name: 'Pate cho chó vị gà', category: 'food', price: 45000, stock: 100, icon: '🥫' },
    { id: 4, name: 'Pate cho mèo vị cá ngừ', category: 'food', price: 42000, stock: 120, icon: '🥫' },
    { id: 5, name: 'Sữa cho chó con', category: 'food', price: 180000, stock: 30, icon: '🍼' },
    
    // Thuốc
    { id: 6, name: 'Thuốc tẩy giun cho chó', category: 'medicine', price: 120000, stock: 60, icon: '💊' },
    { id: 7, name: 'Thuốc tẩy giun cho mèo', category: 'medicine', price: 110000, stock: 55, icon: '💊' },
    { id: 8, name: 'Vitamin tổng hợp', category: 'medicine', price: 250000, stock: 40, icon: '💉' },
    { id: 9, name: 'Thuốc nhỏ mắt', category: 'medicine', price: 85000, stock: 35, icon: '👁️' },
    { id: 10, name: 'Thuốc xịt ve rận', category: 'medicine', price: 150000, stock: 50, icon: '🧴' },
    
    // Phụ kiện
    { id: 11, name: 'Vòng cổ cho chó', category: 'accessories', price: 95000, stock: 80, icon: '⭕' },
    { id: 12, name: 'Dây dắt chó', category: 'accessories', price: 120000, stock: 70, icon: '🔗' },
    { id: 13, name: 'Bát ăn inox', category: 'accessories', price: 65000, stock: 100, icon: '🥣' },
    { id: 14, name: 'Lồng vận chuyển', category: 'accessories', price: 450000, stock: 25, icon: '📦' },
    { id: 15, name: 'Áo cho chó', category: 'accessories', price: 150000, stock: 60, icon: '👕' },
    
    // Đồ chơi
    { id: 16, name: 'Bóng cao su cho chó', category: 'toys', price: 55000, stock: 90, icon: '⚽' },
    { id: 17, name: 'Chuột nhồi bông cho mèo', category: 'toys', price: 35000, stock: 110, icon: '🐭' },
    { id: 18, name: 'Xương gặm cao su', category: 'toys', price: 75000, stock: 85, icon: '🦴' },
    { id: 19, name: 'Cần câu mèo', category: 'toys', price: 45000, stock: 70, icon: '🎣' },
    { id: 20, name: 'Bàn cào móng cho mèo', category: 'toys', price: 280000, stock: 40, icon: '🪵' }
];

let cart = [];
let currentFilter = 'all';

function showShop() {
    openModal('shopModal');
    displayProducts(products);
    updateCartCount();
}

function filterProducts(category) {
    currentFilter = category;
    
    // Update active button
    document.querySelectorAll('.category-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
    
    // Filter products
    const filtered = category === 'all' ? products : products.filter(p => p.category === category);
    displayProducts(filtered);
}

function displayProducts(productList) {
    const grid = document.getElementById('productsGrid');
    if (!grid) return;
    
    grid.innerHTML = productList.map(product => {
        const needsToggle = product.name.length > 25;
        return `
        <div class="product-card" data-category="${product.category}">
            <div class="product-image">${product.icon}</div>
            <div class="product-title-row">
                <div class="product-name" id="name-${product.id}">${product.name}</div>
                ${needsToggle ? `<button class="product-toggle" onclick="toggleProductName(${product.id})">Xem</button>` : ''}
            </div>
            <div class="product-price">${product.price.toLocaleString('vi-VN')} VNĐ</div>
            <div class="product-stock">Còn: ${product.stock} sản phẩm</div>
            <button class="btn btn-primary btn-full" onclick="addToCart(${product.id})" ${product.stock === 0 ? 'disabled' : ''}>
                ${product.stock === 0 ? 'Hết hàng' : 'Thêm vào giỏ'}
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
    
    const product = products.find(p => p.id === productId);
    if (!product || product.stock === 0) return;
    
    const existingItem = cart.find(item => item.id === productId);
    
    if (existingItem) {
        if (existingItem.quantity < product.stock) {
            existingItem.quantity++;
            showNotification('Đã cập nhật số lượng', 'success');
        } else {
            showNotification('Không đủ hàng trong kho', 'info');
            return;
        }
    } else {
        cart.push({
            ...product,
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
    
    const subtotal = cart.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    const discount = Math.floor(subtotal * tierInfo.discount / 100);
    const finalTotal = subtotal - discount;
    const loyaltyPoints = Math.floor(finalTotal / 50000);
    
    cartContent.innerHTML = cart.map(item => `
        <div class="cart-item">
            <div class="cart-item-image">${item.icon}</div>
            <div class="cart-item-info">
                <div class="cart-item-name">${item.name}</div>
                <div class="cart-item-price">${item.price.toLocaleString('vi-VN')} VNĐ</div>
                <div class="cart-item-controls">
                    <button class="cart-qty-btn" onclick="updateCartQuantity(${item.id}, -1)">-</button>
                    <span class="cart-qty">${item.quantity}</span>
                    <button class="cart-qty-btn" onclick="updateCartQuantity(${item.id}, 1)">+</button>
                    <button class="cart-remove-btn" onclick="removeFromCart(${item.id})">Xóa</button>
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
    const item = cart.find(i => i.id === productId);
    if (!item) return;
    
    const product = products.find(p => p.id === productId);
    const newQuantity = item.quantity + change;
    
    if (newQuantity <= 0) {
        removeFromCart(productId);
        return;
    }
    
    if (newQuantity > product.stock) {
        showNotification('Không đủ hàng trong kho', 'info');
        return;
    }
    
    item.quantity = newQuantity;
    displayCart();
    updateCartCount();
}

function removeFromCart(productId) {
    cart = cart.filter(item => item.id !== productId);
    displayCart();
    updateCartCount();
    
    if (cart.length === 0) {
        closeModal('cartModal');
        showNotification('Giỏ hàng trống', 'info');
    }
}

function checkout() {
    const user = JSON.parse(localStorage.getItem('petcarex-user'));
    if (!user) {
        showNotification('Vui lòng đăng nhập', 'info');
        return;
    }
    
    if (cart.length === 0) return;
    
    const tierInfo = getMembershipTier(user.loyaltyPoints || 0);
    const subtotal = cart.reduce((sum, item) => sum + (item.price * item.quantity), 0);
    const discount = Math.floor(subtotal * tierInfo.discount / 100);
    const finalTotal = subtotal - discount;
    
    // Save order
    const order = {
        id: Date.now(),
        userId: user.id,
        items: cart,
        subtotal: subtotal,
        discount: discount,
        total: finalTotal,
        membershipTier: tierInfo.name,
        createdAt: new Date().toLocaleString('vi-VN'),
        status: 'Đã đặt'
    };
    
    let orders = JSON.parse(localStorage.getItem('petcarex-orders')) || [];
    orders.push(order);
    localStorage.setItem('petcarex-orders', JSON.stringify(orders));
    
    // Add loyalty points
    addLoyaltyPoints(finalTotal);
    
    // Update product stock
    cart.forEach(cartItem => {
        const product = products.find(p => p.id === cartItem.id);
        if (product) {
            product.stock -= cartItem.quantity;
        }
    });
    
    // Clear cart
    cart = [];
    updateCartCount();
    
    closeModal('cartModal');
    showNotification('Đặt hàng thành công! Sẽ giao hàng trong 1-2 ngày', 'success');
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

document.addEventListener('DOMContentLoaded', function() {
    console.log('PetCareX website loaded successfully!');
    
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

function handleLogin(event) {
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
    
    // Get accounts and find user
    const accounts = getAllAccounts();
    const user = accounts.find(acc => acc.username === username && acc.password === password);
    
    if (user) {
        console.log('user found, logging in');
        // Lưu info nếu remember
        if (remember) {
            localStorage.setItem('petcarex-remember', JSON.stringify({
                username: username,
                remember: true
            }));
        } else {
            localStorage.removeItem('petcarex-remember');
        }
        
        showNotification('Đăng nhập thành công!', 'success');
        
        // Lưu user info vào petcarex-user
        localStorage.setItem('petcarex-user', JSON.stringify({
            id: user.id,
            username: user.username,
            name: user.fullname,
            loyaltyPoints: user.loyaltyPoints || 0
        }));
        
        // Đóng modal sau 1.5 giây
        setTimeout(() => {
            closeModal('authModal');
            updateNavbarAfterLogin();
        }, 1500);
    } else {
        console.log('login failed, showing error');
        if (errorEl) {
            errorEl.innerHTML = '<span>Tên đăng nhập hoặc mật khẩu không chính xác</span>';
            errorEl.classList.add('show');
        }
    }
    
    return false;
}

function handleSignup(event) {
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
    
    // Validation
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
    
    if (usernameExists(username)) {
        if (errorEl) {
            errorEl.innerHTML = '<span>Tên đăng nhập đã tồn tại</span>';
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
    
    // Create new account
    const accounts = getAllAccounts();
    const newAccount = {
        id: Date.now(),
        fullname: fullName,
        username: username,
        phone: phone,
        password: password,
        loyaltyPoints: 0,
        createdAt: new Date().toLocaleString('vi-VN')
    };
    
    accounts.push(newAccount);
    saveAccounts(accounts);
    
    showNotification('Đăng ký thành công! Hãy đăng nhập', 'success');
    
    // Xóa form
    form.reset();
    
    // Chuyển sang tab login
    setTimeout(() => {
        switchAuthTab('login');
    }, 1500);
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
    
    let benefits = [];
    
    if (loyaltyPoints >= 240) {
        benefits = [
            'Giảm 15% tổng hóa đơn',
            'Ưu tiên đặt lịch',
            'Tư vấn miễn phí',
            'Duy trì với 160 điểm/năm'
        ];
    } else if (loyaltyPoints >= 100) {
        benefits = [
            'Giảm 10% tổng hóa đơn',
            'Hỗ trợ ưu tiên',
            'Khuyến mãi độc quyền',
            'Duy trì với 60 điểm/năm'
        ];
    } else {
        benefits = [
            'Giảm 5% tổng hóa đơn',
            'Tích lũy điểm loyalty',
            'Ưu đãi chi nhánh'
        ];
    }
    
    document.getElementById('memberName').textContent = user.fullname || user.username || 'Khách Hàng';
    document.getElementById('memberEmail').textContent = user.phone || 'Chưa có thông tin';
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
