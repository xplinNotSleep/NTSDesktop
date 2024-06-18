using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// VCT到Shp转换 插件类
    /// </summary>
    public sealed class VCT2SHPCommand : BaseCommand, IUseIcon
    {   
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public VCT2SHPCommand()
        {
            base.m_caption = "VCT到Shp转换";
            base.m_toolTip = "VCT到Shp转换";
            base.m_category = "数据转换工具";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E1+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E1+_32.ico"));        
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
            string fn = Application.StartupPath + "\\vct2shp.exe";
            if (System.IO.File.Exists(fn))
            {
                System.Diagnostics.Process.Start(fn);
            }
        }

        /// <summary>
        /// 创建对象方法
        /// </summary>
        /// <param name="hook"></param>
        public override void OnCreate(object hook)
        {
        } 
    }
}
