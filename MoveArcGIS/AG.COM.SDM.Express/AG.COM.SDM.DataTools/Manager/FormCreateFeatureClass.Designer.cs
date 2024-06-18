namespace AG.COM.SDM.DataTools.Manager
{
    partial class FormCreateFeatureClass
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreateFeatureClass));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgFields = new System.Windows.Forms.DataGridView();
            this.colFieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAliasName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colFieldLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.optAnno = new System.Windows.Forms.RadioButton();
            this.optArea = new System.Windows.Forms.RadioButton();
            this.optLine = new System.Windows.Forms.RadioButton();
            this.optPoint = new System.Windows.Forms.RadioButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btOpenWorkspace = new System.Windows.Forms.Button();
            this.txtWorkspace = new System.Windows.Forms.TextBox();
            this.txtSpatialRef = new System.Windows.Forms.TextBox();
            this.btSelectSpatialRef = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.btImportFields = new System.Windows.Forms.Button();
            this.nudRefScale = new System.Windows.Forms.NumericUpDown();
            this.lblRefScale = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefScale)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgFields);
            this.groupBox1.Location = new System.Drawing.Point(17, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 200);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "字段";
            // 
            // dgFields
            // 
            this.dgFields.CausesValidation = false;
            this.dgFields.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFields.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFieldName,
            this.colAliasName,
            this.colFieldType,
            this.colFieldLength});
            this.dgFields.Location = new System.Drawing.Point(13, 20);
            this.dgFields.Name = "dgFields";
            this.dgFields.RowHeadersWidth = 15;
            this.dgFields.RowTemplate.Height = 23;
            this.dgFields.ShowCellErrors = false;
            this.dgFields.Size = new System.Drawing.Size(403, 174);
            this.dgFields.TabIndex = 0;
            this.dgFields.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgFields_UserAddedRow);
            // 
            // colFieldName
            // 
            this.colFieldName.HeaderText = "名称";
            this.colFieldName.Name = "colFieldName";
            // 
            // colAliasName
            // 
            this.colAliasName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAliasName.HeaderText = "别名";
            this.colAliasName.Name = "colAliasName";
            // 
            // colFieldType
            // 
            this.colFieldType.HeaderText = "类型";
            this.colFieldType.Name = "colFieldType";
            this.colFieldType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colFieldType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colFieldLength
            // 
            this.colFieldLength.HeaderText = "长度";
            this.colFieldLength.Name = "colFieldLength";
            this.colFieldLength.Width = 60;
            // 
            // optAnno
            // 
            this.optAnno.AutoSize = true;
            this.optAnno.Location = new System.Drawing.Point(245, 77);
            this.optAnno.Name = "optAnno";
            this.optAnno.Size = new System.Drawing.Size(47, 16);
            this.optAnno.TabIndex = 26;
            this.optAnno.Text = "注记";
            this.optAnno.UseVisualStyleBackColor = true;
            this.optAnno.CheckedChanged += new System.EventHandler(this.optAnno_CheckedChanged);
            // 
            // optArea
            // 
            this.optArea.AutoSize = true;
            this.optArea.Location = new System.Drawing.Point(192, 77);
            this.optArea.Name = "optArea";
            this.optArea.Size = new System.Drawing.Size(35, 16);
            this.optArea.TabIndex = 25;
            this.optArea.Text = "面";
            this.optArea.UseVisualStyleBackColor = true;
            // 
            // optLine
            // 
            this.optLine.AutoSize = true;
            this.optLine.Location = new System.Drawing.Point(139, 77);
            this.optLine.Name = "optLine";
            this.optLine.Size = new System.Drawing.Size(35, 16);
            this.optLine.TabIndex = 28;
            this.optLine.Text = "线";
            this.optLine.UseVisualStyleBackColor = true;
            // 
            // optPoint
            // 
            this.optPoint.AutoSize = true;
            this.optPoint.Checked = true;
            this.optPoint.Location = new System.Drawing.Point(86, 77);
            this.optPoint.Name = "optPoint";
            this.optPoint.Size = new System.Drawing.Size(35, 16);
            this.optPoint.TabIndex = 27;
            this.optPoint.TabStop = true;
            this.optPoint.Text = "点";
            this.optPoint.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(86, 42);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(321, 21);
            this.txtName.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "位置";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "名称";
            // 
            // btOpenWorkspace
            // 
            this.btOpenWorkspace.Image = ((System.Drawing.Image)(resources.GetObject("btOpenWorkspace.Image")));
            this.btOpenWorkspace.Location = new System.Drawing.Point(420, 10);
            this.btOpenWorkspace.Name = "btOpenWorkspace";
            this.btOpenWorkspace.Size = new System.Drawing.Size(23, 23);
            this.btOpenWorkspace.TabIndex = 20;
            this.btOpenWorkspace.UseVisualStyleBackColor = true;
            this.btOpenWorkspace.Click += new System.EventHandler(this.btOpenWorkspace_Click);
            // 
            // txtWorkspace
            // 
            this.txtWorkspace.Location = new System.Drawing.Point(86, 12);
            this.txtWorkspace.Name = "txtWorkspace";
            this.txtWorkspace.ReadOnly = true;
            this.txtWorkspace.Size = new System.Drawing.Size(321, 21);
            this.txtWorkspace.TabIndex = 19;
            // 
            // txtSpatialRef
            // 
            this.txtSpatialRef.Location = new System.Drawing.Point(86, 312);
            this.txtSpatialRef.Name = "txtSpatialRef";
            this.txtSpatialRef.ReadOnly = true;
            this.txtSpatialRef.Size = new System.Drawing.Size(320, 21);
            this.txtSpatialRef.TabIndex = 19;
            // 
            // btSelectSpatialRef
            // 
            this.btSelectSpatialRef.Image = ((System.Drawing.Image)(resources.GetObject("btSelectSpatialRef.Image")));
            this.btSelectSpatialRef.Location = new System.Drawing.Point(420, 310);
            this.btSelectSpatialRef.Name = "btSelectSpatialRef";
            this.btSelectSpatialRef.Size = new System.Drawing.Size(23, 23);
            this.btSelectSpatialRef.TabIndex = 20;
            this.btSelectSpatialRef.UseVisualStyleBackColor = true;
            this.btSelectSpatialRef.Click += new System.EventHandler(this.btSelectSpatialRef_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "空间参考";
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(262, 350);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(82, 23);
            this.btOK.TabIndex = 31;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(363, 350);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(82, 23);
            this.btCancel.TabIndex = 30;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // btImportFields
            // 
            this.btImportFields.Location = new System.Drawing.Point(16, 350);
            this.btImportFields.Name = "btImportFields";
            this.btImportFields.Size = new System.Drawing.Size(95, 23);
            this.btImportFields.TabIndex = 31;
            this.btImportFields.Text = "导入字段信息";
            this.btImportFields.UseVisualStyleBackColor = true;
            this.btImportFields.Click += new System.EventHandler(this.btImportFields_Click);
            // 
            // nudRefScale
            // 
            this.nudRefScale.Location = new System.Drawing.Point(381, 75);
            this.nudRefScale.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nudRefScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRefScale.Name = "nudRefScale";
            this.nudRefScale.Size = new System.Drawing.Size(62, 21);
            this.nudRefScale.TabIndex = 32;
            this.nudRefScale.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // lblRefScale
            // 
            this.lblRefScale.AutoSize = true;
            this.lblRefScale.Location = new System.Drawing.Point(304, 80);
            this.lblRefScale.Name = "lblRefScale";
            this.lblRefScale.Size = new System.Drawing.Size(77, 12);
            this.lblRefScale.TabIndex = 22;
            this.lblRefScale.Text = "参考比例尺：";
            // 
            // FormCreateFeatureClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 385);
            this.Controls.Add(this.nudRefScale);
            this.Controls.Add(this.btImportFields);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.optAnno);
            this.Controls.Add(this.optArea);
            this.Controls.Add(this.optLine);
            this.Controls.Add(this.optPoint);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblRefScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btSelectSpatialRef);
            this.Controls.Add(this.txtSpatialRef);
            this.Controls.Add(this.btOpenWorkspace);
            this.Controls.Add(this.txtWorkspace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCreateFeatureClass";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "创建图层";
            this.Load += new System.EventHandler(this.FormCreateFeatureClass_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFields)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRefScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgFields;
        private System.Windows.Forms.RadioButton optAnno;
        private System.Windows.Forms.RadioButton optArea;
        private System.Windows.Forms.RadioButton optLine;
        private System.Windows.Forms.RadioButton optPoint;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btOpenWorkspace;
        private System.Windows.Forms.TextBox txtWorkspace;
        private System.Windows.Forms.TextBox txtSpatialRef;
        private System.Windows.Forms.Button btSelectSpatialRef;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btImportFields;
        private System.Windows.Forms.NumericUpDown nudRefScale;
        private System.Windows.Forms.Label lblRefScale;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAliasName;
        private System.Windows.Forms.DataGridViewComboBoxColumn colFieldType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldLength;

    }
}