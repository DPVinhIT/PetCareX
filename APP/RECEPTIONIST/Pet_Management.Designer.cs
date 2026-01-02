namespace RECEPTIONIST
{
    partial class Pet_Management
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
            dgvPetSearch = new DataGridView();
            btnPetSearch = new Button();
            txtSpecies = new RichTextBox();
            txtPetName = new RichTextBox();
            label3 = new Label();
            txtPetID = new RichTextBox();
            label2 = new Label();
            label1 = new Label();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPetSearch).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Window;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(dgvPetSearch);
            panel2.Controls.Add(btnPetSearch);
            panel2.Controls.Add(txtSpecies);
            panel2.Controls.Add(txtPetName);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(txtPetID);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(764, 561);
            panel2.TabIndex = 7;
            // 
            // dgvPetSearch
            // 
            dgvPetSearch.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPetSearch.Dock = DockStyle.Bottom;
            dgvPetSearch.Location = new Point(0, 66);
            dgvPetSearch.Name = "dgvPetSearch";
            dgvPetSearch.Size = new Size(762, 493);
            dgvPetSearch.TabIndex = 8;
            // 
            // btnPetSearch
            // 
            btnPetSearch.BackColor = SystemColors.ActiveCaption;
            btnPetSearch.FlatAppearance.BorderSize = 0;
            btnPetSearch.FlatStyle = FlatStyle.Flat;
            btnPetSearch.ImageAlign = ContentAlignment.MiddleLeft;
            btnPetSearch.Location = new Point(587, 20);
            btnPetSearch.Name = "btnPetSearch";
            btnPetSearch.Size = new Size(70, 25);
            btnPetSearch.TabIndex = 7;
            btnPetSearch.Text = "Search";
            btnPetSearch.UseVisualStyleBackColor = false;
            btnPetSearch.Click += btnPetSearch_Click;
            // 
            // txtSpecies
            // 
            txtSpecies.Location = new Point(445, 22);
            txtSpecies.Name = "txtSpecies";
            txtSpecies.Size = new Size(100, 21);
            txtSpecies.TabIndex = 5;
            txtSpecies.Text = "";
            // 
            // txtPetName
            // 
            txtPetName.Location = new Point(258, 22);
            txtPetName.Name = "txtPetName";
            txtPetName.Size = new Size(100, 21);
            txtPetName.TabIndex = 5;
            txtPetName.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(383, 25);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 4;
            label3.Text = "Species";
            // 
            // txtPetID
            // 
            txtPetID.Location = new Point(69, 22);
            txtPetID.Name = "txtPetID";
            txtPetID.Size = new Size(83, 21);
            txtPetID.TabIndex = 5;
            txtPetID.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(196, 25);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Pet name";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(25, 25);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 4;
            label1.Text = "Pet ID";
            // 
            // Pet_Management
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(764, 561);
            Controls.Add(panel2);
            Name = "Pet_Management";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pet_Management";
            Load += Pet_Management_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPetSearch).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel2;
        private Button btnPetSearch;
        private RichTextBox txtSpecies;
        private RichTextBox txtPetName;
        private Label label3;
        private RichTextBox txtPetID;
        private Label label2;
        private Label label1;
        private DataGridView dgvPetSearch;
    }
}