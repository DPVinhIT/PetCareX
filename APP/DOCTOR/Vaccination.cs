using LOGIN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOCTOR
{
    public partial class Vaccination : Form
    {
        public Vaccination()
        {
            InitializeComponent();
            AutoCompleteVaccineNames();
            ClearTextOnFocus(textVacName);
        }
        private void ClearTextOnFocus(TextBox txt)
        {
            txt.Enter += (s, e) =>
            {
                txt.Clear();
            };
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textVacName.Text)|| cbVacType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn Vaccine và loại Vaccine");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            {
                conn.Open();

                string vaccineID = null;
                using (SqlCommand cmdGetID = new SqlCommand(@"SELECT VaccineID FROM Vaccine WHERE VaccineName = @Name AND VaccineType = @Type", conn))
                {
                    cmdGetID.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = textVacName.Text.Trim();

                    cmdGetID.Parameters.Add("@Type", SqlDbType.NVarChar, 100).Value = cbVacType.SelectedItem.ToString();

                    object result = cmdGetID.ExecuteScalar();
                    if (result != null)
                        vaccineID = result.ToString();
                }

                if (vaccineID == null)
                {
                    MessageBox.Show("Không tìm thấy Vaccine phù hợp");
                    return;
                }

                using (SqlCommand cmd = new SqlCommand("sp_BS_ThucHienTiemPhong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@VID", SqlDbType.VarChar, 20).Value = textVID.Text.Trim();

                    cmd.Parameters.Add("@VaccineID", SqlDbType.VarChar, 20).Value = vaccineID;

                    cmd.Parameters.Add("@Dosage", SqlDbType.NVarChar, 50).Value = textDosage.Text.Trim();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tiêm phòng thành công");
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void AutoCompleteVaccineNames()
        {
            List<string> vaccineList = new List<string>();

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT VaccineName FROM Vaccine", conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        vaccineList.Add(reader["VaccineName"].ToString());
                }
            }

            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(vaccineList.ToArray());

            textVacName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textVacName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textVacName.AutoCompleteCustomSource = source;

            textVacName.Leave += textVacName_Leave;
        }

        private void textVacName_Leave(object sender, EventArgs e)
        {
            cbVacType.Items.Clear();

            if (string.IsNullOrWhiteSpace(textVacName.Text))
                return;

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT VaccineType FROM Vaccine WHERE VaccineName = @name", conn))
            {
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = textVacName.Text.Trim();

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())cbVacType.Items.Add(reader["VaccineType"].ToString());
                }
            }

            if (cbVacType.Items.Count > 0)
                cbVacType.SelectedIndex = 0; 
        }

    }
}
