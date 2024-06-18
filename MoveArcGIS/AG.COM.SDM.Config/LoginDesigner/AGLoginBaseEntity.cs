using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.COM.SDM.Config
{
    [Serializable]
    public class AGLoginBaseEntity
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
    [Serializable]
    public class AGLoginFormEntity : AGLoginBaseEntity
    {
        public Image BackgroundImage { get; set; }
        public Size Size
        {
            get
            {
                if (null != BackgroundImage) return BackgroundImage.Size;
                else return new Size(800, 600);
            }

        }
    }
    [Serializable]
    public class AGLoginUCEntity : AGLoginBaseEntity
    {
        public AGLoginUCEntity()
        {
            Location = new Point(0, 0);
            Size = new Size(100, 20);
            Visible = true;
        }
        public Size Size { get; set; }
        public Point Location { get; set; }
        public bool Visible { get; set; }
    }
}
