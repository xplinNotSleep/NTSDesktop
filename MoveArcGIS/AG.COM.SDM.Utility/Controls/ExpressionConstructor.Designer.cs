namespace AG.COM.SDM.Utility.Controls
{
    partial class ExpressionConstructor
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetUnique = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtExpress = new System.Windows.Forms.TextBox();
            this.btnNotEqual = new System.Windows.Forms.Button();
            this.listFields = new System.Windows.Forms.ListBox();
            this.btnIs = new System.Windows.Forms.Button();
            this.listValues = new System.Windows.Forms.ListBox();
            this.btnEqual = new System.Windows.Forms.Button();
            this.btnLike = new System.Windows.Forms.Button();
            this.btnSmallEqual = new System.Windows.Forms.Button();
            this.btnBottomLine = new System.Windows.Forms.Button();
            this.btnOr = new System.Windows.Forms.Button();
            this.btnBigger = new System.Windows.Forms.Button();
            this.btnSmaller = new System.Windows.Forms.Button();
            this.btnBrackets = new System.Windows.Forms.Button();
            this.btnPercentage = new System.Windows.Forms.Button();
            this.btnBigEqual = new System.Windows.Forms.Button();
            this.btnAnd = new System.Windows.Forms.Button();
            this.btnNot = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetUnique);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtExpress);
            this.groupBox2.Controls.Add(this.btnNotEqual);
            this.groupBox2.Controls.Add(this.listFields);
            this.groupBox2.Controls.Add(this.btnIs);
            this.groupBox2.Controls.Add(this.listValues);
            this.groupBox2.Controls.Add(this.btnEqual);
            this.groupBox2.Controls.Add(this.btnLike);
            this.groupBox2.Controls.Add(this.btnSmallEqual);
            this.groupBox2.Controls.Add(this.btnBottomLine);
            this.groupBox2.Controls.Add(this.btnOr);
            this.groupBox2.Controls.Add(this.btnBigger);
            this.groupBox2.Controls.Add(this.btnSmaller);
            this.groupBox2.Controls.Add(this.btnBrackets);
            this.groupBox2.Controls.Add(this.btnPercentage);
            this.groupBox2.Controls.Add(this.btnBigEqual);
            this.groupBox2.Controls.Add(this.btnAnd);
            this.groupBox2.Controls.Add(this.btnNot);
            this.groupBox2.Location = new System.Drawing.Point(3, -3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 311);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // btnGetUnique
            // 
            this.btnGetUnique.Location = new System.Drawing.Point(280, 186);
            this.btnGetUnique.Name = "btnGetUnique";
            this.btnGetUnique.Size = new System.Drawing.Size(126, 23);
            this.btnGetUnique.TabIndex = 16;
            this.btnGetUnique.Text = "列出字段参考值";
            this.btnGetUnique.UseVisualStyleBackColor = true;
            this.btnGetUnique.Click += new System.EventHandler(this.btnGetUnique_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "SQL表达式:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(281, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "字段";
            // 
            // txtExpress
            // 
            this.txtExpress.Location = new System.Drawing.Point(13, 225);
            this.txtExpress.Multiline = true;
            this.txtExpress.Name = "txtExpress";
            this.txtExpress.Size = new System.Drawing.Size(393, 74);
            this.txtExpress.TabIndex = 17;
            this.txtExpress.TextChanged += new System.EventHandler(this.txtExpress_TextChanged);
            // 
            // btnNotEqual
            // 
            this.btnNotEqual.Location = new System.Drawing.Point(191, 32);
            this.btnNotEqual.Name = "btnNotEqual";
            this.btnNotEqual.Size = new System.Drawing.Size(38, 23);
            this.btnNotEqual.TabIndex = 2;
            this.btnNotEqual.Text = "<>";
            this.btnNotEqual.UseVisualStyleBackColor = true;
            this.btnNotEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // listFields
            // 
            this.listFields.FormattingEnabled = true;
            this.listFields.ItemHeight = 12;
            this.listFields.Location = new System.Drawing.Point(14, 32);
            this.listFields.Name = "listFields";
            this.listFields.Size = new System.Drawing.Size(127, 148);
            this.listFields.TabIndex = 0;
            this.listFields.DoubleClick += new System.EventHandler(this.listFields_DoubleClick);
            this.listFields.SelectedIndexChanged += new System.EventHandler(this.listFields_SelectedIndexChanged);
            // 
            // btnIs
            // 
            this.btnIs.Location = new System.Drawing.Point(147, 148);
            this.btnIs.Name = "btnIs";
            this.btnIs.Size = new System.Drawing.Size(38, 23);
            this.btnIs.TabIndex = 14;
            this.btnIs.Text = "Is";
            this.btnIs.UseVisualStyleBackColor = true;
            this.btnIs.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // listValues
            // 
            this.listValues.BackColor = System.Drawing.SystemColors.Control;
            this.listValues.Enabled = false;
            this.listValues.FormattingEnabled = true;
            this.listValues.ItemHeight = 12;
            this.listValues.Location = new System.Drawing.Point(279, 32);
            this.listValues.Name = "listValues";
            this.listValues.Size = new System.Drawing.Size(127, 148);
            this.listValues.TabIndex = 15;
            this.listValues.EnabledChanged += new System.EventHandler(this.listValues_EnabledChanged);
            this.listValues.DoubleClick += new System.EventHandler(this.listValues_DoubleClick);
            // 
            // btnEqual
            // 
            this.btnEqual.Location = new System.Drawing.Point(147, 32);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(38, 23);
            this.btnEqual.TabIndex = 1;
            this.btnEqual.Text = "=";
            this.btnEqual.UseVisualStyleBackColor = true;
            this.btnEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnLike
            // 
            this.btnLike.Location = new System.Drawing.Point(235, 32);
            this.btnLike.Name = "btnLike";
            this.btnLike.Size = new System.Drawing.Size(38, 23);
            this.btnLike.TabIndex = 3;
            this.btnLike.Text = "like";
            this.btnLike.UseVisualStyleBackColor = true;
            this.btnLike.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnSmallEqual
            // 
            this.btnSmallEqual.Location = new System.Drawing.Point(190, 90);
            this.btnSmallEqual.Name = "btnSmallEqual";
            this.btnSmallEqual.Size = new System.Drawing.Size(38, 23);
            this.btnSmallEqual.TabIndex = 8;
            this.btnSmallEqual.Text = "<=";
            this.btnSmallEqual.UseVisualStyleBackColor = true;
            this.btnSmallEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBottomLine
            // 
            this.btnBottomLine.Location = new System.Drawing.Point(147, 119);
            this.btnBottomLine.Name = "btnBottomLine";
            this.btnBottomLine.Size = new System.Drawing.Size(19, 23);
            this.btnBottomLine.TabIndex = 10;
            this.btnBottomLine.Text = "_";
            this.btnBottomLine.UseVisualStyleBackColor = true;
            this.btnBottomLine.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnOr
            // 
            this.btnOr.Location = new System.Drawing.Point(235, 90);
            this.btnOr.Name = "btnOr";
            this.btnOr.Size = new System.Drawing.Size(38, 23);
            this.btnOr.TabIndex = 9;
            this.btnOr.Text = "Or";
            this.btnOr.UseVisualStyleBackColor = true;
            this.btnOr.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBigger
            // 
            this.btnBigger.Location = new System.Drawing.Point(147, 61);
            this.btnBigger.Name = "btnBigger";
            this.btnBigger.Size = new System.Drawing.Size(38, 23);
            this.btnBigger.TabIndex = 4;
            this.btnBigger.Text = ">";
            this.btnBigger.UseVisualStyleBackColor = true;
            this.btnBigger.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnSmaller
            // 
            this.btnSmaller.Location = new System.Drawing.Point(147, 90);
            this.btnSmaller.Name = "btnSmaller";
            this.btnSmaller.Size = new System.Drawing.Size(38, 23);
            this.btnSmaller.TabIndex = 7;
            this.btnSmaller.Text = "<";
            this.btnSmaller.UseVisualStyleBackColor = true;
            this.btnSmaller.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBrackets
            // 
            this.btnBrackets.Location = new System.Drawing.Point(191, 119);
            this.btnBrackets.Name = "btnBrackets";
            this.btnBrackets.Size = new System.Drawing.Size(38, 23);
            this.btnBrackets.TabIndex = 12;
            this.btnBrackets.Text = "( )";
            this.btnBrackets.UseVisualStyleBackColor = true;
            this.btnBrackets.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnPercentage
            // 
            this.btnPercentage.Location = new System.Drawing.Point(164, 119);
            this.btnPercentage.Name = "btnPercentage";
            this.btnPercentage.Size = new System.Drawing.Size(19, 23);
            this.btnPercentage.TabIndex = 11;
            this.btnPercentage.Text = "%";
            this.btnPercentage.UseVisualStyleBackColor = true;
            this.btnPercentage.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnBigEqual
            // 
            this.btnBigEqual.Location = new System.Drawing.Point(190, 61);
            this.btnBigEqual.Name = "btnBigEqual";
            this.btnBigEqual.Size = new System.Drawing.Size(38, 23);
            this.btnBigEqual.TabIndex = 5;
            this.btnBigEqual.Text = ">=";
            this.btnBigEqual.UseVisualStyleBackColor = true;
            this.btnBigEqual.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnAnd
            // 
            this.btnAnd.Location = new System.Drawing.Point(235, 61);
            this.btnAnd.Name = "btnAnd";
            this.btnAnd.Size = new System.Drawing.Size(38, 23);
            this.btnAnd.TabIndex = 6;
            this.btnAnd.Text = "And";
            this.btnAnd.UseVisualStyleBackColor = true;
            this.btnAnd.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // btnNot
            // 
            this.btnNot.Location = new System.Drawing.Point(236, 119);
            this.btnNot.Name = "btnNot";
            this.btnNot.Size = new System.Drawing.Size(38, 23);
            this.btnNot.TabIndex = 13;
            this.btnNot.Text = "Not";
            this.btnNot.UseVisualStyleBackColor = true;
            this.btnNot.Click += new System.EventHandler(this.btnOperationSymbol_Click);
            // 
            // ExpressionConstructor
            // 
            this.Controls.Add(this.groupBox2);
            this.Name = "ExpressionConstructor";
            this.Size = new System.Drawing.Size(429, 311);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetUnique;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtExpress;
        private System.Windows.Forms.Button btnNotEqual;
        private System.Windows.Forms.ListBox listFields;
        private System.Windows.Forms.Button btnIs;
        private System.Windows.Forms.ListBox listValues;
        private System.Windows.Forms.Button btnEqual;
        private System.Windows.Forms.Button btnLike;
        private System.Windows.Forms.Button btnSmallEqual;
        private System.Windows.Forms.Button btnBottomLine;
        private System.Windows.Forms.Button btnOr;
        private System.Windows.Forms.Button btnBigger;
        private System.Windows.Forms.Button btnSmaller;
        private System.Windows.Forms.Button btnBrackets;
        private System.Windows.Forms.Button btnPercentage;
        private System.Windows.Forms.Button btnBigEqual;
        private System.Windows.Forms.Button btnAnd;
        private System.Windows.Forms.Button btnNot;
    }
}
