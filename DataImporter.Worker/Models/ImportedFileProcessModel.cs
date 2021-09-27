using DataImporter.Common.DateTimeUtilities;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Worker.Models
{
    public class ImportedFileProcessModel : IImportedFileProcessModel
    {
        private IImportedFileService _importedFileService;
        private IAllDataService _allDataService;
        private IDateTimeUtility _dateTimeUtility;

        public ImportedFileProcessModel(IImportedFileService importedFileService, IAllDataService allDataService,
            IDateTimeUtility dateTimeUtility)
        {
            _importedFileService = importedFileService;
            _dateTimeUtility = dateTimeUtility;
            _allDataService = allDataService;
        }

        public void ImportFileInfo()
        {
            //Console.WriteLine("check");
            List<List<string>> ExcelData = new List<List<string>>();
            string FilePath = $"G:/AspDotNetExam-Assignment/DataImporter/DataImporter/wwwroot/confirmfiles";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DirectoryInfo d = new DirectoryInfo(FilePath);
            
            FileInfo[] existingFile = d.GetFiles("*.xlsx");

            if(existingFile.Length != 0)
            {
                foreach (FileInfo file in existingFile)
                {
                    string name = file.Name;

                    //importing data to ExcelData list start
                    string srcFile = file.Directory + "\\" + name;
                    FileInfo exFile = new FileInfo(srcFile);

                    using (ExcelPackage package = new ExcelPackage(exFile))
                    {
                        //get the first worksheet in the workbook
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        int colCount = worksheet.Dimension.End.Column;  //get Column Count
                        int rowCount = worksheet.Dimension.End.Row;     //get row count
                        for (int row = 1; row <= rowCount; row++)
                        {
                            List<string> lst = new List<string>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                lst.Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                            }
                            ExcelData.Add(lst);
                        }
                    }
                    //importing data to ExcelData list end


                    //updating status to ImportedFiles table start
                    var colName = "";
                    if(ExcelData.Count > 0)
                    {
                        for (int i = 0; i < ExcelData[0].Count; i++)
                        {
                            if (i != ExcelData[0].Count - 1)
                                colName += (ExcelData[0][i] + ">");
                            else
                                colName += ExcelData[0][i];
                        }
                    }
                    
                    var data = name.Split('_');
                    var fileName = data[0];
                    var userId = Guid.Parse(data[1]);
                    var GrpName = data[2];
                    var GrpId = data[3].Split('.');
                    var importFileBO = new ImportedFileBO
                    {
                        FileName = fileName,
                        UserId = userId,
                        GroupId = int.Parse(GrpId[0]),
                        GroupName = GrpName,
                    };

                    //retriving file Id from database table
                    int fileId = _importedFileService.GetFile(importFileBO);

                    _importedFileService.UpdateProcessingStatus(fileId, colName);
                    //updating status to ImportedFiles table end


                    ////importing data to ExcelData list start
                    //string srcFile = file.Directory + "\\" + name;
                    //FileInfo exFile = new FileInfo(srcFile);

                    //using (ExcelPackage package = new ExcelPackage(exFile))
                    //{
                    //    //get the first worksheet in the workbook
                    //    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    //    int colCount = worksheet.Dimension.End.Column;  //get Column Count
                    //    int rowCount = worksheet.Dimension.End.Row;     //get row count
                    //    for (int row = 1; row <= rowCount; row++)
                    //    {
                    //        List<string> lst = new List<string>();
                    //        for (int col = 1; col <= colCount; col++)
                    //        {
                    //            lst.Add(worksheet.Cells[row, col].Value?.ToString().Trim());
                    //        }
                    //        ExcelData.Add(lst);
                    //    }
                    //}
                    ////importing data to ExcelData list end


                    //calling below method for inserting data in AllData table
                    ImportDataInAllDataTable(ExcelData, fileId, userId, int.Parse(GrpId[0]), fileName);


                    //updating file status
                    _importedFileService.UpdateStatus(fileId);


                    //deleting file from the directory
                    if (System.IO.File.Exists(srcFile))
                        System.IO.File.Delete(srcFile);

                    //Console.WriteLine("file Id = {0}", fileId);
                }
            }       
        }

        public void ImportDataInAllDataTable(List<List<string>> ExcelData, int fileId, Guid userId, int GrpId, string fileName)
        {
            List<AllDataBO> allDataBO = new List<AllDataBO>();
            var row = ExcelData.Count;
            var col = ExcelData[0].Count;
            string colName = "";

            for(int i=0; i< col; i++)
            {
                if (i != col - 1)
                    colName += (ExcelData[0][i] + ">");
                else
                    colName += ExcelData[0][i];
            }

            for(int i=1; i<row; i++)
            {
                var colVal = "";
                for(int j=0; j<col; j++)
                {
                    if (j != col - 1)
                        colVal += (ExcelData[i][j] + ">");
                    else
                        colVal += ExcelData[i][j];
                }
                var allData = new AllDataBO
                {
                    UserId = userId,
                    GroupId = GrpId,
                    FileId = fileId,
                    FileName = fileName,
                    FileImportDate = _dateTimeUtility.Now,
                    KeyForColumnName = colName,
                    ValueForColumnValue = colVal
                };

                allDataBO.Add(allData);
            }

            _allDataService.CreateAllData(allDataBO);
        }
    }
}
