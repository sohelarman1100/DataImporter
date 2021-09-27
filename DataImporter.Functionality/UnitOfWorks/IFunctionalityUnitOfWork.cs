using DataImporter.Data;
using DataImporter.Functionality.Repositories;
//using DataImporter.Membership.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.UnitOfWorks
{
    public interface IFunctionalityUnitOfWork : IUnitOfWork
    {
        public IGroupRepository Groups { get; }
        public IAllDataRepository AllDatas { get; }
        public IImportedFileRepository ImFiles { get; }
        public IExportedFileRepository ExFiles { get; }
    }
}
