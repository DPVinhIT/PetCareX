using System;
using System.Windows.Forms;

namespace CASHIER
{
    /// <summary>
    /// Main form wrapper for CASHIER module - receives login info from LOGIN_FIX
    /// </summary>
    public class CashierMainForm : Form
    {
        // Store login info
        public string EmployeeId { get; private set; }
        public string FullName { get; private set; }
        public string Role { get; private set; }
        public string Username { get; private set; }

        private frmCashier _cashierForm;

        /// <summary>
        /// Constructor called from LOGIN_FIX
        /// </summary>
        public CashierMainForm(string employeeId, string fullName, string role, string username)
        {
            EmployeeId = employeeId;
            FullName = fullName;
            Role = role;
            Username = username;

            // Setup this form as a container
            this.Text = $"PetCareX - Cashier ({fullName})";
            this.Size = new System.Drawing.Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Borderless, child form will show

            // Create and embed the cashier form with full user info
            _cashierForm = new frmCashier(employeeId, fullName, role, username);
            _cashierForm.TopLevel = false;
            _cashierForm.FormBorderStyle = FormBorderStyle.None;
            _cashierForm.Dock = DockStyle.Fill;
            
            this.Controls.Add(_cashierForm);
            _cashierForm.Show();

            // Handle child form close -> close main form
            _cashierForm.FormClosed += (s, e) => this.Close();
        }

        /// <summary>
        /// Default constructor for designer (not used by LOGIN)
        /// </summary>
        public CashierMainForm() : this("", "", "", "")
        {
        }
    }
}
