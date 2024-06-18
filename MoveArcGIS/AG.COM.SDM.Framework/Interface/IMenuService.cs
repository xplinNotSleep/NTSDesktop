using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 菜单服务接口
    /// </summary>
    public interface IMenuService
    {      
        /// <summary>
        /// 初始化菜单服务，绑定功能到控件
        /// </summary>
        /// <param name="tRootContainer"></param>
        void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer);

        /// <summary>
        /// 根据ICommand的Name获取插件
        /// </summary>
        /// <param name="ICommandName"></param>
        /// <returns></returns>
        UIDesignControl GetPlugin(string ICommandName);
    }
}
