using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SALE
{
    public partial class OrderItem : Form
    {
        Sale mainForm;
        public OrderItem()
        {
            InitializeComponent();
        }
        public OrderItem(Sale MainForm)
        {
            InitializeComponent();
            this.mainForm = MainForm;
        }

        private void lblSearchProduct_TextChanged(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            this.dgv_ProductDisplay.DataSource = modify.findProd(lblSearchProduct.Text, cboCategory.Text);
        }

        private void cboCategory_TextChanged(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            this.dgv_ProductDisplay.DataSource = modify.findProd(lblSearchProduct.Text, cboCategory.Text);
        }

        private void OrderItem_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'petCareX_DBDataSet1.Product' table. You can move, or remove it, as needed.
            this.productTableAdapter.Fill(this.petCareX_DBDataSet1.Product);

        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv_ProductDisplay.SelectedRows)
            {
                int index = dgv_CartArena.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, 1, row.Cells[3].Value);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblSearchProduct.Text = "";
            cboCategory.SelectedIndex = -1;
            dgv_CartArena.ClearSelection();
            dgv_ProductDisplay.ClearSelection();
            dgv_CartArena.Refresh();
            dgv_ProductDisplay.Refresh();
        }

        private void btnDeleteFromCart_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv_CartArena.SelectedRows)
            {
                dgv_CartArena.Rows.Remove(row);
            }
        }

        private void btnSendToCashier_Click(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            string branchID = modify.GetEmployeeBranchID(mainForm.lblEmployeeID.Text).ToString();
            if (dgv_CartArena.Rows.Count > 0)
            {
                string OrderID = modify.CreateOrder(txt_CusID.Text, mainForm.lblEmployeeID.Text, dgv_CartArena.Rows[0].Cells[0].Value.ToString(), dgv_CartArena.Rows[0].Cells[2].Value.ToString(), branchID);
                for (int i = 1; i < dgv_CartArena.Rows.Count; i++)
                {
                    modify.AddProductToOrder(OrderID, dgv_CartArena.Rows[i].Cells[0].Value.ToString(), dgv_CartArena.Rows[i].Cells[2].Value.ToString(), branchID);
                }
                txt_CusID.Text = OrderID;
            }
        }

        private void dgv_CartArena_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            float total = 0;
            foreach (DataGridViewRow row in dgv_CartArena.Rows)
            {
                total += float.Parse(row.Cells[3].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());

            }
            lblSubTotal.Text = total.ToString("N0") + " VND";

        }

        private void dgv_CartArena_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            float total = 0;
            foreach (DataGridViewRow row in dgv_CartArena.Rows)
            {
                total += float.Parse(row.Cells[3].Value.ToString()) * float.Parse(row.Cells[2].Value.ToString());

            }
            lblSubTotal.Text = total.ToString("N0") + " VND";

        }
    }
}
