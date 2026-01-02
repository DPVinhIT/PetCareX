using LOGIN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOCTOR
{
    public partial class Prescription_Form : Form
    {
        public Prescription_Form()
        {
            InitializeComponent();
            AutoCompleteDrugNames(textDrugName1, textDrugType1);
            AutoCompleteDrugNames(textDrugName2, textDrugType2);
            AutoCompleteDrugNames(textDrugName3, textDrugType3);
            ClearTextOnFocus(textDrugName1);
            ClearTextOnFocus(textDrugName2);
            ClearTextOnFocus(textDrugName3);
            


        }

       
        private void AutoCompleteDrugNames(TextBox txtDrugName, TextBox txtDrugType)
        {
            // Auto-complete for drug names
            List<string> drugList = new List<string>();

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT DrugName FROM Drug", conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    drugList.Add(reader["DrugName"].ToString());
                }
            }


            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(drugList.ToArray());

            txtDrugName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDrugName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtDrugName.AutoCompleteCustomSource = source;


            // Set up auto-complete for drug names
            txtDrugName.Leave += (s, e) =>
            {
                using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT DrugType FROM Drug WHERE DrugName = @name", conn))
                {
                    cmd.Parameters.AddWithValue("@name", txtDrugName.Text);
                    conn.Open();
                    var type = cmd.ExecuteScalar();
                    txtDrugType.Text = type?.ToString();
                }
            };
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
            DataTable drugList = Prescription_Add();

            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_BS_KeToaThuoc", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@EID", SqlDbType.VarChar, 20).Value = textEID.Text.Trim();

                cmd.Parameters.Add("@Note", SqlDbType.NVarChar, 255).Value = textNote.Text.Trim();

                SqlParameter pDrugList = cmd.Parameters.Add("@DrugList", SqlDbType.Structured);
                pDrugList.Value = drugList;
                pDrugList.TypeName = "dbo.DrugListType";

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kê toa thuốc thành công");
        }

        private string GetDrugIDByName(string drugName)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("SELECT DrugID FROM Drug WHERE DrugName = @DrugName", conn))
            {
                cmd.Parameters.Add("@DrugName", SqlDbType.NVarChar, 100).Value = drugName;

                conn.Open();
                object result = cmd.ExecuteScalar();

                return result == null ? null : result.ToString();
            }
        }

        private DataTable Prescription_Add()
        {
            DataTable drugList = new DataTable();
            drugList.Columns.Add("DrugID", typeof(string));
            drugList.Columns.Add("Quantity", typeof(int));
            drugList.Columns.Add("UsageInstruction", typeof(string));

            if (string.IsNullOrWhiteSpace(textDrugName1.Text) == false)
            {
                string drugID1 = GetDrugIDByName(textDrugName1.Text.Trim());
                drugList.Rows.Add(drugID1, (int)numericUpDown1.Value, DBNull.Value);
            }
                
            if (string.IsNullOrWhiteSpace(textDrugName2.Text) == false)
            {
                string drugID2 = GetDrugIDByName(textDrugName2.Text.Trim());
                drugList.Rows.Add(drugID2, (int)numericUpDown2.Value, DBNull.Value);
            }
            if (string.IsNullOrWhiteSpace(textDrugName3.Text) == false)
            {
                string drugID3 = GetDrugIDByName(textDrugName3.Text.Trim());
                drugList.Rows.Add(drugID3, (int)numericUpDown3.Value, DBNull.Value);

            }
            return drugList;

        }


        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
