using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace DataAccess.LeaveManagement
{
    public class LeaveGenerateLog
    {

        public static long LeaveGenerateLog_Save(Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog)
        {
            long retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveGenerateLog_Save";
                    cmd.Parameters.AddWithValue("@LeaveGenerateLogId", leaveGenerateLog.LeaveGenerateLogId).Direction = ParameterDirection.InputOutput;
                    if (leaveGenerateLog.LeaveTypeId == 0)
                        cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@LeaveTypeId", leaveGenerateLog.LeaveTypeId);
                    if (string.IsNullOrEmpty(leaveGenerateLog.Month))
                        cmd.Parameters.AddWithValue("@Month", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Month", leaveGenerateLog.Month);
                    if (string.IsNullOrEmpty(leaveGenerateLog.Quarter))
                        cmd.Parameters.AddWithValue("@Quarter", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Quarter", leaveGenerateLog.Quarter);
                    if (leaveGenerateLog.Year == 0)
                        cmd.Parameters.AddWithValue("@Year", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Year", leaveGenerateLog.Year);
                    if (leaveGenerateLog.TotalDistribution == 0)
                        cmd.Parameters.AddWithValue("@TotalDistribution", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@TotalDistribution", leaveGenerateLog.TotalDistribution);
                    if (leaveGenerateLog.ScheduleDateTime == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@ScheduleDateTime", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ScheduleDateTime", leaveGenerateLog.ScheduleDateTime);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    retValue = Convert.ToInt64(cmd.Parameters["@LeaveGenerateLogId"].Value);
                    con.Close();
                }
            }
            return retValue;

        }

        public static DataTable LeaveGenerateLog_GetAll(Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveGenerateLog_GetAll";
                        if (leaveGenerateLog.LeaveTypeId == 0)
                            cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveTypeId", leaveGenerateLog.LeaveTypeId);
                        if (string.IsNullOrEmpty(leaveGenerateLog.Month))
                            cmd.Parameters.AddWithValue("@Month", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Month", leaveGenerateLog.Month);
                        if (string.IsNullOrEmpty(leaveGenerateLog.Quarter))
                            cmd.Parameters.AddWithValue("@Quarter", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Quarter", leaveGenerateLog.Quarter);
                        if (string.IsNullOrEmpty(leaveGenerateLog.Half))
                            cmd.Parameters.AddWithValue("@Half", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Half", leaveGenerateLog.Half);
                        if (leaveGenerateLog.Year == 0)
                            cmd.Parameters.AddWithValue("@Year", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Year", leaveGenerateLog.Year);
                        if (leaveGenerateLog.ScheduleDateTime == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ScheduleDateTime", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ScheduleDateTime", leaveGenerateLog.ScheduleDateTime);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        con.Close();
                    }
                }
                return dt;
            }
        }
    }
}
