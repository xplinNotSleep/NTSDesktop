using AG.COM.SDM.Framework;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace AG.COM.SDM.DataTools.Common
{
    /// <summary>
    /// ���췶Χѡ�� �����
    /// </summary>
    public sealed class CreateStructEnvCommand : BaseCommand 
    {
        private IHookHelperEx m_HookHelper = new HookHelperEx();
     
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CreateStructEnvCommand()
        {
            base.m_caption = "���췶Χѡ��";
            base.m_toolTip = "���췶Χѡ��";
            base.m_name = "CrateStructEnvCommand";  
        }

        /// <summary>
        /// ��ȡ��ǰ����Ŀ���״̬
        /// </summary>
        public override bool Enabled
        {
            get
            {
                if (this.m_HookHelper.FocusMap.LayerCount == 0) return false;
                if (this.m_HookHelper.FocusMap.SelectionCount == 0) return false;

                return true;
            }
        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        public override void OnClick()
        {
            //ʵ��������
            FormCreateStructEnv tFormCreateEnv = new FormCreateStructEnv(this.m_HookHelper.FocusMap);
            tFormCreateEnv.ShowDialog();          
        } 

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="hook">hook����</param>
        public override void OnCreate(object hook)
        {
            this.m_HookHelper.Hook = hook;    
        } 
    }
}
