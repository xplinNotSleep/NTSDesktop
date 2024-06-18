using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using AG.COM.SDM.Workflow.UI;
using System;

namespace AG.COM.SDM.Plugins.Common
{
    public class CmdWorkitemListDB: AGBaseCommand
    {
        private readonly string m_FormGUID = "{B92FE1DE-4C33-4C9E-9611-8633BAD913DB}";

        public CmdWorkitemListDB()
        {
            this.m_caption = "待办事项";

            m_Image = AG.COM.SDM.Plugins.Properties.Resources.dbsx;
            m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.dbsx;
        }

        public override void OnClick()
        {
            try
            {
                if (m_HookHelper.DockDocumentService.ContainsDocument(m_FormGUID) == false)
                {
                    FormWorkitemList formWorkitemList = new FormWorkitemList(WorkitemListType.DB, m_HookHelper);
                    m_HookHelper.DockDocumentService.AddDockDocument(m_FormGUID, formWorkitemList, DockState.Document);
                }
                else
                {
                    FormWorkitemList formWorkitemList = m_HookHelper.DockDocumentService.GetDockDocument(m_FormGUID) as FormWorkitemList;
                    formWorkitemList.Show();
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
