using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Calls
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int CallStatusId { get; set; }
        public int CallRepeatTypeId { get; set; }
        public int CallDirectionId { get; set; }
        public int CallRelatedTo { get; set; }
        public bool PopupReminder { get; set; }
        public bool EmailReminder { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class CallStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRepeatType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRelated
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallDirection
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
