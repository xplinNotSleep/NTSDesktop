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
    /// Depiction:日志查询窗体
    /// </summary>
    public partial class FormLogInfo : Form
    {
        private DataTable m_DataTable ;             //临时数据表
        private long m_ShowRecordCount = 1000;      //每次加载记录的最大数
        private bool m_HasLoaderFinish = false;     //判断记录是否完全加载 

        private IList m_List;                       //总记录查询的数据集(求个数）
        private IList m_LogList;                    //当前查询的记录集
        private ITable m_Table;                     //DataGridView绑定的数据表 
        private ICursor m_Cursor;                   //游标对象
        private ISelectionSet m_SelectSet;          //选择集

        public FormLogInfo()
        {
            InitializeComponent();
        }      

        private void FormLogInfo_Load(object sender, EventArgs e)
        {
            //从数据库中查找日志数据
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
        /// 分页事件
        /// </summary>
        /// <param name="startRecordIndex">开始页码</param>
        /// <param name="endRecordIndex">结束页码</param>
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
                MessageBox.Show("数据填充失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.m_HasLoaderFinish == true) return;

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll && e.Type == ScrollEventType.LargeIncrement)
            {
                if (this.m_Cursor != null)
                {
                    //设置当前光标为忙碌状态
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        //填充记录集到数据
                        this.FillRecordToDataTable();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    //设置当前光标为正常状态
                    this.Cursor = Cursors.Default;
                }
            }

            //防止移动幅度过大
            if (e.NewValue - e.OldValue > 100)
            {
                e.NewValue = e.OldValue + 200;
            }
        }

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //清除所有集合
            this.dataGridView1.DataSource = null;
            EntityHandler tEntetyHandler = EntityHandler.CreateEntityHandler("AG.COM.SDM.Model");
            string strHQL = "from AGSDM_LOGGER t where 1=1 ";
            if (this.cobLoggerName.Text == "" || this.cobLoggerName.Text == "输入登陆者名称")
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
        
        //高级查询
        private void btnAdvanceQuery_Click(object sender, EventArgs e)
        {
            FormAdvancedQuery tAdvancedQuery = new FormAdvancedQuery();
            tAdvancedQuery.LoggerList = this.m_LogList;
            tAdvancedQuery.AdvanceQuery += new FormAdvancedQuery.AdvanceQueryHandler(AdvanceQuery);
            tAdvancedQuery.ShowDialog();
        }

        //导出CSV文件
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Title = "选择保存文件路径";
            saveDlg.Filter = "CSV文件(*.csv)|*.csv";

            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter tStreamWriter = new StreamWriter(saveDlg.FileName, false, Encoding.Unicode))
                {
                    //写列标题
                    for (int j = 0; j < this.dataGridView1.Columns.Count; j++)
                    {
                        tStreamWriter.Write(this.dataGridView1.Columns[j].HeaderText);
                        if (j == this.dataGridView1.Columns.Count - 1)
                            tStreamWriter.Write("\r\n");
                        else
                            tStreamWriter.Write('\t');
                    }

                    //将列表中显示的记录输出到文件
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
                MessageBox.Show("已成功导出日志记录到文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("数据填充失败:" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 填充字段信息到列标头
        /// </summary>
        /// <param name="pFields">字段信息集</param>
        private void FillFieldInfoToColumn(IFields pFields)
        {
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                IField tField = pFields.get_Field(i);
                //实例数据列
                DataColumn tDataColumn = new DataColumn(string.Format("{0}({1})", tField.AliasName, i));

                #region 设置该列是否可编辑

                //判断数据类型
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

                //添加数据列
                this.m_DataTable.Columns.Add(tDataColumn);
            }
        }

        /// <summary>
        /// 填充记录集到数据表
        /// </summary>
        private void FillRecordToDataTable()
        {
            long Recordnum = 0;

            IFields tFields = this.m_Cursor.Fields;                         //获取字段集
            object[] values = new object[tFields.FieldCount];               //定义行对象值集合 

            IRow tRow = this.m_Cursor.NextRow();                            //获取下一行记录
            while (tRow != null)
            {
                for (int i = 0; i < tFields.FieldCount; i++)
                {
                    IField tField = tFields.get_Field(i);
                    if (tField.Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        values[i] = "空间数据";                        
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeBlob)
                    {
                        values[i] = "二进制数据";
                    }
                    else if (tField.Type == esriFieldType.esriFieldTypeRaster)
                    {
                        values[i] = "栅格数据";
                    }
                    else
                        values[i] = tRow.get_Value(i);
                }


                this.m_DataTable.Rows.Add(values);                          //添加行对象 
                if ((Recordnum++) > m_ShowRecordCount) break;               //判断此次加载记录是否大于每次加载记录的最大数

                tRow = this.m_Cursor.NextRow();                             //获取下一行记录
                if (tRow == null) this.m_HasLoaderFinish = true;            //如果下一行记录为空
            }
        }

        private void AdvanceQuery(string strSQL)
        {
            //清除所有集合
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