using System.Drawing;
using System;
using System.Windows.Forms;
using AG.COM.SDM.Plugins.Common;
using AG.COM.SDM.Plugins.Demo;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ������ͼͼ�������Ĳ˵� �����
    /// </summary>
    public sealed class TocDefaultContextMenu:BaseContextMenu
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public TocDefaultContextMenu()
            : base()
        {
            this.AddItem(new CmdTheme2010Blue(), -1, false, ToolStripItemDisplayStyle.ImageAndText);
            this.AddItem(new CmdTheme2013Gray(), -1, false, ToolStripItemDisplayStyle.ImageAndText);

        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            //this.m_hookHelper.Hook = hook;
        }


    }
}
