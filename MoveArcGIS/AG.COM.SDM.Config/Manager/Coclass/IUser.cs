using System.Collections.Generic;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 用户信息读取接口
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>用户信息列表</returns>
        List<AGSDM_SYSTEM_USER> GetUserList();

        /// <summary>
        /// 根据用户编号获取用户信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>用户信息实例</returns>
        AGSDM_SYSTEM_USER GetUser(decimal userID);

        /// <summary>
        /// 根据用户英文名称获取用户信息
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns>用户信息实例</returns>
        AGSDM_SYSTEM_USER GetUser(string userName);

        /// <summary>
        /// 根据用户编号获取角色信息
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>角色信息列表</returns>
        List<AGSDM_ROLE> GetRoles(decimal userID);

        /// <summary>
        /// 通过岗位ID获取用户
        /// </summary>
        /// <param name="postID">岗位编号</param>
        /// <returns>用户信息实例</returns>
        AGSDM_SYSTEM_USER GetUserFromPostID(decimal postID);
    }
}
