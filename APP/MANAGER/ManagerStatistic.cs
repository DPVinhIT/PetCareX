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
    public partial class ManagerStatistic : Form
    {
        private readonly ManagerMainForm _main;

        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        // Dùng khi NHÚNG vào ManagerMainForm
        public ManagerStatistic(ManagerMainForm main)
        {
            InitializeComponent();
            _main = main;

            InitRevenueBy();       // ✅ không phụ thuộc Designer Load event
            // (tuỳ chọn) set date mặc định
            // dtpFrom.Value = DateTime.Today.AddMonths(-1);
            // dtpTo.Value = DateTime.Today;
        }

        // Optional: nếu lỡ tạo từ designer
        public ManagerStatistic() : this(null) { }

        private void InitRevenueBy()
        {
            cboRevenueBy.DrawMode = DrawMode.Normal;
            cboRevenueBy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRevenueBy.FormattingEnabled = true;

            cboRevenueBy.Items.Clear();
            cboRevenueBy.Items.Add("Branch");   // sp_Stat_RevenueByBranch
            cboRevenueBy.Items.Add("Doctor");   // sp_Stat_RevenueByDoctor
            cboRevenueBy.Items.Add("Service Volume");      // sp_Stat_ServiceVolume
            cboRevenueBy.Items.Add("Product Revenue");     // sp_Stat_ProductSalesRevenue
            cboRevenueBy.Items.Add("Total Revenue");       // sp_Stat_TotalRevenue

            cboRevenueBy.SelectedIndex = 0;
        }

        // =========================
        // NAVIGATION
        // =========================
        private void btnPromotionManagement_Click(object sender, EventArgs e)
        {
            if (_main == null) { MessageBox.Show("Không tìm thấy ManagerMainForm."); return; }
            _main.LoadForm<ManagerPromotionManagement>();
        }

        private void btnWorkSchedule_Click(object sender, EventArgs e)
        {
            if (_main == null) { MessageBox.Show("Không tìm thấy ManagerMainForm."); return; }
            _main.LoadForm<frmManagerWorkSchedule>();
        }

        private void btnLeavingApproval_Click(object sender, EventArgs e)
        {
            if (_main == null) { MessageBox.Show("Không tìm thấy ManagerMainForm."); return; }
            _main.LoadForm<frmManagerLeavingApproval>();
        }

        private void btnStaffManagement_Click(object sender, EventArgs e)
        {
            if (_main == null) { MessageBox.Show("Không tìm thấy ManagerMainFormsmainForm."); return; }
            _main.LoadForm<ManagerStaffManagement>();
        }

        // =========================
        // FILTER
        // =========================
        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cboRevenueBy.SelectedItem == null)
            {
                MessageBox.Show("Chọn Revenue by trước.");
                return;
            }

            DateTime fromDate = dtpFrom.Value.Date;
            DateTime toDate = dtpTo.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show("From date must be <= To date");
                return;
            }

            string selected = cboRevenueBy.SelectedItem.ToString();
            string procName;

            switch (selected)
            {
                case "Branch":
                    procName = "sp_Stat_RevenueByBranch";
                    break;

                case "Doctor":
                    procName = "sp_Stat_RevenueByDoctor";
                    break;

                case "Service Volume":
                    procName = "sp_Stat_ServiceVolume";
                    break;

                case "Product Revenue":
                    procName = "sp_Stat_ProductSalesRevenue";
                    break;

                case "Total Revenue":
                    procName = "sp_Stat_TotalRevenue";
                    break;

                default:
                    MessageBox.Show("Revenue by không hợp lệ.");
                    return;
            }

            LoadData(procName, fromDate, toDate);
        }

        private void LoadData(string procName, DateTime from, DateTime to)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo." + procName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = from;
                    cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = to;

                    conn.Open(); // ✅ QUAN TRỌNG (thiếu là không chạy giống bạn bị)

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dgvStatistic.AutoGenerateColumns = true;
                    dgvStatistic.DataSource = dt;

                    // debug nhanh (nếu muốn xem có ra rows không)
                    // MessageBox.Show("Rows: " + dt.Rows.Count);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error " + ex.Number + ":\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load statistic error: " + ex.Message);
            }
        }
    }
}