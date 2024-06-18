using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Framework;
using AG.COM.SDM.SystemUI;

namespace AG.COM.SDM.Framework
{
    /// <summary>
    /// 工具栏服务服务接口
    /// </summary>
    public interface IToolBarService
    {
        /// <summary>
        /// 初始化工具服务，绑定功能到控件
        /// </summary>
        /// <param name="tRootContainer"></param>
        void Init(Dictionary<UIDesignControl, List<UIDesignControl>> tRootContainer);

        /// <summary>
        /// 获取插件
        /// </summary>
        /// <param name="ICommandName"></param>
        /// <returns></returns>
        UIDesignControl GetPlugin(string ICommandName);

        /// <summary>
        /// 保存工具条布局
        /// </summary>
        /// <param name="tFilePath"></param>
        void SaveLayout(string tFilePath);

        /// <summary>
        /// 恢复工具条布局
        /// </summary>
        /// <param name="tFilePath"></param>
        void RecoverLayout(string tFilePath);
    }
}
