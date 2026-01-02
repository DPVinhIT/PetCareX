using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CASHIER
{
    public partial class frmCashier : Form
    {
        // Biến lưu trữ thông tin hiện tại
        private string currentCustomerId = "";
        private string currentOrderId = "";
        private string currentCashierId = "E0317"; // Default fallback
        private string currentFullName = "";
        private string currentRole = "Cashier";
        private string currentUsername = "";
        private string currentCustomerLevel = ""; // Level của khách hàng (L1, L2, L3)
        private decimal totalAmount = 0;          // Tổng tiền gốc
        private decimal discountedAmount = 0;     // Tổng tiền sau giảm
        private DataTable currentOrderDetails;    // Chi tiết đơn hàng để in hóa đơn
        private System.Collections.Generic.List<string> paymentTypeIds = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.List<string> discountIds = new System.Collections.Generic.List<string>();
        private System.Collections.Generic.List<double> discountPercentages = new System.Collections.Generic.List<double>();
        private Timer dateTimeTimer;

        /// <summary>
        /// Constructor with full user info from login
        /// </summary>
        public frmCashier(string employeeId, string fullName, string role, string username) : this()
        {
            currentCashierId = employeeId;
            currentFullName = fullName;
            currentRole = role;
            currentUsername = username;
        }

        public frmCashier()
        {
            InitializeComponent();
            this.Load += FrmCashier_Load;
        }

        private void FrmCashier_Load(object sender, EventArgs e)
        {
            // Test kết nối database với thông báo lỗi chi tiết
            string errorMessage;
            if (!DatabaseHelper.TestConnection(out errorMessage))
            {
                MessageBox.Show("Không thể kết nối đến database!\n\n" + errorMessage, 
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Load danh sách phương thức thanh toán
            LoadPaymentMethods();
            
            // Load danh sách mã giảm giá
            LoadDiscounts();

            // Hiển thị thông tin thu ngân from login
            lblEmployeeID.Text = !string.IsNullOrEmpty(currentCashierId) ? currentCashierId : "EmployeeID";
            lblUserName.Text = !string.IsNullOrEmpty(currentFullName) ? currentFullName : "Thu Ngân";
            lblUserRole.Text = !string.IsNullOrEmpty(currentRole) ? currentRole : "Cashier";
            lblUsername.Text = !string.IsNullOrEmpty(currentUsername) ? currentUsername : "Username";

            // Xóa dữ liệu mặc định trong DataGridView
            dgvOrderDetails.Rows.Clear();

            // Setup date time timer
            SetupDateTimeTimer();

            // Setup Enter key for search
            txtSearch.KeyPress += TxtSearch_KeyPress;
            
            // Setup event khi chọn mã giảm giá
            cboDiscount.SelectedIndexChanged += CboDiscount_SelectedIndexChanged;
        }

        /// <summary>
        /// Setup timer để cập nhật thời gian
        /// </summary>
        private void SetupDateTimeTimer()
        {
            UpdateDateTime();
            dateTimeTimer = new Timer();
            dateTimeTimer.Interval = 1000;
            dateTimeTimer.Tick += (s, e) => UpdateDateTime();
            dateTimeTimer.Start();
        }

        private void UpdateDateTime()
        {
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm:ss");
        }

        /// <summary>
        /// Xử lý Enter trong search box
        /// </summary>
        private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SearchCustomer();
            }
        }

        /// <summary>
        /// Load danh sách phương thức thanh toán vào ComboBox
        /// </summary>
        private void LoadPaymentMethods()
        {
            try
            {
                DataTable dt = DatabaseHelper.GetPaymentMethods();
                cboPaymentMethod.Items.Clear();
                paymentTypeIds.Clear();
                
                cboPaymentMethod.Items.Add("-- Chọn phương thức thanh toán --");
                paymentTypeIds.Add(""); // placeholder cho index 0
                
                foreach (DataRow row in dt.Rows)
                {
                    cboPaymentMethod.Items.Add(row["MethodName"].ToString());
                    paymentTypeIds.Add(row["PaymentTypeID"].ToString());
                }
                cboPaymentMethod.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                // Nếu không lấy được từ DB, dùng danh sách mặc định
                cboPaymentMethod.Items.Clear();
                paymentTypeIds.Clear();
                cboPaymentMethod.Items.AddRange(new object[] { "-- Chọn --", "Tiền mặt", "Chuyển khoản", "Ví điện tử" });
                paymentTypeIds.AddRange(new string[] { "", "PT001", "PT002", "PT003" });
                cboPaymentMethod.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Load danh sách mã giảm giá còn hiệu lực vào ComboBox
        /// customerLevel = null: chưa có khách hàng → hiện tất cả
        /// customerLevel = "": khách không có level → chỉ hiện discount NULL
        /// customerLevel = "L1/L2/L3": lọc theo level
        /// </summary>
        private void LoadDiscounts(string customerLevel = null)
        {
            try
            {
                DataTable dt;
                if (customerLevel == null)
                {
                    // Chưa có khách hàng → Load tất cả discount hiệu lực
                    dt = DatabaseHelper.GetActiveDiscounts();
                }
                else
                {
                    // Đã có khách hàng → Lọc theo level (kể cả khi level rỗng)
                    dt = DatabaseHelper.GetDiscountsByLevel(customerLevel);
                }
                
                cboDiscount.Items.Clear();
                discountIds.Clear();
                discountPercentages.Clear();
                
                cboDiscount.Items.Add("-- Không áp dụng --");
                discountIds.Add(""); 
                discountPercentages.Add(0); // 0% giảm cho không áp dụng
                
                foreach (DataRow row in dt.Rows)
                {
                    string discountName = row["DiscountName"].ToString();
                    double percentage = Convert.ToDouble(row["Percentage"]);
                    cboDiscount.Items.Add($"{discountName} ({percentage * 100:0}%)");
                    discountIds.Add(row["DiscountID"].ToString());
                    discountPercentages.Add(percentage);
                }
                cboDiscount.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                // Nếu không lấy được từ DB, show placeholder
                cboDiscount.Items.Clear();
                discountIds.Clear();
                discountPercentages.Clear();
                cboDiscount.Items.Add("-- Không có mã giảm giá --");
                discountIds.Add("");
                discountPercentages.Add(0);
                cboDiscount.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Button Search - Tìm đơn hàng
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomer();
        }

        /// <summary>
        /// Tìm khách hàng và load đơn hàng
        /// </summary>
        private void SearchCustomer()
        {
            string input = txtSearch.Text.Trim();
            
            if (string.IsNullOrWhiteSpace(input))
            {
                ShowMessage("Vui lòng nhập số điện thoại hoặc mã khách hàng!", "Thông báo", MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tìm khách hàng
                DataRow customer = null;
                
                // Kiểm tra xem input là SĐT hay CustomerID
                if (input.StartsWith("C") || input.StartsWith("c"))
                {
                    customer = DatabaseHelper.GetCustomerInfo(input);
                }
                else
                {
                    customer = DatabaseHelper.GetCustomerByPhone(input);
                }

                if (customer == null)
                {
                    ShowMessage("Không tìm thấy khách hàng!", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }

                // Lưu CustomerID
                currentCustomerId = customer["CustomerID"].ToString();
                
                // Lưu LevelID để lọc discount
                currentCustomerLevel = customer["LevelID"] != DBNull.Value 
                    ? customer["LevelID"].ToString() 
                    : "";

                // Hiển thị thông tin khách hàng
                lblCustomerName.Text = customer["FullName"].ToString();
                lblCustomerPhone.Text = customer["PhoneNumber"].ToString();
                lblCustomerMember.Text = customer["MembershipLevel"].ToString();

                // Load mã giảm giá phù hợp với level khách hàng
                LoadDiscounts(currentCustomerLevel);

                // Load đơn hàng chưa thanh toán
                LoadPendingOrders(currentCustomerId);
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi: " + ex.Message, "Lỗi", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load danh sách đơn hàng chưa thanh toán
        /// </summary>
        private void LoadPendingOrders(string customerId)
        {
            try
            {
                DataTable orders = DatabaseHelper.GetOrdersNotYetPaid(customerId);
                
                if (orders.Rows.Count == 0)
                {
                    ShowMessage("Khách hàng không có đơn hàng chờ thanh toán!", "Thông báo", MessageBoxIcon.Information);
                    return;
                }

                // Hiển thị danh sách đơn hàng để chọn
                if (orders.Rows.Count == 1)
                {
                    // Chỉ có 1 đơn, tự động load
                    currentOrderId = orders.Rows[0]["OrderID"].ToString();
                    LoadOrderDetails(currentOrderId);
                }
                else
                {
                    // Có nhiều đơn, cho chọn
                    string orderList = "Chọn đơn hàng:\n";
                    for (int i = 0; i < orders.Rows.Count; i++)
                    {
                        orderList += $"{i + 1}. {orders.Rows[i]["OrderID"]} - {orders.Rows[i]["CreateDate"]}\n";
                    }
                    
                    string choice = ShowInputDialog(orderList + "\nNhập số thứ tự:", "Chọn đơn hàng");
                    if (int.TryParse(choice, out int index) && index >= 1 && index <= orders.Rows.Count)
                    {
                        currentOrderId = orders.Rows[index - 1]["OrderID"].ToString();
                        LoadOrderDetails(currentOrderId);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi load đơn hàng: " + ex.Message, "Lỗi", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Load chi tiết đơn hàng vào DataGridView
        /// </summary>
        private void LoadOrderDetails(string orderId)
        {
            try
            {
                DataTable details = DatabaseHelper.GetOrderDetail(orderId);
                
                // Lưu để in hóa đơn
                currentOrderDetails = details.Copy();
                
                // Xóa dữ liệu cũ
                dgvOrderDetails.Rows.Clear();
                totalAmount = 0;

                int no = 1;
                foreach (DataRow row in details.Rows)
                {
                    decimal price = Convert.ToDecimal(row["Thành Tiền (Tạm tính)"]);
                    totalAmount += price;

                    dgvOrderDetails.Rows.Add(
                        no++,
                        row["Tên Sản Phẩm"].ToString(),
                        row["Số Lượng"].ToString(),
                        string.Format("{0:N0} đ", row["Đơn Giá Niêm Yết"]),
                        string.Format("{0:N0} đ", price)
                    );
                }

                // Hiển thị tổng tiền
                lblTotalAmount.Text = string.Format("{0:N0} đ", totalAmount);

                // Hiển thị thông tin thanh toán
                UpdatePaymentCalculation();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi load chi tiết đơn hàng: " + ex.Message, "Lỗi", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cập nhật thông tin tính tiền với discount preview
        /// </summary>
        private void UpdatePaymentCalculation()
        {
            // Tính tiền sau giảm giá
            double discountRate = 0;
            string discountName = "";
            
            if (cboDiscount.SelectedIndex > 0 && cboDiscount.SelectedIndex < discountPercentages.Count)
            {
                discountRate = discountPercentages[cboDiscount.SelectedIndex];
                discountName = cboDiscount.SelectedItem.ToString();
            }
            
            discountedAmount = totalAmount * (1 - (decimal)discountRate);
            
            // Cập nhật label tổng tiền
            if (discountRate > 0)
            {
                lblTotalAmount.Text = $"{discountedAmount:N0} đ";
                lblTotalAmount.ForeColor = Color.FromArgb(72, 187, 120); // Màu xanh lá
            }
            else
            {
                lblTotalAmount.Text = $"{totalAmount:N0} đ";
                lblTotalAmount.ForeColor = Color.FromArgb(56, 161, 105); // Màu gốc
            }
            
            // Cập nhật invoice preview
            rtbInvoicePreview.Clear();
            rtbInvoicePreview.SelectionFont = new Font("Consolas", 9F);
            rtbInvoicePreview.AppendText("╔═══════════════════════════╗\n");
            rtbInvoicePreview.AppendText("║    HÓA ĐƠN THANH TOÁN     ║\n");
            rtbInvoicePreview.AppendText("╚═══════════════════════════╝\n\n");
            rtbInvoicePreview.AppendText($"  Mã đơn: {currentOrderId}\n");
            rtbInvoicePreview.AppendText($"  Khách: {lblCustomerName.Text}\n");
            rtbInvoicePreview.AppendText($"  SĐT: {lblCustomerPhone.Text}\n");
            rtbInvoicePreview.AppendText($"  Hạng: {lblCustomerMember.Text}\n\n");
            rtbInvoicePreview.AppendText("────────────────────────────\n");
            rtbInvoicePreview.AppendText($"  Tổng tiền:  {totalAmount:N0} đ\n");
            
            if (discountRate > 0)
            {
                rtbInvoicePreview.SelectionColor = Color.FromArgb(237, 137, 54); // Màu cam
                rtbInvoicePreview.AppendText($"  Giảm giá:  -{discountRate * 100:0}%\n");
                rtbInvoicePreview.SelectionColor = Color.Black;
                rtbInvoicePreview.AppendText("────────────────────────────\n");
                rtbInvoicePreview.SelectionColor = Color.FromArgb(72, 187, 120);
                rtbInvoicePreview.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
                rtbInvoicePreview.AppendText($"  THANH TOÁN: {discountedAmount:N0} đ\n");
            }
            else
            {
                rtbInvoicePreview.AppendText("────────────────────────────\n");
                rtbInvoicePreview.SelectionFont = new Font("Consolas", 10F, FontStyle.Bold);
                rtbInvoicePreview.AppendText($"  THANH TOÁN: {totalAmount:N0} đ\n");
            }
            rtbInvoicePreview.AppendText("════════════════════════════\n");
        }

        /// <summary>
        /// Event khi chọn mã giảm giá - cập nhật preview
        /// </summary>
        private void CboDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (totalAmount > 0)
            {
                UpdatePaymentCalculation();
            }
        }

        /// <summary>
        /// Button Checkout - Thanh toán
        /// </summary>
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentOrderId))
            {
                ShowMessage("Vui lòng tìm và chọn đơn hàng trước!", "Thông báo", MessageBoxIcon.Warning);
                return;
            }

            if (cboPaymentMethod.SelectedIndex <= 0)
            {
                ShowMessage("Vui lòng chọn phương thức thanh toán!", "Thông báo", MessageBoxIcon.Warning);
                return;
            }

            // Nhập số tiền khách đưa - dùng số tiền sau giảm
            decimal amountToPay = discountedAmount > 0 ? discountedAmount : totalAmount;
            string discountInfo = "";
            if (cboDiscount.SelectedIndex > 0 && discountPercentages.Count > cboDiscount.SelectedIndex)
            {
                double rate = discountPercentages[cboDiscount.SelectedIndex] * 100;
                discountInfo = $"🏷️ Giảm giá: {rate:0}%\n";
            }
            
            string paymentInput = ShowInputDialog(
                $"💵 Tổng tiền gốc: {totalAmount:N0} đ\n" +
                discountInfo +
                $"💰 Cần thanh toán: {amountToPay:N0} đ\n\n" +
                "Nhập số tiền khách đưa:", "Thanh toán");
            
            if (!decimal.TryParse(paymentInput, out decimal paymentMoney))
            {
                ShowMessage("Số tiền không hợp lệ!", "Lỗi", MessageBoxIcon.Error);
                return;
            }

            if (paymentMoney < amountToPay)
            {
                ShowMessage($"Số tiền không đủ!\n\n💵 Cần: {amountToPay:N0} đ\n💳 Đưa: {paymentMoney:N0} đ", 
                    "Lỗi", MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Lấy PaymentTypeID thực từ database
                string paymentMethodId = paymentTypeIds[cboPaymentMethod.SelectedIndex];
                
                // Lấy DiscountID nếu có chọn
                string discountId = null;
                if (cboDiscount.SelectedIndex > 0 && cboDiscount.SelectedIndex < discountIds.Count)
                {
                    discountId = discountIds[cboDiscount.SelectedIndex];
                }

                // Gọi stored procedure tạo hóa đơn
                DataTable result = DatabaseHelper.CreateInvoice(
                    currentOrderId,
                    currentCashierId,
                    paymentMethodId,
                    paymentMoney,
                    0,  // promotion
                    discountId // discountId từ ComboBox
                );

                decimal changeAmount = paymentMoney - amountToPay;

                // Hiển thị kết quả
                string message = $"✅ THANH TOÁN THÀNH CÔNG!\n\n" +
                                $"💵 Tổng tiền gốc: {totalAmount:N0} đ\n";
                
                if (discountId != null)
                {
                    double rate = discountPercentages[cboDiscount.SelectedIndex] * 100;
                    message += $"🏷️ Giảm giá: {rate:0}% (-{totalAmount - amountToPay:N0} đ)\n";
                }
                
                message += $"💰 Thanh toán: {amountToPay:N0} đ\n" +
                          $"💳 Tiền khách đưa: {paymentMoney:N0} đ\n" +
                          $"💵 Tiền thối: {changeAmount:N0} đ";

                string invoiceId = "";
                if (result.Rows.Count > 0)
                {
                    invoiceId = result.Rows[0]["InvoiceID"].ToString();
                    message += $"\n\n🧾 Mã hóa đơn: {invoiceId}";
                }

                ShowMessage(message, "Thành công", MessageBoxIcon.Information);

                // Hỏi in hóa đơn
                if (!string.IsNullOrEmpty(invoiceId))
                {
                    DialogResult printResult = MessageBox.Show(
                        "Bạn có muốn in/xuất hóa đơn PDF không?",
                        "In hóa đơn",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (printResult == DialogResult.Yes)
                    {
                        try
                        {
                            // Lấy thông tin giảm giá
                            double discountRate = 0;
                            string discountName = "";
                            if (discountId != null && cboDiscount.SelectedIndex > 0)
                            {
                                discountRate = discountPercentages[cboDiscount.SelectedIndex];
                                discountName = cboDiscount.SelectedItem.ToString();
                            }

                            // Tạo và mở hóa đơn HTML
                            string invoicePath = InvoiceGenerator.GenerateInvoiceHtml(
                                invoiceId,
                                currentOrderId,
                                lblCustomerName.Text,
                                lblCustomerPhone.Text,
                                lblCustomerMember.Text,
                                currentOrderDetails,
                                totalAmount,
                                amountToPay,
                                discountRate,
                                discountName,
                                cboPaymentMethod.SelectedItem.ToString(),
                                paymentMoney,
                                lblUserName.Text
                            );

                            InvoiceGenerator.OpenInvoice(invoicePath);
                        }
                        catch (Exception printEx)
                        {
                            ShowMessage("Lỗi tạo hóa đơn: " + printEx.Message, "Lỗi", MessageBoxIcon.Warning);
                        }
                    }
                }

                // Reset form
                ResetForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi thanh toán: " + ex.Message, "Lỗi", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Button Cancel - Hủy đơn hàng hiện tại
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        /// <summary>
        /// Button History - Xem lịch sử thanh toán trong ngày
        /// </summary>
        private void btnHistory_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable history = DatabaseHelper.GetTodayInvoices();
                
                if (history.Rows.Count == 0)
                {
                    ShowMessage("Chưa có hóa đơn nào trong hôm nay!", "Thông báo", MessageBoxIcon.Information);
                    return;
                }

                // Tạo form popup hiển thị lịch sử
                Form historyForm = new Form
                {
                    Text = "📋 Lịch Sử Thanh Toán Hôm Nay - " + DateTime.Now.ToString("dd/MM/yyyy"),
                    Size = new Size(900, 500),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = Color.FromArgb(247, 250, 252),
                    Font = new Font("Segoe UI", 9F)
                };

                // Header
                Label lblHeader = new Label
                {
                    Text = $"📊 Tổng cộng: {history.Rows.Count} hóa đơn",
                    Dock = DockStyle.Top,
                    Height = 40,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 0, 0),
                    Font = new Font("Segoe UI Semibold", 11F),
                    ForeColor = Color.FromArgb(45, 55, 72)
                };

                // Tính tổng doanh thu
                decimal totalRevenue = 0;
                foreach (DataRow row in history.Rows)
                {
                    if (row["Tổng tiền"] != DBNull.Value)
                    {
                        totalRevenue += Convert.ToDecimal(row["Tổng tiền"]);
                    }
                }

                Label lblRevenue = new Label
                {
                    Text = $"💰 Tổng doanh thu: {totalRevenue:N0} đ",
                    Dock = DockStyle.Top,
                    Height = 35,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 0, 0, 0),
                    Font = new Font("Segoe UI Semibold", 12F),
                    ForeColor = Color.FromArgb(56, 161, 105),
                    BackColor = Color.FromArgb(240, 255, 244)
                };

                // DataGridView
                DataGridView dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = history,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    BackgroundColor = Color.White,
                    BorderStyle = BorderStyle.None,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                    {
                        BackColor = Color.FromArgb(45, 55, 72),
                        ForeColor = Color.White,
                        Font = new Font("Segoe UI Semibold", 9F),
                        Padding = new Padding(5)
                    },
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        SelectionBackColor = Color.FromArgb(66, 153, 225),
                        SelectionForeColor = Color.White
                    }
                };

                // Button đóng
                Button btnClose = new Button
                {
                    Text = "Đóng",
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    BackColor = Color.FromArgb(66, 153, 225),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Semibold", 10F)
                };
                btnClose.FlatAppearance.BorderSize = 0;
                btnClose.Click += (s, ev) => historyForm.Close();

                historyForm.Controls.Add(dgv);
                historyForm.Controls.Add(lblRevenue);
                historyForm.Controls.Add(lblHeader);
                historyForm.Controls.Add(btnClose);

                historyForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi lấy lịch sử: " + ex.Message, "Lỗi", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset form về trạng thái ban đầu
        /// </summary>
        private void ResetForm()
        {
            currentCustomerId = "";
            currentOrderId = "";
            currentCustomerLevel = "";
            totalAmount = 0;
            discountedAmount = 0;

            lblCustomerName.Text = "---";
            lblCustomerPhone.Text = "---";
            lblCustomerMember.Text = "---";
            lblTotalAmount.Text = "0 đ";
            lblTotalAmount.ForeColor = Color.FromArgb(56, 161, 105); // Reset màu

            dgvOrderDetails.Rows.Clear();
            rtbInvoicePreview.Clear();
            cboPaymentMethod.SelectedIndex = 0;
            
            // Reset về tất cả discounts (không lọc theo level)
            LoadDiscounts();
            
            txtSearch.Clear();
            txtSearch.Focus();
        }

        /// <summary>
        /// Hiển thị thông báo với style đẹp hơn
        /// </summary>
        private void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Hiển thị dialog nhập liệu với style đẹp
        /// </summary>
        private string ShowInputDialog(string prompt, string title)
        {
            Form inputForm = new Form()
            {
                Width = 420,
                Height = 200,
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(247, 250, 252),
                Font = new Font("Segoe UI", 10F)
            };

            Label label = new Label() 
            { 
                Left = 20, 
                Top = 20, 
                Width = 370, 
                Height = 70,
                Text = prompt,
                ForeColor = Color.FromArgb(45, 55, 72)
            };
            
            TextBox textBox = new TextBox() 
            { 
                Left = 20, 
                Top = 95, 
                Width = 360,
                Height = 30,
                Font = new Font("Segoe UI", 12F),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            
            Button btnOk = new Button() 
            { 
                Text = "✓ Xác nhận", 
                Left = 200, 
                Top = 135, 
                Width = 90,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(72, 187, 120),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK 
            };
            btnOk.FlatAppearance.BorderSize = 0;
            
            Button btnCancel = new Button() 
            { 
                Text = "✕ Hủy", 
                Left = 295, 
                Top = 135, 
                Width = 85,
                Height = 32,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(160, 174, 192),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel 
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            inputForm.Controls.Add(label);
            inputForm.Controls.Add(textBox);
            inputForm.Controls.Add(btnOk);
            inputForm.Controls.Add(btnCancel);
            inputForm.AcceptButton = btnOk;
            inputForm.CancelButton = btnCancel;

            return inputForm.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (dateTimeTimer != null)
            {
                dateTimeTimer.Stop();
                dateTimeTimer.Dispose();
            }
        }
    }
}
