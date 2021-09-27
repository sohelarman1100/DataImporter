using Autofac;
using DataImporter.Functionality.Services;
using DataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.DataControlArea.Models
{
    public class GetExportedFilesModel
    {
        private IExportedFileService _exportedFileService;
        private ILifetimeScope _scope;

        public void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _exportedFileService = _scope.Resolve<IExportedFileService>();
        }

        public GetExportedFilesModel()
        {

        }

        public GetExportedFilesModel(IExportedFileService exportedFileService)
        {
            _exportedFileService = exportedFileService;
        }

        internal object GetAllFiles(DataTablesAjaxRequestModel tableModel, Guid userId)
        {
            var data = _exportedFileService.GetAllFiles(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "FileName", "GroupName", "ExportDate" }),
                userId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                             record.FileName,
                             record.GroupName,
                             record.ExportDate.ToString(),
                             record.importedFileId.ToString()
                        }
                    ).ToArray()
            };
        }

    }
}
