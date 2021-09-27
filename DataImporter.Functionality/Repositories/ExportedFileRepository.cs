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
    public class ExportedFileRepository : Repository<ExportedFiles, int>, IExportedFileRepository
    {
        public ExportedFileRepository(IFunctionalityDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
