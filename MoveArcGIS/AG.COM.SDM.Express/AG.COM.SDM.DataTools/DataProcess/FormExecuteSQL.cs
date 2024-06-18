using System;
using System.Windows.Forms;
using AG.COM.SDM.QueryStat;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// SQL更新数据 窗体类
    /// </summary>
    public partial class FormExecuteSQL : Form
    {
        private IMap m_Map = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormExecuteSQL()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置地图对象
        /// </summary>
        public IMap Map
        {
            set
            {
                featureLayerSelector1.Map = value;
                m_Map = value;
            }
        } 

        private void btGenSQL_Click(object sender, EventArgs e)
        {
            IFeatureClass fcls=  featureLayerSelector1.FeatureClass;
            if (fcls == null)
            {
                MessageHandler.ShowErrorMsg("请先选择图层！",this.Text);
                return;
            }
            IFeatureLayer layer = new FeatureLayerClass();
            layer.FeatureClass = fcls;

            FormExpressionSQL frm = new FormExpressionSQL();      
            frm.FeatureLayer = layer;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txtSql.Text = frm.SqlExpression;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            IFeatureClass fcls = featureLayerSelector1.FeatureClass;
            if (fcls == null)
            {
                MessageHandler.ShowErrorMsg("请先选择图层！", this.Text);
                return;
            }
            if (txtSql.Text.Trim().Length == 0)
            {
                MessageHandler.ShowErrorMsg("请输入SQL语句！", this.Text);
                return;
            }

            IWorkspace ws = (fcls as IDataset).Workspace;
            IWorkspaceProperty prop =(ws as IWorkspaceProperties).get_Property(esriWorkspacePropertyGroupType.esriWorkspacePropertyGroup, (int)esriWorkspacePropertyType.esriWorkspacePropCanExecuteSQL);
            bool flag = (bool)prop.PropertyValue;
            if (flag == false)
            {
                MessageHandler.ShowErrorMsg("该图层不支持SQL语句！", this.Text);
                return;
            }
            try
            {
                string sql = "update " + (fcls as IDataset).Name + " set ";
                string s;
                for (int i = 0; i <= txtSql.Lines.Length - 1; i++)
                {
                    s = txtSql.Lines[i];
                    if (s.Trim().Length > 0)
                    {
                        s = sql + s;
                        ws.ExecuteSQL(s);
                    }
                } 
                MessageHandler.ShowInfoMsg("执行SQL语句完成!",Text);
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}