using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CASHIER
{
    /// <summary>
    /// Lớp hỗ trợ kết nối và thao tác với database SQL Server
    /// </summary>
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PetCareXDB"].ConnectionString;

        /// <summary>
        /// Lấy danh sách đơn hàng chưa thanh toán theo CustomerID
        /// </summary>
        public static DataTable GetOrdersNotYetPaid(string customerId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("get_orderNotYetPaid", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_customer", customerId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy chi tiết đơn hàng theo OrderID
        /// </summary>
        public static DataTable GetOrderDetail(string orderId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetOrderDetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", orderId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Tạo hóa đơn và xử lý thanh toán
        /// </summary>
        public static DataTable CreateInvoice(string orderId, string cashierId, string paymentMethodId, 
                                               decimal paymentMoney, float promotion = 0, string discountId = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CreateInvoice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderID", orderId);
                    cmd.Parameters.AddWithValue("@CashierID", cashierId);
                    cmd.Parameters.AddWithValue("@PaymentMethodID", paymentMethodId);
                    cmd.Parameters.AddWithValue("@PaymentMoney", paymentMoney);
                    cmd.Parameters.AddWithValue("@promotion", promotion);

                    if (string.IsNullOrEmpty(discountId))
                        cmd.Parameters.AddWithValue("@discountID", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@discountID", discountId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo CustomerID
        /// </summary>
        public static DataRow GetCustomerInfo(string customerId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        c.CustomerID,
                        c.FullName,
                        c.PhoneNumber,
                        cm.LevelID,
                        ISNULL(ml.LevelName, 'Không có') AS MembershipLevel,
                        ISNULL(cm.LoyalPoint, 0) AS LoyalPoint
                    FROM Customer c
                    LEFT JOIN CardMembership cm ON c.CustomerID = cm.CustomerID
                    LEFT JOIN MembershipLevel ml ON cm.LevelID = ml.LevelID
                    WHERE c.CustomerID = @CustomerID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Lấy thông tin khách hàng theo số điện thoại
        /// </summary>
        public static DataRow GetCustomerByPhone(string phoneNumber)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        c.CustomerID,
                        c.FullName,
                        c.PhoneNumber,
                        cm.LevelID,
                        ISNULL(ml.LevelName, 'Không có') AS MembershipLevel,
                        ISNULL(cm.LoyalPoint, 0) AS LoyalPoint
                    FROM Customer c
                    LEFT JOIN CardMembership cm ON c.CustomerID = cm.CustomerID
                    LEFT JOIN MembershipLevel ml ON cm.LevelID = ml.LevelID
                    WHERE c.PhoneNumber = @PhoneNumber";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Lấy danh sách phương thức thanh toán
        /// </summary>
        public static DataTable GetPaymentMethods()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT PaymentTypeID, MethodName FROM PaymentMethod";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách mã giảm giá còn hiệu lực
        /// </summary>
        public static DataTable GetActiveDiscounts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT DiscountID, DiscountName, Percentage 
                    FROM Discount 
                    WHERE GETDATE() BETWEEN StartDate AND EndDate
                    ORDER BY Percentage DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy danh sách mã giảm giá hợp lệ theo level khách hàng
        /// TargetUser = NULL: tất cả khách hàng
        /// TargetUser = L1/L2/L3: level tối thiểu (L1 < L2 < L3)
        /// </summary>
        public static DataTable GetDiscountsByLevel(string customerLevelId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query;
                
                if (string.IsNullOrEmpty(customerLevelId))
                {
                    // Khách không có level → chỉ hiện discount cho tất cả (TargetUser IS NULL)
                    query = @"
                        SELECT DiscountID, DiscountName, TargetUser, Percentage 
                        FROM Discount 
                        WHERE GETDATE() BETWEEN StartDate AND EndDate
                          AND TargetUser IS NULL
                        ORDER BY Percentage DESC";
                }
                else
                {
                    // Khách có level → hiện discount NULL + discount phù hợp với level
                    query = @"
                        SELECT DiscountID, DiscountName, TargetUser, Percentage 
                        FROM Discount 
                        WHERE GETDATE() BETWEEN StartDate AND EndDate
                          AND (
                              TargetUser IS NULL  
                              OR TargetUser = @CustomerLevel
                              OR (TargetUser = 'L1')
                              OR (TargetUser = 'L2' AND @CustomerLevel IN ('L2', 'L3'))
                              OR (TargetUser = 'L3' AND @CustomerLevel = 'L3')
                          )
                        ORDER BY Percentage DESC";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(customerLevelId))
                        cmd.Parameters.AddWithValue("@CustomerLevel", customerLevelId);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Test kết nối database - trả về thông báo lỗi chi tiết
        /// </summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Connection String: {connectionString}\n\nLỗi: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Test kết nối database (phiên bản đơn giản)
        /// </summary>
        public static bool TestConnection()
        {
            return TestConnection(out _);
        }

        /// <summary>
        /// Lấy danh sách hóa đơn đã thanh toán trong ngày
        /// </summary>
        public static DataTable GetTodayInvoices(string cashierId = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetTodayInvoices", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(cashierId))
                        cmd.Parameters.AddWithValue("@CashierID", cashierId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}
