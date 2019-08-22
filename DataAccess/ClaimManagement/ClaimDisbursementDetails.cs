using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ClaimManagement
{
    public class ClaimDisbursementDetails
    {
        public static int ClaimDisbursementDetails_Save(Entity.ClaimManagement.ClaimDisbursementDetails claimDisbursementDetails)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimDisbursementDetails_Save";
                    cmd.Parameters.AddWithValue("@ClaimDisburseDetailsId", claimDisbursementDetails.ClaimDisburseDetailsId);
                    cmd.Parameters.AddWithValue("@ClaimDisburseId", claimDisbursementDetails.ClaimDisburseId);
                    cmd.Parameters.AddWithValue("@ClaimId", claimDisbursementDetails.ClaimId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable ClaimDisbursementDetails_GetById(int claimDisbursementDetailsId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimDisbursementDetails_GetById";
                        cmd.Parameters.AddWithValue("@ClaimDisburseDetailsId", claimDisbursementDetailsId);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
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

        public static DataTable ClaimDisbursementDetails_GetAll(Entity.ClaimManagement.ClaimDisbursementDetails claimDisbursementDetails)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimDisbursementDetails_GetAll";
                        if (string.IsNullOrEmpty(claimDisbursementDetails.ClaimNo))
                            cmd.Parameters.AddWithValue("@ClaimNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimNo", claimDisbursementDetails.ClaimNo);
                        if (claimDisbursementDetails.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", claimDisbursementDetails.FromDate);
                        if (claimDisbursementDetails.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", claimDisbursementDetails.ToDate);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
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
