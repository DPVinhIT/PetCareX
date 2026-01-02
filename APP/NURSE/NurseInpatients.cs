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
    public partial class frmNurseInpatients : Form
    {
        private string currentNurseID = "E0121"; // Default fallback

        /// <summary>
        /// Constructor with nurseID from login
        /// </summary>
        public frmNurseInpatients(string nurseID) : this()
        {
            currentNurseID = nurseID;
        }

        public frmNurseInpatients()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void lblRole_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmNurse_Load(object sender, EventArgs e)
        {
            // Hide unused filter panel
            groupBox1.Visible = false;
            
            // Load inpatients data
            LoadInpatients();
            
            // Setup double-click on grid to show monitoring
            dgv.CellDoubleClick += Dgv_CellDoubleClick;
            
            // Add Monitoring button panel below grid
            AddMonitoringButtonPanel();
        }

        /// <summary>
        /// Thêm panel chứa nút Monitoring ở dưới grid
        /// </summary>
        private void AddMonitoringButtonPanel()
        {
            Panel btnPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = System.Drawing.Color.FromArgb(247, 250, 252)
            };

            Label lblHint = new Label
            {
                Text = "💡 Chọn 1 dòng và bấm nút bên dưới, hoặc Double-click để xem chi tiết",
                AutoSize = true,
                Location = new System.Drawing.Point(10, 5),
                ForeColor = System.Drawing.Color.Gray
            };

            Button btnViewMonitoring = new Button
            {
                Text = "📋 Xem Lịch Sử Monitoring",
                Location = new System.Drawing.Point(10, 25),
                Width = 180,
                Height = 28,
                BackColor = System.Drawing.Color.FromArgb(66, 153, 225),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnViewMonitoring.Click += BtnViewMonitoring_Click;

            Button btnAddLog = new Button
            {
                Text = "➕ Thêm Ghi Chú Mới",
                Location = new System.Drawing.Point(200, 25),
                Width = 160,
                Height = 28,
                BackColor = System.Drawing.Color.FromArgb(56, 161, 105),
                ForeColor = System.Drawing.Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnAddLog.Click += BtnAddLog_Click;

            btnPanel.Controls.Add(lblHint);
            btnPanel.Controls.Add(btnViewMonitoring);
            btnPanel.Controls.Add(btnAddLog);
            
            // Add to the panel containing dgv
            splitContainer2.Panel2.Controls.Add(btnPanel);
        }

        private void BtnViewMonitoring_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0 || dgv.CurrentRow != null)
            {
                var row = dgv.CurrentRow;
                if (row != null && row.Cells["Mã Pet"] != null && row.Cells["Mã Pet"].Value != null)
                {
                    string petID = row.Cells["Mã Pet"].Value.ToString();
                    ShowMonitoringHistory(petID);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bệnh nhân từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnAddLog_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0 || dgv.CurrentRow != null)
            {
                var row = dgv.CurrentRow;
                if (row != null && row.Cells["Mã Pet"] != null && row.Cells["Mã Pet"].Value != null)
                {
                    string petID = row.Cells["Mã Pet"].Value.ToString();
                    string surgeryID = row.Cells["Mã PT"] != null && row.Cells["Mã PT"].Value != null 
                        ? row.Cells["Mã PT"].Value.ToString() 
                        : "";
                    string petName = row.Cells["Tên Thú Cưng"] != null && row.Cells["Tên Thú Cưng"].Value != null 
                        ? row.Cells["Tên Thú Cưng"].Value.ToString() 
                        : "";
                    ShowAddMonitoringLogDialog(petID, surgeryID, petName);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bệnh nhân!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một bệnh nhân từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Load danh sách bệnh nhân đang theo dõi
        /// </summary>
        private void LoadInpatients()
        {
            try
            {
                DataTable inpatients = DatabaseHelper.GetInpatientsList();
                dgv.DataSource = inpatients;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách bệnh nhân: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get PetID from selected row (column "Mã Pet")
                string petID = "";
                if (dgv.Rows[e.RowIndex].Cells["Mã Pet"] != null && 
                    dgv.Rows[e.RowIndex].Cells["Mã Pet"].Value != null)
                {
                    petID = dgv.Rows[e.RowIndex].Cells["Mã Pet"].Value.ToString();
                }
                
                if (!string.IsNullOrEmpty(petID))
                {
                    ShowMonitoringHistory(petID);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy Pet ID!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// YT1 - Hiển thị lịch sử theo dõi sau phẫu thuật
        /// </summary>
        private void ShowMonitoringHistory(string petID)
        {
            try
            {
                // Get Surgery ID from main grid's current row
                string surgeryID = "";
                string petName = "";
                if (dgv.CurrentRow != null)
                {
                    if (dgv.CurrentRow.Cells["Mã PT"] != null && dgv.CurrentRow.Cells["Mã PT"].Value != null)
                        surgeryID = dgv.CurrentRow.Cells["Mã PT"].Value.ToString();
                    if (dgv.CurrentRow.Cells["Tên Thú Cưng"] != null && dgv.CurrentRow.Cells["Tên Thú Cưng"].Value != null)
                        petName = dgv.CurrentRow.Cells["Tên Thú Cưng"].Value.ToString();
                }

                DataTable history = DatabaseHelper.GetHistoryPostSurgeryMonitoring(petID);

                // Create popup form
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
                // Pass all info to dialog
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

        /// <summary>
        /// YT2 - Dialog thêm monitoring log (với auto-fill từ row được chọn)
        /// </summary>
        private void ShowAddMonitoringLogDialog(string petID, string surgeryID = "", string petName = "")
        {
            // Validate surgery ID
            if (string.IsNullOrEmpty(surgeryID))
            {
                MessageBox.Show("Không tìm thấy mã phẫu thuật! Vui lòng chọn bệnh nhân từ danh sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Header với thông tin bệnh nhân (hiện rõ để y tá biết đang thêm cho ai)
            Label lblHeader = new Label 
            { 
                Text = $"🐾 Bệnh nhân: {petName}", 
                Location = new Point(20, 15), 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 55, 72)
            };

            Label lblSubHeader = new Label 
            { 
                Text = $"Pet ID: {petID} | Mã PT: {surgeryID}", 
                Location = new Point(20, 40), 
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Regular),
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
                    string nurseID = currentNurseID; // From login
                    // Extract status value (remove Vietnamese description)
                    string status = cboStatus.Text.Split('-')[0].Trim();
                    DatabaseHelper.AddMonitoringLog(surgeryID, nurseID, status, txtNote.Text);
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
     
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtFilter_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFilter_DateModified_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void pnlDashBoard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboFilter_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboFilter_Status_RegionChanged(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTreatmentList_Click(object sender, EventArgs e)
        {
            // Open Nursing Worklist form
            frmNurseNursingWorklist worklistForm = new frmNurseNursingWorklist();
            worklistForm.Show();
            this.Hide();
        }

        private void btnInpationManagement_Click(object sender, EventArgs e)
        {
            // Already on Inpatients form
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Profile feature coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
