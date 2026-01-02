namespace RECEPTIONIST
{
    partial class ChangePassword
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.lblConfirmPass = new System.Windows.Forms.Label();
            this.lblNewPass = new System.Windows.Forms.Label();
            this.lblOldPass = new System.Windows.Forms.Label();
            this.txtOldPassword = new System.Windows.Forms.RichTextBox();
            this.txtConfirmPassword = new System.Windows.Forms.RichTextBox();
            this.txtNewPassword = new System.Windows.Forms.RichTextBox();
            this.lblPasswordChange = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnChangePass);
            this.panel2.Controls.Add(this.lblConfirmPass);
            this.panel2.Controls.Add(this.lblNewPass);
            this.panel2.Controls.Add(this.lblOldPass);
            this.panel2.Controls.Add(this.txtOldPassword);
            this.panel2.Controls.Add(this.txtConfirmPassword);
            this.panel2.Controls.Add(this.txtNewPassword);
            this.panel2.Controls.Add(this.lblPasswordChange);
            this.panel2.Location = new System.Drawing.Point(5, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 555);
            this.panel2.TabIndex = 2;
            // 
            // btnChangePass
            // 
            this.btnChangePass.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnChangePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePass.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChangePass.Location = new System.Drawing.Point(342, 319);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(75, 23);
            this.btnChangePass.TabIndex = 5;
            this.btnChangePass.Text = "OK";
            this.btnChangePass.UseVisualStyleBackColor = false;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.AutoSize = true;
            this.lblConfirmPass.Location = new System.Drawing.Point(130, 267);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(116, 13);
            this.lblConfirmPass.TabIndex = 4;
            this.lblConfirmPass.Text = "Confirm new password:";
            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = true;
            this.lblNewPass.Location = new System.Drawing.Point(130, 229);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(80, 13);
            this.lblNewPass.TabIndex = 4;
            this.lblNewPass.Text = "New password:";
            // 
            // lblOldPass
            // 
            this.lblOldPass.AutoSize = true;
            this.lblOldPass.Location = new System.Drawing.Point(130, 191);
            this.lblOldPass.Name = "lblOldPass";
            this.lblOldPass.Size = new System.Drawing.Size(74, 13);
            this.lblOldPass.TabIndex = 4;
            this.lblOldPass.Text = "Old password:";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(252, 186);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.Size = new System.Drawing.Size(257, 22);
            this.txtOldPassword.TabIndex = 3;
            this.txtOldPassword.Text = "";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(252, 262);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(257, 22);
            this.txtConfirmPassword.TabIndex = 2;
            this.txtConfirmPassword.Text = "";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(252, 224);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(257, 22);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.Text = "";
            // 
            // lblPasswordChange
            // 
            this.lblPasswordChange.AutoSize = true;
            this.lblPasswordChange.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPasswordChange.Location = new System.Drawing.Point(245, 31);
            this.lblPasswordChange.Name = "lblPasswordChange";
            this.lblPasswordChange.Size = new System.Drawing.Size(268, 37);
            this.lblPasswordChange.TabIndex = 0;
            this.lblPasswordChange.Text = "CHANGE PASSWORD";
            // 
            // ChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 561);
            this.Controls.Add(this.panel2);
            this.Name = "ChangePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sale";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnChangePass;
        private System.Windows.Forms.Label lblConfirmPass;
        private System.Windows.Forms.Label lblNewPass;
        private System.Windows.Forms.Label lblOldPass;
        private System.Windows.Forms.RichTextBox txtOldPassword;
        private System.Windows.Forms.RichTextBox txtConfirmPassword;
        private System.Windows.Forms.RichTextBox txtNewPassword;
        internal System.Windows.Forms.Label lblPasswordChange;
    }
}