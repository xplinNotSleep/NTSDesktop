using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System.Collections.Generic;
using System.Linq;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 全局系统参数帮助类
    /// </summary>
    public class DBSystemConfigHelper
    {
        private static IList<AGSDM_SYSTEMCONFIG> m_SystemConfigs = null;

        /// <summary>
        /// 重新从数据库读取
        /// </summary>
        public static void ReReadFromDB()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList<AGSDM_SYSTEMCONFIG> tSystemConfigs = tEntityHandler.GetEntities<AGSDM_SYSTEMCONFIG>("from AGSDM_SYSTEMCONFIG t ");

            m_SystemConfigs = tSystemConfigs;
        }

        /// <summary>
        /// 获取所有全局系统参数
        /// </summary>
        /// <returns></returns>
        public static IList<AGSDM_SYSTEMCONFIG> GetAllSystemConfig()
        {
            if (m_SystemConfigs == null)
            {
                ReReadFromDB();
                return m_SystemConfigs;
            }
            else
            {
                return m_SystemConfigs;
            }
        }      

        /// <summary>
        /// 输入系统参数的key获取值
        /// </summary>
        /// <param name="tParaKey"></param>
        /// <returns></returns>
        public static string GetParaValue(string tParaKey)
        {
            IList<AGSDM_SYSTEMCONFIG> tSystemConfigs = null;

            if (m_SystemConfigs == null)
            {
                tSystemConfigs = GetAllSystemConfig();
            }
            else
            {
                tSystemConfigs = m_SystemConfigs;
            }

            AGSDM_SYSTEMCONFIG tSystemConfig = tSystemConfigs.FirstOrDefault(t => t.NAME == tParaKey);
            if (tSystemConfig != null)
            {
                return tSystemConfig.VALUE;
            }

            return "";
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="tName"></param>
        /// <param name="tValue"></param>
        /// <param name="tDesc"></param>
        public static void Save(string tName, string tValue, string tDesc)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            //AGSDM_SYSTEMCONFIG tSystemConfig = tEntityHandler.GetEntity<AGSDM_SYSTEMCONFIG>("from AGSDM_SYSTEMCONFIG t where t.NAME='" + tName + "'");
            AGSDM_SYSTEMCONFIG tSystemConfig = tEntityHandler.GetEntity<AGSDM_SYSTEMCONFIG>("from AGSDM_SYSTEMCONFIG t where t.NAME=?", tName);

            if (tSystemConfig != null)
            {
                tSystemConfig.VALUE = tValue;
                tSystemConfig.DESCRIPTION = tDesc;

                tEntityHandler.UpdateEntity(tSystemConfig, tSystemConfig.ID);
            }
            else
            {
                tSystemConfig = new AGSDM_SYSTEMCONFIG();

                tSystemConfig.NAME = tName;
                tSystemConfig.VALUE = tValue;
                tSystemConfig.DESCRIPTION = tDesc;

                tEntityHandler.AddEntity(tSystemConfig);
            }
        }
    }
}
