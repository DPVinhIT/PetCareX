namespace NURSE
{
    partial class frmNurseNursingWorklist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNurseNursingWorklist));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlDashBoard = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.btnDropDownProfile = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.btnNursingWorklist = new System.Windows.Forms.Button();
            this.btnInpationManagement = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.dtpFilter_DateModified = new System.Windows.Forms.DateTimePicker();
            this.lblFilter_DateModified = new System.Windows.Forms.Label();
            this.lblFilter_Name = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TaskName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Priority = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Complete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlDashBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlDashBoard);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(984, 561);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 2;
            // 
            // pnlDashBoard
            // 
            this.pnlDashBoard.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlDashBoard.Controls.Add(this.pictureBox2);
            this.pnlDashBoard.Controls.Add(this.btnLogout);
            this.pnlDashBoard.Controls.Add(this.btnChangePassword);
            this.pnlDashBoard.Controls.Add(this.btnDropDownProfile);
            this.pnlDashBoard.Controls.Add(this.panel1);
            this.pnlDashBoard.Controls.Add(this.btnNursingWorklist);
            this.pnlDashBoard.Controls.Add(this.btnInpationManagement);
            this.pnlDashBoard.Controls.Add(this.btnProfile);
            this.pnlDashBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashBoard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashBoard.Name = "pnlDashBoard";
            this.pnlDashBoard.Size = new System.Drawing.Size(220, 561);
            this.pnlDashBoard.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(49, 20);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(100, 74);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(49, 320);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(168, 24);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "            Log out";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePassword.Image = ((System.Drawing.Image)(resources.GetObject("btnChangePassword.Image")));
            this.btnChangePassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangePassword.Location = new System.Drawing.Point(49, 290);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(168, 24);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "            Change password";
            this.btnChangePassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangePassword.UseVisualStyleBackColor = true;
            // 
            // btnDropDownProfile
            // 
            this.btnDropDownProfile.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDropDownProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDropDownProfile.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDropDownProfile.Image = global::NURSE.Properties.Resources.icons8_dropdown_24;
            this.btnDropDownProfile.Location = new System.Drawing.Point(192, 255);
            this.btnDropDownProfile.Name = "btnDropDownProfile";
            this.btnDropDownProfile.Size = new System.Drawing.Size(25, 25);
            this.btnDropDownProfile.TabIndex = 5;
            this.btnDropDownProfile.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblName);
            this.panel1.Controls.Add(this.lblRole);
            this.panel1.Location = new System.Drawing.Point(20, 495);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(178, 52);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 52);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(58, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(118, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "EmployeeName";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRole.Location = new System.Drawing.Point(58, 27);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(45, 15);
            this.lblRole.TabIndex = 0;
            this.lblRole.Text = "Nurse";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNursingWorklist
            // 
            this.btnNursingWorklist.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnNursingWorklist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNursingWorklist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNursingWorklist.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnNursingWorklist.Image = ((System.Drawing.Image)(resources.GetObject("btnNursingWorklist.Image")));
            this.btnNursingWorklist.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNursingWorklist.Location = new System.Drawing.Point(0, 153);
            this.btnNursingWorklist.Name = "btnNursingWorklist";
            this.btnNursingWorklist.Size = new System.Drawing.Size(220, 35);
            this.btnNursingWorklist.TabIndex = 2;
            this.btnNursingWorklist.Tag = "Treatment";
            this.btnNursingWorklist.Text = "            Nursing Worklist";
            this.btnNursingWorklist.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNursingWorklist.UseVisualStyleBackColor = false;
            this.btnNursingWorklist.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInpationManagement
            // 
            this.btnInpationManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInpationManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInpationManagement.Image = ((System.Drawing.Image)(resources.GetObject("btnInpationManagement.Image")));
            this.btnInpationManagement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInpationManagement.Location = new System.Drawing.Point(0, 201);
            this.btnInpationManagement.Name = "btnInpationManagement";
            this.btnInpationManagement.Size = new System.Drawing.Size(220, 35);
            this.btnInpationManagement.TabIndex = 3;
            this.btnInpationManagement.Text = "            Inpatients";
            this.btnInpationManagement.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInpationManagement.UseVisualStyleBackColor = true;
            // 
            // btnProfile
            // 
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProfile.Image = ((System.Drawing.Image)(resources.GetObject("btnProfile.Image")));
            this.btnProfile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.Location = new System.Drawing.Point(0, 249);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(220, 35);
            this.btnProfile.TabIndex = 3;
            this.btnProfile.Text = "            Profile";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer2.Size = new System.Drawing.Size(760, 561);
            this.splitContainer2.SplitterDistance = 71;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFilter);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.richTextBox2);
            this.groupBox1.Controls.Add(this.dtpFilter_DateModified);
            this.groupBox1.Controls.Add(this.lblFilter_DateModified);
            this.groupBox1.Controls.Add(this.lblFilter_Name);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 71);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter:";
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFilter.Location = new System.Drawing.Point(649, 26);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 7;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.comboBox1.Location = new System.Drawing.Point(501, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Priority:";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(105, 26);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(100, 22);
            this.richTextBox2.TabIndex = 1;
            this.richTextBox2.Text = "";
            // 
            // dtpFilter_DateModified
            // 
            this.dtpFilter_DateModified.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilter_DateModified.Location = new System.Drawing.Point(296, 26);
            this.dtpFilter_DateModified.Name = "dtpFilter_DateModified";
            this.dtpFilter_DateModified.Size = new System.Drawing.Size(120, 22);
            this.dtpFilter_DateModified.TabIndex = 4;
            // 
            // lblFilter_DateModified
            // 
            this.lblFilter_DateModified.AutoSize = true;
            this.lblFilter_DateModified.Location = new System.Drawing.Point(225, 29);
            this.lblFilter_DateModified.Name = "lblFilter_DateModified";
            this.lblFilter_DateModified.Size = new System.Drawing.Size(65, 16);
            this.lblFilter_DateModified.TabIndex = 2;
            this.lblFilter_DateModified.Text = "Due date:";
            // 
            // lblFilter_Name
            // 
            this.lblFilter_Name.AutoSize = true;
            this.lblFilter_Name.Location = new System.Drawing.Point(21, 29);
            this.lblFilter_Name.Name = "lblFilter_Name";
            this.lblFilter_Name.Size = new System.Drawing.Size(78, 16);
            this.lblFilter_Name.TabIndex = 0;
            this.lblFilter_Name.Text = "Task name:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TaskName,
            this.DueDate,
            this.Priority,
            this.Complete});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(760, 486);
            this.dataGridView1.TabIndex = 0;
            // 
            // TaskName
            // 
            this.TaskName.HeaderText = "Task name";
            this.TaskName.Name = "TaskName";
            this.TaskName.ReadOnly = true;
            // 
            // DueDate
            // 
            this.DueDate.HeaderText = "Due date";
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;
            // 
            // Priority
            // 
            this.Priority.HeaderText = "Priority";
            this.Priority.Name = "Priority";
            this.Priority.ReadOnly = true;
            // 
            // Complete
            // 
            this.Complete.HeaderText = "Complete";
            this.Complete.Name = "Complete";
            this.Complete.ReadOnly = true;
            this.Complete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Complete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmNurseNursingWorklist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmNurseNursingWorklist";
            this.Text = "Nurse";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlDashBoard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlDashBoard;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnDropDownProfile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Button btnInpationManagement;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.DateTimePicker dtpFilter_DateModified;
        private System.Windows.Forms.Label lblFilter_DateModified;
        private System.Windows.Forms.Label lblFilter_Name;
        private System.Windows.Forms.Button btnNursingWorklist;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Priority;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Complete;
    }
}