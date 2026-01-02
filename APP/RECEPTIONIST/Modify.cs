using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace RECEPTIONIST
{
    internal class Modify
    {
        SqlDataAdapter dataAdapter;
        SqlCommand command;
        public Modify()
        {

        }

        public String GetEmployeeBranchID(String EmpID) 
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT th.BranchID FROM TransferHistory th WHERE th.EmployeeID = '" + EmpID + "' AND th.StartDate <= GETDATE() AND th.EndDate >= GETDATE()";
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            string res = dataTable.Rows[0][0].ToString();
            return res;
        } 

        public void ChangePassword(String Username, String OldPassword, String NewPassword)
        {
            string query = "sp_ChangePassword";
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //chống SQL Injection và lỗi định dạng dữ liệu
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@OldPassword", OldPassword);
                        cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
                        cmd.Parameters.AddWithValue("@result", 1);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Check lỗi
                    throw new Exception("Lỗi khi đổi mật khẩu: " + ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

        public DataTable getAllPet()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Pet";
            using (SqlConnection sqlConnection = Connection.GetConnectionv2())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dt);
                sqlConnection.Close();
            }
            return dt;
        }

        public DataTable findPet(String PetID, String PetName, String Species) 
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Pet p WHERE p.PetID like '%" + PetID + "%' AND p.PetName like '%" + PetName + "%' AND p.Species like '%" + Species + "%'";
            using (SqlConnection sqlConnection = Connection.GetConnectionv2())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dt);
                sqlConnection.Close();
            }
            return dt;
        }

        public DataTable getAllCustomer()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Customer";
            using (SqlConnection sqlConnection = Connection.GetConnectionv2())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dt);
                sqlConnection.Close();
            }
            return dt;
        }

        public DataTable findCustomer(String CusID, String CusName, String PhoneNumber, String Email, String CCCD)
        {
            DataTable dt = new DataTable();

            // Sử dụng tham số (@CusID, @FullName...) thay vì cộng chuỗi trực tiếp
            string query = "SELECT * FROM Customer WHERE " +
                           "CustomerID LIKE @CusID AND " +
                           "FullName LIKE @FullName AND " +
                           "PhoneNumber LIKE @Phone AND " +
                           "Email LIKE @Email AND " +
                           "CCCD LIKE @CCCD AND ";
                      

            using (SqlConnection sqlConnection = Connection.GetConnectionv2())
            {
                sqlConnection.Open();

                // Dùng SqlCommand để gán các giá trị vào tham số
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    // Thêm các tham số và giá trị tương ứng (đã bao gồm ký tự %)
                    cmd.Parameters.AddWithValue("@CusID", "%" + (CusID ?? "") + "%");
                    cmd.Parameters.AddWithValue("@FullName", "%" + (CusName ?? "") + "%");
                    cmd.Parameters.AddWithValue("@Phone", "%" + (PhoneNumber ?? "") + "%");
                    cmd.Parameters.AddWithValue("@Email", "%" + (Email ?? "") + "%");
                    cmd.Parameters.AddWithValue("@CCCD", "%" + (CCCD ?? "") + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                // sqlConnection sẽ tự động đóng khi thoát khỏi khối using
            }
            return dt;
        }

        public bool CreateCustomer(string name, string phone, string email, string cccd, string gender, string birthday)
        {
            try
            {
                using (SqlConnection sqlConnection = Connection.GetConnectionv2())
                {
                    sqlConnection.Open();

                    // Khởi tạo SqlCommand với tên Stored Procedure
                    using (SqlCommand cmd = new SqlCommand("CreateNewCustomer", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm các tham số khớp chính xác với tên biến trong SQL của bạn
                        cmd.Parameters.AddWithValue("@FullName", name);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@CCCD", cccd);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Birthday", birthday);

                        // Thực thi lệnh 
                        int result = cmd.ExecuteNonQuery();

                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có (ví dụ: trùng khóa chính, lỗi kết nối...)
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message);
                return false;
            }
        }

        public bool RegisterAppointment(string customerID, string branchID, string serviceID,
                                 string receptionistID, DateTime date, TimeSpan time, string room = null)
        {
            try
            {
                using (SqlConnection sqlConnection = Connection.GetConnectionv2())
                {
                    sqlConnection.Open();

                    using (SqlCommand cmd = new SqlCommand("RegisterAppointment", sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Thêm các tham số bắt buộc
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.Parameters.AddWithValue("@BranchID", branchID);
                        cmd.Parameters.AddWithValue("@ServiceID", serviceID);
                        cmd.Parameters.AddWithValue("@ReceptionistID", receptionistID);

                        // Truyền ngày và giờ theo định dạng chuẩn SQL
                        cmd.Parameters.AddWithValue("@AppointmentDate", date.Date);
                        cmd.Parameters.AddWithValue("@AppointmentTime", time);

                        // Xử lý tham số tùy chọn @Room (Nếu null thì gửi DBNull vào SQL)
                        cmd.Parameters.AddWithValue("@Room", (object)room ?? DBNull.Value);

                        // Thực thi lệnh và kiểm tra kết quả
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng ký lịch hẹn: " + ex.Message);
                return false;
            }
        }
    }

}
