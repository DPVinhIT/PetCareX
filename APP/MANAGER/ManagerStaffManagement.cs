using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace MANAGER
{
    public partial class ManagerStaffManagement : Form
    {
        private readonly ManagerMainForm _main;

        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        public ManagerStaffManagement(ManagerMainForm main)
        {
            InitializeComponent();
            _main = main;
            InitGender();
        }

        public ManagerStaffManagement() : this(null) { }

        private void InitGender()
        {
            cboGender.DrawMode = DrawMode.Normal;
            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.FormattingEnabled = true;

            cboGender.Items.Clear();
            cboGender.Items.Add("None");
            cboGender.Items.Add("Nam");
            cboGender.Items.Add("Nữ");
            cboGender.SelectedIndex = 0;
        }

        private void cboFilter_Status_SelectedIndexChanged(object sender, EventArgs e) { }
        private void toolStripButton3_Click(object sender, EventArgs e) { }

        private void btnPromotionManagement_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new ManagerStaffManagement(_main))");
                return;
            }
            _main.LoadForm<ManagerPromotionManagement>();
        }

        private void btnWorkSchedule_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new ManagerStaffManagement(_main))");
                return;
            }
            _main.LoadForm<frmManagerWorkSchedule>();
        }

        private void btnLeavingApproval_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new ManagerStaffManagement(_main))");
                return;
            }
            _main.LoadForm<frmManagerLeavingApproval>();
        }

        private void btnDropDownProfile_Click(object sender, EventArgs e) { }

        private void pnlDashBoard_Paint(object sender, PaintEventArgs e) { }
        private void richTextBox3_TextChanged(object sender, EventArgs e) { }

        private void btnsStatistical_Click_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm.");
                return;
            }
            _main.LoadForm<ManagerStatistic>();
        }

        // =========================
        // ✅ FIX: LoadEmployees đúng proc sp_SearchEmployees(@EmployeeID,@Name,@Gender)
        // =========================
        private void LoadEmployees()
        {
            // ⚠️ đổi đúng tên textbox của bạn nếu khác
            string employeeId = txtEmployeeID.Text.Trim();

            // ⚠️ giả sử textbox tên là txtName (nếu khác thì đổi lại)
            string name = txtName.Text.Trim();

            string gender = cboGender.SelectedItem == null ? "None" : cboGender.SelectedItem.ToString();

            object pEmployeeId = string.IsNullOrWhiteSpace(employeeId) ? (object)DBNull.Value : employeeId;
            object pName = string.IsNullOrWhiteSpace(name) ? (object)DBNull.Value : name;

            object pGender;
            if (string.IsNullOrWhiteSpace(gender) || gender.Equals("None", StringComparison.OrdinalIgnoreCase))
                pGender = DBNull.Value;
            else
                pGender = gender; // "Nam" hoặc "Nữ"

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_SearchEmployees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 20).Value = pEmployeeId;
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = pName;
                    cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = pGender;

                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dgvEmployees.AutoGenerateColumns = true;
                    dgvEmployees.DataSource = dt;

                    // nếu có cột Select thì dồn về cuối (giữ y chang style bạn làm)
                    int selectColIndex = -1;
                    foreach (DataGridViewColumn col in dgvEmployees.Columns)
                    {
                        if (string.Equals(col.HeaderText, "Select", StringComparison.OrdinalIgnoreCase))
                        {
                            selectColIndex = col.Index;
                            break;
                        }
                    }
                    if (selectColIndex >= 0)
                        dgvEmployees.Columns[selectColIndex].DisplayIndex = dgvEmployees.Columns.Count - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi search employee: " + ex.Message);
            }
        }

        // ✅ KHÔNG BỎ HÀM NÀY
        private void btnFilter_Click(object sender, EventArgs e) // Search Button
        {
            LoadEmployees();
        }

        private void button2_Click(object sender, EventArgs e) // update button
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm.");
                return;
            }

            string mid = _main.mid; // manager đang login
            if (string.IsNullOrWhiteSpace(mid))
            {
                MessageBox.Show("Không lấy được MID.");
                return;
            }

            // lấy các dòng được tick Select
            var selectedRows = dgvEmployees.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow
                    && r.Cells["Select"].Value != null
                    && Convert.ToBoolean(r.Cells["Select"].Value) == true)
                .ToList();

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng tick Select ít nhất 1 nhân viên.");
                return;
            }

            // validate trước (tránh update nửa chừng)
            foreach (var row in selectedRows)
            {
                string empId = row.Cells["EmployeeID"].Value?.ToString();
                string fullName = row.Cells["FullName"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(empId))
                {
                    MessageBox.Show("Có dòng bị thiếu EmployeeID.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    MessageBox.Show($"Employee {empId}: FullName không được để trống.");
                    return;
                }

                DateTime? birthday = TryGetDate(row, "Birthday");
                DateTime? startDate = TryGetDate(row, "StartDate");

                if (birthday.HasValue && birthday.Value.Date >= DateTime.Today)
                {
                    MessageBox.Show($"Employee {empId}: Birthday phải < hôm nay.");
                    return;
                }

                if (startDate.HasValue && startDate.Value.Date > DateTime.Today)
                {
                    MessageBox.Show($"Employee {empId}: StartDate không được trong tương lai.");
                    return;
                }

                if (birthday.HasValue && startDate.HasValue && startDate.Value.Date <= birthday.Value.Date)
                {
                    MessageBox.Show($"Employee {empId}: StartDate phải sau Birthday.");
                    return;
                }

                string gender = row.Cells["Gender"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(gender) &&
                    gender != "Nam" && gender != "Nữ" && gender != "Khác")
                {
                    MessageBox.Show($"Employee {empId}: Gender không hợp lệ (Nam/Nữ/Khác).");
                    return;
                }

                decimal salary = TryGetDecimal(row, "BaseSalary");
                if (salary < 0)
                {
                    MessageBox.Show($"Employee {empId}: BaseSalary không được âm.");
                    return;
                }

                string role = row.Cells["Role"].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(role))
                {
                    string[] allowed = { "Manager", "Doctor", "Nurse", "Receptionist", "Cashier", "SalePerson" };
                    if (!allowed.Contains(role))
                    {
                        MessageBox.Show($"Employee {empId}: Role không hợp lệ.");
                        return;
                    }
                }
            }

            // update theo transaction
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        foreach (var row in selectedRows)
                        {
                            string empId = row.Cells["EmployeeID"].Value.ToString();
                            string fullName = row.Cells["FullName"].Value.ToString();

                            DateTime? birthday = TryGetDate(row, "Birthday");
                            string gender = row.Cells["Gender"].Value?.ToString();
                            string phone = row.Cells["PhoneNumber"].Value?.ToString();
                            DateTime? startDate = TryGetDate(row, "StartDate");
                            decimal baseSalary = TryGetDecimal(row, "BaseSalary");
                            string role = row.Cells["Role"].Value?.ToString();

                            using (SqlCommand cmd = new SqlCommand("dbo.sp_UpdateEmployeeById", conn, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 20).Value = empId;
                                cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = fullName;

                                cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value =
                                    birthday.HasValue ? (object)birthday.Value.Date : DBNull.Value;

                                cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value =
                                    string.IsNullOrWhiteSpace(gender) ? (object)DBNull.Value : gender;

                                cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 15).Value =
                                    string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone;

                                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value =
                                    startDate.HasValue ? (object)startDate.Value.Date : DBNull.Value;

                                // decimal param nên set Precision/Scale cho chắc
                                var pSalary = cmd.Parameters.Add("@BaseSalary", SqlDbType.Decimal);
                                pSalary.Precision = 18;
                                pSalary.Scale = 2;
                                pSalary.Value = baseSalary;

                                cmd.Parameters.Add("@Role", SqlDbType.NVarChar, 50).Value =
                                    string.IsNullOrWhiteSpace(role) ? (object)DBNull.Value : role;

                                // MID lấy từ manager login
                                cmd.Parameters.Add("@MID", SqlDbType.VarChar, 20).Value = mid;

                                SqlParameter pRes = new SqlParameter("@result", SqlDbType.Int);
                                pRes.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(pRes);

                                cmd.ExecuteNonQuery();

                                int result = Convert.ToInt32(pRes.Value);
                                if (result != 1)
                                {
                                    tran.Rollback();
                                    MessageBox.Show($"Update thất bại: {empId} (Code={result}). Đã rollback tất cả.");
                                    return;
                                }
                            }
                        }

                        tran.Commit();
                    }
                }

                MessageBox.Show($"Cập nhật thành công {selectedRows.Count} nhân viên.");
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Update employee: " + ex.Message);
            }
        }

        // ===== helper (THÊM vào class nếu bạn chưa có) =====
        private DateTime? TryGetDate(DataGridViewRow row, string colName)
        {
            try
            {
                if (!row.DataGridView.Columns.Contains(colName)) return null;
                var v = row.Cells[colName].Value;
                if (v == null || v == DBNull.Value) return null;
                return Convert.ToDateTime(v).Date;
            }
            catch { return null; }
        }

        private decimal TryGetDecimal(DataGridViewRow row, string colName)
        {
            try
            {
                if (!row.DataGridView.Columns.Contains(colName)) return 0m;
                var v = row.Cells[colName].Value;
                if (v == null || v == DBNull.Value) return 0m;
                return Convert.ToDecimal(v);
            }
            catch { return 0m; }
        }

    }
}
