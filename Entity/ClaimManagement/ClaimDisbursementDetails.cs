using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class ClaimDisbursementDetails
    {
        public ClaimDisbursementDetails()
        {

        }
        public int ClaimDisburseDetailsId { get; set; }
        public int ClaimDisburseId { get; set; }
        public int ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
