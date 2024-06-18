using System;
using System.Collections.Generic;
using System.Text;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// BaseTool��������,���ArcGISϵͳ��BaseTool���߲���ֱ���ڱ�ϵͳ��Ӧ�õ����⡣
    /// </summary>
    public class BaseToolAdapter:BaseTool
    {
        protected ITool m_AETool = null;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public BaseToolAdapter()
        {
             
        }
        
        /// <summary>
        /// ��ȡλͼ����
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
        /// ��ȡ������ʾ�ı�
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
        /// ��ȡ��������Ŀ¼
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
        /// ��ȡ�ö����ѡ��״̬
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
        /// ��ȡ�ö���Ŀ���״̬
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
        /// ��ȡ�ö���İ����ĵ�ID��
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
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            if (m_AETool != null)
                (m_AETool as ICommand).OnClick();
            else
                base.OnClick();
        }

        /// <summary>
        /// ��ȡ�ö���İ����ı�
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
        /// ��ȡ�ö������Ϣ��Ϣ
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
        /// ��ȡ�ö��������
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
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
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
        /// ��ȡ�ö������ʾ��Ϣ
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
        /// ��ȡ�ö�������ʱ���
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
