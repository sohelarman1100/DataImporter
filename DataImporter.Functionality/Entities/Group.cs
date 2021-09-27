using DataImporter.Data;
using DataImporter.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public IList<ImportedFiles> Files { get; set; }
        //public IList<ExportedFiles> ExFiles { get; set; }
    }
}
