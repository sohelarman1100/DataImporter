//using DataImporter.Data;
//using DataImporter.Membership.Contexts;
using DataImporter.Data;
using DataImporter.Functionality.Contexts;
using DataImporter.Functionality.Repositories;
//using DataImporter.Membership.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.UnitOfWorks
{
    public class FunctionalityUnitOfWork : UnitOfWork, IFunctionalityUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }
        public IAllDataRepository AllDatas { get; private set; }

        public IImportedFileRepository ImFiles { get; private set; }

        public IExportedFileRepository ExFiles { get; private set; }

        public FunctionalityUnitOfWork(IFunctionalityDbContext context,
            IGroupRepository groups, IAllDataRepository groupDatas, IImportedFileRepository importedFiles,
            IExportedFileRepository exportedFiles) : base((DbContext)context)
        {
            Groups = groups;
            AllDatas = groupDatas;
            ImFiles = importedFiles;
            ExFiles = exportedFiles;
        }
    }
}
