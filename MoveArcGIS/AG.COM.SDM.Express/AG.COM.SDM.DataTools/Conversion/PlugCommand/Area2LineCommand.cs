using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// ��ת�� �����
    /// </summary>
    public class Area2LineCommand : BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Area2LineCommand()
        { 
            base.m_caption = "���ת�߲�";
            base.m_toolTip = "���ת�߲�";
            base.m_category = "����ת������";
            base.m_name = "Area2LineCommand";
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            FormArea2Line frm = new FormArea2Line();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog();
        }  

        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;               
        }       
    }
}
