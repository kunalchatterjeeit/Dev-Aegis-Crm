using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Entity.Service;

namespace Business.Service
{
    public class ServiceBook
    {
        public ServiceBook()
        { }

        public long Service_ServiceBook_Save(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBook_Save(serviceBook);
        }

        public int Service_ServiceBookDetails_Save(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBookDetails_Save(serviceBook);
        }

        public DataSet Service_ServiceBookMaster_GetByCallId(long callId, CallType callType)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBookMaster_GetByCallId(callId, callType);
        }

        public DataSet Service_AssociatedEngineers_GetByCallId(long callId, CallType callType)
        {
            return DataAccess.Service.ServiceBook.Service_AssociatedEngineers_GetByCallId(callId, callType);
        }

        public int Service_TonerRequest_Approve(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_TonerRequest_Approve(serviceBook);
        }

        public DataSet Service_Tonner_GetByTonnerRequestId(Int64 tonnerRequestId)
        {
            return DataAccess.Service.ServiceBook.Service_Tonner_GetByTonnerRequestId(tonnerRequestId);
        }

        public DataTable ServiceBookMasterHistory_GetAllByCallId(Int64 callId, int callType)
        {
            return DataAccess.Service.ServiceBook.ServiceBookMasterHistory_GetAllByCallId(callId, callType);
        }

        public DataTable Service_ServiceBook_GetAll(int calltype, Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBook_GetAll(calltype, serviceBook);
        }

        public DataTable GetLastMeterReadingByCustomerPurchaseIdAndItemId(int customerPurchaseId, int spareId)
        {
            return DataAccess.Service.ServiceBook.GetLastMeterReadingByCustomerPurchaseIdAndItemId(customerPurchaseId, spareId);
        }

        public DataTable Service_CheckIfAnyOpenTonnerRequest(int customerPurchaseId)
        {
            return DataAccess.Service.ServiceBook.Service_CheckIfAnyOpenTonnerRequest(customerPurchaseId);
        }

        public DataTable Service_CheckIfAnyOpenDocket(int customerPurchaseId)
        {
            return DataAccess.Service.ServiceBook.Service_CheckIfAnyOpenDocket(customerPurchaseId);
        }

        public int Service_MeterReading_Update(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_MeterReading_Update(serviceBook);
        }

        public DataSet Service_CSR_GetByDocketId(Int64 docketNo)
        {
            return DataAccess.Service.ServiceBook.Service_CSR_GetByDocketId(docketNo);
        }

        public DataSet Service_Challan_GetByTonerRequestNo(string requestNo)
        {
            return DataAccess.Service.ServiceBook.Service_Challan_GetByTonerRequestNo(requestNo);
        }

        public DataSet Service_GetLastMeterReadingByCustomerPurchaseId(Int64 customerPurchaseId)
        {
            return DataAccess.Service.ServiceBook.Service_GetLastMeterReadingByCustomerPurchaseId(customerPurchaseId);
        }

        public DataTable GetSpareInventory_ByProductId(Int64 productId, int assetLocationId)
        {
            return DataAccess.Service.ServiceBook.GetSpareInventory_ByProductId(productId, assetLocationId);
        }

        public int Service_ServiceBookDetailsApproval_Save(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBookDetailsApproval_Save(serviceBook);
        }

        public DataTable Service_ServiceBookDetailsApproval_GetAll(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBookDetailsApproval_GetAll(serviceBook);
        }

        public int Service_CallTransfer_Save(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_CallTransfer_Save(serviceBook);
        }

        public bool Service_ServiceSpareApprovalCheck(Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceSpareApprovalCheck(serviceBook);
        }

        public ApprovalStatus Service_GetServiceBookDetailsApprovalStatus(long serviceBookId, long itemId)
        {
            return DataAccess.Service.ServiceBook.Service_GetServiceBookDetailsApprovalStatus(serviceBookId, itemId);
        }
    }
}
