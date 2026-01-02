namespace RECEPTIONIST
{
    partial class Appointment_Form
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
            textBox6 = new TextBox();
            panel2 = new Panel();
            btnAppointmentRegister = new Button();
            dtpTime = new DateTimePicker();
            dtpDate = new DateTimePicker();
            txtFullName = new RichTextBox();
            txtCusID = new RichTextBox();
            textBox11 = new TextBox();
            textBox10 = new TextBox();
            textBox9 = new TextBox();
            textBox8 = new TextBox();
            textBox7 = new TextBox();
            txtServiceID = new RichTextBox();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Segoe UI", 20F);
            textBox6.Location = new Point(301, 59);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(399, 36);
            textBox6.TabIndex = 2;
            textBox6.Text = "APPOINTMENT FORM";
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // panel2
            // 
            panel2.Controls.Add(txtServiceID);
            panel2.Controls.Add(btnAppointmentRegister);
            panel2.Controls.Add(dtpTime);
            panel2.Controls.Add(dtpDate);
            panel2.Controls.Add(txtFullName);
            panel2.Controls.Add(txtCusID);
            panel2.Controls.Add(textBox6);
            panel2.Controls.Add(textBox11);
            panel2.Controls.Add(textBox10);
            panel2.Controls.Add(textBox9);
            panel2.Controls.Add(textBox8);
            panel2.Controls.Add(textBox7);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(984, 561);
            panel2.TabIndex = 3;
            // 
            // btnAppointmentRegister
            // 
            btnAppointmentRegister.BackColor = SystemColors.ActiveCaption;
            btnAppointmentRegister.FlatAppearance.BorderSize = 0;
            btnAppointmentRegister.FlatStyle = FlatStyle.Flat;
            btnAppointmentRegister.ImageAlign = ContentAlignment.MiddleLeft;
            btnAppointmentRegister.Location = new Point(465, 428);
            btnAppointmentRegister.Name = "btnAppointmentRegister";
            btnAppointmentRegister.Size = new Size(70, 25);
            btnAppointmentRegister.TabIndex = 8;
            btnAppointmentRegister.Text = "OK";
            btnAppointmentRegister.UseVisualStyleBackColor = false;
            btnAppointmentRegister.Click += btnAppointmentRegister_Click;
            // 
            // dtpTime
            // 
            dtpTime.Format = DateTimePickerFormat.Time;
            dtpTime.ImeMode = ImeMode.Off;
            dtpTime.Location = new Point(401, 309);
            dtpTime.Name = "dtpTime";
            dtpTime.Size = new Size(199, 23);
            dtpTime.TabIndex = 5;
            dtpTime.Value = new DateTime(2025, 12, 8, 19, 10, 0, 0);
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(401, 263);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(199, 23);
            dtpDate.TabIndex = 5;
            dtpDate.Value = new DateTime(2025, 12, 8, 19, 10, 0, 0);
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(401, 175);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(199, 21);
            txtFullName.TabIndex = 3;
            txtFullName.Text = "";
            // 
            // txtCusID
            // 
            txtCusID.Location = new Point(401, 139);
            txtCusID.Name = "txtCusID";
            txtCusID.Size = new Size(199, 21);
            txtCusID.TabIndex = 3;
            txtCusID.Text = "";
            // 
            // textBox11
            // 
            textBox11.BackColor = SystemColors.Window;
            textBox11.BorderStyle = BorderStyle.None;
            textBox11.Font = new Font("Segoe UI", 10F);
            textBox11.Location = new Point(287, 313);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(48, 18);
            textBox11.TabIndex = 2;
            textBox11.Text = "Time";
            // 
            // textBox10
            // 
            textBox10.BackColor = SystemColors.Window;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Font = new Font("Segoe UI", 10F);
            textBox10.Location = new Point(287, 267);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(108, 18);
            textBox10.TabIndex = 2;
            textBox10.Text = "Date";
            // 
            // textBox9
            // 
            textBox9.BackColor = SystemColors.Window;
            textBox9.BorderStyle = BorderStyle.None;
            textBox9.Font = new Font("Segoe UI", 10F);
            textBox9.Location = new Point(287, 215);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(108, 18);
            textBox9.TabIndex = 2;
            textBox9.Text = "Service ID";
            // 
            // textBox8
            // 
            textBox8.BackColor = SystemColors.Window;
            textBox8.BorderStyle = BorderStyle.None;
            textBox8.Font = new Font("Segoe UI", 10F);
            textBox8.Location = new Point(287, 176);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(108, 18);
            textBox8.TabIndex = 2;
            textBox8.Text = "Fullname";
            // 
            // textBox7
            // 
            textBox7.BackColor = SystemColors.Window;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Font = new Font("Segoe UI", 10F);
            textBox7.Location = new Point(287, 139);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(108, 18);
            textBox7.TabIndex = 2;
            textBox7.Text = "Customer ID";
            // 
            // txtServiceID
            // 
            txtServiceID.Location = new Point(401, 214);
            txtServiceID.Name = "txtServiceID";
            txtServiceID.Size = new Size(199, 21);
            txtServiceID.TabIndex = 9;
            txtServiceID.Text = "";
            // 
            // Appointment_Form
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Window;
            ClientSize = new Size(984, 561);
            Controls.Add(panel2);
            Name = "Appointment_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PetCare";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private RichTextBox txtFullName;
        private RichTextBox txtCusID;
        private Panel panel1;
        private Button button3;
        private TextBox textBox6;
        private Panel panel2;
        private TextBox textBox7;
        private TextBox textBox8;
        private RichTextBox richTextBox1;
        private TextBox textBox11;
        private TextBox textBox10;
        private TextBox textBox9;
        private ComboBox comboBox1;
        private RichTextBox richTextBox2;
        private DateTimePicker dtpTime;
        private DateTimePicker dtpDate;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox5;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox8;
        private Button button2;
        private Button button1;
        private Button button4;
        private Button button7;
        private Button button5;
        private FlowLayoutPanel flowLayoutPanel1;
        private RichTextBox richTextBox4;
        private RichTextBox richTextBox3;
        private PictureBox pictureBox9;
        private Button button6;
        private PictureBox pictureBox10;
        private Button btnAppointmentRegister;
        private RichTextBox txtServiceID;
    }
}