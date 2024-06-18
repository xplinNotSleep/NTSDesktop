using AG.COM.SDM.Config;
using AG.COM.SDM.Utility;
using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AG.COM.SDM.Manager
{
    public partial class FormKeyDict : DockContent
    {
        public FormKeyDict()
        {
            InitializeComponent();
        }

        private void FormKeyDict_Load(object sender, EventArgs e)
        {
            //���ԴX������
            DataGridViewTextBoxColumn tDataGridViewColumn = new DataGridViewTextBoxColumn();

            tDataGridViewColumn.HeaderText = "�ؼ���";
            tDataGridViewColumn.ValueType = typeof(String);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            tDataGridViewColumn.ReadOnly = true;
            this.dtKeyDict.Columns.Add(tDataGridViewColumn);

            //���ԴY������
            tDataGridViewColumn = new DataGridViewTextBoxColumn();
            tDataGridViewColumn.HeaderText = "ֵ";
            tDataGridViewColumn.ValueType = typeof(String);
            tDataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            tDataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            tDataGridViewColumn.ReadOnly = true;
            this.dtKeyDict.Columns.Add(tDataGridViewColumn);

            //ʵ������Դ�ļ�������
            ResourceHelper helper = new ResourceHelper(
                CommonVariables.ConfigFile, CommonConstString.STR_TempPath);
            Hashtable dictHashTable = helper.KeyValues;

            foreach (DictionaryEntry entry in dictHashTable)
            {
                string strKey = entry.Key.ToString();
                string strValue = entry.Value.ToString();
                this.dtKeyDict.Rows.Add(strKey, strValue);
            }
        }

        private void dtKeyDict_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //������к�
            using (SolidBrush brush = new SolidBrush(this.dtKeyDict.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture), e.InheritedRowStyle.Font, brush, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}