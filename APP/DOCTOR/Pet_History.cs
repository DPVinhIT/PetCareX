using LOGIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DOCTOR
{
    public partial class Pet_History : Form
    {
        public Pet_History()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
                using (SqlCommand cmd = new SqlCommand("sp_BS_LichSuPet", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PetID", SqlDbType.VarChar, 20).Value = textPetID.Text.Trim();
                    SqlParameter pFrom = cmd.Parameters.Add("@FromDate", SqlDbType.Date);
                    if (frmTime.Checked)
                        pFrom.Value = frmTime.Value.Date;
                    else
                        pFrom.Value = DBNull.Value;

                    SqlParameter pTo = cmd.Parameters.Add("@ToDate", SqlDbType.Date);
                    if (toTime.Checked)
                        pTo.Value = toTime.Value.Date;
                    else
                        pTo.Value = DBNull.Value;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    conn.Open();
                    da.Fill(dt);

                    //dataGridView1.AutoGenerateColumns = true;
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
