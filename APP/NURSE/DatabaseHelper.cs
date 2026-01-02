using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NURSE
{
    /// <summary>
    /// Database helper for NURSE application - connects to PetCareX database
    /// </summary>
    public static class DatabaseHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["PetCareXDB"].ConnectionString;

        /// <summary>
        /// YT - Get my tasks (sp_GetMyTasks)
        /// </summary>
        public static DataTable GetMyTasks(string nurseID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetMyTasks", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NurseID", nurseID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// YT - Complete a task (sp_CompleteTask)
        /// </summary>
        public static void CompleteTask(string taskID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CompleteTask", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaskID", taskID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// YT - Get post-surgery monitoring history (sp_GetHistoyPostSurgeryMonitoring1)
        /// </summary>
        public static DataTable GetHistoryPostSurgeryMonitoring(string petID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetHistoryPostSurgeryMonitoring", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InputPetID", petID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// YT - Add monitoring log (sp_AddMonitoringLog1)
        /// </summary>
        public static void AddMonitoringLog(string surgeryID, string nurseID, string status, string note)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AddMonitoringLog", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurgeryID", surgeryID);
                    cmd.Parameters.AddWithValue("@NurseID", nurseID);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Note", note);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Test database connection
        /// </summary>
        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
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
        /// Test database connection (simple version)
        /// </summary>
        public static bool TestConnection()
        {
            return TestConnection(out _);
        }

        /// <summary>
        /// YT3 - Assign nurse to surgery (sp_AssignNurseToSurgery)
        /// Trigger will auto-generate checklist tasks
        /// </summary>
        public static void AssignNurseToSurgery(string surgeryID, string nurseID, string note)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_AssignNurseToSurgery", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SurgeryID", surgeryID);
                    cmd.Parameters.AddWithValue("@NurseID", nurseID);
                    cmd.Parameters.AddWithValue("@Note", note);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// YT6 - Get inpatients list (sp_GetInpatientsList)
        /// </summary>
        public static DataTable GetInpatientsList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetInpatientsList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// YT7 - Get pet details (sp_GetPetDetails)
        /// </summary>
        public static DataTable GetPetDetails(string petName)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetPetDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PetName", petName);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}
