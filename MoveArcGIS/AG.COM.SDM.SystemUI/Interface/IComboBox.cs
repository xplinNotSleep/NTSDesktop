using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// ����ѡ������ �ӿ�
    /// </summary>
    public interface IComboBox
    {   
        /// <summary>
        /// ��ǩ����
        /// </summary>
        string LabelText { get;}

        /// <summary>
        /// ��ȡ����ѡ���ĸ߶�
        /// </summary>
        int Height { get;}

        /// <summary>
        /// ��ȡ����ѡ���Ŀ��
        /// </summary>
        int Width { get;} 

        /// <summary>
        /// ��ȡ�󶨵�����Դ���� 
        /// </summary>
        object DataSource { get;}

        /// <summary>
        /// ��ȡ��������ʽ����
        /// </summary>
        ComboBoxStyle ComboBoxStyle { get;}

        /// <summary>
        /// �����´�����
        /// </summary>
        /// <param name="sender">�¼�����</param>
        /// <param name="e">�¼�����</param>
        void OnKeyDown(object sender, KeyEventArgs e);

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sender">�¼�����</param>
        /// <param name="e">�¼�����</param>
        void OnKeyUp(object sender, KeyEventArgs e);

        /// <summary>
        /// �����´�����
        /// </summary>
        /// <param name="sender">�¼�����</param>
        /// <param name="e">�¼�����</param>
        void OnKeyPress(object sender, KeyPressEventArgs e);

        /// <summary>
        /// ����ѡ����仯ʱ�Ĵ�����
        /// </summary>
        /// <param name="sender">�¼�����</param>
        /// <param name="e">�¼�����</param>
        void OnSelectedIndexChanged(object sender, EventArgs e);      
    }
}
