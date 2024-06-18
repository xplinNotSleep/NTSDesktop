using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 面转线 插件类
    /// </summary>
    public class Area2LineCommand : BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Area2LineCommand()
        { 
            base.m_caption = "面层转线层";
            base.m_toolTip = "面层转线层";
            base.m_category = "数据转换工具";
            base.m_name = "Area2LineCommand";
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormArea2Line frm = new FormArea2Line();
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
