using Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class MeetingsDataAccess
    {
        public static List<MeetingStatusDbModel> GetMeetingStatus()
        {
            using (var Context = new CRMContext())
            {
                return Context.MeetingStatus.ToList();
            }
        }
        public static List<GetMeetingsDbModel> GetAllMeetings(GetMeetingsParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetMeetingsDbModel>(
                                "exec dbo.[usp_Sales_Meetings_GetAll] @Name,@MeetingTypeId,@MeetingStatusId,@StartFromDateTime,@StartToDateTime,@LinkId,@LinkType",
                                new Object[]
                                {
                                    new SqlParameter("Name", (!string.IsNullOrEmpty(Param.Name))?Param.Name:(object)DBNull.Value),
                                    new SqlParameter("MeetingTypeId", (Param.MeetingTypeId>0)?Param.MeetingTypeId:(object)DBNull.Value),
                                    new SqlParameter("MeetingStatusId", (Param.MeetingStatusId>0)?Param.MeetingStatusId:(object)DBNull.Value),
                                    new SqlParameter("StartFromDateTime", (Param.StartDateTime!=DateTime.MinValue)?Param.StartDateTime:(object)DBNull.Value),
                                    new SqlParameter("StartToDateTime", (Param.EndDateTime!=DateTime.MinValue)?Param.EndDateTime:(object)DBNull.Value),
                                    new SqlParameter("LinkId", (Param.LinkId>0)?Param.LinkId:(object)DBNull.Value),
                                    new SqlParameter("LinkType", (Param.LinkType != SalesLinkType.None)?(int)Param.LinkType:(object)DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static MeetingsDbModel GetMeetingById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<MeetingsDbModel>(
                                "exec dbo.[usp_Sales_Meetings_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int SaveMeetings(MeetingsDbModel Model)
        {
            var outParam = new SqlParameter();
            outParam.ParameterName = "Id";
            outParam.Value = Model.Id;
            outParam.SqlDbType = SqlDbType.BigInt;
            outParam.Direction = ParameterDirection.InputOutput;
            int result = 0;
            using (var Context = new CRMContext())
            {
                Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Meetings_Save] @Id OUT,@Name,@Description,@StartDateTime,@EndDateTime,@Location," +
                                "@PopupReminder,@EmailReminder,@MeetingTypeId,@MeetingStatusId,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    outParam,
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("StartDateTime", Model.StartDateTime),
                                    new SqlParameter("EndDateTime", Model.EndDateTime),
                                    new SqlParameter("Location", Model.Location),
                                    new SqlParameter("PopupReminder", Model.PopupReminder),
                                    new SqlParameter("EmailReminder", Model.EmailReminder),
                                    new SqlParameter("MeetingTypeId", Model.MeetingTypeId),
                                    new SqlParameter("MeetingStatusId", Model.MeetingStatusId),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
            result = Convert.ToInt32(outParam.Value);
            return result;
        }
        public static int DeleteMeetings(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Meetings_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }

        public static int SaveMeetingLinks(MeetingsDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_MeetingLinks_Save] @Id,@MeetingId,@LinkId,@LinkType",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.MeetingLinkId),
                                    new SqlParameter("MeetingId", Model.Id),
                                    new SqlParameter("LinkId", Model.LinkId),
                                    new SqlParameter("LinkType", (int)Model.LinkType)
                                }
                             );
            }
        }
    }
}
