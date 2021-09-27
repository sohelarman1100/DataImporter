//using DataImporter.Membership.UnitOfWorks;
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
    public class AllDataService : IAllDataService
    {
        private IFunctionalityUnitOfWork _functionalityUnitOfWork;
        private readonly IMapper _mapper;
        public AllDataService(IFunctionalityUnitOfWork functionalityUnitOfWork, IMapper mapper)
        {
            _functionalityUnitOfWork = functionalityUnitOfWork;
            _mapper = mapper;
        }

        public void CreateAllData(List<AllDataBO> allDataBO)
        {
            if (allDataBO.Count == 0)
                throw new InvalidParameterException("data was not provided");
            for(int i=0; i<allDataBO.Count; i++)
            {
                var allDataEntity = _mapper.Map<AllData>(allDataBO[i]);
                _functionalityUnitOfWork.AllDatas.Add(allDataEntity);
            }
            _functionalityUnitOfWork.Save();
        }

        public List<AllDataBO> ExportFile(int id)
        {
            var allRecordsEntity = _functionalityUnitOfWork.AllDatas.Get(x => x.FileId == id);

            var allRecordsBO = (from rerd in allRecordsEntity
                                select _mapper.Map<AllDataBO>(rerd)).ToList();

            return allRecordsBO;
        }
    }
}
