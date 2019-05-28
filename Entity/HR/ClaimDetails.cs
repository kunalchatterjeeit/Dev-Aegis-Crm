using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.HR
{
    public class ClaimDetails
    {
        public ClaimDetails()
        {

        }

        public int ClaimDetailsId { get; set; }
        public int ClaimId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public ClaimCategoryEnum ClaimCategory { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Attachment { get; set; }
        public ClaimStatusEnum ClaimStatus { get; set; }
        public string ApproverRemarks { get; set; }
    }
}
