using Autofac;
using DataImporter.Common;
using DataImporter.Common.Methods;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class FileUploadModel
    {
        public int GroupId { get; set; }
        public int columnMatchesOrNot = 0;
        
        [Required(ErrorMessage = "please select a file")]
        public IFormFile UploadedFile { get; set; }
        private IWebHostEnvironment _hostEnvironment;
        private IGroupService _groupService;
        private ILifetimeScope _scope;
        private IImportedFileService _importedFileService;
        private ICopyExcelDataToList _copyExcelDataToList;
        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _groupService = _scope.Resolve<IGroupService>();
            _importedFileService = _scope.Resolve<IImportedFileService>();
            _copyExcelDataToList = _scope.Resolve<ICopyExcelDataToList>();

        }

        public FileUploadModel()
        {
            
        }

        public FileUploadModel(ILogger<FileUploadModel> logger, IWebHostEnvironment hostEnvironment , IGroupService groupService,
            IImportedFileService importedFileService, ICopyExcelDataToList copyExcelDataToList)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;
            _importedFileService = importedFileService;
            _copyExcelDataToList = copyExcelDataToList;
        }

        public void UploadFile(string userId, string filePath)
        {
            //cleaning tempfiles under wwwroot, for confirming this folder is empty
            //string FilePath = _hostEnvironment.WebRootPath + "/tempfiles/";

            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //DirectoryInfo d = new DirectoryInfo(FilePath);
            //FileInfo[] existingFile = d.GetFiles("*.xlsx");

            //foreach (FileInfo file in existingFile)
            //{
            //    file.Delete();
            //}
            //finish cleaning tempfiles

            //cleaning confirmfiles under wwwroot, for confirming this folder is empty
            //FilePath = _hostEnvironment.WebRootPath + "/confirmfiles/";
            //d = new DirectoryInfo(FilePath);
            //existingFile = d.GetFiles("*.xlsx");

            //foreach (FileInfo file in existingFile)
            //{
            //    file.Delete();
            //}
            //finish cleaning confirmfiles

            ClearFolder(filePath);

            string grpName = _groupService.GetGroupById(GroupId); 

            //saving image to wwwroot/tempfiles start
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(UploadedFile.FileName);
            string extension = Path.GetExtension(UploadedFile.FileName);
            fileName = fileName + "_" + userId + "_" + grpName + "_" + GroupId + extension;
            //PhotoFileName = fileName;
            string path = Path.Combine(wwwRootPath + "/tempfiles/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                UploadedFile.CopyTo(fileStream);
            }
            //saving image to wwwroot/tempfiles end
        }

        internal void CheckigColumnValidity(string userId, string filePath)
        {
            var importedFileBO = _importedFileService.GetFileForMatchingGroupColumn(Guid.Parse(userId), GroupId);
            string colName = importedFileBO.columnName;
            var colList = colName.Split('>');

            FileInfo[] existingFile = _copyExcelDataToList.GetFiles(filePath);

            List<List<string>> ExcelData = _copyExcelDataToList.CopyFileDataToList(existingFile);

            if (ExcelData[0].Count != colList.Length)
            { 
                columnMatchesOrNot = 1;
                ClearFolder(filePath);
            }
            else
            {
                for (int i = 0; i < Math.Min(ExcelData[0].Count, colList.Length); i++)
                {
                    if (ExcelData[0][i] != colList[i])
                    {
                        columnMatchesOrNot = 1;
                        ClearFolder(filePath);
                        break;
                    }
                }
            }
        }

        public void ClearFolder(string folderPath)
        {
            string FilePath = _hostEnvironment.WebRootPath + "/tempfiles/";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DirectoryInfo d = new DirectoryInfo(FilePath);
            FileInfo[] existingFile = d.GetFiles("*.xlsx");

            foreach (FileInfo file in existingFile)
            {
                file.Delete();
            }
        }
    }
}
