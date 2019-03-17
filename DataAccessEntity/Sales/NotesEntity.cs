using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class NotesDbModel
    {
        [Key]
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ContactId { get; set; }
        public int CreatedBy { get; set; }     
        public bool IsActive { get; set; }
    }
    public class GetNotesDbModel
    {
        public Int64 Id { get; set; }     
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string CallStatus { get; set; }     
    }

    public class GetNotesParamDbModel
    {
        public string Name { get; set; }
        public int? ContactId { get; set; }     
    }
}
