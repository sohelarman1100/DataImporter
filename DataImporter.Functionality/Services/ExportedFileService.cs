using AutoMapper;
using DataImporter.Functionality.BusinessObjects;
using DataImporter.Functionality.Entities;
using DataImporter.Functionality.Exceptions;
using DataImporter.Functionality.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public class ExportedFileService : IExportedFileService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        private readonly IMapper _mapper;
        public ExportedFileService(IFunctionalityUnitOfWork functionalityUnitOfWork, IMapper mapper)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
            _mapper = mapper;
        }

        public int SearchFile(int id)
        {
            int cnt = _functionalityUnitOfWork.ExFiles.GetCount(x=> x.importedFileId == id);
            return cnt;
        }

        public void StoreExportedFileInfo(ExportedFileBO exportFileBO)
        {
            if (exportFileBO == null)
                throw new InvalidParameterException("file info was not provided");

            var exortedFileEntity = _mapper.Map<ExportedFiles>(exportFileBO);
            _functionalityUnitOfWork.ExFiles.Add(exortedFileEntity);

            _functionalityUnitOfWork.Save();
        }

        public (IList<ExportedFileBO> records, int total, int totalDisplay) GetAllFiles(int pageIndex, 
            int pageSize, string searchText, string sortText, Guid userId)
        {
            var fileData = _functionalityUnitOfWork.ExFiles.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? x => x.UserId == userId : x => x.GroupName.Contains(searchText)
                && x.UserId == userId, sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from file in fileData.data
                              select _mapper.Map<ExportedFileBO>(file)).ToList();

            return (resultData, fileData.total, fileData.totalDisplay);
        }
    }
}
