using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace SALE
{
    internal class Modify
    {
        SqlDataAdapter dataAdapter;
        SqlCommand command;
        public Modify()
        {

        }
        public DataTable getAllProd()
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM PRODUCT";
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }
        public DataTable findProdByName(String name)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM PRODUCT D WHERE D.PRODUCTNAME LIKE N'%" + name + "%'";
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }

        public DataTable findProdByCategory(String name)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM PRODUCT D WHERE D.ProductType LIKE N'%" + name + "%'";
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }

        public DataTable findProd(String name, String category)
        {
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM PRODUCT D WHERE (D.ProductName LIKE N'%" + name + "%') AND (D.ProductType LIKE N'%" + category + "%')";
            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }

        public string CreateOrder(string CusID, string SID, string ProdID, string Quantity, string BranchID)
        {
            string ret = "";
            // Sử dụng Parameters giúp code sạch và an toàn hơn
            string query = "CreateOrderAndAddItem_BH1";

            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure; // Xác định đây là Stored Procedure

                // Thêm các tham số đầu vào
                cmd.Parameters.AddWithValue("@CustomerID", CusID);
                cmd.Parameters.AddWithValue("@SalesPersonID", SID);
                cmd.Parameters.AddWithValue("@ProductID", ProdID);
                cmd.Parameters.AddWithValue("@Quantity", int.Parse(Quantity));
                cmd.Parameters.AddWithValue("@BranchID", BranchID);

                // Khai báo tham số đầu ra (Output Parameter)
                SqlParameter outputIdParam = new SqlParameter("@OrderID", SqlDbType.VarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                // Thực thi
                cmd.ExecuteNonQuery();

                // Lấy giá trị từ tham số Output sau khi chạy xong
                if (outputIdParam.Value != DBNull.Value)
                {
                    ret = outputIdParam.Value.ToString();
                }

                sqlConnection.Close();
            }
            return ret;
        }

        public void AddProductToOrder(string orderID, string prodID, string quantity, string branchID)
        {
            string query = "AddItemToOrderDetail";

            using (SqlConnection sqlConnection = Connection.GetConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //chống SQL Injection và lỗi định dạng dữ liệu
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@ProductID", prodID);
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(quantity)); // Chuyển sang kiểu số
                        cmd.Parameters.AddWithValue("@BranchID", branchID);

                        //Dùng ExecuteNonQuery để thực thi lệnh thay đổi dữ liệu
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Check lỗi
                    throw new Exception("Lỗi khi thêm sản phẩm: " + ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
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
    }

}
