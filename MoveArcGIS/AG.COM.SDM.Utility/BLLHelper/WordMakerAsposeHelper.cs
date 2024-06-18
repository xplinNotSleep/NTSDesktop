using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

namespace AG.COM.SDM.Utility
{
    public class WordMakerAsposeHelper
    {
        private Document m_Document = null;

        public Document Document
        {
            get { return m_Document; }
            set { m_Document = value; }
        }

        /// <summary>
        /// 通过模板创建新文档
        /// </summary>
        /// <param name="filePath"></param>
        public void CreateNewDocument(string filePath)
        {
            m_Document = new Document(filePath);
        }

        /// <summary>
        /// 创建空文档
        /// </summary>
        public void CreateNewEmptyDocument()
        {
            m_Document = new Document();
        }

        /// <summary>
        /// 在书签处插入值
        /// </summary>
        /// <param name="bookmarkName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InsertValue(string bookmarkName, string value)
        {
            value = value == null ? "" : value;

            Bookmark bookmark = m_Document.Range.Bookmarks.Cast<Bookmark>().FirstOrDefault(t => t.Name == bookmarkName);

            if (bookmark != null)
            {
                bookmark.Text = value;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="bookmarkName"></param>
        /// <param name="picturePath"></param>
        /// <param name="width"></param>
        /// <param name="hight"></param>
        public void InsertPicture(string bookmarkName, string picturePath, float width, float hight)
        {
            DocumentBuilder build = new DocumentBuilder(m_Document);

            build.MoveToBookmark(bookmarkName);
            Shape shape = build.InsertImage(picturePath);
            //这里赋值单位是磅，需要换算
            double widthPt, highPt;
            widthPt = width * 0.75;
            highPt = hight * 0.75;

            shape.Width = widthPt;
            shape.Height = highPt;
        }

        /// <summary>
        /// 插入表格的行并写入值
        /// </summary>
        /// <param name="dt"></param>
        public void InsertTableValue(DataTable dt)
        {
            InsertTableValue(dt, 0);
        }

        /// <summary>
        /// 插入表格的行并写入值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableIndex">表的索引（在word文档里的序号）</param>
        public void InsertTableValue(DataTable dt, int tableIndex)
        {
            DocumentBuilder builder = new DocumentBuilder(m_Document);

            Table table = (Table)m_Document.GetChild(NodeType.Table, tableIndex, true);

            //比DataTable行数少一行，因为预定第一行本身存在
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                Row clonedRow = (Row)table.LastRow.Clone(true);

                //// Remove all content from the cloned row's cells. This makes the row ready for
                //// new content to be inserted into.
                //foreach (Cell cell in clonedRow.Cells)
                //    cell.RemoveAllChildren();

                // Add the row to the end of the table.
                table.AppendChild(clonedRow);
            }

            int workTableRowCount = table.Rows.Count;

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    builder.MoveToCell(tableIndex, workTableRowCount - dt.Rows.Count + i, j, 0); //移动单元格

                    builder.Write(dt.Rows[i][j].ToString());
                }
            }
        }

        /// <summary>
        /// 合并格子
        /// </summary>
        /// <param name="cellRowIdxStart"></param>
        /// <param name="cellColIdxStart"></param>
        /// <param name="cellRowIdxEnd"></param>
        /// <param name="cellColIdxEnd"></param>
        public void MergeCells(int cellRowIdxStart, int cellColIdxStart, int cellRowIdxEnd, int cellColIdxEnd)
        {
            MergeCells(cellRowIdxStart, cellColIdxStart, cellRowIdxEnd, cellColIdxEnd, 0);
        }

        /// <summary>
        /// 合并格子
        /// </summary>
        /// <param name="cellRowIdxStart"></param>
        /// <param name="cellColIdxStart"></param>
        /// <param name="cellRowIdxEnd"></param>
        /// <param name="cellColIdxEnd"></param>
        /// <param name="tableIndex">表的索引（在word文档里的序号）</param>
        public void MergeCells(int cellRowIdxStart, int cellColIdxStart, int cellRowIdxEnd, int cellColIdxEnd, int tableIndex)
        {
            Table table = (Table)m_Document.GetChild(NodeType.Table, tableIndex, true);

            Cell startCell = table.Rows[cellRowIdxStart].Cells[cellColIdxStart];
            Cell endCell = table.Rows[cellRowIdxEnd].Cells[cellColIdxEnd];

            Table parentTable = startCell.ParentRow.ParentTable;

            // Find the row and cell indices for the start and end cell.
            Point startCellPos = new Point(startCell.ParentRow.IndexOf(startCell), parentTable.IndexOf(startCell.ParentRow));
            Point endCellPos = new Point(endCell.ParentRow.IndexOf(endCell), parentTable.IndexOf(endCell.ParentRow));
            // Create the range of cells to be merged based off these indices. Inverse each index if the end cell if before the start cell. 
            Rectangle mergeRange = new Rectangle(Math.Min(startCellPos.X, endCellPos.X), Math.Min(startCellPos.Y, endCellPos.Y),
                Math.Abs(endCellPos.X - startCellPos.X) + 1, Math.Abs(endCellPos.Y - startCellPos.Y) + 1);

            foreach (Row row in parentTable.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    Point currentPos = new Point(row.IndexOf(cell), parentTable.IndexOf(row));

                    // Check if the current cell is inside our merge range then merge it.
                    if (mergeRange.Contains(currentPos))
                    {
                        if (currentPos.X == mergeRange.X)
                            cell.CellFormat.HorizontalMerge = CellMerge.First;
                        else
                            cell.CellFormat.HorizontalMerge = CellMerge.Previous;

                        if (currentPos.Y == mergeRange.Y)
                            cell.CellFormat.VerticalMerge = CellMerge.First;
                        else
                            cell.CellFormat.VerticalMerge = CellMerge.Previous;
                    }
                }
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveDocument(string filePath)
        {
            m_Document.Save(filePath);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="saveFormat">40=Pdf,10=doc,20=docx</param>
        public void SaveDocument(string filePath, int saveFormat)
        {
            m_Document.Save(filePath, (SaveFormat)saveFormat);
        }

        /// <summary>
        /// 合并word文档
        /// </summary>
        /// <param name="document"></param>
        /// <param name="importFormatMode">UseDestinationStyles = 0, KeepSourceFormatting = 1</param>
        public void AppendDocument(WordMakerAsposeHelper wordMakerAspose, int importFormatMode)
        {
            Document document = wordMakerAspose.Document;

            m_Document.AppendDocument(document, (ImportFormatMode)importFormatMode);
        }
    }
}
