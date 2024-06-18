using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 框架实现的菜单访问接口
    /// </summary>
    public class FrameMenu : IMenu 
    {

        #region IMenu 成员

        /// <summary>
        /// 获取给定用户的所有菜单项列表
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <returns>菜单项列表</returns>
        public System.Data.DataTable GetMenus(decimal userID)
        {
            DataTable dt = null;
            if (SystemInfo.IsAdminUser)
            {
                dt = GetFullMenuDataTable();
            }
            else
            {
                dt = GetDataTableFromDatabase(userID);
            }
            return dt;
        }

        /// <summary>
        /// 根据角色编号获取菜单表（未实现）
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>菜单表信息</returns>
        public System.Data.DataTable GetMenusByRoleID(decimal roleID)
        {
            throw new Exception("此接口尚未开放！");
        }

        /// <summary>
        /// 根据菜单编号获取菜单实体
        /// </summary>
        /// <param name="menuID">菜单编号</param>
        /// <returns>菜单信息实体</returns>
        public AGSDM_MENU GetMenuByMenuID(decimal menuID)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            string strHQL = "from AGSDM_MENU where ID = " + menuID;
            AGSDM_MENU menuEntity = tEntityHandler.GetEntities<AGSDM_MENU>(strHQL) as AGSDM_MENU;
            return menuEntity;
        }

        /// <summary>
        /// 根据角色编号获取菜单信息列表（暂未实现）
        /// </summary>
        /// <param name="roleID">角色编号</param>
        /// <returns>菜单信息列表</returns>
        public List<AGSDM_MENU> GetMenuByRoleID(decimal roleID)
        {
            throw new Exception("该接口尚未实现！");
        }

        #endregion

        /// <summary>
        /// 从数据库读取菜单信息
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTableFromDatabase(decimal userID)
        {
            DataTable tDataTable = CreateDataTableStructure();
            EntityHandler tEngityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
           
            //获取角色信息
            string strSQL = "from AGSDM_USER_ROLE where USER_ID ='" + userID + "'";
            IList tUserRoleList = tEngityHandler.GetEntities(strSQL);
            if (tUserRoleList.Count == 0)
            {
                return tDataTable;
            }
            AGSDM_USER_ROLE tUserRole = tUserRoleList[0] as AGSDM_USER_ROLE;

            //获取菜单编号列表
            strSQL = "from AGSDM_ROLE_MENU where ROLE_ID ='" + tUserRole.ROLE_ID.ToString() + "'";
            IList tRoleMenuList = tEngityHandler.GetEntities(strSQL);
            strSQL = "from AGSDM_MENU";
            IList tMenuList = tEngityHandler.GetEntities(strSQL);
            string strHQL = "from AGSDM_MENU as t where t.PARENT_MENU_ID='" + 0 + "' ORDER BY SORTID";
            IList tBaseMenu = tEngityHandler.GetEntities(strHQL);
            for (int i = 0; i < tBaseMenu.Count; i++)
            {
                AGSDM_MENU tMenu = tBaseMenu[i] as AGSDM_MENU;
                if (IsAssigned(tRoleMenuList, tMenu.ID))
                {
                    DataRow tDataRow = CraeteDataRowFromMenu(tMenu, tDataTable);  
                    tDataTable.Rows.Add(tDataRow);
                    LoopAddAssignChildMenu(tDataTable, tMenu.MENU_CODE, tRoleMenuList); 
                }
                else
                {
                    continue;
                }

            }
            return tDataTable;
        }

        /// <summary>
        /// 获取经过排序的所有子菜单列表
        /// </summary>
        /// <param name="menuCode">父菜单编号</param>
        /// <returns>子菜单列表</returns>
        private IList GetSortChildMenu(string menuCode)
        {
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            string strHQL = "from AGSDM_MENU where PARENT_MENU_ID ='" + menuCode + "' order by SORTID";
            IList lstChildMenu = tEntityHandler.GetEntities(strHQL);
            return lstChildMenu;
        }

        /// <summary>
        /// 根据菜单实体信息创建DataRow
        /// </summary>
        /// <param name="menu">菜单实体信息</param>
        /// <param name="dt">DataTable对象</param>
        /// <returns>DataRow对象</returns>
        private DataRow CraeteDataRowFromMenu(AGSDM_MENU menu, DataTable dt)
        {
            DataRow tDataRow = dt.NewRow();
            tDataRow[0] = menu.PARENT_MENU_ID == null ? string.Empty : menu.PARENT_MENU_ID;
            tDataRow[1] = menu.MENU_CODE == null ? string.Empty : menu.MENU_CODE;
            tDataRow[2] = menu.MENU_NAME == null ? string.Empty : menu.MENU_NAME;
            tDataRow[3] = menu.MENU_NAME == null ? string.Empty : menu.MENU_NAME;
            tDataRow[4] = menu.ASSEMBLY_NAME == null ? string.Empty : menu.ASSEMBLY_NAME;
            tDataRow[5] = menu.TYPE_NAME == null ? string.Empty : menu.TYPE_NAME;
            tDataRow[6] = menu.SHORTCUT == null ? string.Empty : menu.SHORTCUT;
            tDataRow[7] = menu.ISBEGINGROUP == null ? "FALSE" : menu.ISBEGINGROUP;
            tDataRow[8] = menu.MENU_TYPE == null ? 1 : menu.MENU_TYPE;
            tDataRow[9] = menu.MENU_LEVEL == null ? 1 : menu.MENU_LEVEL;
            return tDataRow;
        }

        /// <summary>
        /// 递归添加已分配给给定角色的所有子菜单
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <param name="parentMenuCode">父菜单菜单编号</param>
        /// <param name="lstRoleMenu">角色列表</param>
        private void LoopAddAssignChildMenu(DataTable dt,string parentMenuCode,IList lstRoleMenu)
        {
            IList lstChildMenu = GetSortChildMenu(parentMenuCode);
            for (int i = 0; i < lstChildMenu.Count; i++)
            {
                AGSDM_MENU menu = lstChildMenu[i] as AGSDM_MENU;
                if (IsAssigned(lstRoleMenu, menu.ID))
                {
                    DataRow datarow = CraeteDataRowFromMenu(menu, dt);
                    dt.Rows.Add(datarow);
                    LoopAddAssignChildMenu(dt, menu.MENU_CODE, lstRoleMenu);
                }
            }
        }

        /// <summary>
        /// 递归增加所有子菜单
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <param name="parentMenuCode">父菜单菜单编号</param>
        private void LoopAddAllChildMenu(DataTable dt, string parentMenuCode)
        {
            IList lstChildMenu = GetSortChildMenu(parentMenuCode);
            for (int i = 0; i < lstChildMenu.Count; i++)
            {
                AGSDM_MENU menu = lstChildMenu[i] as AGSDM_MENU;
                DataRow datarow = CraeteDataRowFromMenu(menu, dt);
                dt.Rows.Add(datarow);
                LoopAddAllChildMenu(dt, menu.MENU_CODE);
            }
        }

        /// <summary>
        /// 创建DataTable空表
        /// </summary>
        /// <returns>DataTable空表结构</returns>
        private DataTable CreateDataTableStructure()
        {
            string xmlMenuStructurePath = CommonConstString.STR_ConfigPath + "\\MenuStructure.xml";
            DataTable tDataTable = GetDataTable(xmlMenuStructurePath);
            tDataTable.Rows.Clear();
            return tDataTable;
        }

        /// <summary>
        /// 获取全部的菜单配置
        /// </summary>
        /// <returns>DataTable对象</returns>
        private DataTable GetFullMenuDataTable()
        {
            DataTable tDataTable = CreateDataTableStructure();
            EntityHandler tEngityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_MENU as t where t.PARENT_MENU_ID='" + 0 + "' ORDER BY SORTID";
            IList tBaseMenu = tEngityHandler.GetEntities(strHQL);
            for (int i = 0; i < tBaseMenu.Count; i++)
            {
                AGSDM_MENU tMenu = tBaseMenu[i] as AGSDM_MENU;
                DataRow tDataRow = CraeteDataRowFromMenu(tMenu, tDataTable);
                tDataTable.Rows.Add(tDataRow);
                LoopAddAllChildMenu(tDataTable, tMenu.MENU_CODE);
            }
            return tDataTable;
        }

        /// <summary>
        /// 判断给定的菜单是否已经分配给该角色
        /// </summary>
        /// <param name="lstRoleMenu">用户角色菜单列表</param>
        /// <param name="menuID">菜单ID</param>
        /// <returns>是否具有该菜单权限</returns>
        private bool IsAssigned(IList lstRoleMenu, decimal menuID)
        {
            for (int i = 0; i < lstRoleMenu.Count; i++)
            {
                AGSDM_ROLE_MENU roleMenu = lstRoleMenu[i] as AGSDM_ROLE_MENU;
                if (menuID.ToString() == roleMenu.MENU_ID)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 从XML中读取数据,返回DataTable表
        /// </summary>
        /// <param name="xmlfile">xml文件路径</param>
        /// <returns>返回DataTable表</returns>
        private DataTable GetDataTable(string xmlfile)
        {
            DataSet ds = new DataSet();

            //从XML中读取数据.数据结构后面详细讲一下
            ds.ReadXml(xmlfile);

            return ds.Tables[0];
        }
    }
}
