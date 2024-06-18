using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// 扩展DockContent
    /// </summary>
    public class DockContentEx : DockContent
    {
        public DockContentEx()
        {
            ContextMenuStrip tContextMenuStrip = new ContextMenuStrip();
            ToolStripMenuItem tToolStripMenuItem = new ToolStripMenuItem();
            //
            // cms
            //
            tToolStripMenuItem.Name = "cms";
            tToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            tToolStripMenuItem.Text = "关闭";
            tToolStripMenuItem.Click += new System.EventHandler(this.tsmiClose_Click);
            //
            // tsmiClose
            //
            tContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            tToolStripMenuItem});
            tContextMenuStrip.Name = "tsmiClose";
            tContextMenuStrip.Size = new System.Drawing.Size(99, 26);

            this.TabPageContextMenuStrip = tContextMenuStrip;
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            //右键关闭功能
            this.Close();
        }
    }
}
