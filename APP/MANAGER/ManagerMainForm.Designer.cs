namespace MANAGER
{
    partial class ManagerMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerMainForm));
            this.panelMain = new System.Windows.Forms.Panel();
            this.pnlDashBoard = new System.Windows.Forms.Panel();
            this.pnlUserInfo = new System.Windows.Forms.Panel();
            this.lblEmployeeID = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblEmployeeName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnWorkSchedule = new System.Windows.Forms.Button();
            this.btnsStatistical = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnLeavingApproval = new System.Windows.Forms.Button();
            this.btnPromotionManagement = new System.Windows.Forms.Button();
            this.btnStaffManagement = new System.Windows.Forms.Button();
            this.lblUsername = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.pnlDashBoard.SuspendLayout();
            this.pnlUserInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.pnlDashBoard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(984, 561);
            this.panelMain.TabIndex = 0;
            // 
            // pnlDashBoard
            // 
            this.pnlDashBoard.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlDashBoard.Controls.Add(this.pnlUserInfo);
            this.pnlDashBoard.Controls.Add(this.btnWorkSchedule);
            this.pnlDashBoard.Controls.Add(this.btnsStatistical);
            this.pnlDashBoard.Controls.Add(this.btnLogout);
            this.pnlDashBoard.Controls.Add(this.btnChangePassword);
            this.pnlDashBoard.Controls.Add(this.btnLeavingApproval);
            this.pnlDashBoard.Controls.Add(this.btnPromotionManagement);
            this.pnlDashBoard.Controls.Add(this.btnStaffManagement);
            this.pnlDashBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDashBoard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashBoard.Name = "pnlDashBoard";
            this.pnlDashBoard.Size = new System.Drawing.Size(220, 561);
            this.pnlDashBoard.TabIndex = 6;
            // 
            // pnlUserInfo
            // 
            this.pnlUserInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlUserInfo.Controls.Add(this.lblUsername);
            this.pnlUserInfo.Controls.Add(this.lblEmployeeID);
            this.pnlUserInfo.Controls.Add(this.pictureBox1);
            this.pnlUserInfo.Controls.Add(this.lblEmployeeName);
            this.pnlUserInfo.Controls.Add(this.lblRole);
            this.pnlUserInfo.Location = new System.Drawing.Point(18, 26);
            this.pnlUserInfo.Name = "pnlUserInfo";
            this.pnlUserInfo.Size = new System.Drawing.Size(186, 128);
            this.pnlUserInfo.TabIndex = 5;
            // 
            // lblEmployeeID
            // 
            this.lblEmployeeID.AutoSize = true;
            this.lblEmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeID.Location = new System.Drawing.Point(66, 15);
            this.lblEmployeeID.Name = "lblEmployeeID";
            this.lblEmployeeID.Size = new System.Drawing.Size(92, 16);
            this.lblEmployeeID.TabIndex = 3;
            this.lblEmployeeID.Text = "EmployeeID";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 49);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.AutoSize = true;
            this.lblEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployeeName.Location = new System.Drawing.Point(66, 41);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(118, 16);
            this.lblEmployeeName.TabIndex = 1;
            this.lblEmployeeName.Text = "EmployeeName";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(66, 67);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(37, 15);
            this.lblRole.TabIndex = 0;
            this.lblRole.Text = "Role";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnWorkSchedule
            // 
            this.btnWorkSchedule.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnWorkSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWorkSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWorkSchedule.Image = ((System.Drawing.Image)(resources.GetObject("btnWorkSchedule.Image")));
            this.btnWorkSchedule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWorkSchedule.Location = new System.Drawing.Point(0, 201);
            this.btnWorkSchedule.Name = "btnWorkSchedule";
            this.btnWorkSchedule.Size = new System.Drawing.Size(220, 35);
            this.btnWorkSchedule.TabIndex = 3;
            this.btnWorkSchedule.Text = "            Work Schedule";
            this.btnWorkSchedule.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWorkSchedule.UseVisualStyleBackColor = false;
            this.btnWorkSchedule.Click += new System.EventHandler(this.btnWorkSchedule_Click);
            // 
            // btnsStatistical
            // 
            this.btnsStatistical.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnsStatistical.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsStatistical.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsStatistical.Image = ((System.Drawing.Image)(resources.GetObject("btnsStatistical.Image")));
            this.btnsStatistical.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsStatistical.Location = new System.Drawing.Point(0, 325);
            this.btnsStatistical.Name = "btnsStatistical";
            this.btnsStatistical.Size = new System.Drawing.Size(220, 35);
            this.btnsStatistical.TabIndex = 9;
            this.btnsStatistical.Text = "            Statistics";
            this.btnsStatistical.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnsStatistical.UseVisualStyleBackColor = false;
            this.btnsStatistical.Click += new System.EventHandler(this.btnsStatistical_Click_Click_1);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(0, 417);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(220, 35);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "            Log out";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("btnChangePassword.Image")));
            this.btnChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangePassword.Location = new System.Drawing.Point(0, 371);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(220, 36);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "           Change password";
            this.btnChangePassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // btnLeavingApproval
            // 
            this.btnLeavingApproval.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLeavingApproval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeavingApproval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLeavingApproval.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnLeavingApproval.Image = ((System.Drawing.Image)(resources.GetObject("btnLeavingApproval.Image")));
            this.btnLeavingApproval.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLeavingApproval.Location = new System.Drawing.Point(0, 242);
            this.btnLeavingApproval.Name = "btnLeavingApproval";
            this.btnLeavingApproval.Size = new System.Drawing.Size(220, 35);
            this.btnLeavingApproval.TabIndex = 2;
            this.btnLeavingApproval.Tag = "Treatment";
            this.btnLeavingApproval.Text = "            Leaving Approval";
            this.btnLeavingApproval.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLeavingApproval.UseVisualStyleBackColor = false;
            this.btnLeavingApproval.Click += new System.EventHandler(this.btnLeavingApproval_Click);
            // 
            // btnPromotionManagement
            // 
            this.btnPromotionManagement.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPromotionManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromotionManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPromotionManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnPromotionManagement.Image")));
            this.btnPromotionManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPromotionManagement.Location = new System.Drawing.Point(0, 160);
            this.btnPromotionManagement.Name = "btnPromotionManagement";
            this.btnPromotionManagement.Size = new System.Drawing.Size(220, 35);
            this.btnPromotionManagement.TabIndex = 3;
            this.btnPromotionManagement.Text = "            Promotion Management";
            this.btnPromotionManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPromotionManagement.UseVisualStyleBackColor = false;
            this.btnPromotionManagement.Click += new System.EventHandler(this.btnPromotionManagement_Click);
            // 
            // btnStaffManagement
            // 
            this.btnStaffManagement.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnStaffManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaffManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaffManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnStaffManagement.Image")));
            this.btnStaffManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaffManagement.Location = new System.Drawing.Point(0, 283);
            this.btnStaffManagement.Name = "btnStaffManagement";
            this.btnStaffManagement.Size = new System.Drawing.Size(220, 35);
            this.btnStaffManagement.TabIndex = 3;
            this.btnStaffManagement.Text = "            Staff Management";
            this.btnStaffManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaffManagement.UseVisualStyleBackColor = false;
            this.btnStaffManagement.Click += new System.EventHandler(this.btnStaffManagement_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(66, 94);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(73, 15);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManagerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panelMain);
            this.Name = "ManagerMainForm";
            this.Text = "ManagerMainForm";
            this.panelMain.ResumeLayout(false);
            this.pnlDashBoard.ResumeLayout(false);
            this.pnlUserInfo.ResumeLayout(false);
            this.pnlUserInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblEmployeeID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblEmployeeName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Panel pnlUserInfo;
        private System.Windows.Forms.Panel pnlDashBoard;
        private System.Windows.Forms.Button btnWorkSchedule;
        private System.Windows.Forms.Button btnsStatistical;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnLeavingApproval;
        private System.Windows.Forms.Button btnPromotionManagement;
        private System.Windows.Forms.Button btnStaffManagement;
        private System.Windows.Forms.Label lblUsername;
    }
}