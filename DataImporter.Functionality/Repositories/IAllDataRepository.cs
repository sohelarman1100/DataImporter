using DataImporter.Data;
using DataImporter.Functionality.Entities;
//using DataImporter.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Repositories
{
    public interface IAllDataRepository : IRepository<AllData, int>
    {
    }
}
