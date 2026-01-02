using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MANAGER;
//using DOCTOR;
//using CASHIER;
//using RECEPTIONIST;
using NURSE;
using SALE;
namespace LOGIN_FIX
{
    public partial class SignIn : Form
    {
        public event Action<Form> LoginSuccess;
        public event Action ForgotPasswordClicked;
        private readonly string connectionString =
            "Data Source=CHIENCOHUYENTHO\\PVINH;" +
            "Initial Catalog=PetCareX_DB;" +
            "Integrated Security=True;" +
            "Encrypt=True;" +
            "TrustServerCertificate=True;";

        public SignIn()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e) // Login button
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập Username và Password.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("dbo.sp_Login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 20).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = password;

                    var pResult = new SqlParameter("@result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(pResult);

                    conn.Open();

                    string empId = null, fullName = null, role = null;

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            empId = rd["EmployeeID"]?.ToString();
                            fullName = rd["FullName"]?.ToString();
                            role = rd["Role"]?.ToString();
                        }
                    }

                    int result = (pResult.Value == DBNull.Value) ? 0 : Convert.ToInt32(pResult.Value);

                    if (result == 0)
                    {
                        MessageBox.Show("Sai Username hoặc Password.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(role))
                    {
                        MessageBox.Show("Đăng nhập đúng nhưng không lấy được Role.");
                        return;
                    }

                    OpenByRole(empId, fullName, role, username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
            }
        }

        private void OpenByRole(string employeeId, string fullName, string role, string username)
        {
            string r = role.Trim();

            Form next = null;

            switch (r)
            {
                case "Manager":
                    next = new ManagerMainForm(employeeId, fullName, role, username);
                    break;

                case "Doctor":
                    //next = new MainFormDoctor(employeeId, fullName, role, username);
                    break;
                case "Cashier":
                    //next = new CashierMainForm(employeeId, fullName, role, username);
                    break;
                case "Receptionist":
                    //next = new ReceptionistMainForm(employeeId, fullName, role, username);
                    break;
                case "Nurse":
                    next = new NurseMainForm(employeeId, fullName, role, username);
                    break;
                case "SalePerson":
                    next = new Sale(employeeId, fullName, role, username);
                    MessageBox.Show($"Chưa map form cho role: {r}");
                    return;

                default:
                    MessageBox.Show("Role không hợp lệ: " + role);
                    return;
            }
            LoginSuccess?.Invoke(next);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPasswordClicked?.Invoke();
        }
    }
}
