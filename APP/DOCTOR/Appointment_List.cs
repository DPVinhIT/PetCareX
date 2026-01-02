using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


using LOGIN;
//using LOGIN_Class;
namespace DOCTOR
{
    public partial class Appointment_List : Form
    {
        public Appointment_List()
        {
            InitializeComponent();
            //this.Load += Appointment_List_Load;

        }

        private void Appointment_List_Load(object sender, EventArgs e)
        {
            LoadAppointmentList();
        }

        private void LoadAppointmentList()
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_BS_XemLichKham", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@RID", Session.EmployeeID);
                cmd.Parameters.Add("@RID", SqlDbType.NVarChar, 20).Value = Session.EmployeeID;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_BS_XemLichKham", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@RID", Session.EmployeeID);
                cmd.Parameters.Add("@RID", SqlDbType.NVarChar, 20).Value = Session.EmployeeID;
                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = frmDate.Checked ? frmDate.Value.Date : (object)DBNull.Value;
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate.Checked ? toDate.Value.Date : (object)DBNull.Value;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            dataGridView1.DataSource = dt;
        }













        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            

        }

    }
}
