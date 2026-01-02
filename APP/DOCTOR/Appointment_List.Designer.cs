using System.Drawing;
using System.Windows.Forms;
namespace DOCTOR
{
    partial class Appointment_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Appointment_List));
            this.panel2 = new System.Windows.Forms.Panel();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.frmDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblRole = new System.Windows.Forms.RichTextBox();
            this.lblEmployeeName = new System.Windows.Forms.RichTextBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.lblEmployeeID = new System.Windows.Forms.RichTextBox();
            this.btnAppointmentList = new System.Windows.Forms.Button();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPrescription = new System.Windows.Forms.Button();
            this.btnExaminationRecord = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnChangepassword = new System.Windows.Forms.Button();
            this.btnPetHistory = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.btnSurgeryRecord = new System.Windows.Forms.Button();
            this.btnVaccination = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.toDate);
            this.panel2.Controls.Add(this.frmDate);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.dataGridView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(195, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 561);
            this.panel2.TabIndex = 5;
            // 
            // toDate
            // 
            this.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.toDate.Location = new System.Drawing.Point(305, 22);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(103, 20);
            this.toDate.TabIndex = 8;
            // 
            // frmDate
            // 
            this.frmDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.frmDate.Location = new System.Drawing.Point(101, 22);
            this.frmDate.Name = "frmDate";
            this.frmDate.Size = new System.Drawing.Size(103, 20);
            this.frmDate.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(455, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 25);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "From date";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(756, 491);
            this.dataGridView1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lblRole);
            this.panel1.Controls.Add(this.lblEmployeeName);
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Controls.Add(this.pictureBox5);
            this.panel1.Controls.Add(this.lblEmployeeID);
            this.panel1.Controls.Add(this.btnAppointmentList);
            this.panel1.Controls.Add(this.pictureBox8);
            this.panel1.Controls.Add(this.pictureBox6);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox4);
            this.panel1.Controls.Add(this.pictureBox12);
            this.panel1.Controls.Add(this.pictureBox11);
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox9);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnPrescription);
            this.panel1.Controls.Add(this.btnExaminationRecord);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnChangepassword);
            this.panel1.Controls.Add(this.btnPetHistory);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.btnSurgeryRecord);
            this.panel1.Controls.Add(this.btnVaccination);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 561);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // lblRole
            // 
            this.lblRole.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblRole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRole.Location = new System.Drawing.Point(69, 84);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(101, 17);
            this.lblRole.TabIndex = 5;
            this.lblRole.Text = "Role";
            // 
            // lblEmployeeName
            // 
            this.lblEmployeeName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEmployeeName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblEmployeeName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeName.Location = new System.Drawing.Point(69, 56);
            this.lblEmployeeName.Name = "lblEmployeeName";
            this.lblEmployeeName.Size = new System.Drawing.Size(101, 17);
            this.lblEmployeeName.TabIndex = 5;
            this.lblEmployeeName.Text = "EmployeeName";
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(169, 412);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(20, 20);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 3;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(12, 412);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(28, 24);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // lblEmployeeID
            // 
            this.lblEmployeeID.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEmployeeID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblEmployeeID.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmployeeID.Location = new System.Drawing.Point(69, 23);
            this.lblEmployeeID.Name = "lblEmployeeID";
            this.lblEmployeeID.Size = new System.Drawing.Size(120, 22);
            this.lblEmployeeID.TabIndex = 5;
            this.lblEmployeeID.Text = "EmployeeID";
            // 
            // btnAppointmentList
            // 
            this.btnAppointmentList.BackColor = System.Drawing.SystemColors.Window;
            this.btnAppointmentList.FlatAppearance.BorderSize = 0;
            this.btnAppointmentList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAppointmentList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAppointmentList.Location = new System.Drawing.Point(41, 132);
            this.btnAppointmentList.Name = "btnAppointmentList";
            this.btnAppointmentList.Size = new System.Drawing.Size(148, 25);
            this.btnAppointmentList.TabIndex = 1;
            this.btnAppointmentList.Text = "Appointment List";
            this.btnAppointmentList.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAppointmentList.UseVisualStyleBackColor = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(11, 26);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(54, 51);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 3;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(35, 451);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(20, 20);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 3;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(8, 132);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(35, 485);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 20);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox12.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox12.Image")));
            this.pictureBox12.Location = new System.Drawing.Point(11, 368);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(27, 23);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 3;
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox11.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox11.Image")));
            this.pictureBox11.Location = new System.Drawing.Point(12, 323);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(27, 23);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox11.TabIndex = 3;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(11, 276);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(28, 31);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(9, 228);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(30, 25);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 3;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 183);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 25);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnPrescription
            // 
            this.btnPrescription.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPrescription.FlatAppearance.BorderSize = 0;
            this.btnPrescription.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrescription.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrescription.Location = new System.Drawing.Point(41, 228);
            this.btnPrescription.Name = "btnPrescription";
            this.btnPrescription.Size = new System.Drawing.Size(138, 31);
            this.btnPrescription.TabIndex = 1;
            this.btnPrescription.Text = "Prescription";
            this.btnPrescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrescription.UseVisualStyleBackColor = false;
            // 
            // btnExaminationRecord
            // 
            this.btnExaminationRecord.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnExaminationRecord.FlatAppearance.BorderSize = 0;
            this.btnExaminationRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExaminationRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExaminationRecord.Location = new System.Drawing.Point(41, 183);
            this.btnExaminationRecord.Name = "btnExaminationRecord";
            this.btnExaminationRecord.Size = new System.Drawing.Size(128, 25);
            this.btnExaminationRecord.TabIndex = 1;
            this.btnExaminationRecord.Text = "Examination Record";
            this.btnExaminationRecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExaminationRecord.UseVisualStyleBackColor = false;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(73, 485);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(59, 26);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            // 
            // btnChangepassword
            // 
            this.btnChangepassword.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnChangepassword.FlatAppearance.BorderSize = 0;
            this.btnChangepassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangepassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChangepassword.Location = new System.Drawing.Point(61, 446);
            this.btnChangepassword.Name = "btnChangepassword";
            this.btnChangepassword.Size = new System.Drawing.Size(131, 33);
            this.btnChangepassword.TabIndex = 1;
            this.btnChangepassword.Text = "Change password";
            this.btnChangepassword.UseVisualStyleBackColor = false;
            // 
            // btnPetHistory
            // 
            this.btnPetHistory.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPetHistory.FlatAppearance.BorderSize = 0;
            this.btnPetHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPetHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPetHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPetHistory.Location = new System.Drawing.Point(44, 368);
            this.btnPetHistory.Name = "btnPetHistory";
            this.btnPetHistory.Size = new System.Drawing.Size(125, 31);
            this.btnPetHistory.TabIndex = 1;
            this.btnPetHistory.Text = "Pet History";
            this.btnPetHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPetHistory.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(46, 409);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(117, 31);
            this.button7.TabIndex = 1;
            this.button7.Text = "Profile";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.UseVisualStyleBackColor = false;
            // 
            // btnSurgeryRecord
            // 
            this.btnSurgeryRecord.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSurgeryRecord.FlatAppearance.BorderSize = 0;
            this.btnSurgeryRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSurgeryRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSurgeryRecord.Location = new System.Drawing.Point(45, 323);
            this.btnSurgeryRecord.Name = "btnSurgeryRecord";
            this.btnSurgeryRecord.Size = new System.Drawing.Size(125, 31);
            this.btnSurgeryRecord.TabIndex = 1;
            this.btnSurgeryRecord.Text = "Surgery Record";
            this.btnSurgeryRecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSurgeryRecord.UseVisualStyleBackColor = false;
            // 
            // btnVaccination
            // 
            this.btnVaccination.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnVaccination.FlatAppearance.BorderSize = 0;
            this.btnVaccination.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVaccination.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVaccination.Location = new System.Drawing.Point(41, 276);
            this.btnVaccination.Name = "btnVaccination";
            this.btnVaccination.Size = new System.Drawing.Size(125, 31);
            this.btnVaccination.TabIndex = 1;
            this.btnVaccination.Text = "Vaccination";
            this.btnVaccination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVaccination.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 122);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(193, 41);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // Appointment_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Appointment_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PetCare";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel2;
        private DateTimePicker toDate;
        private DateTimePicker frmDate;
        private Button btnSearch;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView1;
        private Panel panel1;
        private RichTextBox lblRole;
        private RichTextBox lblEmployeeName;
        private PictureBox pictureBox7;
        private PictureBox pictureBox5;
        private RichTextBox lblEmployeeID;
        private Button btnAppointmentList;
        private PictureBox pictureBox8;
        private PictureBox pictureBox6;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox12;
        private PictureBox pictureBox11;
        private PictureBox pictureBox3;
        private PictureBox pictureBox9;
        private PictureBox pictureBox1;
        private Button btnPrescription;
        private Button btnExaminationRecord;
        private Button btnLogout;
        private Button btnChangepassword;
        private Button btnPetHistory;
        private Button button7;
        private Button btnSurgeryRecord;
        private Button btnVaccination;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
