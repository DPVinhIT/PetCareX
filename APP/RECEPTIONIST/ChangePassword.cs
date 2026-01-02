using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RECEPTIONIST
{
    public partial class ChangePassword : Form
    {
        ReceptionistMainForm mainForm;
        public ChangePassword()
        {
            InitializeComponent();
        }

        public ChangePassword(ReceptionistMainForm Parent)
        {
            InitializeComponent();
            mainForm = Parent;
        }
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text) 
            {
                var warn = MessageBox.Show(
                        "New password and confirm new password should be similar",
                        "Confirm warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning 
                );
                return;
            }
            Modify modify = new Modify();
            modify.ChangePassword(mainForm.lblUsername.Text, txtOldPassword.Text, txtNewPassword.Text);
        }

    }
}
