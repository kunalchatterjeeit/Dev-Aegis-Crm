using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.HR
{
    public class Claim
    {
        public Claim()
        {

        }

        public DataTable Claim_GetAll(Entity.HR.Claim claim)
        {
            return DataAccess.HR.Claim.Claim_GetAll(claim);
        }
        //public DataTable Holiday_GetById(int holiday)
        //{
        //    return DataAccess.HR.Holiday.Holiday_GetById(holiday);
        //}
        public int Claim_Save(Entity.HR.Claim claim)
        {
            return DataAccess.HR.Claim.Claim_Save(claim);
        }
    }
}
