using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.BusinessObjects
{
    public class ImportedFileBO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Status { get; set; }
        public DateTime ImportDate { get; set; }
        public string columnName { get; set; }
    }
}
