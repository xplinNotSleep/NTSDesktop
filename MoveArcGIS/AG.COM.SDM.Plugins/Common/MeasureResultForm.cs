using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// 测量结果窗体类
    /// </summary>
    public partial class MeasureResultForm : Form 
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MeasureResultForm()
        {
            InitializeComponent();
            System.Drawing.Point tPtMainFormStartPos=new Point();
            foreach (Form form in System.Windows.Forms.Application.OpenForms)
            {
                if (form.Name == "WinMain")
                {
                    tPtMainFormStartPos = form.Location;
                   　break;
                }
            }

            System.Drawing.Point pPtStartPos = new System.Drawing.Point();
            pPtStartPos.X = tPtMainFormStartPos.X + 261;
            pPtStartPos.Y = tPtMainFormStartPos.Y + 81;
            this.Location = pPtStartPos;
        }

        //设置片段长度
        public void SetSegLength(string pSegLength)
        {
            SegLengthResultLabel.Text = pSegLength;
        }
        //设置线段总长度
        public void SetTotalLength(string pTotalLength)
        {
            TotalLengthResultLabel.Text = pTotalLength;
        }

        //设置图形面积
        public void SetArea(string pArea)
        {
            AreaResultLabel.Text = pArea;
        }
        //设置图形周长
        public void SetAreaLength(string pAreaLength)
        {
            AreaLengthResultLabel.Text=pAreaLength.ToString ();
        }
        public void ShowGroup(string pAreaOrLength)
        {
            if (pAreaOrLength == "Area")
            {
                AreaResultgroupBox.Visible = true;
                LengthResultgroupBox.Visible = false;
            }
            else if(pAreaOrLength=="Length")
            {
                LengthResultgroupBox.Visible =true ;
                AreaResultgroupBox.Visible = false;
            }
        }

        private void MeasureResultForm_Load(object sender, EventArgs e)
        {

        }

        private void AreaResultgroupBox_Enter(object sender, EventArgs e)
        {

        }
    }
}