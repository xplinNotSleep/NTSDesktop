using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AG.COM.SDM.Model;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 高级查询窗体类
    /// </summary>
    public partial class FormAdvancedQuery : Form
    {
        #region 变量与属性

        private IList m_LoggerList;

        private List<string> m_UploadUserList = new List<string>();
        private List<string> m_EditUserList = new List<string>();
        private List<string> m_CheckUserList = new List<string>();

        public delegate void AdvanceQueryHandler(string strSQL);
        public event AdvanceQueryHandler AdvanceQuery;

        public IList LoggerList
        {
            set
            {
                this.m_LoggerList = value;
            }
        }

        #endregion

        #region 窗体控件事件方法


        public FormAdvancedQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载窗体
        /// </summary>
        private void FormAdvancedQuery_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string strSqlText = string.Empty;
                string querySql = "from AGSDM_LOGGER t where 1=1";

                //登录名称
                if (this.cobLoggerName.Text.Trim() != string.Empty)
                {
                    querySql += " and t.LOGUSER LIKE '%" + this.cobLoggerName.Text.Trim() + "%'";
                }
                //主机名称
                if (this.cobHostName.Text.Trim() != string.Empty)
                {
                    querySql += " and t.HOSTNAME LIKE '%" + this.cobHostName.Text.Trim() + "%'";
                }
                //用户名
                if (this.cobUsername.Text.Trim() != string.Empty)
                {
                    querySql += " and t.USERNAME LIKE '%" + this.cobUsername.Text.Trim() + "%'";
                }
                //登录类型
                if (this.cobLogType.Text.Trim() != string.Empty)
                {
                    querySql += " and t.LOGTYPE LIKE '%" + this.cobLogType.Text.Trim() + "%'";
                }
                //级别
                if (this.cobLogLever.Text.Trim() != string.Empty)
                {
                    querySql += " and t.LOGLEVEL LIKE '%" + this.cobLogLever.Text.Trim() + "%'";

                }
                //产品名称
                if (this.cobProductName.Text.Trim() != string.Empty)
                {
                    querySql += " and t.PRODUCTNAME LIKE '%" + this.cobProductName.Text.Trim() + "%'";

                }
                string strTimeFormat = "'yyyy-mm-dd'";
                if (dtpLogerBeginTime.Checked)
                {
                    querySql += string.Format(" and t.LOGTIME >= to_date('{0}',{1})", this.dtpLogerBeginTime.Value.ToShortDateString(), strTimeFormat);
                }
                //上传结束时间
                if (this.dtpLogerEndTime.Checked)
                {
                    querySql += string.Format(" and t.LOGTIME < to_date('{0}',{1})+1", this.dtpLogerEndTime.Value.ToShortDateString(), strTimeFormat);
                }
                querySql += "order by t.LOGTIME desc";
                if (null != AdvanceQuery)
                {
                    AdvanceQuery(querySql);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 取消查询
        /// </summary>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 自定义函数

        /// <summary>
        /// 初始化界面内容
        /// </summary>
        private void InitForm()
        {
            try
            {
                for (int i = 0; i < this.m_LoggerList.Count; i++)
                {
                    AGSDM_LOGGER tLogger = this.m_LoggerList[i] as AGSDM_LOGGER;
                    if (tLogger.LOGUSER != null && !this.cobLoggerName.Items.Contains(tLogger.LOGUSER))
                    {
                        this.cobLoggerName.Items.Add(tLogger.LOGUSER);
                    }
                    if (tLogger.HOSTNAME != null && !this.cobHostName.Items.Contains(tLogger.HOSTNAME))
                    {
                        this.cobHostName.Items.Add(tLogger.HOSTNAME);
                    }
                    if (tLogger.USERNAME != null && !this.cobUsername.Items.Contains(tLogger.USERNAME))
                    {
                        this.cobUsername.Items.Add(tLogger.USERNAME);
                    }
                    if (tLogger.LOGTYPE != null && !this.cobLogType.Items.Contains(tLogger.LOGTYPE))
                    {
                        this.cobLogType.Items.Add(tLogger.LOGTYPE);
                    }
                    if (tLogger.LOGLEVEL != null && !this.cobLogLever.Items.Contains(tLogger.LOGLEVEL))
                    {
                        this.cobLogLever.Items.Add(tLogger.LOGLEVEL);
                    }
                    if (tLogger.PRODUCTNAME != null && !this.cobProductName.Items.Contains(tLogger.PRODUCTNAME))
                    {
                        this.cobProductName.Items.Add(tLogger.PRODUCTNAME);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化界面失败:" + ex.Message);
                AG.COM.SDM.Utility.Logger.ExceptionLog.Log.Error(ex.Message);
            }
        }
        
        #endregion
    }
}
