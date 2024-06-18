using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// BaseTool类适配器,解决ArcGIS系统中BaseTool工具不能直接在本系统中应用的问题。
    /// </summary>
    public class BaseToolAdapter:BaseTool
    {
        protected ITool m_AETool = null;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseToolAdapter()
        {
             
        }
        
        /// <summary>
        /// 获取位图对象
        /// </summary>
        public override int Bitmap
        {
            get
            {
                if (m_bitmap == null)
                {
                    if (m_AETool != null)
                        return (m_AETool as ICommand).Bitmap;
                    else
                        return -1;
                }
                else
                    return (int)m_bitmap.GetHbitmap();
            }
        }

        /// <summary>
        /// 获取对象显示文本
        /// </summary>
        public override string Caption
        {
            get
            {
                if (m_caption != null && m_caption.Length>0)
                {
                    return m_caption;
                }
                else
                {
                    if (m_AETool != null)
                        return (m_AETool as ICommand).Caption;
                    else
                        return "";
                }
                 
            }
        }

        /// <summary>
        /// 获取对象所属目录
        /// </summary>
        public override string Category
        {
            get
            {
                if (this.m_category != null && this.m_category.Length > 0)
                    return this.m_category;

                if (m_AETool != null)
                    return (m_AETool as ICommand).Category;
                else
                    return "";
            }
        }

        /// <summary>
        /// 获取该对象的选中状态
        /// </summary>
        public override bool Checked
        {
            get
            {
                if (m_AETool != null)
                    return (m_AETool as ICommand).Checked;
                return base.Checked;
            }
        }

        /// <summary>
        /// 获取该对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (m_AETool != null)
                    return (m_AETool as ICommand).Enabled;

                return base.Enabled;
            }
        }

        /// <summary>
        /// 获取该对象的帮助文档ID号
        /// </summary>
        public override int HelpContextID
        {
            get
            {
                if (m_AETool != null)
                    return (m_AETool as ICommand).HelpContextID;
                return base.HelpContextID;
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            if (m_AETool != null)
                (m_AETool as ICommand).OnClick();
            else
                base.OnClick();
        }

        /// <summary>
        /// 获取该对象的帮助文本
        /// </summary>
        public override string HelpFile
        {
            get
            {
                if (m_AETool != null)
                    return (m_AETool as ICommand).HelpFile;
                return base.HelpFile;
            }
        }

        /// <summary>
        /// 获取该对象的消息信息
        /// </summary>
        public override string Message
        {
            get
            {
                if (m_message == null)
                {
                    if (m_AETool != null)
                        return (m_AETool as ICommand).Message;
                    else
                        return "";
                }
                else
                    return m_message;
            }
        }

        /// <summary>
        /// 获取该对象的名称
        /// </summary>
        public override string Name
        {
            get
            {
                if (m_name == null)
                {
                    if (m_AETool != null)
                        return (m_AETool as ICommand).Name;
                    else
                        return "";
                }
                else
                    return m_name;
            }
        }

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            IHookHelperEx m_hookHelper = new HookHelperEx();
            m_hookHelper.Hook = hook;
            if (m_AETool != null)
            {
                (m_AETool as ICommand).OnCreate(m_hookHelper.MapService.MapControl);
            }
        }

        /// <summary>
        /// 获取该对象的提示信息
        /// </summary>
        public override string Tooltip
        {
            get
            {
                if (m_toolTip != null)
                    return m_toolTip;
                else
                {
                    if (m_AETool != null)
                        return (m_AETool as ICommand).Tooltip;
                    else
                        return "";

                }
            }
        }

        /// <summary>
        /// 获取该对象运行时光标
        /// </summary>
        public override int Cursor
        {
            get
            {
                if (m_cursor != null)
                    return (int)m_cursor.Handle;
                else
                {
                    if (m_AETool != null)
                        return m_AETool.Cursor;
                    else
                        return -1;
                }
            }
        }

        public override bool Deactivate()
        {
            if (m_AETool != null)
                return m_AETool.Deactivate();
            return base.Deactivate();
        }

        public override bool OnContextMenu(int X, int Y)
        {
            if (m_AETool != null)
                return m_AETool.OnContextMenu(X, Y);
            return base.OnContextMenu(X, Y);
        }

        public override void OnDblClick()
        {
            if (m_AETool != null)
                m_AETool.OnDblClick();
            else
                base.OnDblClick();
        }

        public override void OnKeyDown(int keyCode, int Shift)
        {
            if (m_AETool != null)
                m_AETool.OnKeyDown(keyCode, Shift);
            else
                base.OnKeyDown(keyCode, Shift);
        }

        public override void OnKeyUp(int keyCode, int Shift)
        {
            if (m_AETool != null)
                m_AETool.OnKeyUp(keyCode, Shift);
            else
                base.OnKeyUp(keyCode, Shift);
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (m_AETool != null)
                m_AETool.OnMouseDown(Button, Shift, X, Y);
            else
                base.OnMouseDown(Button, Shift, X, Y);
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            if (m_AETool != null)
                m_AETool.OnMouseMove(Button, Shift, X, Y);
            else
                base.OnMouseMove(Button, Shift, X, Y);
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            if (m_AETool != null)
                m_AETool.OnMouseUp(Button, Shift, X, Y);
            else
                base.OnMouseUp(Button, Shift, X, Y);
        }

        public override void Refresh(int hDC)
        {
            if (m_AETool != null)
                m_AETool.Refresh(hDC);
            else
                base.Refresh(hDC);
        }
    }
}
