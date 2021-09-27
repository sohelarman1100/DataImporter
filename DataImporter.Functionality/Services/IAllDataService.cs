using DataImporter.Functionality.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Services
{
    public interface IAllDataService
    {
        void CreateAllData(List<AllDataBO> allDataBO);
        List<AllDataBO> ExportFile(int id);
    }
}
