using System.Collections;
using System.Collections.Generic;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 框架实现的角色信息读取类
    /// </summary>
    public class FrameRole : IRole
    {
        #region IRole 成员

        /// <summary>
        /// 通过角色编号获取角色实体
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>角色信息实体</returns>
        public AG.COM.SDM.Model.AGSDM_ROLE GetRoleByID(decimal roleID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            string strHql = "from AGSDM_ROLE where ROLE_ID = " + roleID;
            AGSDM_ROLE roleEntity = tEntityHandler.GetEntities<AGSDM_ROLE>(strHql) as AGSDM_ROLE;
            return roleEntity;
        }

        /// <summary>
        /// 获取角色表中所有记录
        /// </summary>
        /// <returns>角色信息列表</returns>
        public List<AG.COM.SDM.Model.AGSDM_ROLE> GetAllRole()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            string strHql = "from AGSDM_ROLE ";
            IList lstRoles = tEntityHandler.GetEntities(strHql);
            List<AGSDM_ROLE> result = new List<AGSDM_ROLE>();
            for (int i = 0; i < lstRoles.Count; i++)
            {
                AGSDM_ROLE roleEntity = lstRoles[i] as AGSDM_ROLE;
                result.Add(roleEntity);
            }
            return result;
        }

        #endregion
    }
}
