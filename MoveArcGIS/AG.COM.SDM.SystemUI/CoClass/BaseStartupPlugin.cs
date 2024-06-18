namespace AG.COM.SDM.SystemUI
{
    /// <summary>
    /// ϵͳ������ ���������
    /// </summary>
    public abstract class BaseStartupPlugin:IStartupPlugin
    {
        #region ��������
        protected string m_name="";
        protected string m_caption="";
        protected bool m_enabled = true;
        protected string m_description = "";
        #endregion 

        #region IStartupPlugin ��Ա
        /// <summary>
        /// ��ȡ����������Ϣ
        /// </summary>
        public string Description
        {
            get
            {
                return this.m_description;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public abstract void Startup();      

        #endregion

        #region IPlugin ��Ա

        /// <summary>
        /// ��ȡ��ǰ��������
        /// </summary>
        public string Name
        {
            get { return this.m_name; }
        }

        /// <summary>
        /// ��ȡ��ǰ������ʾ�ı�
        /// </summary>
        public string Caption
        {
            get { return this.m_caption; }
        }

        /// <summary>
        /// ��ȡ��ǰ�������״̬
        /// </summary>
        public bool Enabled
        {
            get { return this.m_enabled; }
        }

        /// <summary>
        /// ���󴴽�ʱ��ʼ������
        /// </summary>
        /// <param name="hook">hook����</param>
        public abstract void OnCreate(object hook);
       
        #endregion
    }
}
