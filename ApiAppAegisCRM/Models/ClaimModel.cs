using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class ClaimModel : BaseModel
    {
        public ClaimModel() { }
        public string ClaimNo { get; set; }
        public string ClaimHeading { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string StatusName { get; set; }
        public string ClaimDateTime { get; set; }
        public decimal TotalAmount { get; set; }
    }
}