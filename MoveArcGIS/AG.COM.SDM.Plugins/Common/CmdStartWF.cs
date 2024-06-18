using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Workflow.UI;
using System;

namespace AG.COM.SDM.Plugins.Common
{
    public class CmdStartWF : AGBaseCommand
    {
        public CmdStartWF()
        {
            this.m_caption = "起草工作";

            m_Image = AG.COM.SDM.Plugins.Properties.Resources.qcgz;
            m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.qcgz;
        }

        public override void OnClick()
        {
            try
            {
                if (m_HookHelper.DockDocumentService.ContainsDocument(FormStartWF.FormGUID) == false)
                {
                    FormStartWF formStartWF = new FormStartWF(m_HookHelper);
                    m_HookHelper.DockDocumentService.AddDockDocument(FormStartWF.FormGUID, formStartWF, DockState.Document);
                }
                else
                {
                    FormStartWF formStartWF = m_HookHelper.DockDocumentService.GetDockDocument(FormStartWF.FormGUID) as FormStartWF;
                    formStartWF.Show();
                }
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                AG.COM.SDM.Utility.Common.MessageHandler.ShowErrorMsg(ex.Message, "错误");
            }
        }
    }
}
