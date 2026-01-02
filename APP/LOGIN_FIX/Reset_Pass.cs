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

namespace LOGIN_FIX
{
    public partial class Reset_Pass : Form
    {
        public event Action ResetSuccess;
        public event Action BackToForgot; // nếu bạn có nút Back về Forgot

        private readonly string _username;

        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        public Reset_Pass(string username)
        {
            InitializeComponent();
            _username = username;
            txtNewPass.UseSystemPasswordChar = true;
            txtConfirm.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newPass = txtNewPass.Text;
            string confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(newPass) || newPass.Length < 6)
            {
                MessageBox.Show("Mật khẩu tối thiểu 6 ký tự.");
                return;
            }
            if (newPass != confirm)
            {
                MessageBox.Show("Confirm không khớp.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_ResetPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20).Value = _username;
                    cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 255).Value = newPass;

                    SqlParameter pRes = new SqlParameter("@result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pRes);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    int result = Convert.ToInt32(pRes.Value);

                    if (result != 1)
                    {
                        MessageBox.Show("Reset thất bại.");
                        return;
                    }

                    MessageBox.Show("Reset thành công.");
                    ResetSuccess?.Invoke();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi reset password: " + ex.Message);
            }
        }
    }
}