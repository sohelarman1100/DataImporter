using DataImporter.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public interface IExportedFileService
    {
        int SearchFile(int id);
        void StoreExportedFileInfo(ExportedFileBO exportFileBO);
        (IList<ExportedFileBO> records, int total, int totalDisplay) GetAllFiles(int pageIndex, int pageSize, 
            string searchText, string sortText, Guid userId);
    }
}
