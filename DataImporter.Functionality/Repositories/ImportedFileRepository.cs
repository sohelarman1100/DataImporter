using DataImporter.Data;
using DataImporter.Functionality.Contexts;
using DataImporter.Functionality.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Repositories
{
    public class ImportedFileRepository : Repository<ImportedFiles, int>, IImportedFileRepository
    {
        public ImportedFileRepository(IFunctionalityDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
