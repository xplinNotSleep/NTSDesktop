using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AG.COM.SDM.Utility.Controls
{
    /// <summary>
    /// 表达式查询 用户控件
    /// </summary>
    public partial class ExpressionConstructor : UserControl
    {
        private Dictionary<string, IField> m_DictOfFields = new Dictionary<string, IField>();
        private IFeatureLayer m_FeatureLayer;
        /// <summary>
        /// 获取或设置要素层
        /// </summary>
        public IFeatureLayer FeatureLayer
        {
            get
            {
                return m_FeatureLayer;
            }
            set
            {
                m_FeatureLayer = value;

                if (m_FeatureLayer != null)
                {
                    //设置通配符
                    IDataset tDataset = this.m_FeatureLayer.FeatureClass as IDataset;
                    if (tDataset.Workspace.IsDirectory() == true)
                    {
                        this.btnPercentage.Text = "%";
                    }
                    else
                    {
                        this.btnPercentage.Text = "*";
                    }
                }
            }
        }

        private string m_SqlExpression;
        /// <summary>
        /// 获取或设置查询表达式
        /// </summary>
        public string SqlExpression
        {
            get { return this.txtExpress.Text; }
            set { m_SqlExpression = value; }
        }  

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ExpressionConstructor()
        {
            //初始化界面组件
            InitializeComponent();
        }

        public override void Refresh()
        {
            InitialControls();
        }

        private void InitialControls()
        {
            //设置默认SqlExpression
            this.txtExpress.Text = m_SqlExpression;
            //设置字段列表
            //获取对应字段集
            IFields tFields = m_FeatureLayer.FeatureClass.Fields;
            //清除原有字段集
            this.m_DictOfFields.Clear();
            //建立字段名称与字段的对应关系
            for (int i = 0; i < tFields.FieldCount; i++)
            {
                IField tField = tFields.get_Field(i);
                switch (tField.Type)
                {
                    case esriFieldType.esriFieldTypeBlob:
                    case esriFieldType.esriFieldTypeGeometry:
                    case esriFieldType.esriFieldTypeRaster:
                        break;
                    default:
						if (tField.Name != "SHAPE" &&
							tField.Name != "OBJECTID" &&
							tField.Name != "STARTTIME" &&
							tField.Name != "ENDTIME" &&
							tField.Name != "SHAPE.LEN" &&
							tField.Name != "ROLECONTAINS")
						{
							this.m_DictOfFields.Add(tField.AliasName, tField);
						}

                        break;
                }
            }

            //得到所有字段的关键字集合
            string[] strFields = new string[this.m_DictOfFields.Count];
            this.m_DictOfFields.Keys.CopyTo(strFields, 0);

            //绑定数据源
            this.listFields.DataSource = strFields;

            //移除所有值项
            this.listValues.Items.Clear();
            this.listValues.Enabled = false;
        }

        private void btnGetUnique_Click(object sender, EventArgs e)
        {
            //得到当前操作的FeatureClass
            IFeatureClass tFeatureClass = m_FeatureLayer.FeatureClass;
            string strField = this.listFields.SelectedItem as string;

            //将当前要素类字段的唯一值作为listValues数据源
			this.listValues.DataSource = LibGeoTable.GetUniqueValueByDataStat(tFeatureClass, this.m_DictOfFields[strField].Name);
            this.listValues.Enabled = true;
        } 

        private void listValues_DoubleClick(object sender, EventArgs e)
        {
            if (this.listValues.SelectedItem != null)
            {
                string strFieldValue = this.listValues.SelectedItem as string;
                string strFieldName = this.listFields.SelectedItem as string;
                if (this.m_DictOfFields[strFieldName].Type == esriFieldType.esriFieldTypeString)
                {
                    strFieldValue = string.Format("'{0}'", strFieldValue);
                }

                //设置Sql表达式
                AddToSqlExpress(strFieldValue);                
            }
        }

        private void listFields_DoubleClick(object sender, EventArgs e)
        {
            if (this.listFields.SelectedItem != null)
            {
                string strFieldName = this.listFields.SelectedItem as string;
                //设置Sql表达式
				AddToSqlExpress(this.m_DictOfFields[strFieldName].Name);                
            }
        }

        private void listFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listValues.DataSource = null; 
            this.listValues.Enabled = false;
        }

        private void listValues_EnabledChanged(object sender, EventArgs e)
        {
            if (this.listValues.Enabled == true)
            {
                this.listValues.BackColor = System.Drawing.SystemColors.Window;
            }
            else
                this.listValues.BackColor = System.Drawing.SystemColors.Control;
        }  

        /// <summary>
        /// 设置SQL表达式内容
        /// </summary>
        /// <param name="sqlexpress">追加的内容</param>
        private void AddToSqlExpress(string sqlexpress)
        {
            this.txtExpress.Focus();

            int startIndex = this.txtExpress.SelectionStart;
            string strPrev = this.txtExpress.Text.Substring(0, startIndex);
            string strNext = this.txtExpress.Text.Substring(startIndex);

            this.txtExpress.Text = string.Format("{0} {1} {2}", strPrev, sqlexpress, strNext).Trim();

            if (string.Equals(sqlexpress, this.btnBrackets.Text))
                this.txtExpress.SelectionStart = string.Format("{0} {1}",strPrev,sqlexpress).Length- 1;
            else
                this.txtExpress.SelectionStart = string.Format("{0} {1}",strPrev,sqlexpress).Length;
        }

        private void btnOperationSymbol_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //设置SQL表达式内空
            AddToSqlExpress(btn.Text);
        }

        private void txtExpress_TextChanged(object sender, EventArgs e)
        {
            if (SqlTextChanged != null)
                SqlTextChanged(sender, e);
        }

        public event EventHandler SqlTextChanged = null;
    }
}
