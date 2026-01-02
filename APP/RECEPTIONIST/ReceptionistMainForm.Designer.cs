namespace RECEPTIONIST
{
    partial class ReceptionistMainForm
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
            PictureBox pictureBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceptionistMainForm));
            splitContainer1 = new SplitContainer();
            pnlMainMenu = new Panel();
            panel2 = new Panel();
            lblUsername = new Label();
            lblRole = new Label();
            lblEmployeeName = new Label();
            lblEmployeeID = new Label();
            btnLogOut = new Button();
            btnChangePassword = new Button();
            btnPetManagement = new Button();
            btnCustomerManagement = new Button();
            btnAppointment = new Button();
            pictureBox10 = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlMainMenu.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(52, 52);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(pnlMainMenu);
            splitContainer1.Size = new Size(984, 561);
            splitContainer1.SplitterDistance = 220;
            splitContainer1.TabIndex = 0;
            // 
            // pnlMainMenu
            // 
            pnlMainMenu.BackColor = SystemColors.ActiveCaption;
            pnlMainMenu.Controls.Add(panel2);
            pnlMainMenu.Controls.Add(btnLogOut);
            pnlMainMenu.Controls.Add(btnChangePassword);
            pnlMainMenu.Controls.Add(btnPetManagement);
            pnlMainMenu.Controls.Add(btnCustomerManagement);
            pnlMainMenu.Controls.Add(btnAppointment);
            pnlMainMenu.Controls.Add(pictureBox10);
            pnlMainMenu.Dock = DockStyle.Fill;
            pnlMainMenu.Location = new Point(0, 0);
            pnlMainMenu.Name = "pnlMainMenu";
            pnlMainMenu.Size = new Size(220, 561);
            pnlMainMenu.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblUsername);
            panel2.Controls.Add(lblRole);
            panel2.Controls.Add(lblEmployeeName);
            panel2.Controls.Add(lblEmployeeID);
            panel2.Controls.Add(pictureBox1);
            panel2.Location = new Point(25, 446);
            panel2.Name = "panel2";
            panel2.Size = new Size(170, 94);
            panel2.TabIndex = 9;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.Location = new Point(58, 73);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(69, 17);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRole.Location = new Point(58, 50);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(84, 17);
            lblRole.TabIndex = 1;
            lblRole.Text = "Receptionist";
            // 
            // lblEmployeeName
            // 
            lblEmployeeName.AutoSize = true;
            lblEmployeeName.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmployeeName.Location = new Point(58, 27);
            lblEmployeeName.Name = "lblEmployeeName";
            lblEmployeeName.Size = new Size(104, 17);
            lblEmployeeName.TabIndex = 1;
            lblEmployeeName.Text = "EmployeeName";
            // 
            // lblEmployeeID
            // 
            lblEmployeeID.AutoSize = true;
            lblEmployeeID.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmployeeID.Location = new Point(58, 4);
            lblEmployeeID.Name = "lblEmployeeID";
            lblEmployeeID.Size = new Size(22, 17);
            lblEmployeeID.TabIndex = 1;
            lblEmployeeID.Text = "ID";
            // 
            // btnLogOut
            // 
            btnLogOut.FlatStyle = FlatStyle.Flat;
            btnLogOut.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLogOut.Image = (Image)resources.GetObject("btnLogOut.Image");
            btnLogOut.ImageAlign = ContentAlignment.MiddleLeft;
            btnLogOut.Location = new Point(0, 371);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(220, 39);
            btnLogOut.TabIndex = 8;
            btnLogOut.Text = "              Log out";
            btnLogOut.TextAlign = ContentAlignment.MiddleLeft;
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogout_Click;
            // 
            // btnChangePassword
            // 
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnChangePassword.Image = (Image)resources.GetObject("btnChangePassword.Image");
            btnChangePassword.ImageAlign = ContentAlignment.MiddleLeft;
            btnChangePassword.Location = new Point(0, 318);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(220, 39);
            btnChangePassword.TabIndex = 8;
            btnChangePassword.Text = "              Change password";
            btnChangePassword.TextAlign = ContentAlignment.MiddleLeft;
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += btnChangePassword_Click;
            // 
            // btnPetManagement
            // 
            btnPetManagement.FlatStyle = FlatStyle.Flat;
            btnPetManagement.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPetManagement.Image = (Image)resources.GetObject("btnPetManagement.Image");
            btnPetManagement.ImageAlign = ContentAlignment.MiddleLeft;
            btnPetManagement.Location = new Point(0, 265);
            btnPetManagement.Name = "btnPetManagement";
            btnPetManagement.Size = new Size(220, 39);
            btnPetManagement.TabIndex = 8;
            btnPetManagement.Text = "              Pet Management";
            btnPetManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnPetManagement.UseVisualStyleBackColor = true;
            btnPetManagement.Click += btnPetManagement_Click;
            // 
            // btnCustomerManagement
            // 
            btnCustomerManagement.FlatStyle = FlatStyle.Flat;
            btnCustomerManagement.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCustomerManagement.Image = (Image)resources.GetObject("btnCustomerManagement.Image");
            btnCustomerManagement.ImageAlign = ContentAlignment.MiddleLeft;
            btnCustomerManagement.Location = new Point(0, 212);
            btnCustomerManagement.Name = "btnCustomerManagement";
            btnCustomerManagement.Size = new Size(220, 39);
            btnCustomerManagement.TabIndex = 8;
            btnCustomerManagement.Text = "              Customer Management";
            btnCustomerManagement.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomerManagement.UseVisualStyleBackColor = true;
            btnCustomerManagement.Click += btnCustomerManagement_Click;
            // 
            // btnAppointment
            // 
            btnAppointment.FlatStyle = FlatStyle.Flat;
            btnAppointment.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAppointment.Image = (Image)resources.GetObject("btnAppointment.Image");
            btnAppointment.ImageAlign = ContentAlignment.MiddleLeft;
            btnAppointment.Location = new Point(0, 162);
            btnAppointment.Name = "btnAppointment";
            btnAppointment.Size = new Size(220, 39);
            btnAppointment.TabIndex = 8;
            btnAppointment.Text = "              Appointment";
            btnAppointment.TextAlign = ContentAlignment.MiddleLeft;
            btnAppointment.UseVisualStyleBackColor = true;
            btnAppointment.Click += btnAppointment_Click;
            // 
            // pictureBox10
            // 
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(48, 8);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(101, 78);
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox10.TabIndex = 6;
            pictureBox10.TabStop = false;
            // 
            // ReceptionistMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 561);
            Controls.Add(splitContainer1);
            Name = "ReceptionistMainForm";
            Text = "Receptionist";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlMainMenu.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel pnlMainMenu;
        private PictureBox pictureBox10;
        private Button btnAppointment;
        private Button btnCustomerManagement;
        private Button btnPetManagement;
        private Button btnChangePassword;
        private Button btnLogOut;
        private Panel panel2;
        private PictureBox pictureBox1;
        public Label lblUsername;
        public Label lblRole;
        public Label lblEmployeeName;
        public Label lblEmployeeID;
    }
}