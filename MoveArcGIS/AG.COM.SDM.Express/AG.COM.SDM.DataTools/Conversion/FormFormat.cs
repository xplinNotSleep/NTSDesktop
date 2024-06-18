using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AG.COM.SDM.DataTools.Conversion
{
    /// <summary>
    /// Shape�������������ݸ�ʽ�໥ת����ʽѡ����
    /// </summary>
    public partial class FormFormat : Form
    {
        private KeyValuePair<string, string> m_SelectItem;
        private bool IsExport = false;//�Ƿ�Ϊ����ѡ�����ʱ����vct����������ʱû��vct

        /// <summary>
        /// ��ʼ��ʵ������
        /// </summary>
        /// <param name="pBool">�Ƿ�shape����Ϊ������ʽ��������Ϊtrue������Ϊ false</param>
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