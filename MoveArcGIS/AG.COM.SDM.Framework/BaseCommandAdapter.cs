using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// BaseCommand��������,���ArcGISϵͳ��BaseCommand���߲���ֱ���ڱ�ϵͳ��Ӧ�õ����⡣
    /// </summary>
    public class BaseCommandAdapter:BaseCommand
    {
        protected ICommand m_Command = null;

        /// <summary>
        /// ʵ�����¶���
        /// </summary>
        public BaseCommandAdapter()
        {
        }

        /// <summary>
        /// ��ȡͼ����
        /// </summary>
        public override int Bitmap
        {
            get
            {
                if (base.m_bitmap != null)
                    return (int)m_bitmap.GetHbitmap();
                else if (m_Command != null)
                    return m_Command.Bitmap;
                else
                    return base.Bitmap;
            }
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public override string Caption
        {
            get
            {
                if (base.m_caption!=null)
                    return m_caption;
                else if (m_Command != null)
                    return m_Command.Caption;
                else 
                    return "";
            }
        }

        /// <summary>
        /// ��ȡ�������Ŀ¼
        /// </summary>
        public override string Category
        {
            get
            {
                if (base.m_category != null)
                    return base.m_category;
                else if (m_Command != null)
                    return m_Command.Category;
                else 
                    return "";
            }
        }

        /// <summary>
        /// ��ȡ��ǰ���ѡ��״̬
        /// </summary>
        public override bool Checked
        {
            get
            {
                if (m_Command != null)
                    return m_Command.Checked;
                else
                    return base.Checked;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ�������״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_Command != null)
                    return m_Command.Enabled;
                else
                    return base.Enabled;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ��������ĵ�ID��
        /// </summary>
        public override int HelpContextID
        {
            get
            {                 
                if (m_Command != null)
                    return m_Command.HelpContextID;
                else
                    return base.HelpContextID;
            }
        }

        public override void OnClick()
        {
            if (m_Command != null)
                m_Command.OnClick();
            else
                base.OnClick();
        }

        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        public override string HelpFile
        {
            get
            {
                if (base.m_helpFile != null)
                    return base.m_helpFile;
                else if (m_Command != null)
                    return m_Command.HelpFile;
                else
                    return "";
            }
        }

        /// <summary>
        /// ��ȡ��Ϣ��ʾ
        /// </summary>
        public override string Message
        {
            get
            {
                if (base.m_message != null)
                    return base.m_message;
                else if (m_Command != null)
                    return m_Command.Message;
                else 
                    return "";
            }
        }

        /// <summary>
        /// ��ȡ�������
        /// </summary>
        public override string Name
        {
            get
            {
                if (base.m_name != null)
                    return base.m_name;
                else if (m_Command != null)
                    return m_Command.Name;
                else 
                    return base.Name;
            }
        }

        public override void OnCreate(object hook)
        {
            IHookHelper m_hookHelper = new HookHelper();
            //m_hookHelper.Hook = hook;
            //if (m_Command != null)
            //{
            //    m_Command.OnCreate(m_hookHelper.MapService.MapControl);
            //}
        }

        /// <summary>
        /// ��ȡ�����ʾ��Ϣ
        /// </summary>
        public override string Tooltip
        {
            get
            {
                if (base.m_toolTip != null)
                    return m_toolTip;
                else if (m_Command != null)
                    return m_Command.Tooltip;
                else
                    return "";
            }
        }         
    }
}
