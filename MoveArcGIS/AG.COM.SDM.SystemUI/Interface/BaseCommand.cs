using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.SystemUI
{
    public abstract class BaseCommand : ICommand
    {
        private uint E_INVALIDARG = 2147942487u;

        private IntPtr m_hBitmap;
        protected Bitmap m_bitmap;
        protected string m_caption;
        protected string m_category;
        protected string m_helpFile;
        protected int m_helpID;
        protected string m_message;
        protected string m_name;
        protected string m_toolTip;
        protected bool m_enabled = true;
        protected bool m_checked;

        public virtual string Message => m_message;

        public virtual int Bitmap
        {
            get
            {
                if (m_bitmap == null)
                {
                    return 0;
                }

                if (m_hBitmap.ToInt32() == 0)
                {
                    SetupBmpHandle();
                }

                return m_hBitmap.ToInt32();
            }
        }

        public virtual string Caption => m_caption;

        public virtual string Tooltip => m_toolTip;

        public virtual int HelpContextID => m_helpID;

        public virtual string Name => m_name;

        public virtual bool Checked => m_checked;

        public virtual bool Enabled => m_enabled;

        public virtual string HelpFile => m_helpFile;

        public virtual string Category
        {
            get
            {
                if (m_category == null || m_category == "")
                {
                    return "Misc.";
                }

                return m_category;
            }
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        protected BaseCommand()
        {
        }

        protected BaseCommand(Bitmap bitmap, string caption, string category, int helpContextId, string helpFile, string message, string name, string toolTip)
        {
            m_bitmap = bitmap;
            m_name = name;
            m_message = message;
            m_caption = caption;
            m_category = category;
            m_toolTip = toolTip;
            m_helpID = helpContextId;
            m_helpFile = helpFile;
        }

        ~BaseCommand()
        {
            if (m_hBitmap.ToInt32() != 0)
            {
                DeleteObject(m_hBitmap);
            }
        }

        public void UpdateBitmap(Bitmap bitmap)
        {
            m_bitmap.Dispose();
            if (m_hBitmap.ToInt32() != 0)
            {
                DeleteObject(m_hBitmap);
            }

            m_hBitmap = IntPtr.Zero;
            try
            {
                m_bitmap = bitmap;
                SetupBmpHandle();
                m_hBitmap = m_bitmap.GetHbitmap();
            }
            catch
            {
                Marshal.ThrowExceptionForHR((int)E_INVALIDARG);
            }
        }

        private void SetupBmpHandle()
        {
            if (m_bitmap != null)
            {
                m_bitmap.MakeTransparent(m_bitmap.GetPixel(0, 0));
                m_hBitmap = m_bitmap.GetHbitmap();
            }
        }

        public virtual void OnClick()
        {
        }

        public abstract void OnCreate(object hook);

    }
}
