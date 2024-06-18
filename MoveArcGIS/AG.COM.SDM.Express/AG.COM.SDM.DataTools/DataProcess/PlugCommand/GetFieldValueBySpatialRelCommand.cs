using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 根据空间关系生成字段值 插件类
    /// </summary>
    public class GetFieldValueBySpatialRelCommand : BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GetFieldValueBySpatialRelCommand()
        {
            base.m_caption = "根据空间关系生成字段值";
            base.m_toolTip = "根据空间关系生成字段值";
            base.m_category = "土地利用工具";
            base.m_name = "CreateFieldValueBySpatialRelCommand"; 
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormCreateFieldValueBySpatialRel frm = new FormCreateFieldValueBySpatialRel();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog();
        } 
    
        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;               
        }
    }
}
