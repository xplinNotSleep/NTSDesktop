using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{
    partial class LayersTreeView
    {
        /// <summary> 
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}



        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayersTreeView));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Layer.bmp");
            this.imageList1.Images.SetKeyName(1, "GroupLayer.bmp");
            this.imageList1.Images.SetKeyName(2, "GeodatabaseFeatureDataset16.png");
            this.imageList1.Images.SetKeyName(3, "GeodatabaseFeatureClassPoint16.png");
            this.imageList1.Images.SetKeyName(4, "GeodatabaseFeatureClassLine16.png");
            this.imageList1.Images.SetKeyName(5, "GeodatabaseFeatureClassPolygon16.png");
            this.imageList1.Images.SetKeyName(6, "GeodatabaseFeatureClassAnnotation16.png");
            // 
            // LayersTreeView
            // 
            this.ItemHeight = 20;
            this.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.LayersTreeView_AfterCheck);
            this.ResumeLayout(false);

        }

        void LayersTreeView_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse || e.Action == TreeViewAction.ByKeyboard)//
            {
                //递归设置子节点选中状态
                this.SetChildNodeChecked(e.Node);
                //递归设置父节点选中状态
                this.SetParentNodeChecked(e.Node);
            }
        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
    }
}
