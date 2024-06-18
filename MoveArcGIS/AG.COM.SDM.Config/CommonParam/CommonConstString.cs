using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 文件路径参数类
    /// </summary>
    public class CommonConstString
    {
        /// <summary>
        /// 应用程序上级目录路径
        /// </summary>
        public static readonly string STR_PreAppPath = System.IO.Directory.GetParent(Application.StartupPath).FullName;

        /// <summary>
        /// 模板方案默认文件夹路径
        /// </summary>
        public static readonly string STR_TemplatePath = string.Format("{0}\\Template", STR_PreAppPath);

        /// <summary>
        /// 样式符号文件夹路径
        /// </summary>
        public static readonly string STR_StylePath = string.Format("{0}\\Styles", STR_PreAppPath);

        /// <summary>
        /// 配置文件夹路径
        /// </summary>
        public static readonly string STR_ConfigPath = string.Format("{0}\\Config", STR_PreAppPath);

        /// <summary>
        /// 临时文件夹路径
        /// </summary>
        public static readonly string STR_TempPath = string.Format("{0}\\Temp", STR_PreAppPath);

        /// <summary>
        /// 数据文件夹路径
        /// </summary>
        public static readonly string STR_DataPath = string.Format("{0}\\Data", STR_PreAppPath);
        /// <summary>
        /// 
        /// </summary>
        public static readonly string STR_ChartPath = string.Format("{0}\\Chart", STR_PreAppPath);

        /// <summary>
        /// NHiberate所需编译模块名称
        /// </summary>
        public static readonly string STR_ModelName = "AG.COM.SDM.Model";

        /// <summary>
        /// AGSDM OLE 系统表标识
        /// </summary>
        public static readonly string STR_AGSDMOleName = "AGSDM系统表连接";

        /// <summary>
        /// AGSDM BS 系统表标识
        /// </summary>
        public static readonly string STR_AGSDMBSOleName = "AGSDMBS系统表连接";

        /// <summary>
        /// 帮助文件文件夹路径
        /// </summary>
        public static string STR_HelpPath = string.Format("{0}\\Help", STR_PreAppPath);

        /// <summary>
        /// 配置文件中当前皮肤的key
        /// </summary>
        public static readonly string SkinKeyInConfig = "DefaultQIOSSkinName";

        /// <summary>
        /// UIDesign本地Xml文件路径
        /// </summary>
        public static readonly string STR_UIDesignXml = CommonConstString.STR_ConfigPath + "\\MainMenu.xml";

        /// <summary>
        /// UIDesign功能绑定本地Xml文件路径
        /// </summary>
        public static readonly string STR_BindFunXml = CommonConstString.STR_ConfigPath + "\\MainMenu_BindFun.xml";

        /// <summary>
        /// UIDesign预览图片路径
        /// </summary>
        public static readonly string STR_UIDesignPreview = CommonConstString.STR_TempPath + "\\UIDesignPreview.png";

        /// <summary>
        /// 工具条布局
        /// </summary>
        public static readonly string STR_ToolBarLayout = CommonConstString.STR_ConfigPath + "\\QToolBarLayout.txt";

        /// <summary>
        /// 菜单配置文件路径
        /// </summary>
        public static readonly string MENUCONFIG_FILE = string.Format("{0}\\MenuConfig.xml", CommonConstString.STR_ConfigPath);
    }
}
