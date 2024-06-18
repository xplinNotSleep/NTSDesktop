using AG.COM.SDM.DAL;
using AG.COM.SDM.Framework;
using AG.COM.SDM.Model;
using AG.COM.SDM.SystemUI;
using AG.COM.SDM.Utility;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AG.COM.SDM.Config.MenuConfig
{
    /// <summary>
    /// 菜单控件加载类
    /// </summary>
    public class MenuLoadHelper
    {
        /// <summary>
        /// 默认的RibbonControl
        /// </summary>
        private RibbonControl m_RibbonControl = null;

        /// <summary>
        /// 加载菜单配置
        /// </summary>
        /// <returns></returns>
        public static IList<AGSDM_UIMENU> LoadMenuConfigEntity(UIDesignFrom loadFrom)
        {
            IList<AGSDM_UIMENU> menus = null;
            //数据库
            if (loadFrom == UIDesignFrom.Database)
            {
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);

                menus = tEntityHandler.GetEntities<AGSDM_UIMENU>("from AGSDM_UIMENU t order by SORT");
            }
            //本地文件
            else
            {
                if (File.Exists(CommonConstString.MENUCONFIG_FILE) == false)
                    return new List<AGSDM_UIMENU>();

                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(List<AGSDM_UIMENU>));
                StreamReader tStreamReader = new StreamReader(CommonConstString.MENUCONFIG_FILE);
                menus = tXmlSerializer.Deserialize(tStreamReader) as List<AGSDM_UIMENU>;
                tStreamReader.Close();
            }

            return menus;
        }

        /// <summary>
        /// 保存菜单配置
        /// </summary>
        /// <param name="loadFrom"></param>
        /// <param name="menus"></param>
        public static void SaveMenuConfigEntity(UIDesignFrom loadFrom, IList<AGSDM_UIMENU> menus)
        {
            //数据库
            if (loadFrom == UIDesignFrom.Database)
            {
                EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
                //先把所有数据清掉（因为此表只会存在一条数据）

                string tableName = tEntityHandler.GetEntityTableName(typeof(AGSDM_UIMENU));
                tEntityHandler.ExecuteNonQuery("DELETE FROM " + tableName);

                //tEntityHandler.ExecuteNonQuery("set IDENTITY_INSERT " + tableName + " ON;");
               
              
                foreach (AGSDM_UIMENU tAGSDM_UIMENU in menus)
                {
                    tEntityHandler.AddEntity(tAGSDM_UIMENU);
                }
                //tEntityHandler.ExecuteNonQuery("set IDENTITY_INSERT " + tableName + " OFF;");
            }
            //本地
            else
            {
                XmlSerializer tXmlSerializer = new XmlSerializer(typeof(List<AGSDM_UIMENU>));
                StreamWriter tStreamWriter = new StreamWriter(CommonConstString.MENUCONFIG_FILE);
                tXmlSerializer.Serialize(tStreamWriter, menus);
                tStreamWriter.Close();
            }
        }

        /// <summary>
        /// 根据当前登录角色过滤菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        public static List<AGSDM_UIMENU> FilterCurrentRoleFun(List<AGSDM_UIMENU> menus)
        {
            //管理员没有权限限制
            if (SystemInfo.IsAdminUser == true) return menus;

            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler(CommonConstString.STR_ModelName);
            //通过当前用户ID找到其角色ID
            //AGSDM_USER_ROLE tUSER_ROLE = tEntityHandler.GetEntityLinq<AGSDM_USER_ROLE>(t => t.USER_ID == SystemInfo.UserID.ToString());
            IList<AGSDM_USER_ROLE> lstUSER_ROLE = tEntityHandler.GetEntitiesLinq<AGSDM_USER_ROLE>(t => t.USER_ID == SystemInfo.UserID.ToString());

            if (lstUSER_ROLE.Count == 0)
            {
                throw new Exception("当前用户没有配置角色，请给用户配置角色。");
            }

            //查询角色的菜单权限
            //IList<AGSDM_UI_ROLEFUNRLT> tUI_ROLEFUNRLTs = tEntityHandler.GetEntitiesLinq<AGSDM_UI_ROLEFUNRLT>(t => t.ROLEID == tUSER_ROLE.ROLE_ID.ToString());

            //查询所有角色的菜单权限
            IList<AGSDM_UI_ROLEFUNRLT> tUI_ROLEFUNRLTs=new List<AGSDM_UI_ROLEFUNRLT>();
            for (int i = 0; i < lstUSER_ROLE.Count; i++)
            {
                IList<AGSDM_UI_ROLEFUNRLT> tmpUI_ROLEFUNRLTs = tEntityHandler.GetEntitiesLinq
                    <AGSDM_UI_ROLEFUNRLT>(t => t.ROLEID == lstUSER_ROLE[i].ROLE_ID.ToString());
                for (int j = 0; j < tmpUI_ROLEFUNRLTs.Count; j++)
                {
                    if(tUI_ROLEFUNRLTs.Any(t => t.CONTROLNAME == tmpUI_ROLEFUNRLTs[j].CONTROLNAME))
                    {
                        continue;
                    }
                    tUI_ROLEFUNRLTs.Add(tmpUI_ROLEFUNRLTs[j]);
                }
            }


            List<AGSDM_UIMENU> result = new List<AGSDM_UIMENU>();
            //只返回有权限的菜单
            foreach (AGSDM_UIMENU tUIMENU in menus)
            {
                if (tUI_ROLEFUNRLTs.Any(t => t.CONTROLNAME == tUIMENU.GUID))
                {
                    result.Add(tUIMENU);
                }
            }

            return result;
        }

        /// <summary>
        /// 初始化菜单控件
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="ribbonControl"></param>
        /// <param name="tRootContainer"></param>
        public void InitMenu(List<AGSDM_UIMENU> menus, RibbonControl ribbonControl, Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer)
        {
            m_RibbonControl = ribbonControl;
            //初始化“菜单”
            AddRibbonPage(menus, "menu", ribbonControl, tRootContainer);

            //初始化“工具条”
            UIDesignControl tUIDesignControl = new UIDesignControl();
            tUIDesignControl.Name = "toolbar";
            tUIDesignControl.Control = ribbonControl.Toolbar;

            List<UIDesignControl> tUIDesignControls = new List<UIDesignControl>();

            tRootContainer.Add(tUIDesignControl, tUIDesignControls);

            AddRibbonToolbarItem(menus, "toolbar", tUIDesignControls);
        }

        /// <summary>
        /// 添加RibbonPage控件
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentGUID"></param>
        /// <param name="parentControl"></param>
        /// <param name="tRootContainer"></param>
        private void AddRibbonPage(List<AGSDM_UIMENU> menus, string parentGUID, RibbonControl parentControl, Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer)
        {
            List<AGSDM_UIMENU> menuTops = menus.Where(t => t.PARENTCODE == parentGUID && (t.MENUTYPE == MenuType.RibbonPage.ToString())).ToList();

            foreach (AGSDM_UIMENU menu in menuTops)
            {
                RibbonPage control = new RibbonPage();

                control.Text = menu.CAPTION;
                parentControl.Pages.Add(control);

                UIDesignControl tUIDesignControl = new UIDesignControl();
                tUIDesignControl.Name = menu.GUID;

                ItemCommandInfo tItemCommandInfo = new ItemCommandInfo();
                tItemCommandInfo.Caption = menu.CAPTION;
                tItemCommandInfo.ControlName = menu.GUID;
                //tItemCommandInfo.PlugAssembly = menu.DLLFILE;
                //tItemCommandInfo.PlugType = menu.CLASSNAME;

                tUIDesignControl.BindFun = tItemCommandInfo;
                tUIDesignControl.Control = control;

                List<UIDesignControl> tUIDesignControls = new List<UIDesignControl>();

                tRootContainer.Add(tUIDesignControl, tUIDesignControls);
                //添加子级控件（PageGroup）
                AddRibbonPageGroup(menus, menu.GUID, control, tUIDesignControls);
            }
        }

        /// <summary>
        /// 添加RibbonPageGroup控件
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentGUID"></param>
        /// <param name="parentControl"></param>
        /// <param name="tUIDesignControls"></param>
        private void AddRibbonPageGroup(List<AGSDM_UIMENU> menus, string parentGUID, RibbonPage parentControl, List<UIDesignControl> tUIDesignControls)
        {
            List<AGSDM_UIMENU> menuTops = menus.Where(t => t.PARENTCODE == parentGUID && (t.MENUTYPE == MenuType.RibbonPageGroup.ToString())).ToList();

            foreach (AGSDM_UIMENU menu in menuTops)
            {
                RibbonPageGroup control = new RibbonPageGroup();
                control.ShowCaptionButton = false;
                control.Text = menu.CAPTION;
                parentControl.Groups.Add(control);

                UIDesignControl tUIDesignControl = new UIDesignControl();
                tUIDesignControl.Name = menu.GUID;

                ItemCommandInfo tItemCommandInfo = new ItemCommandInfo();
                tItemCommandInfo.Caption = menu.CAPTION;
                tItemCommandInfo.ControlName = menu.GUID;
                //tItemCommandInfo.PlugAssembly = menu.DLLFILE;
                //tItemCommandInfo.PlugType = menu.CLASSNAME;

                tUIDesignControl.BindFun = tItemCommandInfo;
                tUIDesignControl.Control = control;
                tUIDesignControls.Add(tUIDesignControl);
                //添加子级控件（Item）
                AddRibbonItem(menus, menu.GUID, control, tUIDesignControls);
            }
        }

        /// <summary>
        /// 添加RibbonItem控件
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentGUID"></param>
        /// <param name="parentControl"></param>
        /// <param name="tUIDesignControls"></param>
        private void AddRibbonItem(List<AGSDM_UIMENU> menus, string parentGUID, RibbonPageGroup parentControl, List<UIDesignControl> tUIDesignControls)
        {
            List<AGSDM_UIMENU> menuTops = menus.Where(t => t.PARENTCODE == parentGUID && (t.MENUTYPE == MenuType.Item.ToString() || t.MENUTYPE == MenuType.ComboBox.ToString() || t.MENUTYPE == MenuType.ComboBoxTreeView.ToString())).ToList();

            foreach (AGSDM_UIMENU tUIMENU in menuTops)
            {
                object objControl = null;

                if (tUIMENU.MENUTYPE == MenuType.Item.ToString())
                {
                    BarButtonItem control = new BarButtonItem();
                    objControl = control;

                    control.Caption = tUIMENU.CAPTION;
                    //是否开始分组
                    bool beginGroup = tUIMENU.EXT1.EqualIgnoreCase("true") ? true : false;
                    //图标大小
                    control.RibbonStyle = tUIMENU.EXT2.EqualIgnoreCase("true") ? RibbonItemStyles.Large : RibbonItemStyles.Default;

                    m_RibbonControl.Items.Add(control);
                    parentControl.ItemLinks.Add(control, beginGroup);
                }
                else if (tUIMENU.MENUTYPE == MenuType.ComboBox.ToString())
                {
                    BarEditItem control = new BarEditItem();

                    control.Caption = tUIMENU.CAPTION;
                    //是否开始分组
                    bool beginGroup = tUIMENU.EXT1.EqualIgnoreCase("true") ? true : false;

                    RepositoryItemComboBox combobox = new RepositoryItemComboBox();

                    control.Edit = combobox;

                    combobox.AutoHeight = true;

                    m_RibbonControl.RepositoryItems.Add(combobox);

                    m_RibbonControl.Items.Add(control);
                    parentControl.ItemLinks.Add(control, beginGroup);

                    objControl = control;
                }
                else if (tUIMENU.MENUTYPE == MenuType.ComboBoxTreeView.ToString())
                {
                    BarEditItem control = new BarEditItem();

                    control.Caption = tUIMENU.CAPTION;
                    //是否开始分组
                    bool beginGroup = tUIMENU.EXT1.EqualIgnoreCase("true") ? true : false;

                    RepositoryItemPopupContainerEdit popupContainer = new RepositoryItemPopupContainerEdit();

                    control.Edit = popupContainer;

                    popupContainer.AutoHeight = true;

                    popupContainer.TextEditStyle = TextEditStyles.DisableTextEditor;

                    m_RibbonControl.RepositoryItems.Add(popupContainer);

                    m_RibbonControl.Items.Add(control);
                    parentControl.ItemLinks.Add(control, beginGroup);

                    objControl = control;
                }

                UIDesignControl tUIDesignControl = new UIDesignControl();
                tUIDesignControl.Name = tUIMENU.GUID;
                //保存插件信息
                ItemCommandInfo tItemCommandInfo = new ItemCommandInfo();
                tItemCommandInfo.Caption = tUIMENU.CAPTION;
                tItemCommandInfo.ControlName = tUIMENU.GUID;
                tItemCommandInfo.PlugAssembly = tUIMENU.DLLFILE;
                tItemCommandInfo.PlugType = tUIMENU.CLASSNAME;
                //保存控件插件绑定信息
                tUIDesignControl.BindFun = tItemCommandInfo;
                tUIDesignControl.Control = objControl;
                tUIDesignControls.Add(tUIDesignControl);
            }
        }

        /// <summary>
        /// 添加Toolbar的Item控件
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="parentGUID"></param>
        /// <param name="tUIDesignControls"></param>
        private void AddRibbonToolbarItem(List<AGSDM_UIMENU> menus, string parentGUID, List<UIDesignControl> tUIDesignControls)
        {
            List<AGSDM_UIMENU> menuTops = menus.Where(t => t.PARENTCODE == parentGUID && (t.MENUTYPE == MenuType.Item.ToString() || t.MENUTYPE == MenuType.ComboBox.ToString())).ToList();

            foreach (AGSDM_UIMENU tUIMENU in menuTops)
            {
                object objControl = null;

                if (tUIMENU.MENUTYPE == MenuType.Item.ToString())
                {
                    BarButtonItem control = new BarButtonItem();
                    objControl = control;

                    control.Caption = tUIMENU.CAPTION;
                    //是否开始分组
                    bool beginGroup = tUIMENU.EXT1.EqualIgnoreCase("true") ? true : false;

                    m_RibbonControl.Items.Add(control);
                    m_RibbonControl.Toolbar.ItemLinks.Add(control, beginGroup);
                }
                else if (tUIMENU.MENUTYPE == MenuType.ComboBox.ToString())
                {
                    BarEditItem control = new BarEditItem();

                    control.Caption = tUIMENU.CAPTION;
                    //是否开始分组
                    bool beginGroup = tUIMENU.EXT1.EqualIgnoreCase("true") ? true : false;

                    RepositoryItemComboBox combobox = new RepositoryItemComboBox();

                    control.Edit = combobox;

                    combobox.AutoHeight = true;

                    m_RibbonControl.RepositoryItems.Add(combobox);

                    m_RibbonControl.Items.Add(control);
                    m_RibbonControl.Toolbar.ItemLinks.Add(control, beginGroup);

                    objControl = control;
                }

                UIDesignControl tUIDesignControl = new UIDesignControl();
                tUIDesignControl.Name = tUIMENU.GUID;
                //保存插件信息
                ItemCommandInfo tItemCommandInfo = new ItemCommandInfo();
                tItemCommandInfo.Caption = tUIMENU.CAPTION;
                tItemCommandInfo.ControlName = tUIMENU.GUID;
                tItemCommandInfo.PlugAssembly = tUIMENU.DLLFILE;
                tItemCommandInfo.PlugType = tUIMENU.CLASSNAME;
                //保存控件插件绑定信息
                tUIDesignControl.BindFun = tItemCommandInfo;
                tUIDesignControl.Control = objControl;
                tUIDesignControls.Add(tUIDesignControl);
            }
        }
    }
}
