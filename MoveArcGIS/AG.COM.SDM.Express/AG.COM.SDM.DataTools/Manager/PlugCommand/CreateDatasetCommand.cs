using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.SystemUI;
using System.Drawing;

namespace AG.COM.SDM.DataTools.Manager
{
    /// <summary>
    /// 创建数据集插件类
    /// </summary>
    public sealed class CreateDatasetCommand : BaseCommand, IUseIcon
    { 
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CreateDatasetCommand()
        {
            base.m_caption = "创建数据集";
            base.m_toolTip = "创建数据集";
            base.m_category = "数据管理工具";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C03.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C03_32.ico"));   
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormCreateDataset frm = new FormCreateDataset();         
            frm.ShowDialog(); 
        }

        /// <summary>
        /// 创建时处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
           
        }
    }
}
