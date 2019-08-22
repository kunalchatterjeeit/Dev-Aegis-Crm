using System.Data;

namespace Business.ClaimManagement
{
    public class VoucherPaymentDetailsDetails
    {
        public VoucherPaymentDetailsDetails()
        {

        }

        public int VoucherPaymentDetails_Save(Entity.ClaimManagement.VoucherPaymentDetails voucherPaymentDetails)
        {
            return DataAccess.ClaimManagement.VoucherPaymentDetails.VoucherPaymentDetails_Save(voucherPaymentDetails);
        }

        public DataTable VoucherPaymentDetails_GetById(int voucherPaymentDetailsId)
        {
            return DataAccess.ClaimManagement.VoucherPaymentDetails.VoucherPaymentDetails_GetById(voucherPaymentDetailsId);
        }

        public DataTable VoucherPaymentDetails_GetAll(Entity.ClaimManagement.VoucherPaymentDetails voucherPaymentDetails)
        {
            return DataAccess.ClaimManagement.VoucherPaymentDetails.VoucherPaymentDetails_GetAll(voucherPaymentDetails);
        }
    }
}
