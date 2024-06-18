using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// ���ݿռ��ϵ�����ֶ�ֵ �����
    /// </summary>
    public class GetFieldValueBySpatialRelCommand : BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public GetFieldValueBySpatialRelCommand()
        {
            base.m_caption = "���ݿռ��ϵ�����ֶ�ֵ";
            base.m_toolTip = "���ݿռ��ϵ�����ֶ�ֵ";
            base.m_category = "�������ù���";
            base.m_name = "CreateFieldValueBySpatialRelCommand"; 
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            FormCreateFieldValueBySpatialRel frm = new FormCreateFieldValueBySpatialRel();
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
