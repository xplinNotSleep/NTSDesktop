using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// �˵�����ӿ�
    /// </summary>
    public interface IMenuService
    {      
        /// <summary>
        /// ��ʼ���˵����񣬰󶨹��ܵ��ؼ�
        /// </summary>
        /// <param name="tRootContainer"></param>
        void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer);

        /// <summary>
        /// ����ICommand��Name��ȡ���
        /// </summary>
        /// <param name="ICommandName"></param>
        /// <returns></returns>
        UIDesignControl GetPlugin(string ICommandName);
    }
}
