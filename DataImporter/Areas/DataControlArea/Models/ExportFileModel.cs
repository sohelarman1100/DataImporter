using Autofac;
using DataImporter.Common.DateTimeUtilities;
using DataImporter.Common.Utilities;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
//using MimeKit;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class ExportFileModel
    {
        private IAllDataService _allDataService;
        private IWebHostEnvironment _hostEnvironment;
        private IEmailService _emailService;
        private ILifetimeScope _scope;
        private IDateTimeUtility _dateTimeUtility;
        private IExportedFileService _exportedFileService;
        private IImportedFileService _importedFileService;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _allDataService = _scope.Resolve<IAllDataService>();
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _emailService = _scope.Resolve<IEmailService>();
            _dateTimeUtility = _scope.Resolve<IDateTimeUtility>();
            _exportedFileService = _scope.Resolve<IExportedFileService>();
            _importedFileService = _scope.Resolve<IImportedFileService>();
        }

        public ExportFileModel()
        {
           
        }
        public ExportFileModel(IEmailService emailService, IAllDataService allDataService, IWebHostEnvironment hostEnvironment, 
            IDateTimeUtility dateTimeUtility, IExportedFileService exportedFileService, IImportedFileService importedFileService)
        {
            _allDataService = allDataService;
            _hostEnvironment = hostEnvironment;
            _emailService = emailService;
            _dateTimeUtility = dateTimeUtility;
            _exportedFileService = exportedFileService;
            _importedFileService = importedFileService;
        }
        internal void ExportFile(int id , string receiverMail, Guid userId)
        {
            //following database call is for checking file exist or not
            var IsFileExist = _exportedFileService.SearchFile(id);

            //following database call is for retriving the group name of the file which user want to export
            var impFileBO = _importedFileService.GetFileById(id);

            //following database call is for retriving all data
            List<AllDataBO> allRecords = _allDataService.ExportFile(id);

            string wwwRootPath = _hostEnvironment.WebRootPath;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if(IsFileExist == 0)
            {
                string fileName = "";
                string filePath = "";

                //generating excel file and saving a directory start
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int cnt = 1;

                    //var impFileBO = _importedFileService.GetFileById(id);

                    var columnName = impFileBO.columnName.Split('>');
                    if(columnName.Length > 0)
                    {
                        for (int j = 0; j < columnName.Length; j++)
                            worksheet.Cells[1, j + 1].Value = columnName[j];
                    }

                    for (int i = 0; i < allRecords.Count; i++)
                    {
                        string data = allRecords[0].KeyForColumnName;
                        //var columnName = data.Split('>');

                        //if (i == 0)
                        //{
                        //    for (int j = 0; j < columnName.Length; j++)
                        //        worksheet.Cells[1, j + 1].Value = columnName[j];
                        //}

                        data = allRecords[i].ValueForColumnValue;
                        var columnValue = data.Split('>');
                        cnt++;
                        for (int j = 0; j < columnValue.Length; j++)
                        {
                            //cnt++;
                            worksheet.Cells[cnt, j + 1].Value = columnValue[j];
                        }
                    }

                    fileName = id.ToString() + ".xlsx";
                    filePath = Path.Combine(wwwRootPath, "exportedFiles", fileName);
                    FileInfo fileSaveAs = new FileInfo(filePath);
                    package.SaveAs(fileSaveAs);
                    //generating excel file and saving a directory end
                }

                //sending the file to the user email
                _emailService.SendEmail(receiverMail, "Exported Excel File", "please save your desired excel file", filePath);

                //following operation is for retriving the group name of the file which user want to export


                var exportFileBO = new ExportedFileBO
                {
                    FileName = impFileBO.FileName,
                    importedFileId = id,
                    UserId = impFileBO.UserId,
                    GroupName = impFileBO.GroupName,
                    ExportDate = _dateTimeUtility.Now
                };
                _exportedFileService.StoreExportedFileInfo(exportFileBO);

            }

            else
            {
                var fileName = id.ToString() + ".xlsx";
                var filePath = Path.Combine(wwwRootPath, "exportedFiles", fileName);
                _emailService.SendEmail(receiverMail, "Exported Excel File", "please save your desire excel file", filePath);
            }
           
        }
    }
}
