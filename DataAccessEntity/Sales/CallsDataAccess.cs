using Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class CallsDataAccess
    {       
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
                                "exec dbo.[usp_Sales_Calls_GetAll] @Subject,@CallStatusId,@StartFromDateTime,@StartToDateTime,@LinkId,@LinkType",
                                new Object[]
                                {
                                    new SqlParameter("Subject", (!string.IsNullOrEmpty(Param.Subject))?Param.Subject:(object)DBNull.Value),
                                    new SqlParameter("CallStatusId", (Param.CallStatusId>0)?Param.CallStatusId:(object)DBNull.Value),
                                    new SqlParameter("StartFromDateTime", (Param.StartDateTime!=DateTime.MinValue)?Param.StartDateTime:(object)DBNull.Value),
                                    new SqlParameter("StartToDateTime", (Param.EndDateTime!=DateTime.MinValue)?Param.EndDateTime:(object)DBNull.Value),
                                    new SqlParameter("LinkId", (Param.LinkId>0)?Param.LinkId:(object)DBNull.Value),
                                    new SqlParameter("LinkType", (Param.LinkType != SalesLinkType.None)?(int)Param.LinkType:(object)DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static CallsDbModel GetCallById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<CallsDbModel>(
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
            var outParam = new SqlParameter();
            outParam.ParameterName = "Id";
            outParam.Value = Model.Id;
            outParam.SqlDbType = SqlDbType.BigInt;
            outParam.Direction = ParameterDirection.InputOutput;

            int result = 0;
            using (var Context = new CRMContext())
            {
                result= Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Calls_Save] @Id OUT,@Subject,@Description,@CallStatusId,@StartDateTime,@EndDateTime," +
                                "@CallRepeatTypeId,@CallDirectionId,@CallRelatedTo,@PopupReminder,@EmailReminder,@CreatedBy,@IsActive",
                                new object[]
                                {
                                    outParam,
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
            int a = Convert.ToInt32(outParam.Value);
            return result;
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
        public static int SaveCallLinks(CallsDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_CallLinks_Save] @Id,@CallId,@LinkId,@LinkType",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.CallLinkId),
                                    new SqlParameter("CallId", Model.Id),
                                    new SqlParameter("LinkId", Model.LinkId),
                                    new SqlParameter("LinkType", Model.LinkId)
                                }
                             );
            }
        }
    }
}
