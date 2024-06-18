using System.Collections.Generic;
using System.Data;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 菜单信息读取接口
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// 根据用户编号获取菜单信息表
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>菜单信息表</returns>
        DataTable GetMenus(decimal userID);

        /// <summary>
        /// 根据角色编号获取菜单信息表
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>菜单信息表</returns>
        DataTable GetMenusByRoleID(decimal roleID);

        /// <summary>
        /// 根据菜单编号获取菜单信息实例
        /// </summary>
        /// <param name="menuID">菜单编号</param>
        /// <returns>菜单信息实例</returns>
        AGSDM_MENU GetMenuByMenuID(decimal menuID);

        /// <summary>
        /// 根据角色编号获取菜单信息列表
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>菜单信息实例列表</returns>
        List<AGSDM_MENU> GetMenuByRoleID(decimal roleID);
    }
}
