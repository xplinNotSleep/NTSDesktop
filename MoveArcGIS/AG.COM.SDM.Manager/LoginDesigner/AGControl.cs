using AG.COM.SDM.Config;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.Manager
{
    public class AGControl : UserControl
    {
        public AGControl()
        {
            //////SetStyle(ControlStyles.SupportsTransparentBackColor
            //////     //| ControlStyles.UserPaint
            //////     //| ControlStyles.AllPaintingInWmPaint
            //////     | ControlStyles.Opaque, true);

            this.BackColor = Color.Transparent;
            this.Size = new Size(100, 25);
        }
        [Category("设计"), Description("当前控件的名称"), ReadOnly(true), DisplayName("Name")]
        public string NewName { get { return base.Name; } set { base.Name = value; } }

        public AGLoginUCEntity AGProperty
        {
            get
            {
                return new AGLoginUCEntity()
                {
                    Location = Location,
                    Size = Size,
                    Name = Name,
                    Visible = Visible,
                    Text = Text
                };
            }
            set
            {
                if (value != null)
                {
                    Location = value.Location;
                    Size = value.Size;
                    Text = value.Text;
                    Visible = value.Visible;
                    Name = value.Name;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Pen p = new Pen(Color.Red, 1);
            p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(p, 1, 1, Width - 2, Height - 2);
            var sf = g.MeasureString(Text, Font);
            g.DrawString(Text, Font, Brushes.Blue, (Width - sf.Width) / 2, (Height - sf.Height) / 2);
            base.OnPaint(e);
        }
    }
}
