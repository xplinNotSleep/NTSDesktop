using System.Drawing;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;

namespace AG.COM.SDM.DataTools.Common
{
    /// <summary>
    /// 设置可选图层 插件类
    /// </summary>
    public sealed class SetLayersSelectable : BaseCommand, IUseIcon
    {
        private HookHelperEx m_HookHelperEx = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SetLayersSelectable()
        {
            m_bitmap =  Properties.Resources.SetLayersSelectable; 
            m_caption = "设置可选图层";
            m_message = "设置可选图层";
            base.m_name = GetType().FullName;
            m_toolTip = "设置可选图层";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "SelectVisibleLayer.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "SelectVisibleLayer32.ico"));            
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
        /// 获取当前对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return (m_HookHelperEx.MapService.FocusMap.LayerCount != 0);
            }
        }

        /// <summary>
        /// 单击事件处理
        /// </summary>
        public override void OnClick()
        {
            FormSetLayersSelectable frmSetLayersSelectableNew = new FormSetLayersSelectable();
            frmSetLayersSelectableNew.Map = m_HookHelperEx.MapService.FocusMap as IBasicMap;
            frmSetLayersSelectableNew.ShowDialog();
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelperEx.Hook=hook;
        }
    }
}
