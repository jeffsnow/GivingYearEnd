﻿using MergeChurchExcel.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeChurchExcel
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Read Excel Spreadsheet
            Console.WriteLine("Reading Files");
            var files = new GetAllModels(Addresses.FolderAddress);
            var models = files.Models;
            Console.WriteLine("Writing Individual Files");
            //Write Excel Spreadsheet
            new GroupLikePeople(models);
            Console.WriteLine("Writing Master File");
            var newExcel = new SaveAllToExcel(models);
            newExcel.Run(GetFileName());
        }

        private static string GetFileName()
        {
            return Path.Combine(Addresses.WriteFolder, "ExcelMaster.xlsx");
        }
    }
}