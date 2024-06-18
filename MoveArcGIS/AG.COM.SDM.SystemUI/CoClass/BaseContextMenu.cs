using AG.COM.SDM.SystemUI.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �����Ĳ˵� ������
    /// </summary>
    public class BaseContextMenu: IContextMenu
    {
        /// <summary>
        /// �Ҽ���ݲ˵�
        /// </summary>
        protected ContextMenuStrip m_contextMenuStrip;
        /// <summary>
        /// �Ҽ��˵�ͼƬ
        /// </summary>
        protected Bitmap m_bitmap;
        /// <summary>
        /// �Ҽ��˵���ʾ�ı�
        /// </summary>
        protected string m_caption;
        protected string m_name;
        protected bool m_enabled=true;

        #region IContextMenu ��Ա
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public BaseContextMenu()
        {
            m_contextMenuStrip = new ContextMenuStrip();
        }

        /// <summary>
        /// ��ȡ�����ø���Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.m_name;
            }
        }

        /// <summary>
        /// ��ȡ�����������Ĳ˵�ͼƬ
        /// </summary>
        public virtual int Bitmap
        {
            get
            {
                return this.m_bitmap.GetHbitmap().ToInt32();
            }
            set
            {
                //��GDIλͼ�ľ��������λͼ
                this.m_bitmap = LibImage.GetImageFromHbitmap(value) as Bitmap;
            }
        }

        /// <summary>
        /// ��ȡ�����������Ĳ˵���ʾ������
        /// </summary>
        public virtual string Caption
        {
            get
            {
                return this.m_caption;
            }
            set
            {
                this.m_caption = value;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ����Ŀ���״̬
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                return this.m_enabled;
            }
        }

        /// <summary>
        /// ȷ�������Ƿ����ָ���ؼ��ֵ���
        /// </summary>
        /// <param name="keyName">ָ���ؼ���</param>
        /// <returns>��������а��������򷵻� true,���򷵻� false</returns>
        public virtual bool ContainsItem(string keyName)
        {
            return this.m_contextMenuStrip.Items.ContainsKey(keyName);
        }

        /// <summary>
        /// ��ȡ�����Ĳ˵�
        /// </summary>
        public ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return this.m_contextMenuStrip;
            }
        }

        /// <summary>
        /// ���ݹؼ��ֲ���ƥ�����
        /// </summary>
        /// <param name="keyName">�ؼ���</param>
        /// <returns>����ҵ����ظò˵�����򷵻�null</returns>
        public virtual ToolStripItem GetItemByKeyName(string keyName)
        {
            ToolStripItem[] toolItems = this.m_contextMenuStrip.Items.Find(keyName, true);
            if (toolItems.Length > 0)
                return toolItems[0];
            else
                return null;
        }

        /// <summary>
        /// ��ָ������λ�ô�����˵���
        /// </summary>
        /// <param name="item">�˵������ Ĭ��Ϊ�̳�ICommand�ӿڵĶ���</param>
        /// <param name="index">�����ָ����Ϊ-1,����ָ������λ��</param>
        /// <param name="beginGroup">�Ƿ�ʼ����</param>
        /// <param name="style">�˵���ʽ����</param>
        public virtual void AddItem(object item, int index, bool beginGroup, ToolStripItemDisplayStyle style)
        {
            ToolStripItem toolItem = GetToolStripItem(item);

            if (toolItem != null)
            {
                if (this.m_contextMenuStrip.Items.Find(toolItem.Name, true).Length == 0)
                {
                    //������ʾ��ʽ
                    toolItem.DisplayStyle = style;

                    if (index > -1)
                    {
                        //ȷ��ָ�������Ƿ��ڷ�Χ֮��
                        if (index >= this.m_contextMenuStrip.Items.Count)
                            throw (new Exception(string.Format("[{0}] ָ���������ڷ�Χ֮��!", index)));

                        //��ָ������λ�ô�����                       
                        if (beginGroup == true)
                        {
                            //��ӷָ���
                            this.m_contextMenuStrip.Items.Insert(index, new ToolStripSeparator());
                            this.m_contextMenuStrip.Items.Insert(index + 1, toolItem);
                        }
                        else
                        {
                            this.m_contextMenuStrip.Items.Insert(index, toolItem);
                        }
                    }
                    else
                    {
                        //δָ������λ��
                        if (beginGroup == true)
                        {
                            //��ӷָ���
                            this.m_contextMenuStrip.Items.Add(new ToolStripSeparator());
                        }

                        //��������Ĳ˵���
                        this.m_contextMenuStrip.Items.Add(toolItem);
                    }
                }
                else
                {
                    throw (new Exception(string.Format("���ܼ���[{0}]��,�������Ĳ˵����Ѵ���������ͬ����.��ȷ��!", toolItem.Name)));
                }
            }
            else
            {
                throw (new Exception("[item] ����Ϊnull,���ܼ���.��ȷ��!"));
            }
        }

        /// <summary>
        /// ��ָ��λ�ô����ز˵���
        /// </summary>
        /// <param name="keyName">�ؼ���</param>
        /// <param name="index">ָ������λ��,�����ָ����Ϊ -1</param>
        /// <param name="beginGroup">�����ʼ������Ϊtrue,����Ϊfalse</param>
        /// <param name="subMenuItem">Ҫ���صĲ˵���</param>
        public virtual void AddSubMenu(string keyName, int index, bool beginGroup, ToolStripItem subMenuItem)
        {
            ToolStripItem[] toolItems = this.m_contextMenuStrip.Items.Find(keyName, true);
            if (toolItems.Length > 0)
            {
                ToolStripMenuItem toolMenuItem = toolItems[0] as ToolStripMenuItem;

                if (index < 0)
                {
                    if (beginGroup == true)
                    {
                        //��ӷָ���
                        toolMenuItem.DropDownItems.Add(new ToolStripSeparator());
                    }
                    //����Ӳ˵���
                    toolMenuItem.DropDownItems.Add(subMenuItem);
                }
                else
                {
                    if (beginGroup == true)
                    {
                        //��ӷָ���
                        toolMenuItem.DropDownItems.Insert(index, new ToolStripSeparator());
                        //��ָ��λ�ô������Ӳ˵���
                        toolMenuItem.DropDownItems.Insert(index + 1, subMenuItem);
                    }
                    else
                    {
                        //��ָ��λ�ô������Ӳ˵���
                        toolMenuItem.DropDownItems.Insert(index, subMenuItem);
                    }
                }
            }
            else
            {
                throw (new Exception(string.Format("�������Ĳ˵����Ҳ���ָ���ؼ�����Ĳ˵�.��ȷ��!", keyName)));
            }
        }

        /// <summary>
        /// ��ָ��λ�ô����ز˵���
        /// </summary>
        /// <param name="keyName">�ؼ���</param>
        /// <param name="index">ָ������λ��,�����ָ����Ϊ -1</param>
        /// <param name="beginGroup">�����ʼ������Ϊtrue,����Ϊfalse</param>
        /// <param name="subMenuItem">Ҫ���صĲ˵���</param>
        public virtual void AddSubMenu(string keyName, int index, bool beginGroup, object subMenuItem)
        {
            //ת������ΪToolStripItem����
            ToolStripItem toolItem = GetToolStripItem(subMenuItem);

            ToolStripItem[] toolItems = this.m_contextMenuStrip.Items.Find(keyName, true);
            if (toolItems.Length > 0)
            {
                ToolStripMenuItem toolMenuItem = toolItems[0] as ToolStripMenuItem;

                if (index < 0)
                {
                    if (beginGroup == true)
                    {
                        //��ӷָ���
                        toolMenuItem.DropDownItems.Add(new ToolStripSeparator());
                    }
                    //����Ӳ˵���
                    toolMenuItem.DropDownItems.Add(toolItem);
                }
                else
                {
                    if (beginGroup == true)
                    {
                        //��ӷָ���
                        toolMenuItem.DropDownItems.Insert(index, new ToolStripSeparator());
                        //��ָ��λ�ô������Ӳ˵���
                        toolMenuItem.DropDownItems.Insert(index + 1, toolItem);
                    }
                    else
                    {
                        //��ָ��λ�ô������Ӳ˵���
                        toolMenuItem.DropDownItems.Insert(index, toolItem);
                    }
                }
            }
            else
            {
                throw (new Exception(string.Format("�������Ĳ˵����Ҳ���ָ���ؼ�����Ĳ˵�.��ȷ��!", keyName)));
            }
        }

        /// <summary>
        /// �ڹ�괦��ʾ�����������Ĳ˵�
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="intPtr"></param>
        public virtual void PopupMenu(int X, int Y, IntPtr intPtr)
        {
            //���ص�ǰ��ָ����������Ŀؼ�
            Control tControl = Control.FromHandle(intPtr);
            this.m_contextMenuStrip.Show(tControl, X, Y);
        }

        /// <summary>
        /// ���ָ���ľ���ؼ������������Ĳ˵�(ArcEngine)
        /// </summary>
        /// <param name="X">X�������</param>
        /// <param name="Y">Y�������</param>
        /// <param name="hwndParent">ָ���ؼ��ľ��</param>
        public virtual void PopupMenu(int X, int Y, int hwndParent)
        {
            IntPtr tIntPtr = new IntPtr(hwndParent);
            //���ص�ǰ��ָ����������Ŀؼ�
            Control tControl = Control.FromHandle(tIntPtr);
            this.m_contextMenuStrip.Show(tControl, X, Y);
        }

        /// <summary>
        /// �Ƴ�ָ���������Ĳ˵�
        /// </summary>
        /// <param name="index">ָ������λ��</param>
        public virtual void Remove(int index)
        {
            this.m_contextMenuStrip.Items.RemoveAt(index);
        }

        /// <summary>
        /// �Ƴ�ָ���ؼ��ֵ���
        /// </summary>
        /// <param name="keyName">ָ���ؼ���</param>
        public virtual void RemoveByKeyName(string keyName)
        {
            if (this.m_contextMenuStrip.Items.ContainsKey(keyName))
            {
                this.m_contextMenuStrip.Items.RemoveByKey(keyName);
            }
        }

        /// <summary>
        /// �Ƴ������Ĳ˵��е�������
        /// </summary>
        public virtual void RemoveAll()
        {
            //�Ƴ�������
            this.m_contextMenuStrip.Items.Clear();
        }

        /// <summary>
        /// ���ô���hook���������ڶ����OnCreate����
        /// </summary>
        /// <param name="hook"></param>
        public virtual void OnCreate(object hook)
        {
            foreach (ToolStripItem toolItem in this.m_contextMenuStrip.Items)
            {
                //�ݹ����ø��˵������������
                SetChildHook(toolItem, hook);

                //���˵���Ӷ���ĳ�ʼ��
                OnCreate(toolItem, hook);
            }
        }

        /// <summary>
        /// ת������ΪToolStripItem
        /// </summary>
        /// <param name="item">�˵�����</param>
        /// <returns>����ToolStripItem����</returns>
        protected virtual ToolStripItem GetToolStripItem(object item)
        {
            ToolStripItem toolItem = null;

            ICommand tCommand = item as ICommand;
            if (tCommand != null)
            {
                //ʵ�����˵������
                toolItem = new ToolStripMenuItem();
                toolItem.Name = tCommand.Name;
                toolItem.Text = tCommand.Caption;
                toolItem.Image = this.GetImageFromHbitmap(tCommand.Bitmap);
                toolItem.ToolTipText = tCommand.Tooltip;
                toolItem.Tag = item;

                //ToolStripItem�����¼���ί�д���
                toolItem.Click += new EventHandler(ToolStripItemClick);
            }
            else if (item is ToolStripItem)
            {
                toolItem = item as ToolStripItem;
                toolItem.Click += new EventHandler(ToolStripItemClick);
            }

            return toolItem;
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// �ݹ����ø��˵���Ĺ��Ӷ���
        /// </summary>
        /// <param name="pToolStripItem"><see cref="ToolStripItem"/>pToolStripItem</param>
        /// <param name="hook">hook����</param>
        private void SetChildHook(ToolStripItem pToolStripItem, object hook)
        {
            ToolStripMenuItem toolStripMenuItem = pToolStripItem as ToolStripMenuItem;
            if (toolStripMenuItem != null)
            {
                foreach (ToolStripItem toolItem in toolStripMenuItem.DropDownItems)
                {
                    //�ݹ���ô˷���
                    SetChildHook(toolItem, hook);

                    //���˵���Ӷ���ĳ�ʼ��
                    OnCreate(toolItem, hook);
                }
            }
        }

        /// <summary>
        /// ���˵���Ӷ���ĳ�ʼ��
        /// </summary>
        /// <param name="pToolStripItem"><see cref="ToolStripItem"/>pToolStripItem</param>
        /// <param name="hook">hook����</param>
        private void OnCreate(ToolStripItem pToolStripItem, object hook)
        {
            //��ѯ���ýӿ�
            ICommand tCommand = pToolStripItem.Tag as ICommand;
            if (tCommand != null)
            {
                //������������
                tCommand.OnCreate(hook);
                //���ö���Ŀ���״̬
                pToolStripItem.Enabled = tCommand.Enabled;
                //���ö���ĵ�ѡ��״̬
                ToolStripMenuItem toolStripMenuitem = pToolStripItem as ToolStripMenuItem;
                if (toolStripMenuitem != null)
                    toolStripMenuitem.Checked = tCommand.Checked;               
            }
        } 

        /// <summary>
        /// ToolStripItem�����¼�������
        /// </summary>
        /// <param name="sender">�¼��ķ�����</param>
        /// <param name="e">�¼�����</param>
        private void ToolStripItemClick(object sender, EventArgs e)
        {
            ToolStripItem toolItem = sender as ToolStripItem;
            if (toolItem.Tag != null)
            {
                //��ѯ���ýӿ�
                ICommand tCommand = toolItem.Tag as ICommand;
                if (tCommand != null)
                {
                     //��������
                     tCommand.OnClick();
                }
            }
        }

        /// <summary>
        /// ��GDIλͼ�ľ��������λͼ����
        /// </summary>
        /// <param name="gdi32">GDIλͼ���ֵ</param>
        /// <returns>����λͼ����</returns>
        private Image GetImageFromHbitmap(int gdi32)
        {
            if (gdi32 == 0) return null;

            IntPtr pIntPtr = new IntPtr(gdi32);
            //�Ӿ������Bitmap
            Bitmap pBitmap = System.Drawing.Bitmap.FromHbitmap(pIntPtr);
            //ʹ����ɫ͸��
            pBitmap.MakeTransparent();

            return pBitmap;
        }
        #endregion
    }
}
