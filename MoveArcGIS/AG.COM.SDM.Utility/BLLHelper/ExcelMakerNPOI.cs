using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace AG.COM.SDM.Utility
{
    /// <summary>
    /// NPOI 读取Excel
    /// </summary>
    public class ExcelMakerNPOI
    {
        #region 字段
        private IWorkbook workbook = null;//工作薄
        private ISheet currentSheet = null;//当前工作表
        private string fullpath = string.Empty;//Excel文件路径
        #endregion

        #region 属性
        /// <summary>
        /// 工作薄
        /// </summary>
        public IWorkbook Workbook
        {
            get { return workbook; }
            set { workbook = value; }
        }

        /// <summary>
        /// 工作表
        /// </summary>
        public ISheet CurrentSheet
        {
            get { return currentSheet; }
            set { currentSheet = value; }
        }

        /// <summary>
        /// 工作表数组
        /// </summary>
        public ISheet[] Sheets
        {
            get
            {
                if (workbook != null)
                {
                    return GetSheets(workbook);
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public ExcelMakerNPOI()
        {

        }

        /// <summary>
        /// 构造函数，新建空Excel
        /// </summary>
        /// <param name="excelFormat">Excel文档格式</param>
        public ExcelMakerNPOI(ExcelFormat excelFormat)
        {
            //根据Excel文件格式，创建工作薄
            if (ExcelFormat.XLS == excelFormat)
            {
                workbook = new HSSFWorkbook();
            }
            if (ExcelFormat.XLSX == excelFormat)
            {
                workbook = new XSSFWorkbook();
            }
        }

        /// <summary>
        /// 打开Excel文件,默认获取第一张表
        /// </summary>
        /// <param name="filepath">Excel文件路径</param>
        /// <returns></returns>
        public void Open(string filepath)
        {
            if (!File.Exists(filepath)) return;

            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook = WorkbookFactory.Create(fs);
                currentSheet = workbook.GetSheetAt(0);
                fs.Dispose();
            }
        }

        /// <summary>
        /// 保存Excel文件
        /// </summary>
        /// <param name="savepath">Excel保存路径</param>
        /// <returns>true:保存成功；false：保存失败</returns>
        public bool Save(string savepath)
        {
            if (string.IsNullOrEmpty(savepath)) return false;

            try
            {
                using (FileStream fs = new FileStream(savepath, FileMode.OpenOrCreate))
                {
                    (workbook).Write(fs);
                    fs.Dispose();
                }
                return true;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 单元格格式
        /// </summary>
        /// <param name="cellStyle"></param>
        /// <returns></returns>
        public virtual void CommonCellType(ref ICellStyle cellStyle)
        {
            #region 对齐
            cellStyle.Alignment = HorizontalAlignment.Center;//水平对齐
            cellStyle.VerticalAlignment = VerticalAlignment.Top;//垂直对齐，靠上top实为居中
            /*NPOI 2.0.6.0垂直对齐方式有误：
              Top=Center
              Center=Bottom
              Bottom=Distributed
              Distributed=Bottom(设为此值，用Office打开无法正常显示)
             */

            //缩进，需要与水平对齐以前使用，且水平对齐方式只能选择靠左Left、靠右Right、分散对齐Justify三者之一
            //cellStyle.Indention = 1;

            //自动换行
            //cellStyle.WrapText = true;

            //文本旋转
            //cellStyle.Rotation = (short)45;//旋转角度-90 到 90.

            //文字竖排
            //未找到方法

            #endregion

            #region 字体
            //IFont font = (workbook).CreateFont();
            //font.FontHeight = 5 * 20;//字体大小，即字号
            ////font.FontHeightInPoints = 5;
            //font.Boldweight = (short)5;//加粗厚度
            //font.Charset = (short)5;
            //font.Color = HSSFColor.Red.Index;//颜色
            //font.FontName = "宋体";//字体样式
            //font.IsItalic = true;//倾斜
            //font.IsStrikeout = true;//删除线？
            //font.Underline = FontUnderlineType.Single;//下划线
            ////上标
            ////小标
            //font.TypeOffset = FontSuperScript.Super;
            //cellStyle.SetFont(font);
            #endregion

            #region 边框
            //样式
            cellStyle.BorderBottom = BorderStyle.Thin;
            cellStyle.BorderLeft = BorderStyle.Thin;
            cellStyle.BorderRight = BorderStyle.Thin;
            cellStyle.BorderTop = BorderStyle.Thin;
            //颜色,默认为黑色
            //cellStyle.BottomBorderColor = HSSFColor.LightBlue.Index;
            //cellStyle.LeftBorderColor = HSSFColor.LightBlue.Index;
            //cellStyle.RightBorderColor = HSSFColor.LightBlue.Index;
            //cellStyle.TopBorderColor = HSSFColor.LightBlue.Index; 
            //对角线
            //cellStyle.BorderDiagonal = BorderDiagonal.Both;
            //cellStyle.BorderDiagonalColor = HSSFColor.Red.Index;
            //cellStyle.BorderDiagonalLineStyle = BorderStyle.DashDot;
            #endregion

            #region 图案
            //图案样式
            //cellStyle.FillPattern = FillPattern.Diamonds;

            //图案颜色
            //cellStyle.FillForegroundColor = HSSFColor.Black.Index;

            //单元格背景
            //cellStyle.FillBackgroundColor = HSSFColor.Black.Index;
            #endregion

            #region 保护
            //cellStyle.IsLocked = true;//锁定
            //cellStyle.IsHidden = true;//隐藏
            #endregion

            //数据格式
        }

        /// <summary>
        /// 单元格赋值
        /// </summary>
        /// <param name="rowIdx">行号</param>
        /// <param name="colIdx">列号</param>
        /// <param name="value">值</param>
        public virtual void SetCellValue(int rowIdx, int colIdx, object value)
        {
            //获取行，若不存在，创建行
            IRow row = currentSheet.GetRow(rowIdx);
            if (row == null)
                row = currentSheet.CreateRow(rowIdx);

            //获取单元格，若不存在，创建单元格
            ICell cell = row.GetCell(colIdx);
            if (cell == null)
                cell = row.CreateCell(colIdx);

            //赋值
            switch (value.GetType().Name)
            {
                case "String":
                    cell.SetCellValue(Convert.ToString(value));
                    break;
                case "DateTime":
                    cell.SetCellValue(Convert.ToDateTime(value));
                    break;
                case "Bool":
                    cell.SetCellValue(Convert.ToBoolean(value));
                    break;
                case "Double":
                    cell.SetCellValue(Convert.ToDouble(value));
                    break;
            }
            //单元格格式
            ICellStyle cellStyle = workbook.CreateCellStyle();
            CommonCellType(ref cellStyle);
            cell.CellStyle = cellStyle;
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="rowStartIdx">起始行号</param>
        /// <param name="colStartIdx">起始列号</param>
        /// <param name="rowEndIdx">终点行号</param>
        /// <param name="colEndIdx">终点列号</param>
        public virtual void MergeCell(int rowStartIdx, int colStartIdx, int rowEndIdx, int colEndIdx)
        {
            //合并格子索引重新调整
            if (rowEndIdx > rowStartIdx)
                rowEndIdx = rowEndIdx - 1;
            if (colEndIdx > colStartIdx)
                colEndIdx = colEndIdx - 1;

            CellRangeAddress range = new CellRangeAddress(rowStartIdx, rowEndIdx, colStartIdx, colEndIdx);
            currentSheet.AddMergedRegion(range);
        }

        /// <summary>
        /// 获取工作薄的所有工作表
        /// </summary>
        /// <param name="workbook">工作薄</param>
        /// <returns>工作表Sheet数组</returns>
        public ISheet[] GetSheets(IWorkbook workbook)
        {
            if (workbook == null) return null;

            IList<ISheet> list = null;
            int numofsheets = workbook.NumberOfSheets;
            for (int i = 0; i < numofsheets; i++)
            {
                list.Add(workbook.GetSheetAt(i));
            }
            return list.ToArray();
        }

        /// <summary>
        /// 将DataTable中的数据填充中Excel中
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowStartIdx">行号</param>
        /// <param name="colStartIdx">列号</param>
        public virtual bool WriteDataTableToExcel(DataTable dt, int rowStartIdx, int colStartIdx)
        {
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    for (int n = 0; n < dt.Columns.Count; n++)
                    {
                        SetCellValue(i + rowStartIdx, n + colStartIdx, Convert.ToString(dr[n]));
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                AG.COM.SDM.Utility.Logger.ExceptionLog.LogError(ex.Message, ex);
                return false;
            }
        }

        /// <summary>
        /// 读取Excel数据到DataTable中
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowStartIdx">起始行号</param>
        /// <param name="colStartIdx">起始列号</param>
        /// <param name="rowEndIdx">终点行号</param>
        /// <param name="colEndIdx">终点列号</param>
        /// <returns></returns>
        public virtual DataTable ReadExcelToDataTable(ISheet sheet, int rowStartIdx, int colStartIdx, int rowEndIdx, int colEndIdx)
        {
            if (sheet == null) return null;

            DataTable dt = new DataTable(sheet.SheetName);
            for (int i = rowStartIdx; i <= rowEndIdx; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = colStartIdx; j < colEndIdx; j++)
                {
                    dr[j] = GetCellValue(sheet, i, j);
                }
            }

            return dt;
        }

        /// <summary>
        /// 获取单元格的值
        /// </summary>
        /// <param name="sheet">工作表</param>
        /// <param name="rowIdx">行号</param>
        /// <param name="colIdx">列号</param>
        /// <returns></returns>
        public virtual object GetCellValue(ISheet sheet, int rowIdx, int colIdx)
        {
            object obj = null;
            ICell cell = sheet.GetRow(rowIdx).GetCell(colIdx);
            if (cell == null) obj = "";

            obj = cell.StringCellValue;
            switch (cell.GetType().Name)
            {
                case "Boolean":
                    obj = cell.BooleanCellValue;
                    break;
                case "DateTime":
                    obj = cell.DateCellValue;
                    break;
                case "Double":
                    obj = cell.NumericCellValue;
                    break;
            }
            return obj;
        }

        /// <summary>
        /// 判断文件是否存在及是否是Excle文件
        /// </summary>
        /// <param name="filepath">Excel文件路径</param>
        /// <returns>True：正确；false：错误</returns>
        private bool IsExcelFile(string filepath)
        {
            if (File.Exists(filepath)) return false;

            string extension = System.IO.Path.GetExtension(filepath);
            if (extension.ToUpper() == ".XLS" || extension.ToUpper() == ".XLSX") return true;
            else return false;
        }

        #endregion
    }

    /// <summary>
    /// Excel文件格式枚举
    /// </summary>
    public enum ExcelFormat
    {
        /// <summary>
        /// Excel 2003
        /// </summary>
        XLS = 0,
        /// <summary>
        /// Excel 2007
        /// </summary>
        XLSX = 1
    }
}
