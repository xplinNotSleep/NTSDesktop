using System;
using System.Drawing;
using System.Windows.Forms;

namespace AG.COM.SDM.Utility.Controls
{    
    /// <summary>
    /// �ɱ༭��ListView�ؼ�
    /// </summary>
    public class ListViewEdit : ListView
    { 
        private ListViewItem m_currentLVItem;
        private int m_nX = 0;
        private int m_nY = 0;
        private string m_strSubItemText;
        private int m_nSubItemSelected = 0;
        private ComboBox[] m_arrComboBoxes = new ComboBox[20];
        private System.Windows.Forms.TextBox editBox;
        public DateTimePicker editDateTime;
        private Font m_fontComboBox;
        private Font m_fontEdit;
        private Color m_bgcolorComboBox;
        private Color m_bgcolorEdit;
        private ComboBox m_CurrentComboBox = null;

        private bool IsEsc = false;
        private string oldText = "";

        /// <summary>
        /// Ĭ�Ϲ��캯��,ʵ�����¶���
        /// </summary>
        public ListViewEdit()
        {
            #region editBox
            editBox = new System.Windows.Forms.TextBox();
            editBox.Size = new System.Drawing.Size(0, 0);
            editBox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.editBox });
            editBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditOver);
            editBox.LostFocus += new System.EventHandler(this.FocusOver);
            editBox.AutoSize = true;
            editBox.Font = this.EditFont;
            editBox.BackColor = this.EditBgColor;
            editBox.BorderStyle = BorderStyle.FixedSingle;
            editBox.Hide();
            editBox.Text = "";
            #endregion

            this.m_fontComboBox = this.Font;
            this.EditFont = this.Font;

            this.EditBgColor = Color.LightBlue;
            this.m_bgcolorComboBox = Color.LightBlue;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SMKMouseDown);
            this.DoubleClick += new System.EventHandler(this.SMKDoubleClick);
            this.GridLines = true;

            #region editDateTime
            editDateTime = new DateTimePicker();
            editDateTime.Size = new System.Drawing.Size(0, 0);
            editDateTime.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { this.editDateTime });
            editDateTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DateTimeEditOver);
            editDateTime.LostFocus += new System.EventHandler(this.DateTimeFocusOver);
            editDateTime.AutoSize = true;
            editDateTime.Font = this.EditFont;
            editDateTime.BackColor = this.EditBgColor;
            editDateTime.Hide();
            editDateTime.Text = "";
            editDateTime.Format = DateTimePickerFormat.Custom;
            editDateTime.CustomFormat = "yyyy-MM-dd";
            #endregion

            this.FullRowSelect = true;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            //ˮƽ������Ϣ276 ����ֱ��������Ϣ277
            //����ʱ���������
            if ((m.Msg == 277) || (m.Msg == 276))
            {
                if (m_CurrentComboBox != null)
                    m_CurrentComboBox.Hide();

                editBox.Hide();
            }            
        }           

        /// <summary>
        /// ��ȡ�����������б�������
        /// </summary>
        public Font ComboBoxFont
        {
            get { return this.m_fontComboBox; }
            set { this.m_fontComboBox = value; }
        }  

        /// <summary>
        /// ��ȡ�����������б��ĸ���ı�����ɫ
        /// </summary>
        public Color ComboBoxBgColor
        {
            get 
            { 
                return this.m_bgcolorComboBox;
            }
            set
            {
                this.m_bgcolorComboBox = value;
                for (int i = 0; i < this.m_arrComboBoxes.Length; i++)
                { 
                    if (m_arrComboBoxes[i] != null) 
                        m_arrComboBoxes[i].BackColor = this.m_bgcolorComboBox;
                }
            }
        }
        
        /// <summary>
        /// ��ȡ�����ñ༭�ı��������
        /// </summary>
        public Font EditFont
        {
            get 
            { 
                return this.m_fontEdit; 
            }
            set
            {
                this.m_fontEdit = value;
                this.editBox.Font = this.m_fontEdit;
            }
        }

        /// <summary>
        /// ��ȡ�����ñ༭�ı���ı�����ɫ
        /// </summary>
        public Color EditBgColor
        {
            get { return this.m_bgcolorEdit; }
            set
            {
                this.m_bgcolorEdit = value;
                this.editBox.BackColor = this.m_bgcolorEdit;
            }
        }

        /// <summary>
        /// ����ָ���������б�����ʽ
        /// </summary>
        /// <param name="columnIndex">ָ��������</param>
        /// <param name="cs">����ʽ</param>
        public void SetColumnStyle(int columnIndex, ALAN_ListViewColumnStyle cs)
        {
            if (columnIndex < 0 || columnIndex > this.Columns.Count)
                throw new Exception("Column index is out of range");
            ((ALAN_ColumnHeader)Columns[columnIndex]).ColumnStyle = cs;
        }

        /// <summary>
        /// ����ָ���������б��ĵ�����Դ
        /// </summary>
        /// <param name="columnIndex">ָ��������</param>
        /// <param name="items">�󶨵ļ�����</param>
        public void BoundListToColumn(int columnIndex, string[] items)
        {
            if (columnIndex < 0 || columnIndex > this.Columns.Count)
                throw new Exception("Column index is out of range");

            if (((ALAN_ColumnHeader)Columns[columnIndex]).ColumnStyle != ALAN_ListViewColumnStyle.ComboBox)
                throw new Exception("Column should be ComboBox style");
            
            ComboBox newbox = new ComboBox();
            newbox.DataSource = items;
            newbox.Size = new System.Drawing.Size(0, 0);
            newbox.Location = new System.Drawing.Point(0, 0);
            this.Controls.AddRange(new System.Windows.Forms.Control[] { newbox });
            newbox.SelectedIndexChanged += new System.EventHandler(this.CmbSelected);
            newbox.LostFocus += new System.EventHandler(this.CmbFocusOver);
            newbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CmbKeyPress);

            newbox.Font = this.ComboBoxFont;
            newbox.BackColor = this.ComboBoxBgColor;
            newbox.DropDownStyle = ComboBoxStyle.DropDownList;
            newbox.Hide();

            this.m_arrComboBoxes[columnIndex] = newbox;
        }

        /// <summary>
        /// �������б����н�����������������
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">�¼�����</param>
        private void CmbKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            ComboBox cmbBox = (ComboBox)sender;

            if (e.KeyChar == 13 || e.KeyChar == 27) //CR or ESC press
            {
                cmbBox.Hide();
            }
        }

        private void CmbSelected(object sender, System.EventArgs e)
        {
            ComboBox cmbBox = (ComboBox)sender;
            int sel = cmbBox.SelectedIndex;
            if (sel >= 0)
            {
                string itemSel = cmbBox.Items[sel].ToString();
                if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != itemSel)
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = itemSel;

                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, itemSel);
                }
            }
        } 

        private void CmbFocusOver(object sender, System.EventArgs e)
        {
            ComboBox cmbBox = (ComboBox)sender;
            cmbBox.Hide();            
        }

        /// <summary>
        /// �ڱ༭�ı�����н��������°���������
        /// </summary>
        /// <param name="sender">Դ����</param>
        /// <param name="e">�¼�����</param>
        private void EditOver(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != editBox.Text)
                {
                    oldText = m_currentLVItem.SubItems[m_nSubItemSelected].Text;

                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = editBox.Text;

                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, editBox.Text);
                }

                editBox.Hide();
            }

            //ESC��
            if (e.KeyChar == 27)
            {
                m_currentLVItem.SubItems[m_nSubItemSelected].Text = oldText;
                IsEsc = true;
                editBox.Hide();
            }
               
        }

        private void FocusOver(object sender, System.EventArgs e)
        {
            if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != editBox.Text)
            {
                //����ǰ����˳�������֮ǰ�༭�Ķ����ϣ�����ԭ��������
                if(IsEsc)
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = oldText;
                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, oldText);
                    //AfterEdit(m_currentLVItem, m_currentLVItem.SubItems[m_nSubItemSelected].Text);
                    IsEsc = false;
                }
                else
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = editBox.Text;
                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, editBox.Text);
                    oldText = editBox.Text; 
                }
                
            }
            editBox.Hide();
        }      

        /// <summary>
        /// Ҫ������ڵĻ����԰��ո��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimeEditOver(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if(!string.IsNullOrEmpty(editDateTime.Text))
            {
                string strTime = editDateTime.Text;
                if(strTime.Contains("����"))
                {
                    int index = strTime.IndexOf('��');
                    string day = strTime[index + 1].ToString();
                    string day1 = "����" + day;
                    strTime.Replace(day1, "");
                    editDateTime.Text = strTime;
                }
            }

            if (e.KeyChar == 13)
            {
                if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != editDateTime.Text)
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = editDateTime.Text;

                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, editDateTime.Text);
                }

                editDateTime.Hide();
            }

            //ESC��
            if (e.KeyChar == 27)
            {
                m_currentLVItem.SubItems[m_nSubItemSelected].Text = oldText;
                IsEsc = true;
                editDateTime.Hide();
            }
            if(e.KeyChar==32)
            {
                //m_currentLVItem.SubItems[m_nSubItemSelected].Text = "";
                editDateTime.Text = "";
                editDateTime.Hide();
            }
                
        }

        private void DateTimeFocusOver(object sender, System.EventArgs e)
        {


            if (m_currentLVItem.SubItems[m_nSubItemSelected].Text != editDateTime.Text)
            {
                if(IsEsc)
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = oldText;
                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, oldText);
                    IsEsc = false;
                }
                else
                {
                    m_currentLVItem.SubItems[m_nSubItemSelected].Text = editDateTime.Text;
                    //if (AfterEdit != null)
                    //    AfterEdit(m_currentLVItem, editDateTime.Text);
                    oldText = editDateTime.Text;
                }
                
            }
            editDateTime.Hide();
        }

        /// <summary>
        /// ˫�����Ա༭��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SMKDoubleClick(object sender, System.EventArgs e)
        {
            int nStart = m_nX; 
            int spos = 0;
            int epos = this.Columns[0].Width;

            for (int i = 0; i < this.Columns.Count; i++)
            {
                if (nStart > spos && nStart < epos)
                {
                    m_nSubItemSelected = i;
                    break;
                }

                spos = epos;  
                epos += this.Columns[i].Width;
            } 

            Point point = Display.DisplayHelper.GetMousePosInControl((int)this.Handle);
            ListViewHitTestInfo hit = this.HitTest(point);

            if (hit.Item == null) return;
            if (hit.SubItem == null) return;
               
            m_strSubItemText = m_currentLVItem.SubItems[m_nSubItemSelected].Text; 
            ALAN_ColumnHeader column = (ALAN_ColumnHeader)Columns[m_nSubItemSelected];

            if (column.ColumnStyle == ALAN_ListViewColumnStyle.ComboBox)
            {
                ComboBox cmbBox = this.m_arrComboBoxes[m_nSubItemSelected];
                if (cmbBox == null)
                    throw new Exception("The ComboxBox control bind to current column is null");

                Rectangle r = hit.SubItem.Bounds;

                cmbBox.Size = editBox.Size = new Size(r.Width, r.Height);
                cmbBox.Location = new Point(r.Left,r.Top); 
                cmbBox.Show();
                cmbBox.Text = m_strSubItemText;
                cmbBox.SelectAll();
                cmbBox.Focus();
                cmbBox.DroppedDown = true;

                m_CurrentComboBox = cmbBox;
            }

            if (column.ColumnStyle == ALAN_ListViewColumnStyle.EditBox)
            {
                if (hit.SubItem != null)
                {
                    Rectangle r = hit.SubItem.Bounds;
                    editBox.Size = new Size(r.Width, r.Height);
                    editBox.Location = new Point(r.Left, r.Top);
                    editBox.Show();
                    editBox.Text = m_strSubItemText;
                    editBox.SelectAll();
                    editBox.Focus();
                } 
            } 

            if(column.ColumnStyle==ALAN_ListViewColumnStyle.DateTimePicker)
            {
                if(hit.SubItem!=null)
                {
                    Rectangle r = hit.SubItem.Bounds;
                    editDateTime.Size = new Size(r.Width, r.Height);
                    editDateTime.Location = new Point(r.Left, r.Top);
                    editDateTime.Show();
                    editDateTime.Text = m_strSubItemText;
                    editDateTime.Focus();
                    editDateTime.Value = DateTime.Now;
                    editDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }

        }

        public void SMKMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m_currentLVItem = this.GetItemAt(e.X, e.Y);
            m_nX = e.X;
            m_nY = e.Y;            
        }

        public event AfterEditDelegate AfterEdit = null;
    }

    public delegate void AfterEditDelegate(ListViewItem item,string value);

    /// <summary>
    /// �з��ö��
    /// </summary>
    public enum ALAN_ListViewColumnStyle
    {
        /// <summary>
        /// ֻ��
        /// </summary>
        ReadOnly,

        /// <summary>
        /// �༭״̬����ʾΪ�ı���
        /// </summary>
        EditBox,

        /// <summary>
        /// �༭״̬����ʾΪ��Ͽ�
        /// </summary>
        ComboBox,  

        /// <summary>
        /// �༭״̬����ʾΪ����ѡ�����
        /// </summary>
        DateTimePicker
    };

    /// <summary>
    /// ������ ��
    /// </summary>
    public class ALAN_ColumnHeader : ColumnHeader
    {
        private ALAN_ListViewColumnStyle cs; //���еķ��
        
        public ALAN_ColumnHeader()
            : base()
        {
            cs = ALAN_ListViewColumnStyle.ReadOnly;
        }

        /// <summary>
        /// ʵ�����������¶���
        /// </summary>
        /// <param name="_cs">ָ�����з��</param>
        public ALAN_ColumnHeader(ALAN_ListViewColumnStyle _cs)
        {
            cs = _cs;
        }

        /// <summary>
        /// ��ȡ������ListView����ʽ
        /// </summary>
        public ALAN_ListViewColumnStyle ColumnStyle
        {
            get { return cs; }
            set { cs = value; }
        }
    } 
}


 
