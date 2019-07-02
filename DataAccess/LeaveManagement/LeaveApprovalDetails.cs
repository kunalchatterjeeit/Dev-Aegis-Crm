using Entity.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.LeaveManagement
{
    public static class LeaveApprovalDetails
    {
        public static int LeaveApprovalDetails_Save(Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApprovalDetails_Save";

                    cmd.Parameters.AddWithValue("@LeaveApprovalDetailId", leaveApprovalDetails.LeaveApprovalDetailId);
                    cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApprovalDetails.LeaveApplicationId);
                    cmd.Parameters.AddWithValue("@ApproverId", leaveApprovalDetails.ApproverId);
                    cmd.Parameters.AddWithValue("@Status", leaveApprovalDetails.Status);
                    cmd.Parameters.AddWithValue("@Remarks", leaveApprovalDetails.Remarks);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int LeaveApprovalDetails_Delete(int leaveApprovalConfigurationId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApprovalConfig_Delete";

                    cmd.Parameters.AddWithValue("@LeaveApprovalConfigId", leaveApprovalConfigurationId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable LeaveApprovalDetails_GetAll(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveApprovalConfig_GetAll";
                        if (leaveApprovalConfiguration.LeaveApprovalConfigurationId == 0)
                            cmd.Parameters.AddWithValue("@LeaveApprovalConfigId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveApprovalConfigId", leaveApprovalConfiguration.LeaveApprovalConfigurationId);
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

        public static DataTable GetLeaveApplications_ByApproverId(int approverId, int statusId, LeaveTypeEnum leaveType, DateTime fromApplicationDate, DateTime toApplicationDate)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetLeaveApplications_ByApproverId";
                        cmd.Parameters.AddWithValue("@ApproverId", approverId);
                        if(statusId == 0)
                            cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Status", statusId);
                        if (leaveType == LeaveTypeEnum.None)
                            cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveTypeId", (int)leaveType);
                        if (fromApplicationDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromLeaveApplicationDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromLeaveApplicationDate", fromApplicationDate);
                        if (toApplicationDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToLeaveApplicationDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToLeaveApplicationDate", toApplicationDate);
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

        public static int LeaveApprove(Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_LeaveApprove";

                    cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApprovalDetails.LeaveApplicationId);
                    cmd.Parameters.AddWithValue("@ApproverId", leaveApprovalDetails.ApproverId);
                    cmd.Parameters.AddWithValue("@StatusId", leaveApprovalDetails.Status);
                    cmd.Parameters.AddWithValue("@Remarks", leaveApprovalDetails.Remarks);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataTable LeaveApprovalDetails_ByRequestorId(int requestorId, int statusId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_LeaveApprovalDetails_ByRequestorId";
                        cmd.Parameters.AddWithValue("@RequestorId", requestorId);
                        if (statusId == 0)
                            cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Status", statusId);
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
