namespace RECEPTIONIST
{
    partial class Add_Customer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel2 = new Panel();
            dtpBirthday = new DateTimePicker();
            btnAddCustomer = new Button();
            cboGender = new ComboBox();
            txtEmail = new RichTextBox();
            txtCCCD = new RichTextBox();
            txtPhoneNumber = new RichTextBox();
            textBox4 = new TextBox();
            txtFullName = new RichTextBox();
            textBox1 = new TextBox();
            textBox6 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox9 = new TextBox();
            textBox8 = new TextBox();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.BackColor = SystemColors.Window;
            panel2.Controls.Add(dtpBirthday);
            panel2.Controls.Add(btnAddCustomer);
            panel2.Controls.Add(cboGender);
            panel2.Controls.Add(txtEmail);
            panel2.Controls.Add(txtCCCD);
            panel2.Controls.Add(txtPhoneNumber);
            panel2.Controls.Add(textBox4);
            panel2.Controls.Add(txtFullName);
            panel2.Controls.Add(textBox1);
            panel2.Controls.Add(textBox6);
            panel2.Controls.Add(textBox2);
            panel2.Controls.Add(textBox3);
            panel2.Controls.Add(textBox9);
            panel2.Controls.Add(textBox8);
            panel2.Dock = DockStyle.Fill;
            panel2.ForeColor = SystemColors.Window;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(984, 561);
            panel2.TabIndex = 7;
            // 
            // dtpBirthday
            // 
            dtpBirthday.Format = DateTimePickerFormat.Short;
            dtpBirthday.Location = new Point(393, 361);
            dtpBirthday.Name = "dtpBirthday";
            dtpBirthday.Size = new Size(199, 23);
            dtpBirthday.TabIndex = 9;
            dtpBirthday.Value = new DateTime(2005, 5, 30, 22, 57, 0, 0);
            // 
            // btnAddCustomer
            // 
            btnAddCustomer.BackColor = SystemColors.ActiveCaption;
            btnAddCustomer.FlatAppearance.BorderSize = 0;
            btnAddCustomer.FlatStyle = FlatStyle.Flat;
            btnAddCustomer.ForeColor = SystemColors.ActiveCaptionText;
            btnAddCustomer.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddCustomer.Location = new Point(457, 428);
            btnAddCustomer.Name = "btnAddCustomer";
            btnAddCustomer.Size = new Size(70, 25);
            btnAddCustomer.TabIndex = 8;
            btnAddCustomer.Text = "Add";
            btnAddCustomer.UseVisualStyleBackColor = false;
            btnAddCustomer.Click += button9_Click;
            // 
            // cboGender
            // 
            cboGender.FormattingEnabled = true;
            cboGender.Items.AddRange(new object[] { "Surgery", "Examination", "Vaccination", "Vaccination Package" });
            cboGender.Location = new Point(393, 215);
            cboGender.Name = "cboGender";
            cboGender.Size = new Size(199, 23);
            cboGender.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(393, 263);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(199, 21);
            txtEmail.TabIndex = 3;
            txtEmail.Text = "";
            // 
            // txtCCCD
            // 
            txtCCCD.Location = new Point(393, 313);
            txtCCCD.Name = "txtCCCD";
            txtCCCD.Size = new Size(199, 21);
            txtCCCD.TabIndex = 3;
            txtCCCD.Text = "";
            // 
            // txtPhoneNumber
            // 
            txtPhoneNumber.Location = new Point(393, 170);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(199, 21);
            txtPhoneNumber.TabIndex = 3;
            txtPhoneNumber.Text = "";
            // 
            // textBox4
            // 
            textBox4.BackColor = SystemColors.Window;
            textBox4.BorderStyle = BorderStyle.None;
            textBox4.Font = new Font("Segoe UI", 10F);
            textBox4.Location = new Point(279, 365);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(108, 18);
            textBox4.TabIndex = 2;
            textBox4.Text = "Birthday";
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(393, 124);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(199, 21);
            txtFullName.TabIndex = 3;
            txtFullName.Text = "";
            // 
            // textBox1
            // 
            textBox1.BackColor = SystemColors.Window;
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Font = new Font("Segoe UI", 10F);
            textBox1.Location = new Point(279, 316);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(108, 18);
            textBox1.TabIndex = 2;
            textBox1.Text = "CCCD";
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Segoe UI", 20F);
            textBox6.Location = new Point(301, 59);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(399, 36);
            textBox6.TabIndex = 2;
            textBox6.Text = "CUSTOMER REGISTRATION";
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.Window;
            textBox2.BorderStyle = BorderStyle.None;
            textBox2.Font = new Font("Segoe UI", 10F);
            textBox2.Location = new Point(279, 171);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(108, 18);
            textBox2.TabIndex = 2;
            textBox2.Text = "Phone number";
            // 
            // textBox3
            // 
            textBox3.BackColor = SystemColors.Window;
            textBox3.BorderStyle = BorderStyle.None;
            textBox3.Font = new Font("Segoe UI", 10F);
            textBox3.Location = new Point(279, 219);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(108, 18);
            textBox3.TabIndex = 2;
            textBox3.Text = "Gender";
            // 
            // textBox9
            // 
            textBox9.BackColor = SystemColors.Window;
            textBox9.BorderStyle = BorderStyle.None;
            textBox9.Font = new Font("Segoe UI", 10F);
            textBox9.Location = new Point(279, 264);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(108, 18);
            textBox9.TabIndex = 2;
            textBox9.Text = "Email";
            // 
            // textBox8
            // 
            textBox8.BackColor = SystemColors.Window;
            textBox8.BorderStyle = BorderStyle.None;
            textBox8.Font = new Font("Segoe UI", 10F);
            textBox8.Location = new Point(279, 125);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(108, 18);
            textBox8.TabIndex = 2;
            textBox8.Text = "Fullname";
            // 
            // Add_Customer
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(984, 561);
            Controls.Add(panel2);
            Name = "Add_Customer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add_Customer";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Panel panel2;
        private Button btnAddCustomer;
        private RichTextBox txtCCCD;
        private RichTextBox txtFullName;
        private TextBox textBox1;
        private TextBox textBox6;
        private TextBox textBox9;
        private TextBox textBox8;
        private RichTextBox txtPhoneNumber;
        private TextBox textBox2;
        private DateTimePicker dtpBirthday;
        private ComboBox cboGender;
        private RichTextBox txtEmail;
        private TextBox textBox4;
        private TextBox textBox3;
    }
}