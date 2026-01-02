using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MANAGER
{
    public partial class frmManagerLeavingApproval : Form
    {
        private readonly ManagerMainForm _main;

        private readonly string connectionString =
           "Data Source=CHIENCOHUYENTHO\\PVINH;" +
           "Initial Catalog=PetCareX_DB;" +
           "Integrated Security=True;" +
           "Encrypt=True;" +
           "TrustServerCertificate=True;";

        public frmManagerLeavingApproval(ManagerMainForm main)
        {
            InitializeComponent();
            _main = main;
            cboStatus.DrawMode = DrawMode.Normal;                 // QUAN TRỌNG
            cboStatus.DropDownStyle = ComboBoxStyle.DropDownList; // optional
            cboStatus.FormattingEnabled = true;                   // optional

            cboStatus.Items.Clear();
            cboStatus.Items.Add("Pending");
            cboStatus.Items.Add("Approved");
            cboStatus.Items.Add("Rejected");

        }

        // (Optional) nếu bạn lỡ tạo từ Designer/Code cũ, vẫn để lại
        // nhưng KHÔNG nên dùng constructor này để điều hướng trong panelMain

        public frmManagerLeavingApproval() : this(null)
        {
        }
        private void LoadLeaveRequests()
        {
            // 1) Lấy dữ liệu filter từ UI
            DateTime? requestDate = null;
            string status = null;

            // Nếu DateTimePicker có ShowCheckBox = true
            if (dtpRequestDate.Checked)
                requestDate = dtpRequestDate.Value.Date;

            if (cboStatus.SelectedItem != null)
            {
                string s = cboStatus.SelectedItem.ToString();
                if (!string.Equals(s, "All", StringComparison.OrdinalIgnoreCase))
                    status = s;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_GetLeaveRequests", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RequestDate", SqlDbType.Date).Value =
                        requestDate.HasValue ? (object)requestDate.Value : DBNull.Value;

                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value =
                        string.IsNullOrEmpty(status) ? (object)DBNull.Value : status;

                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dgvLeaveRequests.AutoGenerateColumns = true; // nếu bạn chưa tự tạo cột
                    dgvLeaveRequests.DataSource = dt;
                    int selectColIndex = -1;

                    // tìm cột có header "Select"
                    foreach (DataGridViewColumn col in dgvLeaveRequests.Columns)
                    {
                        if (string.Equals(col.HeaderText, "Select", StringComparison.OrdinalIgnoreCase))
                        {
                            selectColIndex = col.Index;
                            break;
                        }
                    }

                    if (selectColIndex >= 0)
                    {
                        dgvLeaveRequests.Columns[selectColIndex].DisplayIndex = dgvLeaveRequests.Columns.Count - 1;
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load leave requests: " + ex.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadLeaveRequests();
        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e) { }
        private void lblRole_Click(object sender, EventArgs e) { }
        private void frmManagerLeavingApproval_Load(object sender, EventArgs e)
        {
            LoadLeaveRequests();
        }

        private void btnPromotionManagement_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new frmManagerLeavingApproval(_main))");
                return;
            }
            _main.LoadForm<ManagerPromotionManagement>();
        }

        private void btnWorkSchedule_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new frmManagerLeavingApproval(_main))");
                return;
            }
            _main.LoadForm<frmManagerWorkSchedule>(); // nếu frmManagerWorkSchedule chưa có ctor(main) thì xem note dưới
        }

        private void btnStaffManagement_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new frmManagerLeavingApproval(_main))");
                return;
            }
            _main.LoadForm<ManagerStaffManagement>(); // nếu ManagerStaffManagement chưa có ctor(main) thì xem note dưới
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvLeaveRequests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnsStatistical_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm.");
                return;
            }

            _main.LoadForm<ManagerStatistic>();

        }

        private void button3_Click(object sender, EventArgs e) // Approve button
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm.");
                return;
            }

            DataGridViewRow selectedRow = null;

            foreach (DataGridViewRow row in dgvLeaveRequests.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Select"].Value != null &&
                    Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng tick chọn 1 đơn.");
                return;
            }

            if (selectedRow.Cells["Status"].Value.ToString() != "Pending")
            {
                MessageBox.Show("Chỉ được Approve đơn đang Pending.");
                return;
            }

            string employeeId = selectedRow.Cells["EmployeeID"].Value.ToString();
            DateTime startDate = Convert.ToDateTime(selectedRow.Cells["StartDate"].Value);
            DateTime endDate = Convert.ToDateTime(selectedRow.Cells["EndDate"].Value);
            string mid = _main.mid;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateLeaveRequestStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Status", "Approved");
                cmd.Parameters.AddWithValue("@MID", mid);

                var pResult = new SqlParameter("@result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pResult);

                conn.Open();
                cmd.ExecuteNonQuery();

                int result = (int)pResult.Value;

                if (result == 1)
                {
                    MessageBox.Show("Approve thành công!");
                    LoadLeaveRequests();
                }
                else
                {
                    MessageBox.Show("Approve thất bại. Mã lỗi: " + result);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) // Reject button
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm.");
                return;
            }

            DataGridViewRow selectedRow = null;

            foreach (DataGridViewRow row in dgvLeaveRequests.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Select"].Value != null &&
                    Convert.ToBoolean(row.Cells["Select"].Value))
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow == null)
            {
                MessageBox.Show("Vui lòng tick chọn 1 đơn.");
                return;
            }

            if (selectedRow.Cells["Status"].Value.ToString() != "Pending")
            {
                MessageBox.Show("Chỉ được Reject đơn đang Pending.");
                return;
            }

            string employeeId = selectedRow.Cells["EmployeeID"].Value.ToString();
            DateTime startDate = Convert.ToDateTime(selectedRow.Cells["StartDate"].Value);
            DateTime endDate = Convert.ToDateTime(selectedRow.Cells["EndDate"].Value);
            string mid = _main.mid;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateLeaveRequestStatus", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", employeeId);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
                cmd.Parameters.AddWithValue("@Status", "Rejected");
                cmd.Parameters.AddWithValue("@MID", mid);

                var pResult = new SqlParameter("@result", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(pResult);

                conn.Open();
                cmd.ExecuteNonQuery();

                int result = (int)pResult.Value;

                if (result == 1)
                {
                    MessageBox.Show("Reject thành công!");
                    LoadLeaveRequests();
                }
                else
                {
                    MessageBox.Show("Reject thất bại. Mã lỗi: " + result);
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
