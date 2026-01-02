using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NURSE
{
    public partial class frmNurseNursingWorklist : Form
    {
        private string currentNurseID = "E0121"; // Default fallback
        private string currentFullName = "";
        private string currentRole = "Nurse";
        private string currentUsername = "";
        
        // Content panels for switching
        private Panel pnlWorklistContent;
        private Panel pnlInpatientsContent;
        private DataGridView dgvWorklist;
        private DataGridView dgvInpatients;

        /// <summary>
        /// Constructor with full user info from login
        /// </summary>
        public frmNurseNursingWorklist(string nurseID, string fullName, string role, string username) : this()
        {
            currentNurseID = nurseID;
            currentFullName = fullName;
            currentRole = role;
            currentUsername = username;
            
            // Update UI labels
            UpdateUserInfoLabels();
        }

        /// <summary>
        /// Constructor with nurseID only (backward compatibility)
        /// </summary>
        public frmNurseNursingWorklist(string nurseID) : this(nurseID, "", "Nurse", "")
        {
        }

        public frmNurseNursingWorklist()
        {
            InitializeComponent();
            this.Load += FrmNurseNursingWorklist_Load;
        }
        
        /// <summary>
        /// Update sidebar user info labels
        /// </summary>
        private void UpdateUserInfoLabels()
        {
            if (lblEmployeeID != null) lblEmployeeID.Text = currentNurseID;
            if (lblName != null) lblName.Text = currentFullName;
            if (lblRole != null) lblRole.Text = currentRole;
            if (lblUsername != null) lblUsername.Text = currentUsername;
        }

        private void FrmNurseNursingWorklist_Load(object sender, EventArgs e)
        {
            // Hide original controls (we'll use dynamic panels)
            groupBox1.Visible = false;
            dataGridView1.Visible = false;
            
            // Setup content panels
            SetupContentPanels();
            
            // Show Worklist by default
            ShowWorklistPanel();
            
            // Highlight active button
            HighlightActiveButton(btnNursingWorklist);
        }

        /// <summary>
        /// Setup dynamic content panels for Worklist and Inpatients
        /// </summary>
        private void SetupContentPanels()
        {
            // WORKLIST PANEL
            pnlWorklistContent = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };
            
            dgvWorklist = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                BackgroundColor = Color.White
            };
            dgvWorklist.CellDoubleClick += DgvWorklist_CellDoubleClick; // Add double-click handler
            
            Panel pnlWorklistButtons = CreateWorklistButtonPanel();
            pnlWorklistContent.Controls.Add(dgvWorklist);
            pnlWorklistContent.Controls.Add(pnlWorklistButtons);
            
            // INPATIENTS PANEL
            pnlInpatientsContent = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };
            
            dgvInpatients = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                BackgroundColor = Color.White
            };
            dgvInpatients.CellDoubleClick += DgvInpatients_CellDoubleClick;
            
            Panel pnlInpatientsButtons = CreateInpatientsButtonPanel();
            pnlInpatientsContent.Controls.Add(dgvInpatients);
            pnlInpatientsContent.Controls.Add(pnlInpatientsButtons);
            
            // Add panels to main container
            splitContainer2.Panel2.Controls.Add(pnlWorklistContent);
            splitContainer2.Panel2.Controls.Add(pnlInpatientsContent);
        }

        private Panel CreateWorklistButtonPanel()
        {
            Panel btnPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(247, 250, 252)
            };

            Label lblHint = new Label
            {
                Text = "💡 Tick vào ô ✅ để chọn công việc đã hoàn thành, sau đó bấm nút bên dưới",
                AutoSize = true,
                Location = new Point(10, 8),
                ForeColor = Color.Gray
            };

            Button btnComplete = new Button
            {
                Text = "✔️ Xác Nhận Hoàn Thành",
                Location = new Point(10, 30),
                Width = 180,
                Height = 28,
                BackColor = Color.FromArgb(56, 161, 105),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnComplete.Click += (s, ev) => CompleteSelectedTasks();

            Button btnRefresh = new Button
            {
                Text = "🔄 Làm mới",
                Location = new Point(200, 30),
                Width = 100,
                Height = 28,
                BackColor = Color.FromArgb(66, 153, 225),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRefresh.Click += (s, ev) => LoadWorklistData();

            btnPanel.Controls.Add(lblHint);
            btnPanel.Controls.Add(btnComplete);
            btnPanel.Controls.Add(btnRefresh);
            return btnPanel;
        }

        private Panel CreateInpatientsButtonPanel()
        {
            Panel btnPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(247, 250, 252)
            };

            Label lblHint = new Label
            {
                Text = "💡 Chọn 1 dòng và bấm nút bên dưới, hoặc Double-click để xem chi tiết",
                AutoSize = true,
                Location = new Point(10, 8),
                ForeColor = Color.Gray
            };

            Button btnViewMonitoring = new Button
            {
                Text = "📋 Xem Lịch Sử Monitoring",
                Location = new Point(10, 30),
                Width = 180,
                Height = 28,
                BackColor = Color.FromArgb(66, 153, 225),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnViewMonitoring.Click += BtnViewMonitoring_Click;

            Button btnAddLog = new Button
            {
                Text = "➕ Thêm Ghi Chú Mới",
                Location = new Point(200, 30),
                Width = 160,
                Height = 28,
                BackColor = Color.FromArgb(56, 161, 105),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAddLog.Click += BtnAddLog_Click;

            Button btnRefresh = new Button
            {
                Text = "🔄",
                Location = new Point(370, 30),
                Width = 40,
                Height = 28,
                BackColor = Color.FromArgb(160, 174, 192),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnRefresh.Click += (s, ev) => LoadInpatientsData();

            btnPanel.Controls.Add(lblHint);
            btnPanel.Controls.Add(btnViewMonitoring);
            btnPanel.Controls.Add(btnAddLog);
            btnPanel.Controls.Add(btnRefresh);
            return btnPanel;
        }

        #region Panel Switching
        
        private void ShowWorklistPanel()
        {
            pnlWorklistContent.Visible = true;
            pnlInpatientsContent.Visible = false;
            LoadWorklistData();
            this.Text = "Y Tá - Nursing Worklist";
        }

        private void ShowInpatientsPanel()
        {
            pnlWorklistContent.Visible = false;
            pnlInpatientsContent.Visible = true;
            LoadInpatientsData();
            this.Text = "Y Tá - Inpatients";
        }

        private void HighlightActiveButton(Button activeBtn)
        {
            // Reset all menu buttons
            btnNursingWorklist.BackColor = SystemColors.ActiveCaption;
            btnInpationManagement.BackColor = SystemColors.ActiveCaption;
            
            // Highlight active
            activeBtn.BackColor = SystemColors.ControlLightLight;
        }

        #endregion

        #region Worklist Functions

        private void LoadWorklistData()
        {
            try
            {
                DataTable tasks = DatabaseHelper.GetMyTasks(currentNurseID);
                
                if (!tasks.Columns.Contains("Hoàn thành"))
                {
                    tasks.Columns.Add("Hoàn thành", typeof(bool));
                    foreach (DataRow row in tasks.Rows)
                    {
                        row["Hoàn thành"] = false;
                    }
                }
                
                dgvWorklist.DataSource = tasks;
                
                if (dgvWorklist.Columns["TaskID"] != null)
                    dgvWorklist.Columns["TaskID"].Visible = false;
                if (dgvWorklist.Columns["SurgeryID"] != null)
                    dgvWorklist.Columns["SurgeryID"].Visible = false;
                
                dgvWorklist.ReadOnly = false;
                foreach (DataGridViewColumn col in dgvWorklist.Columns)
                {
                    if (col.Name != "Hoàn thành")
                        col.ReadOnly = true;
                }
                
                // Rename columns
                if (dgvWorklist.Columns["Thú Cưng"] != null)
                    dgvWorklist.Columns["Thú Cưng"].HeaderText = "🐾 Thú Cưng";
                if (dgvWorklist.Columns["Loại Phẫu Thuật"] != null)
                    dgvWorklist.Columns["Loại Phẫu Thuật"].HeaderText = "🏥 Phẫu Thuật";
                if (dgvWorklist.Columns["Việc Cần Làm"] != null)
                    dgvWorklist.Columns["Việc Cần Làm"].HeaderText = "📋 Việc Cần Làm";
                if (dgvWorklist.Columns["Lưu Ý"] != null)
                    dgvWorklist.Columns["Lưu Ý"].HeaderText = "⚠️ Lưu Ý";
                if (dgvWorklist.Columns["Hoàn thành"] != null)
                    dgvWorklist.Columns["Hoàn thành"].HeaderText = "✅";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load tasks: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompleteSelectedTasks()
        {
            try
            {
                // Get list of selected tasks
                List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();
                
                foreach (DataGridViewRow row in dgvWorklist.Rows)
                {
                    if (row.Cells["Hoàn thành"] != null && 
                        row.Cells["Hoàn thành"].Value != null && 
                        Convert.ToBoolean(row.Cells["Hoàn thành"].Value) == true)
                    {
                        selectedRows.Add(row);
                    }
                }

                if (selectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng tick vào ô ✅ để chọn công việc cần hoàn thành!", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Show completion note dialog
                var dialogResult = ShowCompletionNoteDialog(selectedRows);
                if (dialogResult == null)
                    return; // Cancelled

                string note = dialogResult.Item1;
                string status = dialogResult.Item2;

                // Complete all selected tasks and add monitoring logs
                int completedCount = 0;
                foreach (var row in selectedRows)
                {
                    string taskID = row.Cells["TaskID"].Value?.ToString() ?? "";
                    string surgeryID = row.Cells["SurgeryID"].Value?.ToString() ?? "";
                    
                    if (!string.IsNullOrEmpty(taskID))
                    {
                        // Complete the task
                        DatabaseHelper.CompleteTask(taskID);
                        
                        // Add monitoring log if note provided
                        if (!string.IsNullOrEmpty(surgeryID) && !string.IsNullOrEmpty(note))
                        {
                            try
                            {
                                DatabaseHelper.AddMonitoringLog(surgeryID, currentNurseID, status, note);
                            }
                            catch { /* Ignore monitoring log errors */ }
                        }
                        
                        completedCount++;
                    }
                }

                MessageBox.Show($"✅ Đã hoàn thành {completedCount} công việc!", "Thành công", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadWorklistData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi complete task: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Show dialog to input completion note
        /// Returns (note, status) tuple, or null if cancelled
        /// </summary>
        private Tuple<string, string> ShowCompletionNoteDialog(List<DataGridViewRow> selectedRows)
        {
            Form dialog = new Form
            {
                Text = "Hoàn Thành Công Việc",
                Size = new Size(420, 300),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.FromArgb(247, 250, 252)
            };

            Label lblInfo = new Label
            {
                Text = $"✅ Hoàn thành {selectedRows.Count} công việc",
                Location = new Point(20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(56, 161, 105)
            };

            // Status dropdown
            Label lblStatus = new Label { Text = "Tình trạng:", Location = new Point(20, 50), AutoSize = true };
            ComboBox cboStatus = new ComboBox
            {
                Location = new Point(100, 47),
                Width = 280,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboStatus.Items.AddRange(new object[] { 
                "Completed - Đã hoàn thành",
                "Good Progress - Tiến triển tốt", 
                "Stable - Ổn định"
            });
            cboStatus.SelectedIndex = 0;

            // Note textbox
            Label lblNote = new Label { Text = "Ghi chú:", Location = new Point(20, 85), AutoSize = true };
            TextBox txtNote = new TextBox
            {
                Location = new Point(20, 110),
                Width = 360,
                Height = 80,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Buttons
            Button btnComplete = new Button
            {
                Text = "✔️ Xác Nhận",
                Location = new Point(120, 210),
                Width = 120,
                Height = 35,
                BackColor = Color.FromArgb(56, 161, 105),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            Button btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(250, 210),
                Width = 80,
                Height = 35,
                BackColor = Color.FromArgb(160, 174, 192),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            Tuple<string, string> result = null;
            
            btnComplete.Click += (s, ev) =>
            {
                string status = cboStatus.Text.Split('-')[0].Trim();
                result = new Tuple<string, string>(txtNote.Text, status);
                dialog.DialogResult = DialogResult.OK;
                dialog.Close();
            };

            btnCancel.Click += (s, ev) =>
            {
                dialog.DialogResult = DialogResult.Cancel;
                dialog.Close();
            };

            dialog.Controls.AddRange(new Control[] { lblInfo, lblStatus, cboStatus, lblNote, txtNote, btnComplete, btnCancel });
            dialog.AcceptButton = btnComplete;
            dialog.CancelButton = btnCancel;

            if (dialog.ShowDialog(this) == DialogResult.OK)
                return result;
            
            return null;
        }

        /// <summary>
        /// Double-click on worklist row to view pet details
        /// </summary>
        private void DgvWorklist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvWorklist.Rows[e.RowIndex];
                string petName = row.Cells["Thú Cưng"]?.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(petName))
                {
                    ShowPetDetails(petName);
                }
            }
        }

        /// <summary>
        /// Show pet details popup
        /// </summary>
        private void ShowPetDetails(string petName)
        {
            try
            {
                DataTable petInfo = DatabaseHelper.GetPetDetails(petName);
                
                if (petInfo.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin thú cưng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataRow pet = petInfo.Rows[0];

                Form dialog = new Form
                {
                    Text = $"🐾 Thông Tin Thú Cưng: {petName}",
                    Size = new Size(400, 420),
                    StartPosition = FormStartPosition.CenterParent,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    BackColor = Color.FromArgb(247, 250, 252)
                };

                int y = 20;
                int labelWidth = 130;

                // Pet Info Section
                Label lblPetTitle = new Label { Text = "🐾 THÔNG TIN THÚ CƯNG", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(45, 55, 72) };
                dialog.Controls.Add(lblPetTitle);
                y += 30;

                AddInfoRow(dialog, "Mã Pet:", pet["Mã Pet"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Tên:", pet["Tên Thú Cưng"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Loài:", pet["Loài"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Giống:", pet["Giống"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Giới tính:", pet["Giới Tính"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Tình trạng:", pet["Tình Trạng Sức Khỏe"]?.ToString() ?? "", ref y, labelWidth);

                y += 15;

                // Owner Info Section
                Label lblOwnerTitle = new Label { Text = "👤 THÔNG TIN CHỦ SỞ HỮU", Location = new Point(20, y), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.FromArgb(45, 55, 72) };
                dialog.Controls.Add(lblOwnerTitle);
                y += 30;

                AddInfoRow(dialog, "Tên chủ:", pet["Tên Chủ Sở Hữu"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Số điện thoại:", pet["SĐT Chủ"]?.ToString() ?? "", ref y, labelWidth);
                AddInfoRow(dialog, "Email:", pet["Email Chủ"]?.ToString() ?? "", ref y, labelWidth);

                // Close button
                Button btnClose = new Button
                {
                    Text = "Đóng",
                    Location = new Point(150, y + 10),
                    Width = 100,
                    Height = 30,
                    BackColor = Color.FromArgb(66, 153, 225),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnClose.Click += (s, ev) => dialog.Close();
                dialog.Controls.Add(btnClose);

                dialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Helper to add info row to dialog
        /// </summary>
        private void AddInfoRow(Form dialog, string label, string value, ref int y, int labelWidth)
        {
            Label lbl = new Label { Text = label, Location = new Point(30, y), Width = labelWidth, ForeColor = Color.Gray };
            Label val = new Label { Text = value, Location = new Point(30 + labelWidth, y), AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(45, 55, 72) };
            dialog.Controls.Add(lbl);
            dialog.Controls.Add(val);
            y += 22;
        }

        #endregion

        #region Inpatients Functions

        private void LoadInpatientsData()
        {
            try
            {
                DataTable inpatients = DatabaseHelper.GetInpatientsList();
                dgvInpatients.DataSource = inpatients;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách bệnh nhân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvInpatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvInpatients.Rows[e.RowIndex];
                string petID = row.Cells["Mã Pet"]?.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(petID))
                {
                    ShowMonitoringHistory(petID);
                }
            }
        }

        private void BtnViewMonitoring_Click(object sender, EventArgs e)
        {
            if (dgvInpatients.CurrentRow != null)
            {
                string petID = dgvInpatients.CurrentRow.Cells["Mã Pet"]?.Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(petID))
                {
                    ShowMonitoringHistory(petID);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnAddLog_Click(object sender, EventArgs e)
        {
            if (dgvInpatients.CurrentRow != null)
            {
                string petID = dgvInpatients.CurrentRow.Cells["Mã Pet"]?.Value?.ToString() ?? "";
                string surgeryID = dgvInpatients.CurrentRow.Cells["Mã PT"]?.Value?.ToString() ?? "";
                string petName = dgvInpatients.CurrentRow.Cells["Tên Thú Cưng"]?.Value?.ToString() ?? "";
                
                if (!string.IsNullOrEmpty(surgeryID))
                {
                    ShowAddMonitoringLogDialog(petID, surgeryID, petName);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ShowMonitoringHistory(string petID)
        {
            try
            {
                string surgeryID = dgvInpatients.CurrentRow?.Cells["Mã PT"]?.Value?.ToString() ?? "";
                string petName = dgvInpatients.CurrentRow?.Cells["Tên Thú Cưng"]?.Value?.ToString() ?? "";

                DataTable history = DatabaseHelper.GetHistoryPostSurgeryMonitoring(petID);

                Form historyForm = new Form
                {
                    Text = $"📋 Lịch Sử Theo Dõi - {petName} (Pet ID: {petID})",
                    Size = new Size(800, 500),
                    StartPosition = FormStartPosition.CenterParent,
                    BackColor = Color.FromArgb(247, 250, 252)
                };

                DataGridView dgvHistory = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = history,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    BackgroundColor = Color.White
                };

                Button btnAddLog = new Button
                {
                    Text = "➕ Thêm Ghi Chú Mới",
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    BackColor = Color.FromArgb(56, 161, 105),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnAddLog.Click += (s, ev) => ShowAddMonitoringLogDialog(petID, surgeryID, petName);

                historyForm.Controls.Add(dgvHistory);
                historyForm.Controls.Add(btnAddLog);
                historyForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAddMonitoringLogDialog(string petID, string surgeryID, string petName)
        {
            if (string.IsNullOrEmpty(surgeryID))
            {
                MessageBox.Show("Không tìm thấy mã phẫu thuật!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form dialog = new Form
            {
                Text = "Thêm Ghi Chú Theo Dõi",
                Size = new Size(450, 280),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblHeader = new Label 
            { 
                Text = $"🐾 Bệnh nhân: {petName}", 
                Location = new Point(20, 15), 
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            Label lblSubHeader = new Label 
            { 
                Text = $"Pet ID: {petID} | Mã PT: {surgeryID}", 
                Location = new Point(20, 40), 
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            Label lblStatus = new Label { Text = "Tình trạng:", Location = new Point(20, 75), AutoSize = true };
            ComboBox cboStatus = new ComboBox
            {
                Location = new Point(100, 72),
                Width = 290,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cboStatus.Items.AddRange(new object[] { 
                "Stable - Ổn định", 
                "Good Progress - Tiến triển tốt", 
                "Mild Pain - Đau nhẹ", 
                "Close Monitoring Required - Cần theo dõi sát", 
                "Ready for Discharge - Sẵn sàng xuất viện" 
            });
            cboStatus.SelectedIndex = 0;

            Label lblNote = new Label { Text = "Ghi chú:", Location = new Point(20, 110), AutoSize = true };
            TextBox txtNote = new TextBox 
            { 
                Location = new Point(100, 107), 
                Width = 290, 
                Height = 70, 
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            Button btnSave = new Button
            {
                Text = "💾 Lưu Ghi Chú",
                Location = new Point(100, 190),
                Width = 130,
                Height = 35,
                BackColor = Color.FromArgb(56, 161, 105),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            Button btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(240, 190),
                Width = 80,
                Height = 35,
                BackColor = Color.FromArgb(160, 174, 192),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, ev) => dialog.Close();

            btnSave.Click += (s, ev) =>
            {
                try
                {
                    string status = cboStatus.Text.Split('-')[0].Trim();
                    DatabaseHelper.AddMonitoringLog(surgeryID, currentNurseID, status, txtNote.Text);
                    MessageBox.Show("Đã thêm ghi chú thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            dialog.Controls.AddRange(new Control[] { lblHeader, lblSubHeader, lblStatus, cboStatus, lblNote, txtNote, btnSave, btnCancel });
            dialog.ShowDialog(this);
        }

        #endregion

        #region Event Handlers

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e) { }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            // Nursing Worklist button
            ShowWorklistPanel();
            HighlightActiveButton(btnNursingWorklist);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CompleteSelectedTasks();
        }

        private void btnInpationManagement_Click(object sender, EventArgs e)
        {
            // Switch to Inpatients panel (no new form!)
            ShowInpatientsPanel();
            HighlightActiveButton(btnInpationManagement);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Change password feature coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        #endregion
    }
}

