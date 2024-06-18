using System;
using System.Windows.Forms;

namespace AG.COM.SDM.Config
{
    /// <summary>
    /// 自定义分页控件
    /// </summary>
    public partial class CtrPageUC : UserControl
    {
        /// <summary>
        /// 需要改变数据显示发生的事件
        /// </summary>
        public event DataChangedEventHandler NeedDataDiaplayChanged;
        public event ItemChangedEventHandler NeedItemDisplayChanged;
        private bool m_changeCurrentPageManual = false; //是否手动改变当前页
        private int m_recordPerPage = 20;                //每页显示行数
        private int m_recordCount = 0;                   //总记录数
        private int m_currentPage = 1;                   //当前页号
        private int m_pageCount = 1;
        private int m_currentPageStartIndex = 0;
        private int m_currentPageEndIndex = 0;
        private string m_strSQL;                        //查询条件

        /// <summary>
        /// 设置查询条件
        /// </summary>
        public string strSQL
        {
            set
            {
                this.m_strSQL = value;
            }
        }

        /// <summary>
        /// 获取或设置总记录条数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return m_recordCount;
            }
            set
            {
                m_recordCount = value;
            }
        }

        /// <summary>
        /// 获取总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return m_pageCount;
            }
        }

        /// <summary>
        /// 获取或设置当前页号
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return this.m_currentPage;
            }
            set
            {
                this.m_currentPage = value;
            }
        }

        /// <summary>
        /// 获取或设置每页显示行数
        /// </summary>
        public int RecordPerPage
        {
            get
            {
                return this.m_recordPerPage;
            }
            set
            {
                if (value <= 0)
                {
                    this.m_recordPerPage = 20;
                    throw new Exception("每页显示的行数不能小于0");
                }
                this.m_recordPerPage = value;
            }
        }

        /// <summary>
        /// 默认构造方法
        /// </summary>
        public CtrPageUC()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="recordPerPage">每页要显示的记录数</param>
        public CtrPageUC(int recordPerPage):this()
        {
            this.RecordPerPage = recordPerPage;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="recordCount">总记录数</param>
        /// <param name="recordPerPage">每页要显示的记录数</param>
        public CtrPageUC(int recordCount, int recordPerPage)
            : this(recordPerPage)
        {
            m_recordCount = recordCount;
            m_currentPage = 1;    //当前页数从1开始
            m_pageCount = GetPageCount(m_recordCount, m_recordPerPage);
        }

        private void PageUC_Load(object sender, EventArgs e)
        {

        }

        private void tscRecordPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tscRecordPerPage_TextChanged(object sender, EventArgs e)
        {
            int recordPerPage = 20; 
            bool canToInt = int.TryParse(tscRecordPerPage.Text.Trim(), out recordPerPage);
            if (!canToInt || (recordPerPage <= 0))
            {
                MessageBox.Show("请输入正确的页码", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tscRecordPerPage.Text = "20";
                return;
            }
            if (recordPerPage > 1000)
            {
                MessageBox.Show("您输入的页码大于显示的最大值，请重新输入", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tscRecordPerPage.Text = "20";
                return;
            }
            m_recordPerPage = recordPerPage;
            m_pageCount = GetPageCount(m_recordCount, m_recordPerPage);
            m_currentPage = 1;
            ChangeData();
        }

        //当前页改变
        private void txtCurrentPage_TextChanged(object sender, EventArgs e)
        {
            if (m_changeCurrentPageManual)
            {
                int currentPage = 1;
                bool canToInt = int.TryParse(txtCurrentPage.Text.Trim(), out currentPage);
                if (!canToInt || (currentPage <= 0) || (currentPage > m_pageCount))
                {
                    currentPage = m_pageCount;
                }
                m_currentPage = currentPage;
                ChangeData();
                ChangeItem();
            }
        }

        //当前页
        private void txtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            m_changeCurrentPageManual = true;
        }

        //前一页
        private void tsbPreviousPage_Click(object sender, EventArgs e)
        {
            m_currentPage -= 1;
            ChangeData();
            ChangeItem();
        }

        //首页
        private void tsbFirstPage_Click(object sender, EventArgs e)
        {
            m_currentPage = 1;
            ChangeData();
            ChangeItem();
        }

        //下一页
        private void tsbNextPage_Click(object sender, EventArgs e)
        {
            m_currentPage += 1;
            ChangeData();
            ChangeItem();
        }

        //末页
        private void tsbLastPage_Click(object sender, EventArgs e)
        {
            m_currentPage = m_pageCount;
            ChangeData();
            ChangeItem();
        }

        /// <summary>
        /// 刷新控件的显示（设置各属性项后调用）
        /// </summary>
        public void UpdateDiaplay()
        {
            m_currentPage = 1;    //当前页数从1开始
            m_pageCount = GetPageCount(m_recordCount, m_recordPerPage);
            ChangeData();
        }

        /// <summary>
        /// 设置控件的显示效果
        /// </summary>
        private void SetControls()
        {
            this.tslAllRecordCount.Text = m_recordCount.ToString();
            this.txtCurrentPage.Text = m_currentPage.ToString();
            m_changeCurrentPageManual = false;
            this.tslPageCount.Text = m_pageCount.ToString();
            this.tslAllpage.Text = this.tslPageCount.Text;
            int currentPageRecordCount = m_currentPageEndIndex - m_currentPageStartIndex + 1;
            this.tslCurrentPageRecordCount.Text = currentPageRecordCount.ToString();

            if (this.tslAllpage.Text.Equals(this.txtCurrentPage.Text))
            {
                this.tsbLastPage.Enabled = false;
                this.tsbNextPage.Enabled = false;
                if (this.tslAllpage.Text.Equals("1"))
                {
                    this.tsbPreviousPage.Enabled = false;
                    this.tsbFirstPage.Enabled = false;
                }
                else
                {
                    this.tsbPreviousPage.Enabled = true;
                    this.tsbFirstPage.Enabled = true;
                }
            }
            else
            {
                this.tsbLastPage.Enabled = true;
                this.tsbNextPage.Enabled = true;
                if (this.txtCurrentPage.Text.Equals("1"))
                {
                    this.tsbPreviousPage.Enabled = false;
                    this.tsbFirstPage.Enabled = false;
                }
                else
                {
                    this.tsbPreviousPage.Enabled = true;
                    this.tsbFirstPage.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 根据总记录数和每页记录数获取页数
        /// </summary>
        /// <param name="recordCount">总记录数</param>
        /// <param name="recordPerPage">每页记录数</param>
        /// <returns></returns>
        private int GetPageCount(int recordCount, int recordPerPage)
        {
            int pageCount = (recordCount / recordPerPage);    //计算出总页数
            if ((recordCount % recordPerPage) > 0) pageCount++;
            if (pageCount < 1)
            {
                pageCount = 1;
            }
            return pageCount;
        }

        /// <summary>
        /// 更新本页起始和终止记录的序号
        /// </summary>
        private void UpdateRecordStartAndEndIndex()
        {
            m_currentPageStartIndex = (m_currentPage - 1) * m_recordPerPage;
            if (m_currentPage == m_pageCount)
            {
                m_currentPageEndIndex = m_recordCount - 1;
            }
            else
            {
                m_currentPageEndIndex = m_currentPage * m_recordPerPage - 1;
            }

            if (m_currentPageStartIndex < 0)
            {
                m_currentPageStartIndex = 0;
            }
            if (m_currentPageEndIndex < 0)
            {
                m_currentPageEndIndex = 0;
            }
        }

        /// <summary>
        /// 改变数据
        /// </summary>
        private void ChangeData()
        {
            UpdateRecordStartAndEndIndex();
            if (null != NeedDataDiaplayChanged)
            {
                NeedDataDiaplayChanged(m_strSQL, m_currentPageStartIndex, m_recordPerPage);
            }
           SetControls();
        }

        private void ChangeItem()
        {
            UpdateRecordStartAndEndIndex();
            if (null != NeedItemDisplayChanged)
            {
                NeedItemDisplayChanged(m_currentPageStartIndex, m_currentPageEndIndex);
            }
            SetControls();
        }
    }

    /// <summary>
    /// 数据改变事件句柄
    /// </summary>
    /// <param name="startRecordIndex">起始序号</param>
    /// <param name="endRecordIndex">终止序号</param>
    public delegate void DataChangedEventHandler(string strSQL,int startRecordIndex,int endRecordIndex);
    public delegate void ItemChangedEventHandler(int startRecordIndex,int endRecordIndex);
}
