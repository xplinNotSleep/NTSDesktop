using AG.COM.SDM.Config;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Plugins
{
    /// <summary>
    /// ����ͼ���������
    /// </summary>
    public sealed class DeptLayerManageCommand : BaseCommand
    {
        private IHookHelper m_hookHelper;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public DeptLayerManageCommand()
        {
            m_hookHelper = new HookHelper();
            this.m_caption = "����ͼ�����";
            this.m_name = "DeptLayerManageCommand";
        }

        public override void OnClick()
        {
            FormPrivilegeManage tFrm = new FormPrivilegeManage();
            tFrm.ShowInTaskbar = false;
            tFrm.PrivilegeType = EnumPrivilegeType.DeptLayer;
            tFrm.ShowDialog();
        }

        public override void OnCreate(object hook)
        {
            this.m_hookHelper.Framework = hook as IFramework;
        }
    }
}
