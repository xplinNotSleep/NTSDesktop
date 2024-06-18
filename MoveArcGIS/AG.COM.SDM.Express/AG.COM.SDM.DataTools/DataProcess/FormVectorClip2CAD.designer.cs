namespace AG.COM.SDM.DataTools.DataProcess
{
    partial class FormVectorClip2CAD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVectorClip2CAD));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkExportElement = new System.Windows.Forms.CheckBox();
            this.cmbCADTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOutput = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.controlSelArea1 = new AG.COM.SDM.DataTools.DataProcess.ControlSelArea();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ltvLayer = new AG.COM.SDM.Utility.Controls.LayersTreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCADVersion = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbCADVersion);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkExportElement);
            this.groupBox1.Controls.Add(this.cmbCADTemplate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOutput);
            this.groupBox1.Controls.Add(this.txtOutput);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.controlSelArea1);
            this.groupBox1.Location = new System.Drawing.Point(9, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(319, 229);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "导出范围设置";
            // 
            // chkExportElement
            // 
            this.chkExportElement.AutoSize = true;
            this.chkExportElement.Location = new System.Drawing.Point(181, 204);
            this.chkExportElement.Name = "chkExportElement";
            this.chkExportElement.Size = new System.Drawing.Size(96, 16);
            this.chkExportElement.TabIndex = 15;
            this.chkExportElement.Text = "导出临时元素";
            this.chkExportElement.UseVisualStyleBackColor = true;
            // 
            // cmbCADTemplate
            // 
            this.cmbCADTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCADTemplate.FormattingEnabled = true;
            this.cmbCADTemplate.Location = new System.Drawing.Point(12, 162);
            this.cmbCADTemplate.Name = "cmbCADTemplate";
            this.cmbCADTemplate.Size = new System.Drawing.Size(265, 20);
            this.cmbCADTemplate.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "CAD模板：";
            // 
            // btnOutput
            // 
            this.btnOutput.Image = ((System.Drawing.Image)(resources.GetObject("btnOutput.Image")));
            this.btnOutput.Location = new System.Drawing.Point(283, 115);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(30, 29);
            this.btnOutput.TabIndex = 14;
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(11, 120);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(266, 21);
            this.txtOutput.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "导出CAD文件：";
            // 
            // controlSelArea1
            // 
            this.controlSelArea1.Location = new System.Drawing.Point(31, 20);
            this.controlSelArea1.MainForm = null;
            this.controlSelArea1.Name = "controlSelArea1";
            this.controlSelArea1.SelectType = AG.COM.SDM.DataTools.DataProcess.AreaSelectType.TYPE_NONE;
            this.controlSelArea1.Size = new System.Drawing.Size(275, 80);
            this.controlSelArea1.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(12, 240);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(256, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "AutoCad文件|*.dwg";
            this.saveFileDialog1.Title = "输出CAD文件";
            // 
            // ltvLayer
            // 
            this.ltvLayer.CheckBoxes = true;
            this.ltvLayer.ImageIndex = 0;
            this.ltvLayer.ItemHeight = 20;
            this.ltvLayer.Location = new System.Drawing.Point(2, 269);
            this.ltvLayer.Name = "ltvLayer";
            this.ltvLayer.SelectedImageIndex = 0;
            this.ltvLayer.Size = new System.Drawing.Size(332, 224);
            this.ltvLayer.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 187);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "导出CAD版本：";
            // 
            // cmbCADVersion
            // 
            this.cmbCADVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCADVersion.FormattingEnabled = true;
            this.cmbCADVersion.Location = new System.Drawing.Point(11, 202);
            this.cmbCADVersion.Name = "cmbCADVersion";
            this.cmbCADVersion.Size = new System.Drawing.Size(164, 20);
            this.cmbCADVersion.TabIndex = 17;
            // 
            // FormVectorClip2CAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 496);
            this.Controls.Add(this.ltvLayer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVectorClip2CAD";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义范围输出CAD";
            this.Load += new System.EventHandler(this.FormVectorClip_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormVectorClip_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ControlSelArea controlSelArea1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private AG.COM.SDM.Utility.Controls.LayersTreeView ltvLayer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOutput;
        private System.Windows.Forms.ComboBox cmbCADTemplate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkExportElement;
        private System.Windows.Forms.ComboBox cmbCADVersion;
        private System.Windows.Forms.Label label2;
    }
}