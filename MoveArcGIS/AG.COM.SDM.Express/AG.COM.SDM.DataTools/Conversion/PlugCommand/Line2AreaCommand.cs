using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// ��ת������
    /// </summary>
    public sealed class Line2AreaCommand:BaseCommand
    {
        private IHookHelperEx m_hookHelper = new HookHelperEx();

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public Line2AreaCommand()
        {
            base.m_caption = "�߲�ת��";
            base.m_toolTip = "�߲�ת��";
            base.m_name = "Line2AreaCommand";    
        }

        /// <summary>
        /// �����¼�������
        /// </summary>
        public override void OnClick()
        {
            FormLine2Area frm = new FormLine2Area();
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
