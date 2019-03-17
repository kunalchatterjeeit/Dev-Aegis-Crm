using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class CallsDataAccess
    {
        public static List<CallStatusDbModel> GetCallStatus()
        {
            using (var Context = new CRMContext())
            {
                return Context.CallStatus.ToList();
            }
        }
        public static List<CallDirectionDbModel> GetCallDirection()
        {
            using (var Context = new CRMContext())
            {
                return Context.CallDirection.ToList();
            }
        }
        public static List<CallRelatedDbModel> GetCallRelated()
        {
            using (var Context = new CRMContext())
            {
                return Context.CallRelated.ToList();
            }
        }
        public static List<CallRepeatTypeDbModel> GetCallRepeatType()
        {
            var Context = new CRMContext();
            {
                return Context.CallRepeatType.ToList();
            }
        }

        public static List<GetCallsDbModel> GetAllCalls(GetCallsParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetCallsDbModel>(
                                "exec dbo.[usp_Sales_Calls_GetAll] @Subject,@CallStatusId,@StartFromDateTime,@StartToDateTime",
                                new Object[]
                                {
                                    new SqlParameter("Subject", Param.Subject),
                                    new SqlParameter("CallStatusId", Param.CallStatusId),
                                    new SqlParameter("StartFromDateTime", Param.StartDateTime),
                                    new SqlParameter("StartToDateTime", Param.EndDateTime)
                                }
                             ).ToList();
            }
        }
        public static GetCallsDbModel GetCallById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetCallsDbModel>(
                                "exec dbo.[usp_Sales_Calls_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int SaveCalls(CallsDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Calls_Save] @Id,@Subject,@Description,@CallStatusId,@StartDateTime,@EndDateTime," +
                                "@CallRepeatTypeId,@CallDirectionId,@CallRelatedTo,@PopupReminder,@EmailReminder,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Subject", Model.Subject),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("CallStatusId", Model.CallStatusId),
                                    new SqlParameter("StartDateTime", Model.StartDateTime),
                                    new SqlParameter("EndDateTime", Model.EndDateTime),
                                    new SqlParameter("CallRepeatTypeId", Model.CallRepeatTypeId),
                                    new SqlParameter("CallDirectionId", Model.CallDirectionId),
                                    new SqlParameter("CallRelatedTo", Model.CallRelatedTo),
                                    new SqlParameter("PopupReminder", Model.PopupReminder),
                                    new SqlParameter("EmailReminder", Model.EmailReminder),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static int DeleteCalls(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Calls_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)                                   
                                }
                             );
            }
        }
    }
}
