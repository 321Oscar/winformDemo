using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class AsposeExcel
    {
        private string outFileName = "";
        private string fullFilename = "";
        private Workbook book = null;
        private Worksheet sheet = null;

        /// <summary>
        /// 导出构造函数
        /// </summary>
        /// <param name="outfilename">导出路径</param>
        /// <param name="tempfilename">导出名称</param>
        public AsposeExcel(string outfilename, string tempfilename) //
        {
            outFileName = outfilename;
            book = new Workbook();
            // book.Open(tempfilename);这里我们暂时不用模板 
            sheet = book.Worksheets[0];
        }
        /// <summary>
        /// 导入构造数 
        /// </summary>
        /// <param name="fullfilename">文件路径</param>
        public AsposeExcel(string fullfilename)
        {
            fullFilename = fullfilename;
            book = new Workbook();
            // book.Open(tempfilename); 
            sheet = book.Worksheets[0];
        }
        private void AddTitle(string title, int columnCount)
        {
            sheet.Cells.Merge(0, 0, 1, columnCount);
            sheet.Cells.Merge(1, 0, 1, columnCount);
            Cell cell1 = sheet.Cells[0, 0];
            cell1.PutValue(title);
            //cell1.Style.HorizontalAlignment = TextAlignmentType.Center;
            //cell1.Style.Font.Name = "黑体";
            //cell1.Style.Font.Size = 14;
            //cell1.Style.Font.IsBold = true;
            Cell cell2 = sheet.Cells[1, 0];
            cell1.PutValue("查询时间：" + DateTime.Now.ToLocalTime());
            //cell2.SetStyle(cell1.Style);
        }
        private void AddHeader(DataTable dt)
        {
            Cell cell = null;
            for (int col = 0; col < dt.Columns.Count; col++)
            {
                cell = sheet.Cells[0, col];
                cell.PutValue(dt.Columns[col].ColumnName);
                //cell.Style.Font.IsBold = true;
            }
        }
        private void AddBody(DataTable dt)
        {
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    sheet.Cells[r + 1, c].PutValue(dt.Rows[r][c].ToString());
                }
            }
        }

        //导出------------下一篇会用到这个方法 
        public Boolean DatatableToExcel(DataTable dt)
        {
            Boolean yn = false;
            try
            {
                //sheet.Name = sheetName;
                //AddTitle(title, dt.Columns.Count);
                AddHeader(dt);
                AddBody(dt);
                sheet.AutoFitColumns();
                //sheet.AutoFitRows(); 
                book.Save(outFileName);
                yn = true;
                return yn;
            }
            catch (Exception e)
            {
                return yn;
                // throw e; 
            }
        }
        public DataTable ExcelToDatatalbe()//导入 
        {
            Workbook book = new Workbook(fullFilename);
            //book.Open();
            WorkbookDesigner designer = new WorkbookDesigner(book);
            Worksheet sheet = book.Worksheets[0];
            Cells cells = sheet.Cells;
            //获取excel中的数据保存到一个datatable中 
            DataTable dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);
            // dt_Import. 
            return dt_Import;
           // return null;
        }


        
    }

}
