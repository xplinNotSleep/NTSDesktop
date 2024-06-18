using System.Collections.Generic;

namespace AG.COM.SDM.DAL
{
    /// <summary>
    /// 会话连接数据库连接参数设置管理类
    /// </summary>
    public class SessionParameterManage
    {
        private Dictionary<string, SessionParameter> m_SessionParameters = new Dictionary<string, SessionParameter>();

        public SessionParameter this[string assemblyName]
        {
            get 
            {
                if (m_SessionParameters.ContainsKey(assemblyName.ToUpper()))
                {
                    return m_SessionParameters[assemblyName.ToUpper()];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                m_SessionParameters[assemblyName.ToUpper()] = value;
            }
        }

        //新增配置
        public void Add(string assemblyName, SessionParameter pSessionParameer)
        {
            if (m_SessionParameters.ContainsKey(assemblyName.ToUpper()))
            {
                this[assemblyName.ToUpper()] = pSessionParameer;
            }
            else
            {
                m_SessionParameters.Add(assemblyName.ToUpper(), pSessionParameer);
            }
        }

        //删除配置
        public void Delete(string assemblyName)
        {
            if (string.IsNullOrEmpty(assemblyName)) return;

            if (m_SessionParameters.ContainsKey(assemblyName.ToUpper()))
            {
                m_SessionParameters.Remove(assemblyName.ToUpper());
            }
        }

        //清除配置
        public void Clear()
        {
            m_SessionParameters.Clear();
        }
    }
}
