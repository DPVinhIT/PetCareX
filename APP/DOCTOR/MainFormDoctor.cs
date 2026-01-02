using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using LOGIN_Class;

//using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOGIN;

namespace DOCTOR
{
    public partial class MainFormDoctor : Form
    {

        private readonly Dictionary<Type, Form> _cache = new Dictionary<Type, Form>();
        private Form _currentForm;

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

        private readonly string _employeeId;
        private readonly string _fullName;
        private readonly string _role;


        

        private List<Button> _menuButtons;

        public MainFormDoctor()
        {
            InitializeComponent();

            panelMain.Dock = DockStyle.Fill;
            panelMain.Visible = true;

           
            EnableDoubleBuffer(panelMain);

            
            //MessageBox.Show(Session.EmployeeID + " " + Session.FullName + " " + Session.Role);

            // ===== INIT MENU BUTTONS =====
            _menuButtons = new List<Button>
            {
                btnAppointmentList,
                btnChangepassword,
                btnExaminationRecord,
                btnLogout,
                btnPetHistory,
                btnPrescription,
                btnSurgeryRecord,
                btnVaccination,


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

        public MainFormDoctor(string employeeId, string fullName, string role) : this()
        {
            _employeeId = employeeId;
            _fullName = fullName;
            _role = role;
        }

        private static void EnableDoubleBuffer(Control c)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(c, true, null);
        }

        private void ApplyUserInfo()
        {
            lblEmployeeID.Text = string.IsNullOrWhiteSpace(_employeeId) ? "N/A" : _employeeId;
            lblEmployeeName.Text = string.IsNullOrWhiteSpace(_fullName) ? "N/A" : _fullName;
            lblRole.Text = string.IsNullOrWhiteSpace(_role) ? "N/A" : _role;
            Session.EmployeeID = lblEmployeeID.Text;
            Session.FullName = lblEmployeeName.Text;
            Session.Role = lblRole.Text;
        }
        private void MainFormDoctor_Shown(object sender, EventArgs e)
        {
            ApplyUserInfo();

            // mặc định active AppointmentList
            
            SetActiveButton(btnAppointmentList);
            BeginInvoke(new Action(() => LoadForm<Appointment_List>()));
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
                // ưu tiên ctor (ManagerMainForm), fallback ctor mặc định
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







        private void MainFormDoctor_Load(object sender, EventArgs e)
        {

        }
    }
}
