using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins.WindView
{
    /// <summary>
    /// 数据视图控制插件类 
    /// </summary>
    public sealed class TocView : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TocView()
            : base()
        {
            base.m_caption = "数据视图";
            base.m_toolTip = "数据视图";
            base.m_name = GetType().FullName;
            base.m_category = "视图控制";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "CatalogTreeWindowShow16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "CatalogTreeWindowShow32.ico"));             
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
        /// 获取对象选中状态
        /// </summary>
        public override bool Checked
        {
            get
            {
                bool bIsHidden = this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()).IsHidden;
                return !bIsHidden;
            }
        }

        /// <summary>
        /// 获取对象可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return this.m_HookHelper.DockDocumentService.ContainsDocument(EnumDocumentType.TocDocument.ToString());
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (this.m_checked == true)
            {
                this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()).AutoHide = true;
                m_checked = false;
                MessageBox.Show("设置为自动隐藏");
            }
            else
            {
                this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.TocDocument.ToString()).AutoHide = false;
                m_checked = true;
                MessageBox.Show("取消自动隐藏");
            }
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelper.Framework = hook as IFramework;
        }
    }
}
