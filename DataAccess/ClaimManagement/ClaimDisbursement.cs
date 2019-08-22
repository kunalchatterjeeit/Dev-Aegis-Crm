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
    public class ClaimDisbursement
    {
        public static int ClaimDisbursement_Save(Entity.ClaimManagement.ClaimDisbursement ClaimDisbursement)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimDisbursement_Save";
                    cmd.Parameters.AddWithValue("@ClaimDisburseId", ClaimDisbursement.ClaimDisbursementId);
                    cmd.Parameters.AddWithValue("@VoucherId", ClaimDisbursement.VoucherId);
                    cmd.Parameters.AddWithValue("@CreatedBy", ClaimDisbursement.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable ClaimDisbursement_GetById(int ClaimDisbursementId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimDisbursement_GetById";
                        cmd.Parameters.AddWithValue("@ClaimDisburseId", ClaimDisbursementId);
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

        public static DataTable ClaimDisbursement_GetAll(Entity.ClaimManagement.ClaimDisbursement claimDisbursement)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimDisbursement_GetAll";
                        if (string.IsNullOrEmpty(claimDisbursement.VoucherNo))
                            cmd.Parameters.AddWithValue("@VoucherNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@VoucherNo", claimDisbursement.VoucherNo);
                        if (claimDisbursement.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", claimDisbursement.FromDate);
                        if (claimDisbursement.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", claimDisbursement.ToDate);
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

        public static decimal GetClaimAccountBalance(int employeeId)
        {
            decimal retValue = 0;
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_GetClaimAccountBalance";
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        con.Close();
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    retValue = Convert.ToDecimal(dt.Rows[0]["BalaceAmount"].ToString());
                }
                return retValue;
            }
        }
    }
}
