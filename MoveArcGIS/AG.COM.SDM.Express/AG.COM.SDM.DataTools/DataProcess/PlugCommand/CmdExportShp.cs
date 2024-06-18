using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.DataTools.DataProcess
{
    public class CmdExportShp : AGBaseCommand
    {
        /// <summary>
        /// 初始化对象实例
        /// </summary>
        public CmdExportShp()
        {
            base.m_caption = "导出Shp";
            base.m_name = GetType().FullName;

            //this.m_Icon = AG.PS.DATA.Plugins.Properties.Resources.MdbImport_16;
            //this.m_Icon32 = AG.PS.DATA.Plugins.Properties.Resources.MdbImport_32;
        }

        public override void OnClick()
        {
            try
            {
                FormExportShp form = new FormExportShp(m_HookHelper);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
    }
}
