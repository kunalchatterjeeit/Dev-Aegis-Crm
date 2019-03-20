using System;
using System.Collections.Generic;
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
        public static List<MeetingTypeDbModel> GetMeetingType()
        {
            using (var Context = new CRMContext())
            {
                return Context.MeetingType.ToList();
            }
        }
        public static List<GetMeetingsDbModel> GetAllMeetings(GetMeetingsParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetMeetingsDbModel>(
                                "exec dbo.[usp_Sales_Meetings_GetAll] @Name,@MeetingTypeId,@MeetingStatusId,@StartFromDateTime,@StartToDateTime",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("MeetingTypeId", DBNull.Value),
                                    new SqlParameter("MeetingStatusId", DBNull.Value),
                                    new SqlParameter("StartFromDateTime", DBNull.Value),
                                    new SqlParameter("StartToDateTime", DBNull.Value)
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
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Meetings_Save] @Id,@Name,@Description,@Location,@StartDateTime,@EndDateTime," +
                                "@MeetingTypeId,@MeetingStatusId,@PopupReminder,@EmailReminder,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("Location", Model.Location),
                                    new SqlParameter("StartDateTime", Model.StartDateTime),
                                    new SqlParameter("EndDateTime", Model.EndDateTime),
                                    new SqlParameter("MeetingTypeId", Model.MeetingTypeId),
                                    new SqlParameter("MeetingStatusId", Model.MeetingStatusId),
                                    new SqlParameter("PopupReminder", Model.PopupReminder),
                                    new SqlParameter("EmailReminder", Model.EmailReminder),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
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
    }
}
