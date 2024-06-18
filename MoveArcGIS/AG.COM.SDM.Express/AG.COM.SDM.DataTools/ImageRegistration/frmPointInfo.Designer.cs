namespace AG.COM.SDM.DataTools.ImageRegistration
{
    partial class frmPointInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPointInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtImageX = new System.Windows.Forms.TextBox();
            this.txtImageY = new System.Windows.Forms.TextBox();
            this.txtMapX = new System.Windows.Forms.TextBox();
            this.txtMapY = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnFromMap = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图像 X：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "图像 Y：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "地图 X：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "地图 Y：";
            // 
            // txtImageX
            // 
            this.txtImageX.Location = new System.Drawing.Point(80, 17);
            this.txtImageX.Name = "txtImageX";
            this.txtImageX.Size = new System.Drawing.Size(171, 21);
            this.txtImageX.TabIndex = 1;
            // 
            // txtImageY
            // 
            this.txtImageY.Location = new System.Drawing.Point(80, 49);
            this.txtImageY.Name = "txtImageY";
            this.txtImageY.Size = new System.Drawing.Size(171, 21);
            this.txtImageY.TabIndex = 1;
            // 
            // txtMapX
            // 
            this.txtMapX.Location = new System.Drawing.Point(80, 82);
            this.txtMapX.Name = "txtMapX";
            this.txtMapX.Size = new System.Drawing.Size(171, 21);
            this.txtMapX.TabIndex = 1;
            // 
            // txtMapY
            // 
            this.txtMapY.Location = new System.Drawing.Point(80, 115);
            this.txtMapY.Name = "txtMapY";
            this.txtMapY.Size = new System.Drawing.Size(171, 21);
            this.txtMapY.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(23, 150);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(54, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnFromMap
            // 
            this.btnFromMap.Location = new System.Drawing.Point(89, 150);
            this.btnFromMap.Name = "btnFromMap";
            this.btnFromMap.Size = new System.Drawing.Size(97, 23);
            this.btnFromMap.TabIndex = 5;
            this.btnFromMap.Text = "从地图获取点..";
            this.btnFromMap.UseVisualStyleBackColor = true;
            this.btnFromMap.Click += new System.EventHandler(this.btnFromMap_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmPointInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 186);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFromMap);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtMapY);
            this.Controls.Add(this.txtMapX);
            this.Controls.Add(this.txtImageY);
            this.Controls.Add(this.txtImageX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPointInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "控制点信息";
            this.Load += new System.EventHandler(this.frmPointInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtImageX;
        private System.Windows.Forms.TextBox txtImageY;
        private System.Windows.Forms.TextBox txtMapX;
        private System.Windows.Forms.TextBox txtMapY;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnFromMap;
        private System.Windows.Forms.Button btnCancel;
    }
}