using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ClaimManagement
{
    public class Voucher
    {
        public Voucher()
        {

        }

        public int Voucher_Save(Entity.ClaimManagement.Voucher voucher)
        {
            return DataAccess.ClaimManagement.Voucher.Voucher_Save(voucher);
        }

        public DataTable Voucher_GetById(int VoucherId)
        {
            return DataAccess.ClaimManagement.Voucher.Voucher_GetById(VoucherId);
        }

        public DataSet Voucher_GetAll(Entity.ClaimManagement.Voucher voucher)
        {
            return DataAccess.ClaimManagement.Voucher.Voucher_GetAll(voucher);
        }
    }
}
