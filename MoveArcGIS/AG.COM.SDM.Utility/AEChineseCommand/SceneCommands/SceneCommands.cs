using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Utility.AEChineseCommand
{
   /// <summary>
   /// 全图
   /// </summary>
   public class SceneFullExtentCommand: ControlsSceneFullExtentCommandClass
    {
        public SceneFullExtentCommand()
        {
           
        }
        public override string Caption
        {
            get
            {
                return "全图显示";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "全图显示";
            }
        }


        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }

        }

    /// <summary>
    /// 导航
    /// </summary>
    public class SceneNavigateCommand : ControlsSceneNavigateToolClass
    {
        public override string Caption
        {
            get
            {
                return "导航";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "导航";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
    /// <summary>
    /// 平移
    /// </summary>
    public class ScenePanCommand : ControlsScenePanToolClass
    {
        public override string Caption
        {
            get
            {
                return "平移";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "平移";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
    /// <summary>
    /// 目标居中
    /// </summary>
    public class SceneTargetCenterCommand : ControlsSceneTargetCenterToolClass
    {
        public override string Caption
        {
            get
            {
                return "目标居中";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "目标居中";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
    /// <summary>
    /// 目标缩放
    /// </summary>
    public class SceneTargetZoomCommand : ControlsSceneTargetZoomToolClass
    {
        public override string Caption
        {
            get
            {
                return "目标缩放";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "目标缩放";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
    /// <summary>
    /// 等比放大缩小
    /// </summary>
    public class SceneZoomInOutCommand : ControlsSceneZoomInOutToolClass
    {
        public override string Caption
        {
            get
            {
                return "等比放大缩小";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "等比放大缩小";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
    /// <summary>
    /// 放大
    /// </summary>
    public class SceneZoomOutCommand : ControlsSceneZoomOutToolClass
    {
        public override string Caption
        {
            get
            {
                return "放大";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "放大";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
    /// <summary>
    /// 缩小
    /// </summary>
    public class SceneZoomInCommand : ControlsSceneZoomInToolClass
    {
        public override string Caption
        {
            get
            {
                return "缩小";
            }
        }
        public override string Tooltip
        {
            get
            {
                return "缩小";
            }
        }
        public override bool Enabled { get { return base.Enabled; } }
        public override void OnCreate(object Hook) { base.OnCreate(Hook); }
        public override void OnClick() { base.OnClick(); }
        public override bool Checked { get { return base.Checked; } }
        public override string Name { get { return base.Name; } }
        public override string Message { get { return base.Message; } }
        public override string HelpFile { get { return base.HelpFile; } }
        public override int HelpContextID { get { return base.HelpContextID; } }
        public override int Bitmap { get { return base.Bitmap; } }
        public override string Category { get { return base.Category; } }
    }
}
