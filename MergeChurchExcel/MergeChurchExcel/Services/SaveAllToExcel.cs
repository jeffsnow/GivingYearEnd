using MergeChurchExcel.Helpers;
using MergeChurchExcel.Model;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;

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
            DeleteIfExist(fileLocation);
            FileInfo file = new FileInfo(fileLocation);
            using (ExcelPackage pck = new ExcelPackage(file))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                ws.Cells["A1"].LoadFromDataTable(_dataTable, true);
                pck.Save();
            }
        }

        private void DeleteIfExist(string fileLocation)
        {
            if (File.Exists(fileLocation))
            { File.Delete(fileLocation); }
        }
    }
}