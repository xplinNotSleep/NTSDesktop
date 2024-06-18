using AG.COM.SDM.Framework;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AG.COM.SDM.Express
{
    public partial class WinMainTest : RibbonForm
    {
        private IFramework m_Framework = new AG.COM.SDM.Framework.Framework();

        public WinMainTest()
        {
            InitializeComponent();
        }
    }
}
