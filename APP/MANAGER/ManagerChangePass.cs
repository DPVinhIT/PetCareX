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
    public partial class ManagerChangePass : Form
    {
        private readonly ManagerMainForm _main;

        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        public ManagerChangePass(ManagerMainForm main)
        {
            InitializeComponent();
            _main = main;

            // (tuỳ chọn) ẩn ký tự password
            txtOldPassword.UseSystemPasswordChar = true;
            txtNewPassword.UseSystemPasswordChar = true;
            txtConfirmPassword.UseSystemPasswordChar = true;
        }

        public ManagerChangePass() : this(null) { }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e) // OK button
        {
            if (_main == null)
            {
                MessageBox.Show("Không lấy được thông tin tài khoản (main = null).");
                return;
            }

            // ✅ lấy username từ main
            string username = _main.Username;

            // ✅ OldPassword đúng nghĩa (không phải phone nữa)
            string oldPass = txtOldPassword.Text.Trim();
            string newPass = txtNewPassword.Text;
            string confirm = txtConfirmPassword.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username đang rỗng.");
                return;
            }

            if (string.IsNullOrWhiteSpace(oldPass) ||
                string.IsNullOrWhiteSpace(newPass) ||
                string.IsNullOrWhiteSpace(confirm))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_ChangePasswordE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20).Value = username;
                    cmd.Parameters.Add("@OldPassword", SqlDbType.NVarChar, 255).Value = oldPass;
                    cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 255).Value = newPass;
                    cmd.Parameters.Add("@ConfirmPassword", SqlDbType.NVarChar, 255).Value = confirm;

                    var pResult = new SqlParameter("@result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pResult);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int result = (pResult.Value == DBNull.Value) ? -1 : Convert.ToInt32(pResult.Value);

                    switch (result)
                    {
                        case 1:
                            MessageBox.Show("Đổi mật khẩu thành công!");
                            txtOldPassword.Clear();
                            txtNewPassword.Clear();
                            txtConfirmPassword.Clear();
                            break;

                        case 0:
                            MessageBox.Show("Old Password không đúng.");
                            break;

                        case -4:
                            MessageBox.Show("New Password và Confirm Password không khớp.");
                            break;

                        case -3:
                            MessageBox.Show("Username không tồn tại.");
                            break;

                        case -2:
                            MessageBox.Show("Thiếu dữ liệu.");
                            break;

                        default:
                            MessageBox.Show("Lỗi hệ thống.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message);
            }
        }
    }
}