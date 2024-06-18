using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// 重新设置标识码 插件类
    /// </summary>
    public class ResetBSMCommand : BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ResetBSMCommand()
        {
            base.m_caption = "重新设置标识码";
            base.m_toolTip = "重新设置标识码";
            base.m_category = "土地利用工具";
            base.m_name = "ResetBSMCommand";
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            FormResetBSM frm = new FormResetBSM();
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
