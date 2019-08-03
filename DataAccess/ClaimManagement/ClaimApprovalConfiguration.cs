using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.HR;

namespace DataAccess.ClaimManagement
{
    public class ClaimApprovalConfiguration
    {
        public static int ClaimApprovalConfig_Save(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimApprovalConfig_Save";

                    cmd.Parameters.AddWithValue("@ClaimApprovalConfigId", ClaimApprovalConfiguration.ClaimApprovalConfigurationId);
                    cmd.Parameters.AddWithValue("@ClaimDesignationConfigId", ClaimApprovalConfiguration.ClaimDesignationConfigurationId);
                    cmd.Parameters.AddWithValue("@ApproverDesignationId", ClaimApprovalConfiguration.ApproverDesignationId);
                    cmd.Parameters.AddWithValue("@ApproverLevel", ClaimApprovalConfiguration.ApprovalLevel);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int ClaimApprovalConfig_Delete(int ClaimApprovalConfigurationId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimApprovalConfig_Delete";

                    cmd.Parameters.AddWithValue("@ClaimApprovalConfigId", ClaimApprovalConfigurationId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable ClaimApprovalConfig_GetAll(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimApprovalConfig_GetAll";
                        if (ClaimApprovalConfiguration.ClaimApprovalConfigurationId == 0)
                            cmd.Parameters.AddWithValue("@ClaimApprovalConfigId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimApprovalConfigId", ClaimApprovalConfiguration.ClaimApprovalConfigurationId);
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

        public static int ClaimEmployeeWiseApprovalConfiguration_Save(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimEmployeeWiseApprovalConfiguration_Save";

                    cmd.Parameters.AddWithValue("@ClaimEmployeeWiseApprovalConfigId", ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfigurationId);
                    cmd.Parameters.AddWithValue("@EmployeeId", ClaimApprovalConfiguration.EmployeeId);
                    cmd.Parameters.AddWithValue("@ApproverId", ClaimApprovalConfiguration.ApproverId);
                    cmd.Parameters.AddWithValue("@ApproverLevel", ClaimApprovalConfiguration.ApprovalLevel);
                    cmd.Parameters.AddWithValue("@CreatedBy", ClaimApprovalConfiguration.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int ClaimEmployeeWiseApprovalConfiguration_Delete(long ClaimEmployeeWiseApprovalConfigurationId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimEmployeeWiseApprovalConfiguration_Delete";

                    cmd.Parameters.AddWithValue("@ClaimEmployeeWiseApprovalConfigurationId", ClaimEmployeeWiseApprovalConfigurationId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable ClaimEmployeeWiseApprovalConfiguration_GetAll(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimEmployeeWiseApprovalConfiguration_GetAll";

                        if (ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfigurationId == 0)
                            cmd.Parameters.AddWithValue("@ClaimEmployeeWiseApprovalConfigId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimEmployeeWiseApprovalConfigId", ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfigurationId);

                        if (ClaimApprovalConfiguration.EmployeeId == 0)
                            cmd.Parameters.AddWithValue("@EmployeeMasterId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeMasterId", ClaimApprovalConfiguration.EmployeeId);
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
