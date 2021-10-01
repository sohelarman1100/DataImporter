using DataImporter.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public interface IImportedFileService
    {
        void CreateImportedFile(ImportedFileBO importFileBO);
        int GetFile(ImportedFileBO importFileBO);
        void UpdateStatus(int fileId);
        (IList<ImportedFileBO> records, int total, int totalDisplay) GetAllFiles(int pageIndex, int pageSize, 
            string searchText, string sortText, Guid userId);
        void DeleteImportedFile(int id);
        ImportedFileBO isFileExistOrNot(Guid userId, int groupId, string fileName);
        void UpdateProcessingStatus(int fileId, string colName);
        ImportedFileBO GetFileById(int id);
        ImportedFileBO GetFileForMatchingGroupColumn(Guid userId, int groupId);
    }
}
