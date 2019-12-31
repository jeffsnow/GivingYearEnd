using MergeChurchExcel.Helpers;
using MergeChurchExcel.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeChurchExcel.Services
{
    internal class SaveAllToExcel
    {
        private DataTable _dataTable;

        public SaveAllToExcel(IEnumerable<Transactions> models)
        {
            _dataTable = DataTableFromEnumeration.ToDataTable(models);
        }

        public void Run(string fileLocation)
        {
            FileInfo file = new FileInfo(fileLocation);
            using (ExcelPackage pck = new ExcelPackage(file))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                ws.Cells["A1"].LoadFromDataTable(_dataTable, true);
                pck.Save();
            }
        }
    }
}