using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class ClaimDesignationWiseConfiguration
    {
        public ClaimDesignationWiseConfiguration()
        {

        }

        public int ClaimDesignationConfigId { get; set; }
        public decimal Limit { get; set; }
        public string Active { get; set; }
        public int DesignationId { get; set; }
        public int ClaimCategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int FollowupInterval { get; set; }
    }
}
