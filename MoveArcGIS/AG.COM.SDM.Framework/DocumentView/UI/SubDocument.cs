using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Framework.DocumentView.UI
{
    public partial class SubDocument : DockDocument, IDocumentView
    {
        private IFramework m_Framework;
        public SubDocument(IFramework p_Framework)
        {
            InitializeComponent();
            m_Framework = p_Framework;
        }

        public string SubDocText
        {
            get
            {
                return this.subTextBox.Text;
            }
            set
            {
                this.subTextBox.Text = value;
            }
        }

        #region IDocumentView 成员

        /// <summary>
        /// 文档标题
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return this.TabText;
            }
            set
            {
                this.TabText = value;
            }
        }

        /// <summary>
        /// 文档类型
        /// </summary>
        public EnumDocumentType DocumentType
        {
            get { return EnumDocumentType.MainDocument; }
        }

        /// <summary>
        /// 获取AxMapControl对象
        /// </summary>
        public object Hook
        {
            get { return this.Handle; }
        }

        #endregion



    }
}
