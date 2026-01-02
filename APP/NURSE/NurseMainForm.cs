using System;
using System.Windows.Forms;

namespace NURSE
{
    /// <summary>
    /// Main form wrapper for NURSE module - receives login info from LOGIN_FIX
    /// </summary>
    public class NurseMainForm : Form
    {
        // Store login info
        public string EmployeeId { get; private set; }
        public string FullName { get; private set; }
        public string Role { get; private set; }
        public string Username { get; private set; }

        private frmNurseNursingWorklist _worklistForm;

        /// <summary>
        /// Constructor called from LOGIN_FIX
        /// </summary>
        public NurseMainForm(string employeeId, string fullName, string role, string username)
        {
            EmployeeId = employeeId;
            FullName = fullName;
            Role = role;
            Username = username;

            // Setup this form as a container
            this.Text = $"PetCareX - Nurse ({fullName})";
            this.Size = new System.Drawing.Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None; // Borderless, child form will show

            // Create and embed the worklist form with full user info
            _worklistForm = new frmNurseNursingWorklist(employeeId, fullName, role, username);
            _worklistForm.TopLevel = false;
            _worklistForm.FormBorderStyle = FormBorderStyle.None;
            _worklistForm.Dock = DockStyle.Fill;
            
            this.Controls.Add(_worklistForm);
            _worklistForm.Show();

            // Handle child form close -> close main form
            _worklistForm.FormClosed += (s, e) => this.Close();
        }

        /// <summary>
        /// Default constructor for designer (not used by LOGIN)
        /// </summary>
        public NurseMainForm() : this("", "", "", "")
        {
        }
    }
}
