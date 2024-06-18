using AG.COM.SDM.SystemUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// BaseCommand类适配器,解决ArcGIS系统中BaseCommand工具不能直接在本系统中应用的问题。
    /// </summary>
    public class BaseCommandAdapter:BaseCommand
    {
        protected ICommand m_Command = null;

        /// <summary>
        /// 实例化新对象
        /// </summary>
        public BaseCommandAdapter()
        {
        }

        /// <summary>
        /// 获取图像句柄
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
        /// 获取标题属性
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
        /// 获取插件所处目录
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
        /// 获取当前插件选中状态
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
        /// 获取当前插件可用状态
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
        /// 获取当前插件帮助文档ID号
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
        /// 获取帮助文件
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
        /// 获取消息提示
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
        /// 获取插件名称
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
        /// 获取插件提示信息
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
