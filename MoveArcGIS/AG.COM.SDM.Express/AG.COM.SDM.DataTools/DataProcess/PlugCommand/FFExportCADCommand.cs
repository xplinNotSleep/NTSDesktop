using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public class FFExportCADCommand: BaseTool, IUseIcon
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        private FormFFExportCAD m_FormFFExportCAD = null;

        /// <summary>
        /// 初始化对象实例
        /// </summary>
        public FFExportCADCommand()
        {
            base.m_caption = "按图幅导出CAD";
            base.m_name = GetType().FullName;
            base.m_message = "按图幅导出CAD";
            base.m_cursor = new Cursor(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_CURPATH + "PrintSelectExtent.cur"));

            this.m_Icon = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "F4+.ico"));
            this.m_Icon32 = new Icon(GetType().Assembly.GetManifestResourceStream(ConstVariantDataTools.STR_IMAGEPATH + "F4+_32.ico"));   
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
                tInitTag.PrintTheme = "分幅";

                if (m_FormFFExportCAD == null)
                {
                    m_FormFFExportCAD = new FormFFExportCAD(tInitTag, m_hookHelper);
                    m_FormFFExportCAD.FormClosed += new FormClosedEventHandler(FormSelectExtentPrint_OnFormClosed);
                    m_FormFFExportCAD.ExtentFeatureClassName = TFFeatureClass;
                    m_FormFFExportCAD.ExtentFieldName = TFField;
                    m_FormFFExportCAD.Show(m_hookHelper.Win32Window);
                }
                else
                {
                    if (!m_FormFFExportCAD.Visible)
                    {
                        m_FormFFExportCAD.Show(m_hookHelper.Win32Window);
                    }
                    m_FormFFExportCAD.WindowState = FormWindowState.Normal;
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
            if (m_FormFFExportCAD == null) return;

            m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);

            IGeometry tGeometry = m_hookHelper.MapService.MapControl.TrackPolygon();

            if (m_FormFFExportCAD != null && m_FormFFExportCAD.WindowState == FormWindowState.Minimized)
            {
                m_FormFFExportCAD.WindowState = FormWindowState.Normal;

                if (tGeometry != null)
                {
                    m_FormFFExportCAD.OuterSelectExtent(tGeometry);
                    m_hookHelper.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewForeground, null, null);
                }
            }
        }

        private void FormSelectExtentPrint_OnFormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                m_FormFFExportCAD = null;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
    }
}
