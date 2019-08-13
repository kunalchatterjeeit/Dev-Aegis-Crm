using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class ClaimCategory
    {
        public ClaimCategory()
        {

        }

        public int ClaimCategoryId { get; set; }
        public string ClaimCategoryName { get; set; }
        public string ClaimCategoryDescription { get; set; }
    }
}
