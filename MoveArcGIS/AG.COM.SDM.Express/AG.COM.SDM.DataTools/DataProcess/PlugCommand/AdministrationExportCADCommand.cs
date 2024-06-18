using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public class AdministrationExportCADCommand : BaseTool, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        private FormSelectExtentExportCAD m_FormSelectExtentExportCAD = null;

        /// <summary>
        /// 初始化对象实例
        /// </summary>
        public AdministrationExportCADCommand()
        {
            base.m_caption = "按行政区导出CAD";
            base.m_name = GetType().FullName;
            base.m_message = "按行政区导出CAD";
            base.m_cursor = new Cursor(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_CURPATH + "PrintSelectExtent.cur"));

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "F5+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "F5+_32.ico"));   
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

        public override void OnCreate(object hook)
        {
            m_hookHelper.Hook = hook;
        }

        public override void OnClick()
        {
            try
            {
                //从打印模板参数中获取图幅图层名称和图幅字段名称
                SystemParaHelper.ReadFromXml();
               
                string XZQFeatureClass = SystemParaHelper.GetParaValue(ExportCADConst.XZQFeatureClassName);
                if (string.IsNullOrEmpty(XZQFeatureClass))
                {
                    MessageBox.Show("请在系统参数设置中设置名为（" + ExportCADConst.XZQFeatureClassName
                        + "）的行政区图层名称");
                    return;
                }

                string XZQField = SystemParaHelper.GetParaValue(ExportCADConst.XZQMCFieldName);
                if (string.IsNullOrEmpty(XZQField))
                {
                    MessageBox.Show("请在系统参数设置中设置名为（" + ExportCADConst.XZQMCFieldName
                        + "）的行政区名称字段名称");
                    return;
                }

                string TFFeatureClass = SystemParaHelper.GetParaValue(ExportCADConst.TFFeatureClassName);
                if (string.IsNullOrEmpty(TFFeatureClass))
                {
                    MessageBox.Show("请在系统参数设置中设置名为（" + ExportCADConst.TFFeatureClassName
                        + "）的图幅图层名称");
                    return;
                }

                string TFField = SystemParaHelper.GetParaValue(ExportCADConst.TFMCFieldName);
                if (string.IsNullOrEmpty(TFField))
                {
                    MessageBox.Show("请在系统参数设置中设置名为（" + ExportCADConst.TFMCFieldName
                        + "）的图幅名称字段名称");
                    return;
                }

                SelectExtentExportCADInitTag tInitTag = new SelectExtentExportCADInitTag();
                tInitTag.PrintTheme = "行政区";

                //清空所有保存的临时图形
                //IGraphicsContainer container = m_hookHelper.ActiveView.GraphicsContainer;
                //container.DeleteAllElements();
                //m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                if (m_FormSelectExtentExportCAD == null)
                {
                    m_FormSelectExtentExportCAD = new FormSelectExtentExportCAD(tInitTag, m_hookHelper);
                    m_FormSelectExtentExportCAD.FormClosed += new FormClosedEventHandler(FormSelectExtentPrint_OnFormClosed);
                    m_FormSelectExtentExportCAD.ExtentFeatureClassName = XZQFeatureClass;
                    m_FormSelectExtentExportCAD.ExtentFieldName = XZQField;                   
                    m_FormSelectExtentExportCAD.Show(m_hookHelper.Win32Window);
                }
                else
                {
                    if (!m_FormSelectExtentExportCAD.Visible)
                    {
                        m_FormSelectExtentExportCAD.Show(m_hookHelper.Win32Window);
                    }
                    m_FormSelectExtentExportCAD.WindowState = FormWindowState.Normal;
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            if (m_FormSelectExtentExportCAD == null) return;

            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);

            IGeometry tGeometry = m_hookHelper.MapService.MapControl.TrackPolygon();

            if (m_FormSelectExtentExportCAD != null && m_FormSelectExtentExportCAD.WindowState == FormWindowState.Minimized)
            {
                m_FormSelectExtentExportCAD.WindowState = FormWindowState.Normal;

                if (tGeometry != null)
                {
                    m_FormSelectExtentExportCAD.OuterSelectExtent(tGeometry);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
            }
        }

        private void FormSelectExtentPrint_OnFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {               
                m_FormSelectExtentExportCAD = null;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
    }
}
