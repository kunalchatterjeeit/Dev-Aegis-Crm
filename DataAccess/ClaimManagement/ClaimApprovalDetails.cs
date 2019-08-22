using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.ClaimManagement
{
    public class ClaimApprovalDetails
    {
        public static int ClaimApprovalDetails_Save(Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimApprovalDetails_Save";

                    cmd.Parameters.AddWithValue("@ClaimApprovalDetailId", ClaimApprovalDetails.ClaimApprovalDetailId);
                    cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApprovalDetails.ClaimApplicationId);
                    cmd.Parameters.AddWithValue("@ApproverId", ClaimApprovalDetails.ApproverId);
                    cmd.Parameters.AddWithValue("@Status", ClaimApprovalDetails.Status);
                    cmd.Parameters.AddWithValue("@Remarks", ClaimApprovalDetails.Remarks);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int ClaimApprovalDetails_Delete(int ClaimApprovalConfigurationId)
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

        public static DataTable ClaimApprovalDetails_GetAll(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
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

        public static DataTable GetClaimApplications_ByApproverId(int approverId, int statusId, DateTime fromApplicationDate, DateTime toApplicationDate)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetClaimApplications_ByApproverId";
                        cmd.Parameters.AddWithValue("@ApproverId", approverId);
                        if (statusId == 0)
                            cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Status", statusId);
                        if (fromApplicationDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromClaimApplicationDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromClaimApplicationDate", fromApplicationDate);
                        if (toApplicationDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToClaimApplicationDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToClaimApplicationDate", toApplicationDate);
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

        public static int Claim_Approve(Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_Claim_Approve";

                    cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApprovalDetails.ClaimApplicationId);
                    cmd.Parameters.AddWithValue("@ApproverId", ClaimApprovalDetails.ApproverId);
                    cmd.Parameters.AddWithValue("@StatusId", ClaimApprovalDetails.Status);
                    cmd.Parameters.AddWithValue("@Remarks", ClaimApprovalDetails.Remarks);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataTable ClaimApprovalDetails_ByRequestorId(int requestorId, int statusId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_ClaimApprovalDetails_ByRequestorId";
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
