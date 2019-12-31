using MergeChurchExcel.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace MergeChurchExcel.Services
{
    internal class GetSingleWeek
    {
        private string _fileName;

        public GetSingleWeek(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerable<Transactions> Models
        {
            get {
                return GetModels();
            }
        }

        private IEnumerable<Transactions> GetModels()
        {
            var models = new List<Transactions>();
            var fileInfo = new FileInfo(_fileName);
            using (var package = new ExcelPackage(fileInfo))
            {
                var sheet = package.Workbook.Worksheets["Giving"];
                var valuesExist = true;
                var startLine = 2;
                while (valuesExist)
                {
                    var checkNumber = GetInt(GetString(sheet, startLine, 4));    //row then column
                    var person = GetString(sheet, startLine, 7);
                    var date = GetDate(GetInt(GetString(sheet, startLine, 6)));
                    var category = GetString(sheet, startLine, 9);
                    var cash = GetDecimal(GetString(sheet, startLine, 12));
                    var check = GetDecimal(GetString(sheet, startLine, 11));
                    if (person == string.Empty)
                    {
                        valuesExist = false;
                        continue;
                    }
                    var model = new Transactions
                    {
                        CheckNumber = checkNumber,
                        Person = person,
                        Date = date,
                        Category = category,
                        CashAmount = cash,
                        CheckAmount = check
                    };
                    startLine++; //gets next row
                    models.Add(model);
                }
            }

            return models;
        }

        private string GetString(ExcelWorksheet sheet, int row, int col)
        {
            object objValue = sheet.Cells[row, col].Value ?? string.Empty;
            return objValue.ToString();
        }

        private decimal GetDecimal(string v = null)
        {
            if (v == null) { return 0m; }
            decimal.TryParse(v, out decimal amount);
            return amount;
        }

        private DateTime GetDate(int days)
        {
            var date = new DateTime(1899, 12, 31).AddDays(days);
            return date;
        }

        private int GetInt(string v)
        {
            int.TryParse(v, out int checkNum);
            return checkNum;
        }
    }
}