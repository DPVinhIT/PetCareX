namespace RECEPTIONIST
{
    partial class Customer_Management
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            panel2 = new Panel();
            btnAddCustomer = new Button();
            btnSearchCustomer = new Button();
            txtFullName = new RichTextBox();
            txtPhoneNumber = new RichTextBox();
            txtEmail = new RichTextBox();
            label7 = new Label();
            txtCCCD = new RichTextBox();
            label6 = new Label();
            label5 = new Label();
            txtID = new RichTextBox();
            label2 = new Label();
            lblID = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.Location = new Point(0, 101);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(783, 458);
            dataGridView1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnAddCustomer);
            panel2.Controls.Add(btnSearchCustomer);
            panel2.Controls.Add(txtFullName);
            panel2.Controls.Add(txtPhoneNumber);
            panel2.Controls.Add(txtEmail);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(txtCCCD);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(txtID);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(lblID);
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(-21, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(785, 561);
            panel2.TabIndex = 4;
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.BackColor = Color.Lime;
            btnAddCustomer.FlatAppearance.BorderSize = 0;
            btnAddCustomer.FlatStyle = FlatStyle.Flat;
            btnAddCustomer.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddCustomer.Location = new Point(684, 20);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(70, 25);
            btnAddCustomer.TabIndex = 7;
            btnAddCustomer.Text = "Add";
            btnAddCustomer.UseVisualStyleBackColor = false;
            btnAddCustomer.Click += btnAddCustomer_Click;
            // 
            // btnSearchCustomer
            // 
            btnSearchCustomer.BackColor = SystemColors.ActiveCaption;
            btnSearchCustomer.FlatAppearance.BorderSize = 0;
            btnSearchCustomer.FlatStyle = FlatStyle.Flat;
            btnSearchCustomer.ImageAlign = ContentAlignment.MiddleLeft;
            btnSearchCustomer.Location = new Point(599, 20);
            btnSearchCustomer.Name = "btnSearchCustomer";
            btnSearchCustomer.Size = new Size(70, 25);
            btnSearchCustomer.TabIndex = 7;
            btnSearchCustomer.Text = "Search";
            btnSearchCustomer.UseVisualStyleBackColor = false;
            btnSearchCustomer.Click += button9_Click;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(216, 19);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(131, 21);
            txtFullName.TabIndex = 5;
            txtFullName.Text = "";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(273, 53);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(99, 21);
            txtPhoneNumber.TabIndex = 5;
            txtPhoneNumber.Text = "";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(74, 53);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(99, 21);
            txtEmail.TabIndex = 5;
            txtEmail.Text = "";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(179, 56);
            label7.Name = "label7";
            label7.Size = new Size(88, 15);
            label7.TabIndex = 4;
            label7.Text = "Phone Number";
            // 
            // txtCCCD
            // 
            txtCCCD.Location = new Point(398, 19);
            txtCCCD.Name = "txtCCCD";
            txtCCCD.Size = new Size(123, 21);
            txtCCCD.TabIndex = 5;
            txtCCCD.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 56);
            label6.Name = "label6";
            label6.Size = new Size(36, 15);
            label6.TabIndex = 4;
            label6.Text = "Email";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(353, 22);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 4;
            label5.Text = "CCCD";
            // 
            // txtID
            // 
            txtID.Location = new Point(49, 22);
            txtID.Name = "txtID";
            txtID.Size = new Size(99, 21);
            txtID.TabIndex = 5;
            txtID.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(154, 22);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 4;
            label2.Text = "Fullname";
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(25, 25);
            lblID.Name = "lblID";
            lblID.Size = new Size(18, 15);
            lblID.TabIndex = 4;
            lblID.Text = "ID";
            // 
            // Customer_Management
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(764, 561);
            Controls.Add(panel2);
            Name = "Customer_Management";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PetCareX";
            Load += Service_Form_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private DataGridView dataGridView1;
        private Panel panel2;
        private Label label2;
        private Label lblID;
        private RichTextBox txtFullName;
        private RichTextBox txtID;
        private Button btnSearchCustomer;
        private Button btnAddCustomer;
        private RichTextBox txtPhoneNumber;
        private RichTextBox txtEmail;
        private Label label7;
        private RichTextBox txtCCCD;
        private Label label6;
        private Label label5;
    }
}
