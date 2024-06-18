using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using AG.COM.SDM.DAL;
using AG.COM.SDM.Model;
using AG.COM.SDM.Utility;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// Depiction:��־��ѯ����
    /// </summary>
    public partial class FormLogInfo : Form
    {
        private DataTable m_DataTable ;             //��ʱ���ݱ�
        private long m_ShowRecordCount = 1000;      //ÿ�μ��ؼ�¼�������
        private bool m_HasLoaderFinish = false;     //�жϼ�¼�Ƿ���ȫ���� 

        private IList m_List;                       //�ܼ�¼��ѯ�����ݼ�(�������
        private IList m_LogList;                    //��ǰ��ѯ�ļ�¼��
        private ITable m_Table;                     //DataGridView�󶨵����ݱ� 
        private ICursor m_Cursor;                   //�α����
        private ISelectionSet m_SelectSet;          //ѡ��

        public FormLogInfo()
        {
            InitializeComponent();
        }      

        private void FormLogInfo_Load(object sender, EventArgs e)
        {
            //�����ݿ��в�����־����
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "SELECT COUNT(*) from AGSDM_LOGGER t";
            IList tListLogger = tEntityHandler.GetEntities(strHQL);
            this.m_List = tListLogger;
            ctrPageUC1.NeedDataDiaplayChanged += new DataChangedEventHandler(ctrPageUC1_NeedDataDiaplayChanged);
            if (this.m_List.Count != 0)
            {
                ctrPageUC1.RecordCount = Convert.ToInt32(this.m_List[0]);
            }
            ctrPageUC1.strSQL = " from AGSDM_LOGGER t order by t.LOGTIME desc";
            ctrPageUC1.UpdateDiaplay();
        }

        /// <summary>
        /// ��ҳ�¼�
        /// </summary>
        /// <param name="startRecordIndex">��ʼҳ��</param>
        /// <param name="endRecordIndex">����ҳ��</param>
        private void ctrPageUC1_NeedDataDiaplayChanged(string strSQL,int startRecordIndex, int endRecordIndex)
        {
            this.dataGridView1.Rows.Clear();
            EntityHandler tEntityHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            m_LogList = tEntityHandler.GetCustomerPageModel(strSQL, startRecordIndex, endRecordIndex);
            try
            {
                for (int i = 0; i < m_LogList.Count; i++)
                {
                    AGSDM_LOGGER tLogger = m_LogList[i] as AGSDM_LOGGER;
                    DataGridViewRow tRow = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                    tRow.Cells[0].Value = tLogger.OBJECTID;
                    tRow.Cells[1].Value = tLogger.LOGUSER;
                    tRow.Cells[2].Value = tLogger.HOSTNAME;
                    tRow.Cells[3].Value = tLogger.USERNAME;
                    tRow.Cells[4].Value = tLogger.LOGTIME;
                    tRow.Cells[5].Value = tLogger.LOGMSG;
                    tRow.Cells[6].Value = tLogger.LOGTYPE;
                    tRow.Cells[7].Value = tLogger.LOGLEVEL;
                    tRow.Cells[8].Value = tLogger.PRODUCTNAME;
                }
                if (this.dataGridView1.Rows.Count != 0)
                {
                    this.dataGridView1.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ʧ��:" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.m_HasLoaderFinish == true) return;

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll && e.Type == ScrollEventType.LargeIncrement)
            {
                if (this.m_Cursor != null)
                {
                    //���õ�ǰ���Ϊæµ״̬
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        //����¼��������
                        this.FillRecordToDataTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //���õ�ǰ���Ϊ����״̬
                    this.Cursor = Cursors.Default;
                }
            }

            //��ֹ�ƶ����ȹ���
            if (e.NewValue - e.OldValue > 100)
            {
                e.NewValue = e.OldValue + 200;
            }
        }

        //��ѯ
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //������м���
            this.dataGridView1.DataSource = null;
            EntityHandler tEntetyHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_LOGGER t where 1=1 ";
            if (this.cobLoggerName.Text == "" || this.cobLoggerName.Text == "�����½������")
            {

                if (this.m_List.Count != 0)
                {
                    ctrPageUC1.RecordCount = Convert.ToInt32(this.m_List[0]);
                    ctrPageUC1.strSQL = " from AGSDM_LOGGER t order by t.LOGTIME desc";
                }                
            }
            else
            {
                strHQL += " and t.LOGUSER='" + this.cobLoggerName.Text + "'order by t.LOGTIME desc";
                IList tListLogger = tEntetyHandler.GetEntities("SELECT COUNT(*) from AGSDM_LOGGER t where t.LOGUSER='" + this.cobLoggerName.Text + "'");
                if (tListLogger.Count != 0)
                {
                    ctrPageUC1.RecordCount = Convert.ToInt32(tListLogger[0]);
                }
                ctrPageUC1.strSQL = strHQL;                
            }            
            ctrPageUC1.UpdateDiaplay();
            
        }
        
        //�߼���ѯ
        private void btnAdvanceQuery_Click(object sender, EventArgs e)
        {
            FormAdvancedQuery tAdvancedQuery = new FormAdvancedQuery();
            tAdvancedQuery.LoggerList = this.m_LogList;
            tAdvancedQuery.AdvanceQuery += new FormAdvancedQuery.AdvanceQueryHandler(AdvanceQuery);
            tAdvancedQuery.ShowDialog();
        }

        //����CSV�ļ�
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "ѡ�񱣴��ļ�·��";
            saveDlg.Filter = "CSV�ļ�(*.csv)|*.csv";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter tStreamWriter = new StreamWriter(saveDlg.FileName, false, Encoding.Unicode))
                {
                    //д�б���
                    for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                    {
                        tStreamWriter.Write(this.dataGridView1.Columns[j].HeaderText);
                        if (j == this.dataGridView1.Columns.Count - 1)
                            tStreamWriter.Write("\r\n");
                        else
                            tStreamWriter.Write('\t');
                    }

                    //���б�����ʾ�ļ�¼������ļ�
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        DataGridViewRow tDtRow = this.dataGridView1.Rows[i];

                        for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                        {
                            tStreamWriter.Write(tDtRow.Cells[j].Value);
                            if (j == this.dataGridView1.Columns.Count - 1)
                                tStreamWriter.Write("\r\n");
                            else
                                tStreamWriter.Write('\t');
                        }
                    }
                }
                MessageBox.Show("�ѳɹ�������־��¼���ļ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cobLoggerName_Enter(object sender, EventArgs e)
        {
            this.cobLoggerName.Text = "";
        }

        private void FormLogInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.m_Cursor != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(this.m_Cursor);
            }
        } 

        private void UpdataView(IList tListLogger)
        {          
            try
            {
                for (int i = 0; i < tListLogger.Count; i++)
                {
                    AGSDM_LOGGER tLogger = tListLogger[i] as AGSDM_LOGGER;
                    DataGridViewRow tRow = this.dataGridView1.Rows[this.dataGridView1.Rows.Add()];
                    tRow.Cells[0].Value = tLogger.OBJECTID;
                    tRow.Cells[1].Value = tLogger.LOGUSER;
                    tRow.Cells[2].Value = tLogger.HOSTNAME;
                    tRow.Cells[3].Value = tLogger.USERNAME;
                    tRow.Cells[4].Value = tLogger.LOGTIME;
                    tRow.Cells[5].Value = tLogger.LOGMSG;
                    tRow.Cells[6].Value = tLogger.LOGTYPE;
                    tRow.Cells[7].Value = tLogger.LOGLEVEL;
                    tRow.Cells[8].Value = tLogger.PRODUCTNAME;
                }
                if (this.dataGridView1.Rows.Count != 0)
                {
                    this.dataGridView1.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������ʧ��:" + ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// ����ֶ���Ϣ���б�ͷ
        /// </summary>
        /// <param name="pFields">�ֶ���Ϣ��</param>
        private void FillFieldInfoToColumn(IFields pFields)
        {
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField tField = pFields.get_Field(i);
                //ʵ��������
                DataColumn tDataColumn = new DataColumn(string.Format("{0}({1})", tField.AliasName, i));

                #region ���ø����Ƿ�ɱ༭

                //�ж���������
                switch (tField.Type)
                {
                    case esriFieldType.esriFieldTypeBlob:
                    case esriFieldType.esriFieldTypeGeometry:
                    case esriFieldType.esriFieldTypeRaster:
                    case esriFieldType.esriFieldTypeGUID:
                    case esriFieldType.esriFieldTypeOID:
                    case esriFieldType.esriFieldTypeGlobalID:
                        tDataColumn.ReadOnly = true;
                        break;
                    default:
                        tDataColumn.ReadOnly = !(tField.Editable);
                        break;
                }
                #endregion

                //���������
                this.m_DataTable.Columns.Add(tDataColumn);
            }
        }

        /// <summary>
        /// ����¼�������ݱ�
        /// </summary>
        private void FillRecordToDataTable()
        {
            long Recordnum = 0;

            IFields tFields = this.m_Cursor.Fields;                         //��ȡ�ֶμ�
            object[] values = new object[tFields.FieldCount];               //�����ж���ֵ���� 

            IRow tRow = this.m_Cursor.NextRow();                            //��ȡ��һ�м�¼
            while (tRow != null)
            {
                for (int i = 0; i < tFields.FieldCount; i++)
                {
                    IField tField = tFields.get_Field(i);
                    if (tField.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        values[i] = "�ռ�����";                        
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        values[i] = "����������";
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeRaster)
                    {
                        values[i] = "դ������";
                    }
                    else
                        values[i] = tRow.get_Value(i);
                }


                this.m_DataTable.Rows.Add(values);                          //����ж��� 
                if ((Recordnum++) > m_ShowRecordCount) break;               //�жϴ˴μ��ؼ�¼�Ƿ����ÿ�μ��ؼ�¼�������

                tRow = this.m_Cursor.NextRow();                             //��ȡ��һ�м�¼
                if (tRow == null) this.m_HasLoaderFinish = true;            //�����һ�м�¼Ϊ��
            }
        }

        private void AdvanceQuery(string strSQL)
        {
            //������м���
            this.dataGridView1.DataSource = null;
            EntityHandler tEntetyHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string CountSql = "SELECT COUNT(*) " + strSQL;
            IList tListLogger = tEntetyHandler.GetEntities(CountSql);
            ctrPageUC1.strSQL = strSQL;
            if (tListLogger.Count != 0)
            {
                ctrPageUC1.RecordCount = Convert.ToInt32( tListLogger[0]);
            }
            ctrPageUC1.UpdateDiaplay();
        }

    }
}