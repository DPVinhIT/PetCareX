namespace LOGIN
{
    partial class Reset_Pass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reset_Pass));
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            richTextBox6 = new RichTextBox();
            button9 = new Button();
            richTextBox1 = new RichTextBox();
            textBox10 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
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
            pictureBox1.TabIndex = 34;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(richTextBox6);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(textBox10);
            panel1.Controls.Add(textBox6);
            panel1.Controls.Add(textBox7);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(267, 561);
            panel1.TabIndex = 35;
            // 
            // richTextBox6
            // 
            richTextBox6.Location = new Point(19, 243);
            richTextBox6.Name = "richTextBox6";
            richTextBox6.Size = new Size(223, 21);
            richTextBox6.TabIndex = 27;
            richTextBox6.Text = "";
            // 
            // button9
            // 
            button9.BackColor = SystemColors.ActiveCaption;
            button9.FlatAppearance.BorderSize = 0;
            button9.FlatStyle = FlatStyle.Flat;
            button9.ImageAlign = ContentAlignment.MiddleLeft;
            button9.Location = new Point(93, 308);
            button9.Name = "button9";
            button9.Size = new Size(70, 25);
            button9.TabIndex = 29;
            button9.Text = "OK";
            button9.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(19, 179);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(223, 21);
            richTextBox1.TabIndex = 28;
            richTextBox1.Text = "";
            // 
            // textBox10
            // 
            textBox10.BackColor = SystemColors.Window;
            textBox10.BorderStyle = BorderStyle.None;
            textBox10.Font = new Font("Segoe UI", 10F);
            textBox10.Location = new Point(19, 219);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(164, 18);
            textBox10.TabIndex = 24;
            textBox10.Text = "Confirm new password";
            // 
            // textBox6
            // 
            textBox6.BorderStyle = BorderStyle.None;
            textBox6.Font = new Font("Segoe UI", 20F);
            textBox6.Location = new Point(7, 79);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(252, 36);
            textBox6.TabIndex = 30;
            textBox6.Text = "RESET PASSWORD";
            textBox6.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            textBox7.BackColor = SystemColors.Window;
            textBox7.BorderStyle = BorderStyle.None;
            textBox7.Font = new Font("Segoe UI", 10F);
            textBox7.Location = new Point(19, 155);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(98, 18);
            textBox7.TabIndex = 25;
            textBox7.Text = "New password";
            // 
            // Reset_Pass
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(984, 561);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Name = "Reset_Pass";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PetCare";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private RichTextBox richTextBox6;
        private Button button9;
        private RichTextBox richTextBox1;
        private TextBox textBox10;
        private TextBox textBox6;
        private TextBox textBox7;
    }
}