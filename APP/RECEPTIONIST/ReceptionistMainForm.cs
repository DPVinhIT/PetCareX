using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace RECEPTIONIST
{
    public partial class ReceptionistMainForm : Form
    {
        private readonly Dictionary<Type, Form> _cache = new Dictionary<Type, Form>();
        private Form _currentForm;

        private readonly string _username;
        private readonly string _employeeId;
        private readonly string _fullName;
        private readonly string _role;

        // ===== MENU BUTTON COLOR =====
        private List<Button> _menuButtons;
        private readonly Color _sidebarColor = Color.FromArgb(176, 202, 219); // màu xanh sidebar

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED (chống flicker)
                return cp;
            }
        }
        public ReceptionistMainForm()
        {
            InitializeComponent();

            EnableDoubleBuffer(pnlMainMenu);

            // ===== INIT MENU BUTTONS =====
            _menuButtons = new List<Button>
            {
                btnAppointment,
                btnCustomerManagement,
                btnPetManagement,
                btnChangePassword,
                btnLogOut,
            };

            this.Shown += ReceptionistMainForm_Shown;
        }
        // ===== CTOR LOGIN =====
        public ReceptionistMainForm(string employeeId, string fullName, string role, string username) : this()
        {
            _employeeId = employeeId;
            _fullName = fullName;
            _role = role;
            _username = username;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            ApplyUserInfo();
        }
        public string Username => lblUsername.Text;
        public string mid => lblEmployeeID.Text;
        // ===== USER INFO =====
        private void ApplyUserInfo()
        {
            lblEmployeeID.Text = string.IsNullOrWhiteSpace(_employeeId) ? "N/A" : _employeeId;
            lblEmployeeName.Text = string.IsNullOrWhiteSpace(_fullName) ? "N/A" : _fullName;
            lblRole.Text = string.IsNullOrWhiteSpace(_role) ? "N/A" : _role;
            lblUsername.Text = string.IsNullOrWhiteSpace(_username) ? "N/A" : _username;

        }

        private void ReceptionistMainForm_Shown(object sender, EventArgs e)
        {
            ApplyUserInfo();
        }

        private static void EnableDoubleBuffer(Control c)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(c, true, null);
        }

        // ===== ACTIVE BUTTON COLOR =====
        private void SetActiveButton(Button activeBtn)
        {
            foreach (var btn in _menuButtons)
            {
                btn.BackColor = Color.LightSteelBlue;
                btn.ForeColor = Color.Black;
                btn.FlatAppearance.BorderSize = 1;
            }

            activeBtn.BackColor = Color.White; // ✅ NỀN TRẮNG
            activeBtn.ForeColor = Color.Black;
        }

        // ===== LOAD CHILD FORM =====
        public void LoadForm<T>() where T : Form
        {
            if (pnlMainMenu == null) return;

            var key = typeof(T);

            if (!_cache.TryGetValue(key, out var frm) || frm == null || frm.IsDisposed)
            {
                // ưu tiên ctor (ManagerMainForm), fallback ctor mặc định
                var ctor = typeof(T).GetConstructor(new[] { typeof(ReceptionistMainForm) });
                frm = ctor != null
                    ? (Form)ctor.Invoke(new object[] { this })
                    : (Form)Activator.CreateInstance(typeof(T));

                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Visible = false;

                splitContainer1.Panel2.Controls.Add(frm);
                _cache[key] = frm;
            }

            if (_currentForm != null && !_currentForm.IsDisposed && !ReferenceEquals(_currentForm, frm))
                _currentForm.Visible = false;

            frm.Visible = true;
            frm.BringToFront();
            frm.Focus();

            _currentForm = frm;
        }

        // ===== MENU EVENTS =====


        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChangePassword);
            LoadForm<ChangePassword>();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnLogOut);
            var confirm = MessageBox.Show(
                "Bạn chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                // ✅ YES = giống nút X (đóng form hiện tại)
                this.Close();
                return;
            }

            // ❌ NO -> quay lại trang mặc định
            SetActiveButton(btnAppointment);
            LoadForm<Appointment_Form>();
        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAppointment);
            LoadForm<Appointment_Form>();
        }

        private void btnCustomerManagement_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnCustomerManagement);
            LoadForm<Customer_Management>();
        }

        private void btnPetManagement_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnPetManagement);
            LoadForm<Pet_Management>();
        }
    }
}
