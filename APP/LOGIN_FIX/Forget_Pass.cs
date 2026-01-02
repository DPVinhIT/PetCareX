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
    public partial class Forget_Pass : Form
    {
        public event Action BackToSignin;
        public event Action<string> VerifiedOk; // truyền Username sang Reset

        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        public Forget_Pass()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BackToSignin?.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string phone = txtPhoneNumber.Text.Trim();
            string empId = txtEmployeeID.Text.Trim();
            string managerId = txtManagerID.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(empId) ||
                string.IsNullOrWhiteSpace(managerId))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Username, Phone, EmployeeID và ManagerID.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_VerifyForgotPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20).Value = username;
                    cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 15).Value = phone;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 20).Value = empId;
                    cmd.Parameters.Add("@ManagerID", SqlDbType.VarChar, 20).Value = managerId;

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
                        MessageBox.Show("Thông tin xác thực không đúng.");
                        return;
                    }

                    // ✅ đúng → sang Reset
                    VerifiedOk?.Invoke(username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Forgot Password: " + ex.Message);
            }
        }
    }
}