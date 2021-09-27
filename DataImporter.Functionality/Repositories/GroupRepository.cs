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
    public class GroupRepository : Repository<Group, int>, IGroupRepository
    {
        public GroupRepository(IFunctionalityDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
