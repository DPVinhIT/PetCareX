namespace RECEPTIONIST
{
    public partial class Customer_Management : Form
    {
        public Customer_Management()
        {
            InitializeComponent();
        }

        private void Service_Form_Load(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            dataGridView1.DataSource = modify.getAllCustomer();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Modify modify = new Modify();
            dataGridView1.DataSource = modify.findCustomer(txtID.Text, txtFullName.Text, txtPhoneNumber.Text, txtEmail.Text, txtCCCD.Text);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Add_Customer f = new Add_Customer();
            f.ShowDialog();
        }
    }
}
