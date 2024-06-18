namespace AG.COM.SDM.Config
{
    partial class FormSnap
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
            this.components = new System.ComponentModel.Container();
            this.chkVertex = new System.Windows.Forms.CheckBox();
            this.chkEndPoint = new System.Windows.Forms.CheckBox();
            this.chkCentroid = new System.Windows.Forms.CheckBox();
            this.chkEdge = new System.Windows.Forms.CheckBox();
            this.chkPendicular = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancelAll = new System.Windows.Forms.Button();
            this.mapLayersTreeView1 = new AG.COM.SDM.Utility.Controls.MapLayersTreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTolerance = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkSnapEnabled = new System.Windows.Forms.CheckBox();
            this.btnSetSelected = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).BeginInit();
            this.SuspendLayout();
            // 
            // chkVertex
            // 
            this.chkVertex.AutoSize = true;
            this.chkVertex.Location = new System.Drawing.Point(15, 38);
            this.chkVertex.Name = "chkVertex";
            this.chkVertex.Size = new System.Drawing.Size(48, 16);
            this.chkVertex.TabIndex = 1;
            this.chkVertex.Text = "节点";
            this.chkVertex.UseVisualStyleBackColor = true;
            this.chkVertex.Click += new System.EventHandler(this.chkVertex_Click);
            // 
            // chkEndPoint
            // 
            this.chkEndPoint.AutoSize = true;
            this.chkEndPoint.Location = new System.Drawing.Point(15, 81);
            this.chkEndPoint.Name = "chkEndPoint";
            this.chkEndPoint.Size = new System.Drawing.Size(48, 16);
            this.chkEndPoint.TabIndex = 1;
            this.chkEndPoint.Text = "端点";
            this.chkEndPoint.UseVisualStyleBackColor = true;
            this.chkEndPoint.Click += new System.EventHandler(this.chkVertex_Click);
            // 
            // chkCentroid
            // 
            this.chkCentroid.AutoSize = true;
            this.chkCentroid.Location = new System.Drawing.Point(15, 128);
            this.chkCentroid.Name = "chkCentroid";
            this.chkCentroid.Size = new System.Drawing.Size(48, 16);
            this.chkCentroid.TabIndex = 1;
            this.chkCentroid.Text = "重心";
            this.chkCentroid.UseVisualStyleBackColor = true;
            this.chkCentroid.Click += new System.EventHandler(this.chkVertex_Click);
            // 
            // chkEdge
            // 
            this.chkEdge.AutoSize = true;
            this.chkEdge.Location = new System.Drawing.Point(15, 175);
            this.chkEdge.Name = "chkEdge";
            this.chkEdge.Size = new System.Drawing.Size(36, 16);
            this.chkEdge.TabIndex = 1;
            this.chkEdge.Text = "边";
            this.chkEdge.UseVisualStyleBackColor = true;
            this.chkEdge.Click += new System.EventHandler(this.chkVertex_Click);
            // 
            // chkPendicular
            // 
            this.chkPendicular.AutoSize = true;
            this.chkPendicular.Location = new System.Drawing.Point(5, 210);
            this.chkPendicular.Name = "chkPendicular";
            this.chkPendicular.Size = new System.Drawing.Size(96, 16);
            this.chkPendicular.TabIndex = 1;
            this.chkPendicular.Text = "垂足(未实现)";
            this.chkPendicular.UseVisualStyleBackColor = true;
            this.chkPendicular.Visible = false;
            this.chkPendicular.Click += new System.EventHandler(this.chkVertex_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkPendicular);
            this.groupBox1.Controls.Add(this.chkVertex);
            this.groupBox1.Controls.Add(this.chkEdge);
            this.groupBox1.Controls.Add(this.chkEndPoint);
            this.groupBox1.Controls.Add(this.chkCentroid);
            this.groupBox1.Location = new System.Drawing.Point(279, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 242);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "捕捉类型";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(294, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "退出";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(216, 302);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(72, 24);
            this.btOK.TabIndex = 3;
            this.btOK.Text = "确定";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.chkOK_Click);
            // 
            // btCancelAll
            // 
            this.btCancelAll.Location = new System.Drawing.Point(93, 302);
            this.btCancelAll.Name = "btCancelAll";
            this.btCancelAll.Size = new System.Drawing.Size(72, 24);
            this.btCancelAll.TabIndex = 3;
            this.btCancelAll.Text = "全部取消";
            this.btCancelAll.UseVisualStyleBackColor = true;
            this.btCancelAll.Click += new System.EventHandler(this.btCancelAll_Click);
            // 
            // mapLayersTreeView1
            // 
            this.mapLayersTreeView1.CheckBoxes = true;
            this.mapLayersTreeView1.ImageIndex = 0;
            this.mapLayersTreeView1.ItemHeight = 20;
            this.mapLayersTreeView1.Location = new System.Drawing.Point(12, 12);
            this.mapLayersTreeView1.Name = "mapLayersTreeView1";
            this.mapLayersTreeView1.SelectedImageIndex = 0;
            this.mapLayersTreeView1.Size = new System.Drawing.Size(261, 234);
            this.mapLayersTreeView1.TabIndex = 0;
            this.mapLayersTreeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mapLayersTreeView1_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "容差：";
            // 
            // nudTolerance
            // 
            this.nudTolerance.Location = new System.Drawing.Point(47, 260);
            this.nudTolerance.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTolerance.Name = "nudTolerance";
            this.nudTolerance.Size = new System.Drawing.Size(105, 21);
            this.nudTolerance.TabIndex = 5;
            this.nudTolerance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTolerance.ValueChanged += new System.EventHandler(this.nudTolerance_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "像素";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(14, 287);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 5);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // chkSnapEnabled
            // 
            this.chkSnapEnabled.AutoSize = true;
            this.chkSnapEnabled.Location = new System.Drawing.Point(201, 261);
            this.chkSnapEnabled.Name = "chkSnapEnabled";
            this.chkSnapEnabled.Size = new System.Drawing.Size(72, 16);
            this.chkSnapEnabled.TabIndex = 7;
            this.chkSnapEnabled.Text = "启用捕捉";
            this.chkSnapEnabled.UseVisualStyleBackColor = true;
            this.chkSnapEnabled.Click += new System.EventHandler(this.chkSnapEnabled_Click);
            // 
            // btnSetSelected
            // 
            this.btnSetSelected.Location = new System.Drawing.Point(12, 302);
            this.btnSetSelected.Name = "btnSetSelected";
            this.btnSetSelected.Size = new System.Drawing.Size(75, 23);
            this.btnSetSelected.TabIndex = 8;
            this.btnSetSelected.Text = "设置选择";
            this.btnSetSelected.UseVisualStyleBackColor = true;
            this.btnSetSelected.Click += new System.EventHandler(this.btnSetSelected_Click);
            // 
            // FormSnap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 332);
            this.Controls.Add(this.btnSetSelected);
            this.Controls.Add(this.chkSnapEnabled);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.nudTolerance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancelAll);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapLayersTreeView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSnap";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "捕捉设置";
            this.Load += new System.EventHandler(this.FormSnap_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTolerance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AG.COM.SDM.Utility.Controls.MapLayersTreeView mapLayersTreeView1;
        private System.Windows.Forms.CheckBox chkVertex;
        private System.Windows.Forms.CheckBox chkEndPoint;
        private System.Windows.Forms.CheckBox chkCentroid;
        private System.Windows.Forms.CheckBox chkEdge;
        private System.Windows.Forms.CheckBox chkPendicular;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancelAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTolerance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkSnapEnabled;
        private System.Windows.Forms.Button btnSetSelected;
    }
}