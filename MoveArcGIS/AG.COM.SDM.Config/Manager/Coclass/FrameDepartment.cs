using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System.Collections;
using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 系统自带的部门管理类，提供用户对部门信息的一系列控制
    /// </summary>
    public class FrameDepartment : IDepartment 
    { 
        #region IDepartment 成员

        /// <summary>
        /// 获取所有部门信息列表
        /// </summary>
        /// <returns>部门信息列表</returns>
        public List<AG.COM.SDM.Model.AGSDM_ORG> GetDepartmentList()
        {
            List<AGSDM_ORG> lstDepartment = new List<AGSDM_ORG>();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_ORG");
            for (int i = 0; i < lstEntity.Count; i++)
            {
                lstDepartment.Add(lstEntity[i] as AGSDM_ORG);
            }
            return lstDepartment;
        }

        /// <summary>
        /// 查找给定部门编号的部门信息
        /// </summary>
        /// <param name="orgID">部门编号</param>
        /// <returns>部门信息类</returns>
        public AG.COM.SDM.Model.AGSDM_ORG GetDepartment(decimal orgID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_ORG where ORG_ID =" + orgID);
            if (lstEntity.Count > 0)
                return lstEntity[0] as AGSDM_ORG;
            else
                return null;
        }

        /// <summary>
        /// 获取所有根部门
        /// </summary>
        /// <returns>根部门列表</returns>
        public List<AGSDM_ORG> GetMainORG()
        {
            List<AGSDM_ORG> lstORG = new List<AGSDM_ORG>();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstEntity = tEntityHandler.GetEntities("from AGSDM_ORG where ORG_ID =" + 0);
            
            for (int i = 0; i < lstEntity.Count; i++)
            {
                lstORG.Add(lstEntity[i] as AGSDM_ORG);
            }
            return lstORG;
        }

        /// <summary>
        /// 获取所有根部门
        /// </summary>
        /// <param name="lstAllORG">所有部门信息列表</param>
        /// <returns>根部门列表</returns>
        public List<AGSDM_ORG> GetMainORG(List<AGSDM_ORG> lstAllORG)
        {
            List<AGSDM_ORG> lstORG = new List<AGSDM_ORG>();
            for (int i = 0; i < lstAllORG.Count; i++)
            {
                AGSDM_ORG org = lstAllORG[i];
                if (org.ID == 0 || org.ID == null)
                    lstORG.Add(org);
            }
            return lstORG;
        }

        #endregion
    }
}
