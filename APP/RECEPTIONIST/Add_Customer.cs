using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RECEPTIONIST
{
    public partial class Add_Customer : Form
    {
        public Add_Customer()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            if (modify.CreateCustomer(txtFullName.Text, txtPhoneNumber.Text, txtEmail.Text, txtCCCD.Text, cboGender.Text, dtpBirthday.Text))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
        }
    }
}
