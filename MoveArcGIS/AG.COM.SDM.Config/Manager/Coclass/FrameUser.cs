using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System.Collections;
using System.Collections.Generic;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 框架实现的用户信息读取类
    /// </summary>
    public class FrameUser : IUser
    {
        #region IUser 成员

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户信息列表</returns>
        public List<AGSDM_SYSTEM_USER> GetUserList()
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstUser = tEntityHandler.GetEntities("from AGSDM_SYSTEM_USER");
            List<AGSDM_SYSTEM_USER> lstResult = new List<AGSDM_SYSTEM_USER>();
            for (int i = 0; i < lstUser.Count; i++)
            {
                lstResult.Add(lstUser[i] as AGSDM_SYSTEM_USER);
            }
            return lstResult;
        }

        /// <summary>
        /// 根据用户编号获取用户实体
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>用户实体</returns>
        public AGSDM_SYSTEM_USER GetUser(decimal userID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstUser = tEntityHandler.GetEntities("from AGSDM_SYSTEM_USER where USER_ID=" + userID);
            if (lstUser.Count > 0)
                return lstUser[0] as AGSDM_SYSTEM_USER;
            else
                return null;
        }

        /// <summary>
        /// 根据用户英文名称获取用户信息
        /// </summary>
        /// <param name="userName">用户英文名称</param>
        /// <returns>用户信息类</returns>
        public AGSDM_SYSTEM_USER GetUser(string userName)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            string strHQL = "from AGSDM_SYSTEM_USER as t where t.NAME_EN='" + userName + "'";
            IList lstUser = tEntityHandler.GetEntities(strHQL);
            if (lstUser.Count > 0)
            {
                return lstUser[0] as AGSDM_SYSTEM_USER;
            }
            else
                return null;
        }

        /// <summary>
        /// 根据用户编号获取用户角色信息列表
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>角色信息列表</returns>
        public List<AGSDM_ROLE> GetRoles(decimal userID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            IList lstUserRole = tEntityHandler.GetEntities("from AGSDM_USER_ROLE where USER_ID =" + userID);

            List<AGSDM_ROLE> lstRole = new List<AGSDM_ROLE>();
            for (int i = 0; i < lstUserRole.Count; i++)
            {
                AGSDM_USER_ROLE userRole = lstUserRole[i] as AGSDM_USER_ROLE;
                AGSDM_ROLE role = tEntityHandler.GetEntity<AGSDM_ROLE>("from AGSDM_ROLE whrer ID =" + userRole.ROLE_ID);
                if (role != null)
                    lstRole.Add(role);
            }
            return lstRole;
        }

        /// <summary>
        /// 根据岗位编号获取用户信息
        /// </summary>
        /// <param name="postID">岗位编号</param>
        /// <returns>用户信息实例</returns>
        public AGSDM_SYSTEM_USER GetUserFromPostID(decimal postID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            string strSQL = "from AGSDM_USERPOSITIONRLT t where t.POS_ID=" + postID;
            AGSDM_USERPOSITIONRLT tUserPosit = tEntityHandler.GetEntity<AGSDM_USERPOSITIONRLT>(strSQL);
            if (tUserPosit != null && tUserPosit.USER_ID != 0)
            {
                strSQL = "from AGSDM_SYSTEM_USER t where t.USER_ID='" + tUserPosit.USER_ID + "'";
                AGSDM_SYSTEM_USER tSysUser = tEntityHandler.GetEntity<AGSDM_SYSTEM_USER>(strSQL);
                if (tSysUser != null)
                {
                    return tSysUser;
                }
                else
                    return null;
            }
            else
                return null;
        }

        #endregion
    }
}
