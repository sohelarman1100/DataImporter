using Autofac;
using DataImporter.Functionality.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class GetImportedFilesModel
    {
        public Guid UserId { get; set; }
        private IImportedFileService _importedFileService;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _importedFileService = _scope.Resolve<IImportedFileService>();
        }

        public GetImportedFilesModel()
        {
            
        }

        public GetImportedFilesModel(IImportedFileService importedFileService)
        {
            _importedFileService = importedFileService;
        }

        internal object GetAllFiles(DataTablesAjaxRequestModel tableModel)
        {
            var data = _importedFileService.GetAllFiles(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "FileName", "GroupName" }),
                UserId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.FileName,
                             record.GroupName,
                             record.Status,
                             record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void DeleteImportedFile(int id)
        {
            _importedFileService.DeleteImportedFile(id);
        }
    }
}
