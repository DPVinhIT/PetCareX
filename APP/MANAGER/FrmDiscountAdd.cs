using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MANAGER
{
    public partial class FrmDiscountAdd : Form
    {
        public string DiscountName => txtName.Text.Trim();
        public DateTime StartDate => dtpStart.Value.Date;
        public DateTime EndDate => dtpEnd.Value.Date;
        public string TargetUser => txtTargetUser.Text.Trim();

        // percent kiểu double để truyền vào proc
        public double Percentage { get; private set; }

        public FrmDiscountAdd()
        {
            InitializeComponent();

            dtpStart.Format = DateTimePickerFormat.Short;
            dtpEnd.Format = DateTimePickerFormat.Short;
        }

        private void FrmDiscountAdd_Load(object sender, EventArgs e)
        {
        }

        private void btnAdd_Click_1(object sender, EventArgs e) //add button
        {
            // 1) validate name
            if (string.IsNullOrWhiteSpace(DiscountName))
            {
                MessageBox.Show("Discount Name không được để trống.");
                txtName.Focus();
                return;
            }

            // 2) validate date
            if (EndDate < StartDate)
            {
                MessageBox.Show("End Date phải >= Start Date.");
                dtpEnd.Focus();
                return;
            }

            // 3) validate target user
            if (string.IsNullOrWhiteSpace(TargetUser))
            {
                MessageBox.Show("Target User không được để trống.");
                txtTargetUser.Focus();
                return;
            }

            // 4) validate percent (float 0-100)
            string raw = (txtPercent.Text ?? "").Trim().Replace(",", "."); // chống lỗi nhập 10,5
            if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out double p))
            {
                MessageBox.Show("Percent phải là số (ví dụ 10 hoặc 12.5).");
                txtPercent.Focus();
                txtPercent.SelectAll();
                return;
            }

            if (p <= 0 || p > 100)
            {
                MessageBox.Show("Percent phải trong khoảng (0..100].");
                txtPercent.Focus();
                txtPercent.SelectAll();
                return;
            }

            Percentage = p;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
