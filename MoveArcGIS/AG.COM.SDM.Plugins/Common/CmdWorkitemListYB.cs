using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using System;

namespace AG.COM.SDM.Plugins.Common
{
    public class CmdWorkitemListYB : AGBaseCommand
    {
        private readonly string m_FormGUID = "{AA5B79A2-B823-46BF-8FF5-AC603CA12B65}";

         public CmdWorkitemListYB()
         {
             this.m_caption = "已办事项";

             m_Image = AG.COM.SDM.Plugins.Properties.Resources.ybsx;
             m_Image32 = AG.COM.SDM.Plugins.Properties.Resources.ybsx;
         }

        public override void OnClick()
        {
            try
            {
                if (m_HookHelper.DockDocumentService.ContainsDocument(m_FormGUID) == false)
                {
                    FormWorkitemList formWorkitemList = new FormWorkitemList(WorkitemListType.YB, m_HookHelper);
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
