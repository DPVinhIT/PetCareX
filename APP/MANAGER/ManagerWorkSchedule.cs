using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANAGER
{
    public partial class frmManagerWorkSchedule : Form
    {
        private readonly ManagerMainForm _main;

        private readonly string connectionString =
           "Data Source=CHIENCOHUYENTHO\\PVINH;" +
           "Initial Catalog=PetCareX_DB;" +
           "Integrated Security=True;" +
           "Encrypt=True;" +
           "TrustServerCertificate=True;";

        // Constructor dùng khi nhúng trong ManagerMainForm
        public frmManagerWorkSchedule(ManagerMainForm main)
        {
            InitializeComponent();
            _main = main;
            InitShift();
        }

        // Optional: để không vỡ designer / code cũ
        public frmManagerWorkSchedule() : this(null)
        {
        }

        private void InitShift()
        {
            cboShift.DrawMode = DrawMode.Normal;
            cboShift.DropDownStyle = ComboBoxStyle.DropDownList;
            cboShift.FormattingEnabled = true;

            cboShift.Items.Clear();
            cboShift.Items.Add("Ca sáng");
            cboShift.Items.Add("Ca chiều");
            cboShift.Items.Add("Ca tối");
            cboShift.Items.Add("None");

            cboShift.SelectedIndex = 0;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void btnPromotionManagement_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new frmManagerWorkSchedule(_main))");
                return;
            }
            _main.LoadForm<ManagerPromotionManagement>();
        }

        private void btnLeavingApproval_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new frmManagerWorkSchedule(_main))");
                return;
            }
            _main.LoadForm<frmManagerLeavingApproval>();
        }

        private void btnStaffManagement_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm. Hãy mở form này bằng _main.LoadForm(new frmManagerWorkSchedule(_main))");
                return;
            }
            _main.LoadForm<ManagerStaffManagement>();
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

        // ✅ ADD: hàm gọi proc sp_GetWorkSchedule
        private void LoadWorkSchedule()
        {
            // StaffID (rỗng => NULL)
            object pEmployeeID = string.IsNullOrWhiteSpace(txtEmployeeID.Text)
                ? (object)DBNull.Value
                : txtEmployeeID.Text.Trim();

            // Workday: luôn lọc theo ngày đang chọn
            object pWorkDate = dtpWorkDate.Value.Date;

            // Shift: None => NULL (không lọc)
            object pShift =
                (cboShift.SelectedItem == null || cboShift.SelectedItem.ToString() == "None")
                    ? (object)DBNull.Value
                    : cboShift.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetWorkSchedule", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 20).Value = pEmployeeID;
                    cmd.Parameters.Add("@WorkDate", SqlDbType.Date).Value = pWorkDate;
                    cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 50).Value = pShift;

                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dgvWorkSchedule.AutoGenerateColumns = true;
                    dgvWorkSchedule.DataSource = dt;
                    int selectColIndex = -1;

                    // tìm cột có header "Select"
                    foreach (DataGridViewColumn col in dgvWorkSchedule.Columns)
                    {
                        if (string.Equals(col.HeaderText, "Select", StringComparison.OrdinalIgnoreCase))
                        {
                            selectColIndex = col.Index;
                            break;
                        }
                    }

                    if (selectColIndex >= 0)
                    {
                        dgvWorkSchedule.Columns[selectColIndex].DisplayIndex = dgvWorkSchedule.Columns.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lọc lịch làm việc: " + ex.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e) // Lọc lịch làm việc
        {
            LoadWorkSchedule();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            // MID lấy từ lblEmployeeID (bạn nói label này có trên main)
            string mid = _main.mid;   // ✅ nếu lblEmployeeID public
            if (string.IsNullOrWhiteSpace(mid))
            {
                MessageBox.Show("Không lấy được MID từ lblEmployeeID.");
                return;
            }

            using (FrmWorkScheduleAdd f = new FrmWorkScheduleAdd())
            {
                if (f.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = new SqlCommand("dbo.sp_AssignWorkSchedule", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 20).Value = f.EmployeeID;
                        cmd.Parameters.Add("@WorkDate", SqlDbType.Date).Value = f.WorkDate;
                        cmd.Parameters.Add("@WorkTime", SqlDbType.Int).Value = f.WorkTime;
                        cmd.Parameters.Add("@Shift", SqlDbType.NVarChar, 50).Value = f.Shift;
                        cmd.Parameters.Add("@MID", SqlDbType.VarChar, 20).Value = mid;

                        SqlParameter pRes = new SqlParameter("@result", SqlDbType.Int);
                        pRes.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(pRes);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        int result = Convert.ToInt32(pRes.Value);
                        if (result == 1)
                        {
                            MessageBox.Show("Thêm WorkSchedule thành công.");
                            LoadWorkSchedule();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại. Code=" + result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thêm lịch: " + ex.Message);
                }
            }
        }

    }
}