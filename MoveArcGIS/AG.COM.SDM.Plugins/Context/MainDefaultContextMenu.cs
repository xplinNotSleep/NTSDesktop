using System;
using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Plugins.Common;
using AG.COM.SDM.Plugins.Demo;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ��ͼ�ĵ��Ҽ���ݲ˵�
    /// </summary>
    public sealed class MainDefaultContextMenu : BaseContextMenu
    {
        private IHookHelper m_hookHelper = new HookHelper();
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public MainDefaultContextMenu()
            : base()
        {
            this.AddItem(new CmdCommand(), -1, false, ToolStripItemDisplayStyle.ImageAndText);
            this.AddItem(new CmdTheme2010Black(), -1, false, ToolStripItemDisplayStyle.ImageAndText);

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

        #region �̳е�BaseContextMenu�еķ���

        ///// <summary>
        ///// ת������ΪToolStripItem
        ///// </summary>
        ///// <param name="item">�˵�����</param>
        ///// <returns>����ToolStripItem����</returns>
        //protected override ToolStripItem GetToolStripItem(object item)
        //{
        //    ToolStripItem toolItem = null;

        //    ICommand tCommand = item as ICommand;
        //    if (tCommand != null)
        //    {
        //        //ʵ�����˵������
        //        toolItem = new ToolStripMenuItem();
        //        toolItem.Name = tCommand.Name;
        //        toolItem.Text = tCommand.Caption;
        //        toolItem.Image = this.GetImageFromHbitmap(tCommand.Bitmap);
        //        toolItem.ToolTipText = tCommand.Tooltip;
        //        toolItem.Tag = item;

        //        //ToolStripItem�����¼���ί�д���
        //        toolItem.Click += new EventHandler(ToolStripItemClick);
        //    }
        //    else if (item is ToolStripItem)
        //    {
        //        toolItem = item as ToolStripItem;
        //        toolItem.Click += new EventHandler(ToolStripItemClick);
        //    }

        //    return toolItem;
        //}

        ///// <summary>
        ///// ToolStripItem�����¼�������
        ///// </summary>
        ///// <param name="sender">�¼��ķ�����</param>
        ///// <param name="e">�¼�����</param>
        //private void ToolStripItemClick(object sender, EventArgs e)
        //{
        //    ToolStripItem toolItem = sender as ToolStripItem;
        //    if (toolItem.Tag != null)
        //    {
        //        //��ѯ���ýӿ�

        //        ICommand tCommand = toolItem.Tag as ICommand;
        //        if (tCommand != null)
        //        {
        //            try
        //            {
        //                //��������
        //                tCommand.OnClick();
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(string.Format("[{0}]��������,"+ex.Message, toolItem.Text));
        //            }

        //        }

        //    }
        //}

        ///// <summary>
        ///// ��GDIλͼ�ľ��������λͼ����
        ///// </summary>
        ///// <param name="gdi32">GDIλͼ���ֵ</param>
        ///// <returns>����λͼ����</returns>
        //private Image GetImageFromHbitmap(int gdi32)
        //{
        //    if (gdi32 == 0) return null;

        //    IntPtr pIntPtr = new IntPtr(gdi32);
        //    //�Ӿ������Bitmap
        //    Bitmap pBitmap = System.Drawing.Bitmap.FromHbitmap(pIntPtr);
        //    //ʹ����ɫ͸��
        //    pBitmap.MakeTransparent();

        //    return pBitmap;
        //}

        #endregion

    }
}
