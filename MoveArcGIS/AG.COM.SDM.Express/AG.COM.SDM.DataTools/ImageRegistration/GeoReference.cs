using System.Drawing;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.ImageRegistration
{
    /// <summary>
    /// 影像图配准插件类
    /// </summary>
    public class GeoReference : BaseCommand, IUseIcon
    {
        private HookHelperEx m_HookHelperEx = new HookHelperEx();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GeoReference()
        {
            try
            {
                m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream(ConstVariant.ConstImages + "GeoReference.bmp"));
                this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C19+.ico"));
                this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "C19_32.ico"));      
            }
            catch
            {
                m_bitmap = null;
            }
            finally
            {
                m_caption = "影像图配准";
                m_message = "影像图配准";
                base.m_name = GetType().FullName;
                m_toolTip = "影像图配准";
            }
        }

        private Icon m_Icon32 = null;
        private Icon m_Icon = null;
        /// <summary>
        /// ico图标对象16*16
        /// </summary>
        public Icon Icon16
        {
            get { return m_Icon; }
        }
        /// <summary>
        /// ico图标对象32*32
        /// </summary>
        public Icon Icon32
        {
            get { return m_Icon32; }
        }

        /// <summary>
        /// 获取对象的可用状态
        /// </summary>
        public override bool Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 单击事件处理方法
        /// </summary>
        public override void OnClick()
        {
            frmGeoReference frmGeoReferenceNew = new frmGeoReference();
            frmGeoReferenceNew.HookHelperEx = m_HookHelperEx;
            frmGeoReferenceNew.ShowInTaskbar = false;
            frmGeoReferenceNew.Owner =(Form)Form.FromHandle(m_HookHelperEx.Win32Window.Handle);
            frmGeoReferenceNew.Show();
        } 

        /// <summary>
        /// 创建对象处理方法
        /// </summary>
        /// <param name="hook">hook对象</param>
        public override void OnCreate(object hook)
        {
            m_HookHelperEx.Hook = hook;
        }
    }
}
