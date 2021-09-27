using DataImporter.Data;
using DataImporter.Functionality.Contexts;
using DataImporter.Functionality.Entities;
//using DataImporter.Membership.Contexts;
//using DataImporter.Membership.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Repositories
{
    public class AllDataRepository : Repository<AllData, int>, IAllDataRepository
    {
        public AllDataRepository(IFunctionalityDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
