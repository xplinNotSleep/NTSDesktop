using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 根据属性合并图形 插件类
    /// </summary>
    public sealed class CombineByAttributeCommand:BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CombineByAttributeCommand()
        {
            base.m_caption = "根据属性合并图形";
            base.m_toolTip = "根据属性合并图形";
            base.m_category = "数据管理工具";
            base.m_name = "CombineByAttributeCommand";
        }

        /// <summary>
        /// 创建时处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;               
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormCombineByAttribute frm = new FormCombineByAttribute();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog(); 
        }
    }
}
