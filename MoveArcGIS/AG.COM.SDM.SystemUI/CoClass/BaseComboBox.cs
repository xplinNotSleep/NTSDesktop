using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// 下拉列表框 基础类
    /// </summary>
    public abstract class BaseComboBox: IComboBox
    {
        #region 私有成员变量
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

        #region IComboBox 成员
        /// <summary>
        /// 获取标签文本
        /// </summary>
        public virtual string LabelText
        {
            get
            {
                return this.m_labeltext;
            }
        }


        /// <summary>
        /// 获取该对象的高度
        /// </summary>
        public virtual int Height
        {
            get { return this.m_height ; }
        }

        /// <summary>
        /// 获取该对象的宽度
        /// </summary>
        public virtual int Width
        {
            get { return this.m_width; }
        }

        /// <summary>
        /// 获取该对象的绑定源.
        /// 该属性值为实现 IList 接口的对象，如 DataSet 或 Array。
        /// </summary>
        public virtual object DataSource
        {
            get { return m_dataSource; }
        }

        /// <summary>
        /// 获取下拉框样式值
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
        /// 键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnKeyDown(object sender, KeyEventArgs e)
        {
  
        }

        /// <summary>
        /// 键弹起事件
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

        #region ICommand 成员

        /// <summary>
        /// 获取该对象的名称
        /// </summary>
        public virtual string Name
        {
            get { return this.m_name; }
        }

        /// <summary>
        /// 获取该对象的工具栏提示
        /// </summary>
        public virtual string Tooltip
        {
            get { return this.m_tooltip; }
        }

        /// <summary>
        /// 获取该对象的可用状态
        /// </summary>
        public virtual bool Enabled
        {
            get { return this.m_enabled; }
        }

        /// <summary>
        /// 获取GDI位图对象
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
        /// 获取Caption值
        /// </summary>
        public virtual string Caption
        {
            get { return this.m_caption; }
        }

        /// <summary>
        /// 获取对象所在目录
        /// </summary>
        public virtual string Category
        {
            get { return this.m_category; }
        }

        /// <summary>
        /// 获取对象的签出状态 
        /// </summary>
        public virtual bool Checked
        {
            get { return false; }
        }

        /// <summary>
        /// 获取帮助文件ID编号
        /// </summary>
        public virtual int HelpContextID
        {
            get { return 0; }
        }

        /// <summary>
        /// 获取帮助文件名称
        /// </summary>
        public virtual string HelpFile
        {
            get { return ""; }
        }

        /// <summary>
        /// 获取提示信息
        /// </summary>
        public virtual string Message
        {
            get 
            {
                return this.m_message;
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public virtual void OnClick()
        {
            
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public abstract void OnCreate(object hook);

        #endregion
    }
}
