﻿using System;
using ESRI.ArcGIS.Controls;

namespace AG.COM.SDM.Utility.AEChineseCommand
{
    /// <summary>
    /// 固定比例缩小（继承AE原装的Command功能，并把名称等翻译成中文）
    /// </summary>
    public sealed class MapZoomOutFixedCommand: ControlsMapZoomOutFixedCommandClass
    {
        public MapZoomOutFixedCommand()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public override string Tooltip
        {
            get
            {
                //				return base.Tooltip;
                return "固定比例缩小";
            }
        }

        public override int Bitmap
        {
            get
            {
                return base.Bitmap;
            }
        }

        public override string Caption
        {
            get
            {
                return "固定比例缩小";
            }
        }

        public override string Category
        {
            get
            {
                return base.Category;
            }
        }

        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
        }

        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int HelpContextID
        {
            get
            {
                return base.HelpContextID;
            }
        }

        public override string HelpFile
        {
            get
            {
                return base.HelpFile;
            }
        }

        public override string Message
        {
            get
            {
                return base.Message;
            }
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }
        }

        public override void OnClick()
        {
            base.OnClick();
        }

        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}