using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.HR
{
    public class Claim
    {
        public Claim()
        {

        }

        public static DataTable Claim_GetAll(Entity.HR.Claim claim)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Claim_GetAll";
                        if (string.IsNullOrEmpty(claim.ClaimNo))
                            cmd.Parameters.AddWithValue("@ClaimNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimNo", claim.ClaimNo);
                        if (claim.EmployeeId == 0)
                            cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeId", claim.EmployeeId);
                        if (claim.PeriodFrom == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@PeriodFrom", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@PeriodFrom", claim.PeriodFrom);
                        if (claim.PeriodTo == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@PeriodTo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@PeriodTo", claim.PeriodTo);
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

        //public static DataTable Holiday_GetById(int holidayId)
        //{
        //    using (DataTable dt = new DataTable())
        //    {
        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
        //        {
        //            using (SqlCommand cmd = new SqlCommand())
        //            {
        //                cmd.Connection = con;
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "usp_HR_Holiday_GetById";
        //                cmd.Parameters.AddWithValue("@HolidayId", holidayId);
        //                if (con.State == ConnectionState.Closed)
        //                    con.Open();
        //                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //                {
        //                    da.Fill(dt);
        //                }
        //                con.Close();
        //            }
        //        }
        //        return dt;
        //    }
        //}

        public static int Claim_Save(Entity.HR.Claim claim)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Claim_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ClaimId", claim.ClaimId);
                    cmd.Parameters.AddWithValue("@ClaimNo", claim.ClaimNo);
                    cmd.Parameters.AddWithValue("@EmployeeId", claim.EmployeeId);
                    cmd.Parameters.AddWithValue("@PeriodFrom", claim.PeriodFrom);
                    cmd.Parameters.AddWithValue("@PeriodTo", claim.PeriodTo);
                    cmd.Parameters.AddWithValue("@ClaimDateTime", claim.ClaimDateTime);
                    cmd.Parameters.AddWithValue("@CreatedBy", claim.CreatedBy);
                    cmd.Parameters.AddWithValue("@TotalAmount", claim.TotalAmount);
                    cmd.Parameters.AddWithValue("@Status", claim.ClaimStatus);
                    cmd.Parameters.AddWithValue("@ApproverId", claim.ApproverId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }
    }
}
