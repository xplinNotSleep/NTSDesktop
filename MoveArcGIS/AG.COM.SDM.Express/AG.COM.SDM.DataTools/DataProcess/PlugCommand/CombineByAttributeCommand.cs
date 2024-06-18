using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.DataProcess
{
    /// <summary>
    /// �������Ժϲ�ͼ�� �����
    /// </summary>
    public sealed class CombineByAttributeCommand:BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CombineByAttributeCommand()
        {
            base.m_caption = "�������Ժϲ�ͼ��";
            base.m_toolTip = "�������Ժϲ�ͼ��";
            base.m_category = "���ݹ�����";
            base.m_name = "CombineByAttributeCommand";
        }

        /// <summary>
        /// ����ʱ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Hook = hook;               
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            FormCombineByAttribute frm = new FormCombineByAttribute();
            frm.Map = m_hookHelper.FocusMap;
            frm.ShowDialog(); 
        }
    }
}
