using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SALE
{
    public partial class Sale : Form
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


        public Sale()
        {
            InitializeComponent();
            // ===== INIT MENU BUTTONS =====
            _menuButtons = new List<Button>
            {
                btnOrderItem,
                btnChangePassword,
                btnLogout,
            };
        }

        public Sale(string employeeId, string fullName, string role, string username) : this()
        {
            _employeeId = employeeId;
            _fullName = fullName;
            _role = role;
            _username = username;
        }

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
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            ApplyUserInfo();
        }
        private void ApplyUserInfo()
        {
            lblEmployeeID.Text = string.IsNullOrWhiteSpace(_employeeId) ? "N/A" : _employeeId;
            lblEmployeeName.Text = string.IsNullOrWhiteSpace(_fullName) ? "N/A" : _fullName;
            lblRole.Text = string.IsNullOrWhiteSpace(_role) ? "N/A" : _role;
            lblUsername.Text = string.IsNullOrWhiteSpace(_username) ? "N/A" : _username;

        }

        private void Sale_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'petCareX_DBDataSet.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.petCareX_DBDataSet.Product);

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.Diagnostics.Process.Start("C:\\Private\\2025\\CSDLNC\\PetCareX\\LOGIN\\bin\\Debug\\net10.0-windows\\LOGIN.exe");
        }

        private void OpenChildForm(Form childForm)
        {
            // Xóa các Form cũ đang hiển thị 
            if (splitContainer1.Panel2.Controls.Count > 0)
                splitContainer1.Panel2.Controls.Clear();

            // Thiết lập Form con
            childForm.TopLevel = false;          
            childForm.FormBorderStyle = FormBorderStyle.None; // Bỏ thanh tiêu đề
            childForm.Dock = DockStyle.Fill;     // Lấp đầy vùng chứa

            splitContainer1.Panel2.Controls.Add(childForm);
            splitContainer1.Panel2.Tag = childForm;
            childForm.Show();
        }
        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnChangePassword);
            ChangePassword f = new ChangePassword(this);
            OpenChildForm(f);
        }

        private void btnStaffManagement_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnOrderItem);
            OrderItem f = new OrderItem(this);
            OpenChildForm(f);
        }
    }
}
