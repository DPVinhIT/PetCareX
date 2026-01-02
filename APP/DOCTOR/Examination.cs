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
    public partial class Examination : Form
    {
        public Examination()
        {
            InitializeComponent();
        }

        private void Examination_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEID.Text))
            {
                MessageBox.Show("Vui lòng nhập EID.");
                return;
            }
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_BS_ThemHoSoKham", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@RID", Session.EmployeeID);
                cmd.Parameters.Add("@EID", SqlDbType.NVarChar, 10).Value = textEID.Text.Trim();
                cmd.Parameters.Add("@PetID", SqlDbType.NVarChar, 10).Value = textPetID.Text.Trim();
                //cmd.Parameters.Add("@ExaminationDate", SqlDbType.DateTime).Value = dateExamDate.Value;
                cmd.Parameters.Add("@Symptoms", SqlDbType.NVarChar, 255).Value = textSymptoms.Text.Trim();
                cmd.Parameters.Add("@Diagnosis", SqlDbType.NVarChar, 255).Value = textDiagnoses.Text.Trim();
                cmd.Parameters.Add("@FollowUpDate", SqlDbType.Date).Value = dateFollowUp.Checked ? (object)dateFollowUp.Value.Date : DBNull.Value;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
