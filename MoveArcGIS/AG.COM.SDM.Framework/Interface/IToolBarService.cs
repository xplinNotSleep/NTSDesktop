using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// �������������ӿ�
    /// </summary>
    public interface IToolBarService
    {
        /// <summary>
        /// ��ʼ�����߷��񣬰󶨹��ܵ��ؼ�
        /// </summary>
        /// <param name="tRootContainer"></param>
        void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer);

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <param name="ICommandName"></param>
        /// <returns></returns>
        UIDesignControl GetPlugin(string ICommandName);

        /// <summary>
        /// ���湤��������
        /// </summary>
        /// <param name="tFilePath"></param>
        void SaveLayout(string tFilePath);

        /// <summary>
        /// �ָ�����������
        /// </summary>
        /// <param name="tFilePath"></param>
        void RecoverLayout(string tFilePath);
    }
}
