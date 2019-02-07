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

        public DataTable ServiceBookMasterHistory_GetAllByCallId(long callId, int callType)
        {
            return DataAccess.Service.ServiceBook.ServiceBookMasterHistory_GetAllByCallId(callId, callType);
        }

        public DataTable Service_ServiceBook_GetAll(int calltype, Entity.Service.ServiceBook serviceBook)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBook_GetAll(calltype, serviceBook);
        }

        //public DataTable GetLastMeterReadingByCustomerPurchaseIdAndItemId(int customerPurchaseId, int spareId)
        //{
        //    return DataAccess.Service.ServiceBook.GetLastMeterReadingByCustomerPurchaseIdAndItemId(customerPurchaseId, spareId);
        //}

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

        public DataSet Service_CSR_GetByDocketNo(string docketNo)
        {
            return DataAccess.Service.ServiceBook.Service_CSR_GetByDocketNo(docketNo);
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

        public DataSet Service_ServiceBookDetailsApproval_GetById(long serviceBookId, long itemId)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceBookDetailsApproval_GetById(serviceBookId, itemId);
        }

        public DataSet Service_ServiceCallAttendance_GetAll(Entity.Service.ServiceCallAttendance serviceCallAttendance)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceCallAttendance_GetAll(serviceCallAttendance);
        }

        public int Service_CallAttendance_Save(ServiceCallAttendance serviceCallAttendance)
        {
            return DataAccess.Service.ServiceBook.Service_CallAttendance_Save(serviceCallAttendance);
        }

        public int Service_CallAttendance_Edit(ServiceCallAttendance serviceCallAttendance)
        {
            return DataAccess.Service.ServiceBook.Service_CallAttendance_Edit(serviceCallAttendance);
        }

        public int Service_CallAttendance_Delete(long serviceCallAttendanceId)
        {
            return DataAccess.Service.ServiceBook.Service_CallAttendance_Delete(serviceCallAttendanceId);
        }

        public DataTable Service_ServiceCallAttendanceByServiceBookId(long serviceBookId)
        {
            return DataAccess.Service.ServiceBook.Service_ServiceCallAttendanceByServiceBookId(serviceBookId);
        }

        public bool IsCallInPresent(long serviceBookId)
        {
            bool retValue = false;
            DataTable response = Service_ServiceCallAttendanceByServiceBookId(serviceBookId);
            if (response != null)
            {
                retValue = response.Select("InTime IS NOT NULL AND OutTime IS NULL AND ServiceBookId = " + serviceBookId.ToString()).Any();
            }
            return retValue;
        }
    }
}
