using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MANAGER
{
    public partial class FrmWorkScheduleAdd : Form
    {
        public string EmployeeID => txtEmployeeID.Text.Trim();
        public DateTime WorkDate => dtpWorkDate.Value.Date;
        public int WorkTime => int.Parse(cboWorkTime.SelectedItem.ToString());
        public string Shift => cboShift.SelectedItem?.ToString();

        public FrmWorkScheduleAdd()
        {
            InitializeComponent();
            InitTime();
            InitShift();


        }
        private void InitTime()
        {
            cboWorkTime.DrawMode = DrawMode.Normal;
            cboWorkTime.DropDownStyle = ComboBoxStyle.DropDownList;
            cboWorkTime.FormattingEnabled = true;

            cboWorkTime.Items.Clear();
            cboWorkTime.Items.Add(7);
            cboWorkTime.Items.Add(13);
            cboWorkTime.Items.Add(18);

            cboWorkTime.SelectedIndex = 0;
        }

        private void InitShift()
        {
            cboShift.DrawMode = DrawMode.Normal;
            cboShift.DropDownStyle = ComboBoxStyle.DropDownList;
            cboShift.FormattingEnabled = true;

            cboShift.Items.Clear();
            cboShift.Items.Add("Ca sáng");
            cboShift.Items.Add("Ca chiều");
            cboShift.Items.Add("Ca tối");

            cboShift.SelectedIndex = 0;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeID))
            {
                MessageBox.Show("EmployeeID không được để trống.");
                txtEmployeeID.Focus();
                return;
            }

            if (WorkDate < DateTime.Today)
            {
                MessageBox.Show("WorkDate không được là ngày trong quá khứ.");
                dtpWorkDate.Focus();
                return;
            }

            if (cboWorkTime.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn WorkTime.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Shift))
            {
                MessageBox.Show("Vui lòng chọn Shift.");
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
