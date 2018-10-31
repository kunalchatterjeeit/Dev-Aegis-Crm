using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Service
{
    public class TonerRequest
    {
        public TonerRequest()
        { }

        public DataTable Service_TonerRequest_Save(Entity.Service.TonerRequest tonerRequest)
        {
            return DataAccess.Service.TonnerRequest.Service_TonerRequest_Save(tonerRequest);
        }

        public DataTable Service_Toner_GetAllByCustomerId(Int64 CustomerPurchaseId)
        {
            return DataAccess.Service.TonnerRequest.Service_Tonner_GetAllByCustomerId(CustomerPurchaseId);
        }

        public DataSet Service_TonerRequest_GetAll(Entity.Service.TonerRequest tonnerRequest)
        {
            return DataAccess.Service.TonnerRequest.Service_TonnerRequest_GetAll(tonnerRequest);
        }

        public DataSet Service_TonnerRequest_GetAllMinimal(Entity.Service.TonerRequest tonnerRequest)
        {
            return DataAccess.Service.TonnerRequest.Service_TonnerRequest_GetAllMinimal(tonnerRequest);
        }

        public DataTable Service_TonerRequest_GetLast10()
        {
            return DataAccess.Service.TonnerRequest.Service_TonnerRequest_GetLast10();
        }

        public Entity.Service.TonerRequest Service_TonnerRequest_GetByTonnerRequestId(int tonnerRequestid)
        {
            return DataAccess.Service.TonnerRequest.Service_TonnerRequest_GetByTonnerRequestId(tonnerRequestid);
        }

        public int Service_TonerRequest_Delete(int tonnerRequestid)
        {
            return DataAccess.Service.TonnerRequest.Service_TonnerRequest_Delete(tonnerRequestid);
        }

        public bool Service_TonerLowYieldCheck(Entity.Service.TonerRequest tonerRequest)
        {
            return DataAccess.Service.TonnerRequest.Service_TonerLowYieldCheck(tonerRequest);
        }
    }
}
