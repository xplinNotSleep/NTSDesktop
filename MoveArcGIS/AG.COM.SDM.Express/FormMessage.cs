using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Express
{
    public partial class FormMessage : Form,IMessageForm
    {
        public FormMessage()
        {
            InitializeComponent();          
        } 

        public void SetMessage(string msg)
        {
            lblMessage.Text = msg;
            Application.DoEvents();
        }
    }   
}