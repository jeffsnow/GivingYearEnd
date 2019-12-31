using MergeChurchExcel.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace MergeChurchExcel.Services
{
    internal class GetAllModels
    {
        private string _folderAddress;

        public GetAllModels(string folderAddress)
        {
            _folderAddress = folderAddress;
        }

        public IEnumerable<Transactions> Models
        {
            get {
                var list = new List<Transactions>();
                //Get all files in folder
                var files = Directory.GetFiles(_folderAddress);
                //Enumerate through each file and extract models
                foreach (var file in files)
                {
                    list.AddRange(new GetSingleWeek(file).Models);
                }

                return list;
            }
        }
    }
}