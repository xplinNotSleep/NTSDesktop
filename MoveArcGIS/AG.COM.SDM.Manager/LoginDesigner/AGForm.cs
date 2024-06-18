using AG.COM.SDM.Config;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AG.COM.SDM.Manager
{
    public partial class AGForm : UserControl
    {
        public AGForm()
        {
        }

        [Category("外观"), Description("与控件关联的文本"), Browsable(true), DisplayName("Text")]
        public string NewText { get { return base.Text; } set { base.Text = value; } }

        [Category("设计"), Description("窗体的名称"), ReadOnly(true), DisplayName("Name")]
        public string NewName { get { return base.Name; } set { base.Name = value; } }


        public AGLoginFormEntity AGProperty
        {
            get
            {
                return new AGLoginFormEntity()
                {
                    BackgroundImage = BackgroundImage,
                    Text = Text,
                    Name = Name
                };
            }
            set
            {
                if (value != null)
                {
                    BackgroundImage = value.BackgroundImage;
                    Text = value.Text;
                    Name = value.Name;
                    Refresh();
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (null != AGProperty.BackgroundImage && Size != AGProperty.Size)
                this.Size = AGProperty.BackgroundImage.Size;

            base.OnSizeChanged(e);
        }
        protected override void OnBackgroundImageChanged(EventArgs e)
        {
            OnSizeChanged(e);
            base.OnBackgroundImageChanged(e);
        }
    }

}
