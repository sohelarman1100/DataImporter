using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.BusinessObjects
{
    public class ExportedFileBO
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int importedFileId { get; set; }
        public Guid UserId { get; set; }
        //public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime ExportDate { get; set; }
    }
}
