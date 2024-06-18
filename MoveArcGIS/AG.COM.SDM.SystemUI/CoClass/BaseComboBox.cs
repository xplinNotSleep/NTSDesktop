using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// �����б�� ������
    /// </summary>
    public abstract class BaseComboBox: IComboBox
    {
        #region ˽�г�Ա����
        protected string m_name="";
        protected string m_tooltip = "";
        protected string m_text = "";
        protected string m_labeltext = "";
        protected string m_caption = "";
        protected string m_message = "";
        protected string m_category = "";

        protected int m_height = 25;
        protected int m_width = 130;

   
        protected bool m_enabled = false;
        protected bool m_checked = false;

        protected Bitmap m_bitmap = null;
        protected ComboBoxStyle m_comboBoxStyle = ComboBoxStyle.DropDown;

        protected object m_dataSource = null;
        #endregion

        #region IComboBox ��Ա
        /// <summary>
        /// ��ȡ��ǩ�ı�
        /// </summary>
        public virtual string LabelText
        {
            get
            {
                return this.m_labeltext;
            }
        }


        /// <summary>
        /// ��ȡ�ö���ĸ߶�
        /// </summary>
        public virtual int Height
        {
            get { return this.m_height ; }
        }

        /// <summary>
        /// ��ȡ�ö���Ŀ��
        /// </summary>
        public virtual int Width
        {
            get { return this.m_width; }
        }

        /// <summary>
        /// ��ȡ�ö���İ�Դ.
        /// ������ֵΪʵ�� IList �ӿڵĶ����� DataSet �� Array��
        /// </summary>
        public virtual object DataSource
        {
            get { return m_dataSource; }
        }

        /// <summary>
        /// ��ȡ��������ʽֵ
        /// </summary>
        public virtual ComboBoxStyle ComboBoxStyle
        {
            get 
            {
                return this.m_comboBoxStyle;
            }
        }

        public virtual void OnSelectedIndexChanged(object sender, EventArgs e)
        {
           
        } 

        /// <summary>
        /// �������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnKeyDown(object sender, KeyEventArgs e)
        {
  
        }

        /// <summary>
        /// �������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        public virtual void OnKeyPress(object sender, KeyPressEventArgs e)
        {
        }

        #endregion

        #region ICommand ��Ա

        /// <summary>
        /// ��ȡ�ö��������
        /// </summary>
        public virtual string Name
        {
            get { return this.m_name; }
        }

        /// <summary>
        /// ��ȡ�ö���Ĺ�������ʾ
        /// </summary>
        public virtual string Tooltip
        {
            get { return this.m_tooltip; }
        }

        /// <summary>
        /// ��ȡ�ö���Ŀ���״̬
        /// </summary>
        public virtual bool Enabled
        {
            get { return this.m_enabled; }
        }

        /// <summary>
        /// ��ȡGDIλͼ����
        /// </summary>
        public virtual int Bitmap
        {
            get 
            {
                if (this.m_bitmap != null)
                    return m_bitmap.GetHbitmap().ToInt32();
                else
                    return 0;
            }
        }

        /// <summary>
        /// ��ȡCaptionֵ
        /// </summary>
        public virtual string Caption
        {
            get { return this.m_caption; }
        }

        /// <summary>
        /// ��ȡ��������Ŀ¼
        /// </summary>
        public virtual string Category
        {
            get { return this.m_category; }
        }

        /// <summary>
        /// ��ȡ�����ǩ��״̬ 
        /// </summary>
        public virtual bool Checked
        {
            get { return false; }
        }

        /// <summary>
        /// ��ȡ�����ļ�ID���
        /// </summary>
        public virtual int HelpContextID
        {
            get { return 0; }
        }

        /// <summary>
        /// ��ȡ�����ļ�����
        /// </summary>
        public virtual string HelpFile
        {
            get { return ""; }
        }

        /// <summary>
        /// ��ȡ��ʾ��Ϣ
        /// </summary>
        public virtual string Message
        {
            get 
            {
                return this.m_message;
            }
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public virtual void OnClick()
        {
            
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public abstract void OnCreate(object hook);

        #endregion
    }
}
