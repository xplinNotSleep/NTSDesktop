using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AG.COM.SDM.Framework;
//using ESRI.ArcGIS.ADF.BaseClasses;
using AG.COM.SDM.SystemUI;
using System.Drawing;
using AG.COM.SDM.Workflow.UI;
using AG.COM.SDM.Utility;
using System.Windows.Forms;

namespace AG.COM.SDM.Plugins.Common
{
    public class WorkflowCommand : AGBaseCommand
    {
        /// <summary>
        /// 初始化对象实例
        /// </summary>
        public WorkflowCommand()
        {
            m_caption = "工作流";          

            m_Image = AG.COM.SDM.Plugins.Properties.Resources.workflow16;
            m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.workflow32;           
        }      

       
        public override void OnClick()
        {
            try
            {
                FormWorkflowNew frmWorkflowNew = new FormWorkflowNew();
                frmWorkflowNew.ShowDialog();
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
