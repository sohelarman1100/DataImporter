using DataImporter.Data;
using DataImporter.Functionality.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Repositories
{
    public interface IExportedFileRepository : IRepository<ExportedFiles, int>
    {
    }
}
