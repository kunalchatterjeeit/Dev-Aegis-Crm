using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.HR;

namespace DataAccess.LeaveManagement
{
    public static class LeaveApprovalConfiguration
    {
        public static int LeaveApprovalConfig_Save(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApprovalConfig_Save";

                    cmd.Parameters.AddWithValue("@LeaveApprovalConfigId", leaveApprovalConfiguration.LeaveApprovalConfigurationId);
                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", leaveApprovalConfiguration.LeaveDesignationConfigurationId);
                    cmd.Parameters.AddWithValue("@ApproverDesignationId", leaveApprovalConfiguration.ApproverDesignationId);
                    cmd.Parameters.AddWithValue("@ApproverLevel", leaveApprovalConfiguration.ApprovalLevel);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int LeaveApprovalConfig_Delete(int leaveApprovalConfigurationId)
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

        public static DataTable LeaveApprovalConfig_GetAll(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
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

        public static int LeaveEmployeeWiseApprovalConfiguration_Save(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveEmployeeWiseApprovalConfiguration_Save";

                    cmd.Parameters.AddWithValue("@LeaveEmployeeWiseApprovalConfigId", leaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfigurationId);
                    cmd.Parameters.AddWithValue("@EmployeeId", leaveApprovalConfiguration.EmployeeId);
                    cmd.Parameters.AddWithValue("@ApproverId", leaveApprovalConfiguration.ApproverId);
                    cmd.Parameters.AddWithValue("@ApproverLevel", leaveApprovalConfiguration.ApprovalLevel);
                    cmd.Parameters.AddWithValue("@CreatedBy", leaveApprovalConfiguration.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int LeaveEmployeeWiseApprovalConfiguration_Delete(long leaveEmployeeWiseApprovalConfigurationId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveEmployeeWiseApprovalConfiguration_Delete";

                    cmd.Parameters.AddWithValue("@LeaveEmployeeWiseApprovalConfigurationId", leaveEmployeeWiseApprovalConfigurationId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable LeaveEmployeeWiseApprovalConfiguration_GetAll(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveEmployeeWiseApprovalConfiguration_GetAll";

                        if (leaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfigurationId == 0)
                            cmd.Parameters.AddWithValue("@LeaveEmployeeWiseApprovalConfigId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveEmployeeWiseApprovalConfigId", leaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfigurationId);

                        if (leaveApprovalConfiguration.EmployeeId == 0)
                            cmd.Parameters.AddWithValue("@EmployeeMasterId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeMasterId", leaveApprovalConfiguration.EmployeeId);
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
