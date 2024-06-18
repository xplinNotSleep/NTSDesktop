using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.Plugins.Common
{
    /// <summary>
    /// �������������
    /// </summary>
    public partial class MeasureResultForm : Form 
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
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
                   ��break;
                }
            }

            System.Drawing.Point pPtStartPos = new System.Drawing.Point();
            pPtStartPos.X = tPtMainFormStartPos.X + 261;
            pPtStartPos.Y = tPtMainFormStartPos.Y + 81;
            this.Location = pPtStartPos;
        }

        //����Ƭ�γ���
        public void SetSegLength(string pSegLength)
        {
            SegLengthResultLabel.Text = pSegLength;
        }
        //�����߶��ܳ���
        public void SetTotalLength(string pTotalLength)
        {
            TotalLengthResultLabel.Text = pTotalLength;
        }

        //����ͼ�����
        public void SetArea(string pArea)
        {
            AreaResultLabel.Text = pArea;
        }
        //����ͼ���ܳ�
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