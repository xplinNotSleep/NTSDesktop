using AG.COM.SDM.SystemUI.Utility;
using System;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// ��ɫ�幤�������
    /// </summary>
    public class BaseToolPalette : IToolPalette
    {
        protected ToolStripDropDown m_ToolStripDropDown;
        protected ToolStripItem m_ActiveItem;
        protected string m_Name;      
        protected string m_Caption;
        protected bool m_Enabled = true;
        protected object m_hook;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public BaseToolPalette()
        {
            //ʵ���������б�����
            this.m_ToolStripDropDown = new ToolStripDropDown();
            this.m_ToolStripDropDown.Tag = this;
            //���ò�����ʽ
            this.m_ToolStripDropDown.LayoutStyle = ToolStripLayoutStyle.Table;
            //����������������
            (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).ColumnCount = 3;           
        }

        /// <summary>
        /// ��ȡ����Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ɫ������
        /// </summary>
        public virtual string Caption
        {
            get
            {
                return this.m_Caption;
            }
            set
            {
                this.m_Caption = value;
            }
        }

        /// <summary>
        /// ���ض���Ŀ���״̬
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                return this.m_Enabled;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ɫ�岼��������������
        /// </summary>
        public virtual int ColumnCount
        {
            get
            {
                return (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).ColumnCount;
            }
            set
            {
                (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).ColumnCount = value;
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ɫ�岼��������������
        /// </summary>
        public virtual int RowCount
        {
            get
            {
                return (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).RowCount;
            }
            set
            {
                (this.m_ToolStripDropDown.LayoutSettings as TableLayoutSettings).RowCount = value;
            }
        }

        /// <summary>
        /// ����������ɫ������Ĺ���������
        /// </summary>
        public virtual int Count
        {
            get
            {
                return this.m_ToolStripDropDown.Items.Count;
            }
        }

        /// <summary>
        /// ȷ���������Ƿ����ָ���ؼ��ֵ���
        /// </summary>
        /// <param name="keyName">ָ���ؼ���</param>
        /// <returns>��������򷵻� true�����򷵻� false</returns>
        public virtual bool ContainsItem(string keyName)
        {
            return this.m_ToolStripDropDown.Items.ContainsKey(keyName);
        }

        /// <summary>
        /// ��ȡָ���Ĺؼ��ֻ�ȡToolStripItem����
        /// </summary>
        /// <param name="keyName">ָ���ؼ���</param>
        /// <returns>�������ָ���ؼ��ֶ����򷵻ظö��󣬷��򷵻� null</returns>
        public virtual ToolStripItem GetItemByKeyName(string keyName)
        {
            if (ContainsItem(keyName))
            {
                ToolStripItem[] toolItems = this.m_ToolStripDropDown.Items.Find(keyName, false);
                return toolItems[0];
            }

            return null;
        }

        /// <summary>
        /// ��ȡToolStripDropDown
        /// </summary>
        public virtual ToolStripDropDown ToolStripDropDown
        {
            get
            {
                return this.m_ToolStripDropDown;
            }
        }

        /// <summary>
        /// ��ָ������λ�ô�������
        /// </summary>
        /// <param name="item">����ʵ��ICommand�ӿڵĶ������ToolStripButton</param>
        /// <param name="index">��������λ�ã����δָ����Ϊ -1</param> 
        public virtual void AddItem(object item, int index)
        {
            if (index >= this.m_ToolStripDropDown.Items.Count)
                throw (new Exception("index ����ֵ������Χ"));

            ToolStripItem toolItem = null;

            //��ѯ���ýӿ�
            ICommand tCommand = item as ICommand;
            if (tCommand != null)
            {
                //ʵ����ToolStripButton����
                toolItem = new ToolStripButton();
                toolItem.Name = tCommand.Name;
                toolItem.Text = tCommand.Caption;
                //ָ����GDIλͼ����д�������
                toolItem.Image = LibImage.GetImageFromHbitmap(tCommand.Bitmap);
                toolItem.ToolTipText = tCommand.Tooltip;
                toolItem.Tag = item;
                toolItem.Click += new EventHandler(ToolStripItemClick);
            }
            else if (item is ToolStripItem)
            {
                toolItem = item as ToolStripItem;
            }

            if (toolItem != null)
            {
                if (index > 0)
                    this.m_ToolStripDropDown.Items.Insert(index, toolItem);
                else
                    //��ָ������λ�ô��������
                    this.m_ToolStripDropDown.Items.Add(toolItem);
            }
        }

        /// <summary>
        /// �Ƴ�ָ���ؼ��ֵĶ���
        /// </summary>
        /// <param name="keyName">ָ���Ĺؼ��ֶ���</param>
        public virtual void RemoveByKey(string keyName)
        {
            if (this.m_ToolStripDropDown.Items.ContainsKey(keyName))
            {
                this.m_ToolStripDropDown.Items.RemoveByKey(keyName);
            }
        }

        /// <summary>
        /// ���������
        /// </summary>
        public virtual void RemoveAll()
        {
            this.m_ToolStripDropDown.Items.Clear();
        }

        /// <summary>
        /// ��ָ��λ�ô���ʾ
        /// </summary>
        /// <param name="X">X����</param>
        /// <param name="Y">Y����</param>
        /// <param name="hWndParent">�ؼ����</param>
        public virtual void PopupPalette(int X, int Y, int hWndParent)
        {
            IntPtr tIntPtr = new IntPtr(hWndParent);
            //���ص�ǰ��ָ����������Ŀؼ�
            Control tControl = Control.FromHandle(tIntPtr);
            //��ָ��λ�ô���ʾ
            this.m_ToolStripDropDown.Show(tControl, X, Y);
        }

        /// <summary>
        /// ���ù��Ӷ���
        /// </summary>
        /// <param name="hook">���Ӷ���</param>
        public virtual void OnCreate(object hook)
        {
            this.m_hook = hook;

            //ѭ�����ö���Ŀ���״̬
            foreach (ToolStripItem toolItem in this.m_ToolStripDropDown.Items)
            {
                if (toolItem.Tag != null)
                {
                    ICommand tCommand = toolItem.Tag as ICommand;
                    if (tCommand != null)
                    {
                        //����ʱ���ù��Ӷ���
                        tCommand.OnCreate(hook);
                        //���ö���Ŀ���״̬
                        toolItem.Enabled = tCommand.Enabled;
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�����õ�ǰ������
        /// </summary>
        public virtual ToolStripItem ActiveItem
        {
            get
            {
                if (this.m_ActiveItem == null && this.m_ToolStripDropDown.Items.Count>0)
                    this.m_ActiveItem = this.m_ToolStripDropDown.Items[0];

                return this.m_ActiveItem;
            }
            set
            {
                m_ActiveItem = value;
            }
        }

        /// <summary>
        /// ToolStripItem�����¼�������
        /// </summary>
        /// <param name="sender">�¼��ķ�����</param>
        /// <param name="e">�¼�����</param>
        private void ToolStripItemClick(object sender, EventArgs e)
        {
            this.m_ActiveItem = sender as ToolStripItem;
        }
    } 
}
