using Autofac;
using DataImporter.Functionality.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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
        
        [Required(ErrorMessage = "please select a file")]
        public IFormFile UploadedFile { get; set; }
        private IWebHostEnvironment _hostEnvironment;
        private IGroupService _groupService;
        private ILifetimeScope _scope;
        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _hostEnvironment = _scope.Resolve<IWebHostEnvironment>();
            _groupService = _scope.Resolve<IGroupService>();
        }

        public FileUploadModel()
        {

        }

        public FileUploadModel(ILogger<FileUploadModel> logger, IWebHostEnvironment hostEnvironment , IGroupService groupService)
        {
            _hostEnvironment = hostEnvironment;
            _groupService = groupService;
        }

        public void UploadFile(string userId)
        {
            //cleaning tempfiles under wwwroot, for confirming this folder is empty
            string FilePath = _hostEnvironment.WebRootPath + "/tempfiles/";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            DirectoryInfo d = new DirectoryInfo(FilePath);
            FileInfo[] existingFile = d.GetFiles("*.xlsx");

            foreach (FileInfo file in existingFile)
            {
                file.Delete();
            }
            //finish cleaning tempfiles

            //cleaning confirmfiles under wwwroot, for confirming this folder is empty
            FilePath = _hostEnvironment.WebRootPath + "/confirmfiles/";
            d = new DirectoryInfo(FilePath);
            existingFile = d.GetFiles("*.xlsx");

            foreach (FileInfo file in existingFile)
            {
                file.Delete();
            }
            //finish cleaning confirmfiles

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
    }
}
