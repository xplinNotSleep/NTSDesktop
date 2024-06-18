using System;
using System.Collections;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    public partial class FormServiceManage : Form
    {
        private CtrArcgisServer m_CtrService = new CtrArcgisServer();

        public FormServiceManage()
        {
            InitializeComponent();
        }

        //添加
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = new TreeNode();
            node.Text = "新建服务";
            AGSDM_DATA_SERVICE service = new AGSDM_DATA_SERVICE();
            service.SERVICENAME = "新建服务";
            node.Tag = service;
            tvwServices.Nodes.Add(node);
            tvwServices.SelectedNode = node;
        }

        private void FormServiceManage_Load(object sender, EventArgs e)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstServices = tEntityHandler.GetEntities("from AGSDM_DATA_SERVICE");
            for (int i = 0; i < lstServices.Count; i++)
            {
                AGSDM_DATA_SERVICE t = lstServices[i] as AGSDM_DATA_SERVICE;
                TreeNode node = new TreeNode();
                node.Text = t.SERVICENAME;
                node.Tag = t;
                tvwServices.Nodes.Add(node);
            }
        }

        private void tvwServices_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (panel2.Controls.Count == 0)
            {
                panel2.Controls.Add(m_CtrService); 
            }
            m_CtrService.SetDataService(e.Node.Tag as AGSDM_DATA_SERVICE);
        }

        //删除
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (tvwServices.SelectedNode == null)
                return;

            AGSDM_DATA_SERVICE service = tvwServices.SelectedNode.Tag as AGSDM_DATA_SERVICE;
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            if (service.ID > 0)
            {
                tEntityHandler.DeleteEntity(service);
            }
            tvwServices.SelectedNode.Remove();
        }

        //保存
        private void tsbSave_Click(object sender, EventArgs e)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            foreach (TreeNode node in tvwServices.Nodes)
            {
                AGSDM_DATA_SERVICE service = node.Tag as AGSDM_DATA_SERVICE;
                if (service.ID <=0)
                {
                    tEntityHandler.AddEntity(service);
                }
                else if(service.IsChanged)
                {
                    tEntityHandler.UpdateEntity(service,service.ID);
                }
            }
        }
    }
}
