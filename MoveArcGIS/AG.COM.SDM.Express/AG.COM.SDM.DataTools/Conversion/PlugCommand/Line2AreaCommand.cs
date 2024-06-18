using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// 线转面插件类
    /// </summary>
    public sealed class Line2AreaCommand:BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Line2AreaCommand()
        {
            base.m_caption = "线层转面";
            base.m_toolTip = "线层转面";
            base.m_name = "Line2AreaCommand";    
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormLine2Area frm = new FormLine2Area();
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
