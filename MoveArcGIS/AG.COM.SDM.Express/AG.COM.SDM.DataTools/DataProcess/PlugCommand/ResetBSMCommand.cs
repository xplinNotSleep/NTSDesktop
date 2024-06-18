using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// �������ñ�ʶ�� �����
    /// </summary>
    public class ResetBSMCommand : BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ResetBSMCommand()
        {
            base.m_caption = "�������ñ�ʶ��";
            base.m_toolTip = "�������ñ�ʶ��";
            base.m_category = "�������ù���";
            base.m_name = "ResetBSMCommand";
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            FormResetBSM frm = new FormResetBSM();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog(); 
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;
        }        
    }
}
