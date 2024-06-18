using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// SQL更新插件类
    /// </summary>
    public class ExcuteSqlCommand : BaseCommand, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ExcuteSqlCommand()
        {
            base.m_caption = "SQL更新";
            base.m_toolTip = "SQL更新";
            base.m_category = "数据处理工具";
            base.m_name = GetType().FullName;

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E7.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "E7_32.ico"));        
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
            FormExecuteSQL frm = new FormExecuteSQL();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog();
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
