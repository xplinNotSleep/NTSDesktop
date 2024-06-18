using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using System;
using System.Drawing;

namespace AG.COM.SDM.Plugins.WindView
{
    /// <summary>
    /// 帮助视图控制 插件类
    /// </summary>
    public sealed class HelpView : BaseCommand, IUseIcon
    {
        private IHookHelper m_HookHelper = new HookHelper();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public HelpView()
            : base()
        {
            base.m_caption = "帮助视图";
            base.m_toolTip = "帮助视图";
            base.m_name = GetType().FullName;
            base.m_category = "帮助视图";

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "helpView16.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariant.STR_IMAGEPATH + "helpView32.ico"));           
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
                if (this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.HelpDocument.ToString()) == null)
                {
                    return base.m_checked = false;
                }
                bool bIsHidden = this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.HelpDocument.ToString()).IsHidden;
                base.m_checked = !bIsHidden;
                return base.m_checked;
            }
        }

        /// <summary>
        /// 获取对象可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return this.m_HookHelper.DockDocumentService.ContainsDocument(EnumDocumentType.HelpDocument.ToString());
            }
        }

        /// <summary>
        /// 单击对象处理方法
        /// </summary>
        public override void OnClick()
        {
            if (this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.HelpDocument.ToString()) == null)
            {
                //帮助文档对象
                AG.COM.SDM.Framework.DocumentView.HelpDocument tHelpDocument = new AG.COM.SDM.Framework.DocumentView.HelpDocument();
                this.m_HookHelper.DockDocumentService.AddDockDocument(Convert.ToString(EnumDocumentType.HelpDocument), tHelpDocument, DockState.Right);
            }
            if (this.m_checked == true)
            {
                this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.HelpDocument.ToString()).AutoHide = true;
                m_checked = false;
            }
            else
            {
                this.m_HookHelper.DockDocumentService.GetDockDocument(EnumDocumentType.HelpDocument.ToString()).AutoHide = false;
                m_checked = true;
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
