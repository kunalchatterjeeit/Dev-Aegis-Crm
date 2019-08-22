using System.Data;

namespace Business.ClaimManagement
{
    public class VoucherPayment
    {
        public VoucherPayment()
        {

        }

        public int VoucherPayment_Save(Entity.ClaimManagement.VoucherPayment voucherPayment)
        {
            return DataAccess.ClaimManagement.VoucherPayment.VoucherPayment_Save(voucherPayment);
        }

        public DataTable VoucherPayment_GetById(int voucherPaymentId)
        {
            return DataAccess.ClaimManagement.VoucherPayment.VoucherPayment_GetById(voucherPaymentId);
        }

        public DataTable VoucherPayment_GetAll(Entity.ClaimManagement.VoucherPayment voucherPayment)
        {
            return DataAccess.ClaimManagement.VoucherPayment.VoucherPayment_GetAll(voucherPayment);
        }

        public DataTable VoucherPaymentMode_GetAll()
        {
            return DataAccess.ClaimManagement.VoucherPayment.VoucherPaymentMode_GetAll();
        }
    }
}
