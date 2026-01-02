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
    public partial class ManagerPromotionManagement : Form
    {
        private readonly ManagerMainForm _main;

        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        public ManagerPromotionManagement(ManagerMainForm main)
        {
            InitializeComponent();
            _main = main;
        }

        public ManagerPromotionManagement() : this(null) { }

        private void ManagerPromotionManagement_Load(object sender, EventArgs e)
        {
            LoadDiscounts();
        }

        // ================== SEARCH ==================
        private void button5_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string name = txtName.Text.Trim();
            bool useDate = dtpSD.Checked || dtpDD.Checked;

            if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(name) && !useDate)
            {
                MessageBox.Show("Vui lòng nhập ID, Name hoặc tick chọn khoảng ngày.");
                return;
            }

            if (useDate && dtpSD.Checked && dtpDD.Checked)
            {
                if (dtpSD.Value.Date > dtpDD.Value.Date)
                {
                    MessageBox.Show("Start date phải <= Due date.");
                    return;
                }
            }

            LoadDiscounts();
        }

        // ================== LOAD / CALL PROC ==================
        private void LoadDiscounts()
        {
            object pId = string.IsNullOrWhiteSpace(txtID.Text) ? (object)DBNull.Value : txtID.Text.Trim();
            object pName = string.IsNullOrWhiteSpace(txtName.Text) ? (object)DBNull.Value : txtName.Text.Trim();
            object pStartDate = dtpSD.Checked ? (object)dtpSD.Value.Date : DBNull.Value;
            object pEndDate = dtpDD.Checked ? (object)dtpDD.Value.Date : DBNull.Value;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_SearchDiscounts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DiscountID", SqlDbType.VarChar, 20).Value = pId;
                    cmd.Parameters.Add("@DiscountName", SqlDbType.NVarChar, 100).Value = pName;
                    cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = pStartDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = pEndDate;

                    conn.Open();

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    dgvPromotion.AutoGenerateColumns = true;
                    dgvPromotion.DataSource = dt;
                    if (dgvPromotion.Columns.Contains("Status"))
                        dgvPromotion.Columns["Status"].Visible = false;
                    int selectColIndex = -1;

                    // tìm cột có header "Select"
                    foreach (DataGridViewColumn col in dgvPromotion.Columns)
                    {
                        if (string.Equals(col.HeaderText, "Select", StringComparison.OrdinalIgnoreCase))
                        {
                            selectColIndex = col.Index;
                            break;
                        }
                    }

                    if (selectColIndex >= 0)
                    {
                        dgvPromotion.Columns[selectColIndex].DisplayIndex = dgvPromotion.Columns.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load discounts: " + ex.Message);
            }
        }

        // ================== CLEAR FILTER (GIỮ NGUYÊN) ==================
        private void ClearFilters()
        {
            txtID.Clear();
            txtName.Clear();
            dtpSD.Checked = false;
            dtpDD.Checked = false;
        }
      

        // ================== NAV (GIỮ NGUYÊN) ==================
        private void btnWorkSchedule_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("_main = null (Form này đang bị mở sai cách)");
                return;
            }
            _main.LoadForm<frmManagerWorkSchedule>();
        }

        private void btnLeavingApproval_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("_main = null (Form này đang bị mở sai cách)");
                return;
            }
            _main.LoadForm<frmManagerLeavingApproval>();
        }

        private void btnStaffManagement_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("_main = null (Form này đang bị mở sai cách)");
                return;
            }
            _main.LoadForm<ManagerStaffManagement>();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e) { }

        private void button6_Click(object sender, EventArgs e)
        {
            if (_main == null)
            {
                MessageBox.Show("Không tìm thấy ManagerMainForm.");
                return;
            }
            _main.LoadForm<ManagerStatistic>();
        }

        // =====================================================
        // ================== PHẦN THÊM MỚI ====================
        // =====================================================

        // ✅ LẤY MID TỪ LABEL (đổi tên label cho đúng form bạn)

        // ================== ADD PROMOTION ==================
        private void button1_Click(object sender, EventArgs e)
        {
            string mid = _main.mid;
            if (string.IsNullOrWhiteSpace(mid))
            {
                MessageBox.Show("Không lấy được MID.");
                return;
            }

            using (FrmDiscountAdd f = new FrmDiscountAdd())
            {
                if (f.ShowDialog() != DialogResult.OK) return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_AddDiscount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DiscountName", SqlDbType.NVarChar, 100).Value = f.DiscountName;
                    cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = f.StartDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = f.EndDate;
                    cmd.Parameters.Add("@TargetUser", SqlDbType.NVarChar, 50).Value = f.TargetUser;
                    cmd.Parameters.Add("@Percentage", SqlDbType.Float).Value = f.Percentage;
                    cmd.Parameters.Add("@MID", SqlDbType.VarChar, 20).Value = mid;

                    SqlParameter pRes = new SqlParameter("@result", SqlDbType.Int);
                    pRes.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pRes);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int result = (int)pRes.Value;
                    if (result == 1)
                    {
                        MessageBox.Show("Thêm discount thành công.");
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại. Code: " + result);
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string mid = _main.mid;
            if (string.IsNullOrWhiteSpace(mid))
            {
                MessageBox.Show("Không lấy được MID.");
                return;
            }

            // 1) lấy danh sách dòng được tick
            var selectedRows = dgvPromotion.Rows
                .Cast<DataGridViewRow>()
                .Where(r => !r.IsNewRow
                    && r.Cells["Select"].Value != null
                    && Convert.ToBoolean(r.Cells["Select"].Value) == true)
                .ToList();

            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng tick Select ít nhất 1 dòng.");
                return;
            }

            // 2) validate trước (tránh update nửa chừng)
            foreach (var row in selectedRows)
            {
                string discountId = row.Cells["DiscountID"].Value?.ToString();
                string discountName = row.Cells["DiscountName"].Value?.ToString();
                string targetUser = row.Cells["TargetUser"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(discountName))
                {
                    MessageBox.Show($"Discount {discountId}: DiscountName không được rỗng.");
                    return;
                }

                DateTime startDate = Convert.ToDateTime(row.Cells["StartDate"].Value);
                DateTime endDate = Convert.ToDateTime(row.Cells["EndDate"].Value);
                if (endDate < startDate)
                {
                    MessageBox.Show($"Discount {discountId}: EndDate phải >= StartDate.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(targetUser))
                {
                    MessageBox.Show($"Discount {discountId}: TargetUser không được rỗng.");
                    return;
                }
            }

            // 3) update theo transaction
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        foreach (var row in selectedRows)
                        {
                            string discountId = row.Cells["DiscountID"].Value.ToString();
                            string discountName = row.Cells["DiscountName"].Value.ToString();
                            DateTime startDate = Convert.ToDateTime(row.Cells["StartDate"].Value);
                            DateTime endDate = Convert.ToDateTime(row.Cells["EndDate"].Value);
                            string targetUser = row.Cells["TargetUser"].Value.ToString();
                            double percentage = Convert.ToDouble(row.Cells["Percentage"].Value); // giữ nguyên

                            using (SqlCommand cmd = new SqlCommand("dbo.sp_UpdateDiscount", conn, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("@DiscountID", SqlDbType.VarChar, 20).Value = discountId;
                                cmd.Parameters.Add("@DiscountName", SqlDbType.NVarChar, 100).Value = discountName;
                                cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate;
                                cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate;
                                cmd.Parameters.Add("@TargetUser", SqlDbType.NVarChar, 50).Value = targetUser;
                                cmd.Parameters.Add("@Percentage", SqlDbType.Float).Value = percentage;
                                cmd.Parameters.Add("@MID", SqlDbType.VarChar, 20).Value = mid;

                                SqlParameter pRes = new SqlParameter("@result", SqlDbType.Int);
                                pRes.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(pRes);

                                cmd.ExecuteNonQuery();

                                int result = Convert.ToInt32(pRes.Value);
                                if (result != 1)
                                {
                                    tran.Rollback();
                                    MessageBox.Show($"Update thất bại: {discountId} (Code={result}). Đã rollback tất cả.");
                                    return;
                                }
                            }
                        }

                        tran.Commit();
                    }
                }

                MessageBox.Show($"Cập nhật thành công {selectedRows.Count} dòng.");
                LoadDiscounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Update nhiều dòng: " + ex.Message);
            }
        }

    }
}

