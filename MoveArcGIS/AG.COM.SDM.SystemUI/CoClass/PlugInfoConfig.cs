using System;

namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// ���������Ϣ��
    /// </summary>
    [Serializable]
    public class PlugInfoConfig
    {
        private string m_Caption;
        private string m_PlugAssembly;
        private string m_PlugType;      
        private string m_ParentID;          
        private int m_Level;
        private int m_SubType;     
        private object m_Tag;

        /// <summary>
        /// ʵ����PlugInfoConfig�¶���
        /// </summary>
        public PlugInfoConfig()
        {
            m_Caption = "";
            m_PlugAssembly = "";
            m_PlugType = "";                     
            m_Level = 0;
            m_SubType = -1;          
        }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string Caption
        {
            get
            {
                return m_Caption;
            }
            set
            {
                m_Caption = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public string PlugAssembly
        {
            get
            {
                return m_PlugAssembly;
            }
            set
            {
                m_PlugAssembly = value;
            }
        }

        /// <summary>
        /// ִ�з�������
        /// </summary>
        public string PlugType
        {
            get
            {
                return m_PlugType;
            }
            set
            {
                m_PlugType = value;
            }
        }

        /// <summary>
        /// ���ظ��ڵ���
        /// </summary>
        public string ParentID
        {
            get
            {
                return m_ParentID;
            }
            set
            {
                m_ParentID = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                m_Level = value;
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public int SubType
        {
            get
            {
                return this.m_SubType;
            }
            set
            {
                this.m_SubType = value;
            }
        } 

        /// <summary>
        /// ��ȡ�������йز����Ϣ������
        /// </summary>
        public object Tag
        {
            get
            {
                return this.m_Tag;
            }
            set
            {
                this.m_Tag = value;
            }
        }

        /// <summary>
        /// ����ToString()����
        /// </summary>
        /// <returns>������ʾ�ı���Ϣ</returns>
        public override string ToString()
        {
            return this.m_Caption;
        }
    }
}
