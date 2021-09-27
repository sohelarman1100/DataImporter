using DataImporter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Functionality.Entities
{
    public class AllData : IEntity<int>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int GroupId { get; set; }
        public int FileId { get; set; }
        public ImportedFiles File { get; set; }
        public string FileName { get; set; }
        public DateTime FileImportDate { get; set; }
        public string KeyForColumnName { get; set; }
        public string ValueForColumnValue { get; set; }
    }
}
