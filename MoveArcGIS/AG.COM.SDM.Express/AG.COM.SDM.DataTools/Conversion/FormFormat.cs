using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// Shape数据与其它数据格式相互转换格式选择窗体
    /// </summary>
    public partial class FormFormat : Form
    {
        private KeyValuePair<string, string> m_SelectItem;
        private bool IsExport = false;//是否为导出选项，导出时带有vct，导入数据时没有vct

        /// <summary>
        /// 初始化实例对象
        /// </summary>
        /// <param name="pBool">是否shape导出为其它格式，如是则为true，否则为 false</param>
        public FormFormat(bool pBool)
        {
            InitializeComponent();
            IsExport = pBool;
        }

        public KeyValuePair<string, string> SelectItem
        {
            get { return m_SelectItem; }
        }

        private void frmFormat_Load(object sender, EventArgs e)
        {
            InitListView(IsExport);
        }

        private void InitListView(bool pBool)
        {
            Dictionary<string, string> tDictionary = new Dictionary<string, string>();
            tDictionary.Add("E00", "ESRI ArcInfo Export(E00)");
            if (IsExport)
            {
                tDictionary.Add("vct", "IDRSI Vector Format");
            }
            tDictionary.Add("mif", "MapInfo MIF/MID");
            tDictionary.Add("dwg", "Autodesk AutoCAD DWG/DXF");
            tDictionary.Add("gml", "GML(Geography Markup Language)");
            tDictionary.Add("adf", "ESRI ARCINFO Converage");

            foreach (KeyValuePair<string, string> keyValuePair in tDictionary)
            {
                ListViewItem tListView = new ListViewItem(keyValuePair.Value);
                tListView.Tag = keyValuePair;
                tListView.SubItems.Add(keyValuePair.Key);
                lvwFormat.Items.Add(tListView);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                m_SelectItem = lvwFormat.SelectedItems[0].Tag is KeyValuePair<string, string> ? (KeyValuePair<string, string>) lvwFormat.SelectedItems[0].Tag : new KeyValuePair<string, string>();
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}