using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Common
{
    /// <summary>
    /// 构造范围选择 插件类
    /// </summary>
    public sealed class CreateStructEnvCommand : BaseCommand 
    {
        private IHookHelperEx m_HookHelper = new HookHelperEx();
     
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CreateStructEnvCommand()
        {
            base.m_caption = "构造范围选择";
            base.m_toolTip = "构造范围选择";
            base.m_name = "CrateStructEnvCommand";  
        }

        /// <summary>
        /// 获取当前对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_HookHelper.FocusMap.LayerCount == 0) return false;
                if (this.m_HookHelper.FocusMap.SelectionCount == 0) return false;

                return true;
            }
        }

        /// <summary>
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            //实例化对象
            FormCreateStructEnv tFormCreateEnv = new FormCreateStructEnv(this.m_HookHelper.FocusMap);
            tFormCreateEnv.ShowDialog();          
        } 

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            this.m_HookHelper.Hook = hook;    
        } 
    }
}
