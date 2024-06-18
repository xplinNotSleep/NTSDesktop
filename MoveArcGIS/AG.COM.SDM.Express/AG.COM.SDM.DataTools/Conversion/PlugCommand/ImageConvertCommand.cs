using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    ///<summary>
    /// 栅格数据格式转换工具
    ///</summary>
    public class ImageConvertCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        public ImageConvertCommand()
        {
            base.m_caption = "栅格数据格式转换工具";
            base.m_toolTip = "栅格数据格式转换工具";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C16_32.ico"));         
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
            FormImageConvert tImageConvert = new FormImageConvert();
            tImageConvert.ShowDialog(this.m_hookHelper.Win32Window);           
        }

        /// <summary>
        /// 创建时处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;
        }
    }
}
