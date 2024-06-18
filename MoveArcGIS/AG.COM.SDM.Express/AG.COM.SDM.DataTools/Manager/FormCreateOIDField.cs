using System;
using System.Windows.Forms;
using AG.COM.SDM.Utility.Common;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// 创建OID字段 窗体类
    /// </summary>
    public partial class FormCreateOIDField : Form
    {
        private IMap m_Map = null;

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

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public FormCreateOIDField()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            IFeatureClass fcls = featureLayerSelector1.FeatureClass;
            if (fcls == null)
            {
                MessageHandler.ShowErrorMsg("图层不能为空！", this.Text);
                return;
            }
            if (fcls.HasOID)
            {
                MessageHandler.ShowErrorMsg("该图层已有OID字段！", this.Text);
                return;
            }

            try
            {
                IField fld = new FieldClass();
                (fld as IFieldEdit).Name_2 = "ObjectID";
                (fld as IFieldEdit).Type_2 = esriFieldType.esriFieldTypeOID;
                fcls.AddField(fld);

                if (DialogResult.No == MessageBox.Show("添加成功！", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMsg(ex.Message, this.Text);
                return;
            }
        }

        private void FormCreateOIDField_Load(object sender, EventArgs e)
        {
            featureLayerSelector1.RegisterCustomFilter(new AG.COM.SDM.GeoDataBase.LayerFilterDelegate(MyFilter));
        }

        private bool MyFilter(ILayer layer)
        {
            if (layer is IFeatureLayer)
            {
                if ((layer as IFeatureLayer).FeatureClass.HasOID)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
    }
}