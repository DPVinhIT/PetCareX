namespace SALE
{
    partial class OrderItem
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnAddToCart = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgv_ProductDisplay = new System.Windows.Forms.DataGridView();
            this.productIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sellingPriceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.petCareX_DBDataSet1 = new SALE.PetCareX_DBDataSet1();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSearchProduct = new System.Windows.Forms.RichTextBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grb_CustomerInfo = new System.Windows.Forms.GroupBox();
            this.txt_CusID = new System.Windows.Forms.RichTextBox();
            this.lbl_CusID = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteFromCart = new System.Windows.Forms.Button();
            this.btnSendToCashier = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblSubTotalText = new System.Windows.Forms.Label();
            this.dgv_CartArena = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productTableAdapter = new SALE.PetCareX_DBDataSet1TableAdapters.ProductTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProductDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.petCareX_DBDataSet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.grb_CustomerInfo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CartArena)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grb_CustomerInfo);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(764, 561);
            this.splitContainer2.SplitterDistance = 382;
            this.splitContainer2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.btnAddToCart);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(382, 561);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Area";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(292, 520);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 27);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToCart.Location = new System.Drawing.Point(184, 520);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(102, 27);
            this.btnAddToCart.TabIndex = 2;
            this.btnAddToCart.Text = "Add to cart";
            this.btnAddToCart.UseVisualStyleBackColor = true;
            this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgv_ProductDisplay);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(376, 443);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Product Display";
            // 
            // dgv_ProductDisplay
            // 
            this.dgv_ProductDisplay.AllowUserToAddRows = false;
            this.dgv_ProductDisplay.AllowUserToDeleteRows = false;
            this.dgv_ProductDisplay.AllowUserToOrderColumns = true;
            this.dgv_ProductDisplay.AutoGenerateColumns = false;
            this.dgv_ProductDisplay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ProductDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ProductDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productIDDataGridViewTextBoxColumn,
            this.productNameDataGridViewTextBoxColumn,
            this.productTypeDataGridViewTextBoxColumn,
            this.sellingPriceDataGridViewTextBoxColumn});
            this.dgv_ProductDisplay.DataSource = this.productBindingSource;
            this.dgv_ProductDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ProductDisplay.Location = new System.Drawing.Point(3, 18);
            this.dgv_ProductDisplay.Name = "dgv_ProductDisplay";
            this.dgv_ProductDisplay.ReadOnly = true;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_ProductDisplay.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ProductDisplay.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_ProductDisplay.Size = new System.Drawing.Size(370, 422);
            this.dgv_ProductDisplay.TabIndex = 0;
            // 
            // productIDDataGridViewTextBoxColumn
            // 
            this.productIDDataGridViewTextBoxColumn.DataPropertyName = "ProductID";
            this.productIDDataGridViewTextBoxColumn.HeaderText = "ProductID";
            this.productIDDataGridViewTextBoxColumn.Name = "productIDDataGridViewTextBoxColumn";
            this.productIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // productNameDataGridViewTextBoxColumn
            // 
            this.productNameDataGridViewTextBoxColumn.DataPropertyName = "ProductName";
            this.productNameDataGridViewTextBoxColumn.HeaderText = "ProductName";
            this.productNameDataGridViewTextBoxColumn.Name = "productNameDataGridViewTextBoxColumn";
            this.productNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // productTypeDataGridViewTextBoxColumn
            // 
            this.productTypeDataGridViewTextBoxColumn.DataPropertyName = "ProductType";
            this.productTypeDataGridViewTextBoxColumn.HeaderText = "ProductType";
            this.productTypeDataGridViewTextBoxColumn.Name = "productTypeDataGridViewTextBoxColumn";
            this.productTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sellingPriceDataGridViewTextBoxColumn
            // 
            this.sellingPriceDataGridViewTextBoxColumn.DataPropertyName = "SellingPrice";
            this.sellingPriceDataGridViewTextBoxColumn.HeaderText = "SellingPrice";
            this.sellingPriceDataGridViewTextBoxColumn.Name = "sellingPriceDataGridViewTextBoxColumn";
            this.sellingPriceDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // productBindingSource
            // 
            this.productBindingSource.DataMember = "Product";
            this.productBindingSource.DataSource = this.petCareX_DBDataSet1;
            // 
            // petCareX_DBDataSet1
            // 
            this.petCareX_DBDataSet1.DataSetName = "PetCareX_DBDataSet1";
            this.petCareX_DBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblSearchProduct);
            this.panel2.Controls.Add(this.cboCategory);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 53);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Category:";
            // 
            // lblSearchProduct
            // 
            this.lblSearchProduct.Location = new System.Drawing.Point(101, 14);
            this.lblSearchProduct.Name = "lblSearchProduct";
            this.lblSearchProduct.Size = new System.Drawing.Size(93, 23);
            this.lblSearchProduct.TabIndex = 6;
            this.lblSearchProduct.Text = "";
            this.lblSearchProduct.TextChanged += new System.EventHandler(this.lblSearchProduct_TextChanged);
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Items.AddRange(new object[] {
            "Phụ kiện",
            "Vệ sinh",
            "Chăm sóc sức khỏe",
            "Đồ chơi",
            "Thức ăn mèo",
            "Thức ăn chó",
            "Dụng cụ grooming"});
            this.cboCategory.Location = new System.Drawing.Point(264, 13);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(109, 24);
            this.cboCategory.TabIndex = 5;
            this.cboCategory.TextChanged += new System.EventHandler(this.cboCategory_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search product:";
            // 
            // grb_CustomerInfo
            // 
            this.grb_CustomerInfo.Controls.Add(this.txt_CusID);
            this.grb_CustomerInfo.Controls.Add(this.lbl_CusID);
            this.grb_CustomerInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grb_CustomerInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_CustomerInfo.Location = new System.Drawing.Point(0, 0);
            this.grb_CustomerInfo.Name = "grb_CustomerInfo";
            this.grb_CustomerInfo.Size = new System.Drawing.Size(378, 71);
            this.grb_CustomerInfo.TabIndex = 1;
            this.grb_CustomerInfo.TabStop = false;
            this.grb_CustomerInfo.Text = "Customer Infomation";
            // 
            // txt_CusID
            // 
            this.txt_CusID.Location = new System.Drawing.Point(103, 33);
            this.txt_CusID.Name = "txt_CusID";
            this.txt_CusID.Size = new System.Drawing.Size(126, 23);
            this.txt_CusID.TabIndex = 1;
            this.txt_CusID.Text = "";
            // 
            // lbl_CusID
            // 
            this.lbl_CusID.AutoSize = true;
            this.lbl_CusID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CusID.Location = new System.Drawing.Point(19, 37);
            this.lbl_CusID.Name = "lbl_CusID";
            this.lbl_CusID.Size = new System.Drawing.Size(78, 15);
            this.lbl_CusID.TabIndex = 0;
            this.lbl_CusID.Text = "Customer ID:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeleteFromCart);
            this.groupBox2.Controls.Add(this.btnSendToCashier);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Controls.Add(this.dgv_CartArena);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 490);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cart Area";
            // 
            // btnDeleteFromCart
            // 
            this.btnDeleteFromCart.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDeleteFromCart.Location = new System.Drawing.Point(3, 411);
            this.btnDeleteFromCart.Name = "btnDeleteFromCart";
            this.btnDeleteFromCart.Size = new System.Drawing.Size(118, 76);
            this.btnDeleteFromCart.TabIndex = 3;
            this.btnDeleteFromCart.Text = "Delete from cart";
            this.btnDeleteFromCart.UseVisualStyleBackColor = true;
            this.btnDeleteFromCart.Click += new System.EventHandler(this.btnDeleteFromCart_Click);
            // 
            // btnSendToCashier
            // 
            this.btnSendToCashier.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSendToCashier.Location = new System.Drawing.Point(121, 411);
            this.btnSendToCashier.Name = "btnSendToCashier";
            this.btnSendToCashier.Size = new System.Drawing.Size(254, 76);
            this.btnSendToCashier.TabIndex = 2;
            this.btnSendToCashier.Text = "Send to cashier";
            this.btnSendToCashier.UseVisualStyleBackColor = true;
            this.btnSendToCashier.Click += new System.EventHandler(this.btnSendToCashier_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.flowLayoutPanel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 374);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(372, 37);
            this.panel3.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblSubTotal);
            this.flowLayoutPanel1.Controls.Add(this.lblSubTotalText);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(361, 31);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Location = new System.Drawing.Point(314, 0);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(44, 16);
            this.lblSubTotal.TabIndex = 1;
            this.lblSubTotal.Text = "None";
            // 
            // lblSubTotalText
            // 
            this.lblSubTotalText.AutoSize = true;
            this.lblSubTotalText.Location = new System.Drawing.Point(236, 0);
            this.lblSubTotalText.Name = "lblSubTotalText";
            this.lblSubTotalText.Size = new System.Drawing.Size(72, 16);
            this.lblSubTotalText.TabIndex = 0;
            this.lblSubTotalText.Text = "Sub total:";
            // 
            // dgv_CartArena
            // 
            this.dgv_CartArena.AllowUserToAddRows = false;
            this.dgv_CartArena.AllowUserToDeleteRows = false;
            this.dgv_CartArena.AllowUserToOrderColumns = true;
            this.dgv_CartArena.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_CartArena.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CartArena.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv_CartArena.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_CartArena.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_CartArena.Location = new System.Drawing.Point(3, 18);
            this.dgv_CartArena.Name = "dgv_CartArena";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_CartArena.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_CartArena.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_CartArena.Size = new System.Drawing.Size(372, 356);
            this.dgv_CartArena.TabIndex = 0;
            this.dgv_CartArena.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CartArena_CellValueChanged);
            this.dgv_CartArena.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dgv_CartArena_RowsAdded);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Quantity";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Price";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // productTableAdapter
            // 
            this.productTableAdapter.ClearBeforeFill = true;
            // 
            // OrderItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 561);
            this.Controls.Add(this.splitContainer2);
            this.Name = "OrderItem";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.OrderItem_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ProductDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.petCareX_DBDataSet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grb_CustomerInfo.ResumeLayout(false);
            this.grb_CustomerInfo.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CartArena)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAddToCart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgv_ProductDisplay;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox lblSearchProduct;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grb_CustomerInfo;
        private System.Windows.Forms.RichTextBox txt_CusID;
        private System.Windows.Forms.Label lbl_CusID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDeleteFromCart;
        private System.Windows.Forms.Button btnSendToCashier;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblSubTotalText;
        private System.Windows.Forms.DataGridView dgv_CartArena;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private PetCareX_DBDataSet1 petCareX_DBDataSet1;
        private System.Windows.Forms.BindingSource productBindingSource;
        private PetCareX_DBDataSet1TableAdapters.ProductTableAdapter productTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn productIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sellingPriceDataGridViewTextBoxColumn;
    }
}