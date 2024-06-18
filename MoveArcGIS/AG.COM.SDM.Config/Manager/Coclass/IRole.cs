using System.Collections.Generic;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 角色信息读取接口
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// 根据角色编号获取角色信息
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>角色信息实例</returns>
        AGSDM_ROLE GetRoleByID(decimal roleID);

        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>角色信息列表</returns>
        List<AGSDM_ROLE> GetAllRole();
    }
}
