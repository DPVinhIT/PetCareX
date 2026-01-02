using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace DOCTOR
{
    public partial class MainFormDoctor : Form
    {
        private readonly Dictionary<Type, Form> _cache = new Dictionary<Type, Form>();
        private Form _currentForm;

        private readonly string _username;
        private readonly string _employeeId;
        private readonly string _fullName;
        private readonly string _role;

        private List<Button> _menuButtons;
        private readonly Color _sidebarColor = Color.FromArgb(176, 202, 219);

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_EX_COMPOSITED
                return cp;
            }
        }

        // ===== CTOR DESIGNER =====
        public MainFormDoctor()
        {
            InitializeComponent();

            panelMain.Dock = DockStyle.Fill;
            panelMain.Visible = true;

            EnableDoubleBuffer(panelMain);

            _menuButtons = new List<Button>
            {
                btnAppointmentList,
                btnChangepassword,
                btnExaminationRecord,
                btnPetHistory,
                btnPrescription,
                btnSurgeryRecord,
                btnVaccination,
                btnLogout,
            };

            foreach (var btn in _menuButtons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.UseVisualStyleBackColor = false;
                btn.BackColor = _sidebarColor;
                btn.ForeColor = Color.Black;
                btn.FlatAppearance.BorderSize = 0;
            }

            this.Shown += MainFormDoctor_Shown;
        }

        // ===== CTOR LOGIN (match SignIn) =====
        public MainFormDoctor(string employeeId, string fullName, string role, string username) : this()
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

        // ✅ public getters giống Manager để form con lấy
        public string Username => lblUsername?.Text;        // nếu Doctor có lblUsername
        public string Did => lblEmployeeID?.Text;           // Doctor ID
        public string FullName => lblEmployeeName?.Text;
        public string Role => lblRole?.Text;

        private void ApplyUserInfo()
        {
            if (lblEmployeeID != null)
                lblEmployeeID.Text = string.IsNullOrWhiteSpace(_employeeId) ? "N/A" : _employeeId;

            if (lblEmployeeName != null)
                lblEmployeeName.Text = string.IsNullOrWhiteSpace(_fullName) ? "N/A" : _fullName;

            if (lblRole != null)
                lblRole.Text = string.IsNullOrWhiteSpace(_role) ? "N/A" : _role;

            // ⚠️ chỉ set nếu bạn có label username trên Doctor
            if (lblUsername != null)
                lblUsername.Text = string.IsNullOrWhiteSpace(_username) ? "N/A" : _username;
        }

        private void MainFormDoctor_Shown(object sender, EventArgs e)
        {
            ApplyUserInfo();

            // mặc định active AppointmentList
            SetActiveButton(btnAppointmentList);
            BeginInvoke(new Action(() => LoadForm<Appointment_List>()));
        }

        private static void EnableDoubleBuffer(Control c)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(c, true, null);
        }

        private void SetActiveButton(Button activeBtn)
        {
            foreach (var btn in _menuButtons)
            {
                btn.BackColor = Color.LightSteelBlue;
                btn.ForeColor = Color.Black;
                btn.FlatAppearance.BorderSize = 1;
            }

            activeBtn.BackColor = Color.White;
            activeBtn.ForeColor = Color.Black;
        }

        // ===== LOAD CHILD FORM =====
        public void LoadForm<T>() where T : Form
        {
            if (panelMain == null) return;

            var key = typeof(T);

            if (!_cache.TryGetValue(key, out var frm) || frm == null || frm.IsDisposed)
            {
                // ưu tiên ctor nhận MainFormDoctor
                var ctor = typeof(T).GetConstructor(new[] { typeof(MainFormDoctor) });
                frm = ctor != null
                    ? (Form)ctor.Invoke(new object[] { this })
                    : (Form)Activator.CreateInstance(typeof(T));

                frm.TopLevel = false;
                frm.FormBorderStyle = FormBorderStyle.None;
                frm.Dock = DockStyle.Fill;
                frm.Visible = false;

                panelMain.Controls.Add(frm);
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
        private void btnAppointmentList_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnAppointmentList);
            LoadForm<Appointment_List>();
        }

        private void btnChangepassword_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChangepassword);
            LoadForm<Change_password>();
        }

        private void btnExaminationRecord_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnExaminationRecord);
            LoadForm<Examination>();
        }

        private void btnPetHistory_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnPetHistory);
            LoadForm<Pet_History>();
        }

        private void btnPrescription_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnPrescription);
            LoadForm<Prescription_Form>();
        }

        private void btnSurgeryRecord_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnSurgeryRecord);
            LoadForm<Surgery>();
        }

        private void btnVaccination_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnVaccination);
            LoadForm<Vaccination>();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnLogout);

            var confirm = MessageBox.Show(
                "Bạn chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                this.Close();
                return;
            }

            // NO -> quay lại mặc định
            SetActiveButton(btnAppointmentList);
            LoadForm<Appointment_List>();
        }

        private void MainFormDoctor_Load(object sender, EventArgs e)
        {
        }
    }
}