using AG.COM.SDM.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace AG.COM.SDM.Manager
{
    public partial class FormLoginSetting : Form
    {
        public FormLoginSetting()
        {
            InitializeComponent();
        }

        AGDesignSurface agds = null;

        private void FormLoginSetting_Load(object sender, EventArgs e)
        {
            LoginDesignHelper.Instance.Init(true);
            var helper = LoginDesignHelper.Instance;

            agds = new AGDesignSurface();
            var f = agds.CreateRootComponent(helper.MainForm);

            foreach (var item in helper.Controls)
                agds.CreateControl(item);

            this.propertyGrid1.SelectedObject = new AGCustomProperty(f, 1);


            var _selectionService = (ISelectionService)(agds.GetIDesignerHost().GetService(typeof(ISelectionService)));
            if (null != _selectionService)
                _selectionService.SelectionChanged += (s, a) =>
                {
                    ISelectionService selectionService = null;
                    selectionService = agds.GetIDesignerHost().GetService(typeof(ISelectionService)) as ISelectionService;
                    int t = 1;
                    if (selectionService.PrimarySelection is AGControl)
                        t = 2;
                    this.propertyGrid1.SelectedObject = new AGCustomProperty(selectionService.PrimarySelection, t);
                };

            var ctrl = agds.GetView();
            if (null == ctrl) return;
            this.splitContainer1.Panel1.Controls.Add(ctrl);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var f = agds.GetIDesignerHost().RootComponent as AGForm;

            var ins = LoginDesignHelper.Instance;
            ins.MainForm = f.AGProperty;
            ins.Controls = new List<AGLoginUCEntity>();

            foreach (Control ctrl in f.Controls)
                if (ctrl is AGControl) ins.Controls.Add(((AGControl)ctrl).AGProperty);

            ins.Save();
            if (MessageBox.Show("保存成功，请将当前目录下的【" + LoginDesignHelper.xmlFile + "】配置文件放到正式项目中，要打开当前文件吗？", "保存成功", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                System.Diagnostics.Process.Start("Explorer", "/select," + AppDomain.CurrentDomain.BaseDirectory + LoginDesignHelper.xmlFile);

        }

        private void FormLoginSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("确定是否已保存当前配置，如果要保存当前配置请点击“确定”按钮！", "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //    btnSave_Click(null, null);
            //}
        }
    }
}
