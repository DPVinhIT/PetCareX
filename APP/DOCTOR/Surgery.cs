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
    public partial class Surgery : Form
    {
        public Surgery()
        {
            InitializeComponent();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textSurID.Text))
            {
                MessageBox.Show("Vui lòng nhập mã ca phẫu thuật");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_BS_CapNhatHoSoPhauThuat", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@SurgeryID", SqlDbType.VarChar, 20).Value = textSurID.Text.Trim();

                cmd.Parameters.Add("@PetID", SqlDbType.VarChar, 20).Value = textPetID.Text.Trim();

                cmd.Parameters.Add("@SurgeryType", SqlDbType.NVarChar, 100).Value = textSurType.Text.Trim();

                cmd.Parameters.Add("@AnesthesiaType", SqlDbType.NVarChar, 100).Value = textAnes.Text.Trim();

                cmd.Parameters.Add("@SurgeryStatus", SqlDbType.NVarChar, 100).Value = textStatus.Text.Trim();

                cmd.Parameters.Add("@DiagnosisNote", SqlDbType.NVarChar, 200).Value = textDiag.Text.Trim();

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật hồ sơ phẫu thuật thành công");
                }
                catch (SqlException ex)
                {
                    
                    MessageBox.Show(ex.Message, "Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
