using System;
using ESRI.ArcGIS.Controls;

namespace AG.COM.SDM.Utility.AEChineseCommand
{
    /// <summary>
    /// 缩小（继承AE原装的Command功能，并把名称等翻译成中文）
    /// </summary>
    public sealed class MapZoomOutTool : ControlsMapZoomOutToolClass
    {
        public MapZoomOutTool()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
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
                return "缩小";
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

        public override string Tooltip
        {
            get
            {
                //return base.Tooltip;
                return "缩小";
            }
        }

        public override int Cursor
        {
            get
            {
                return base.Cursor;
            }
        }

        public override bool Deactivate()
        {
            return base.Deactivate();
        }

        public override bool OnContextMenu(int x, int y)
        {
            return base.OnContextMenu(x, y);
        }

        public override void OnDblClick()
        {
            base.OnDblClick();
        }

        public override void OnKeyDown(int keyCode, int shift)
        {
            base.OnKeyDown(keyCode, shift);
        }

        public override void OnKeyUp(int keyCode, int shift)
        {
            base.OnKeyUp(keyCode, shift);
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            base.OnMouseDown(button, shift, x, y);
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            base.OnMouseMove(button, shift, x, y);
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            base.OnMouseUp(button, shift, x, y);
        }

        public override void Refresh(int hdc)
        {
            base.Refresh(hdc);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
