namespace LOGIN
{
    partial class SignIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignIn));
            pictureBox1 = new PictureBox();
            button9 = new Button();
            richTextBox6 = new RichTextBox();
            richTextBox1 = new RichTextBox();
            textBox10 = new TextBox();
            textBox7 = new TextBox();
            textBox6 = new TextBox();
            panel1 = new Panel();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Right;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(265, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(719, 561);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // button9
            // 
            button9.BackColor = SystemColors.ActiveCaption;
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatStyle = FlatStyle.Flat;
            button9.ImageAlign = ContentAlignment.MiddleLeft;
            button9.Location = new Point(89, 277);
            button9.Name = "button9";
            button9.Size = new Size(70, 25);
            button9.TabIndex = 29;
            button9.Text = "OK";
            button9.UseVisualStyleBackColor = false;
            // 
            // richTextBox6
            // 
            richTextBox6.Location = new Point(101, 202);
            richTextBox6.Name = "richTextBox6";
            richTextBox6.Size = new Size(137, 21);
            richTextBox6.TabIndex = 27;
            richTextBox6.Text = "";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(101, 163);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(137, 21);
            richTextBox1.TabIndex = 28;
            richTextBox1.Text = "";
            // 
            // textBox10
            // 
            textBox10.BackColor = SystemColors.Window;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Font = new Font("Segoe UI", 10F);
            textBox10.Location = new Point(12, 202);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(73, 18);
            textBox10.TabIndex = 24;
            textBox10.Text = "Password";
            // 
            // textBox7
            // 
            textBox7.BackColor = SystemColors.Window;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Font = new Font("Segoe UI", 10F);
            textBox7.Location = new Point(12, 163);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(73, 18);
            textBox7.TabIndex = 25;
            textBox7.Text = "Username";
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Segoe UI", 20F);
            textBox6.Location = new Point(57, 84);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(144, 36);
            textBox6.TabIndex = 30;
            textBox6.Text = "SIGN IN";
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(richTextBox6);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(linkLabel1);
            panel1.Controls.Add(textBox10);
            panel1.Controls.Add(textBox6);
            panel1.Controls.Add(textBox7);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 561);
            panel1.TabIndex = 31;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(144, 243);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(94, 15);
            linkLabel1.TabIndex = 31;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Forget password";
            // 
            // PetCare
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(984, 561);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Name = "PetCare";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PetCare";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button button9;
        private RichTextBox richTextBox6;
        private RichTextBox richTextBox1;
        private TextBox textBox10;
        private TextBox textBox7;
        private TextBox textBox6;
        private Panel panel1;
        private LinkLabel linkLabel1;
    }
}
