using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class TonerModel : BaseModel
    {
        public string TonerNo { get; set; }
        public string TonerDateTime { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public string CallStatus { get; set; }
        public string ContactPerson { get; set; }
    }
}