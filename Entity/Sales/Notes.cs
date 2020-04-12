using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Notes
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ContactId { get; set; }
        public int CreatedBy { get; set; }       
        public bool IsActive { get; set; }
        public int NoteLinkId { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
    }
    public class GetNotes
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string ContactName { get; set; }
        public string CallStatus { get; set; }
    }

    public class GetNotesParam
    {
        public string Name { get; set; }
        public int? ContactId { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
        public int AssignEngineer { get; set; }
    }
}
