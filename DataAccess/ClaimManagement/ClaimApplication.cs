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
    public class ClaimApplication
    {
        public static Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster_Save(Entity.ClaimManagement.ClaimApplicationMaster claimApplicationMaster)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_Claim_Save";

                    cmd.Parameters.AddWithValue("@ClaimId", claimApplicationMaster.ClaimApplicationId);
                    cmd.Parameters.AddWithValue("@ClaimNo", claimApplicationMaster.ClaimApplicationNumber);
                    if (claimApplicationMaster.EmployeeId == 0)
                        cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@EmployeeId", claimApplicationMaster.EmployeeId);
                    if (claimApplicationMaster.PeriodFrom == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@PeriodFrom", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@PeriodFrom", claimApplicationMaster.PeriodFrom);
                    if (claimApplicationMaster.PeriodTo == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@PeriodTo", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@PeriodTo", claimApplicationMaster.PeriodTo);
                    if (claimApplicationMaster.CreatedBy == 0)
                        cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@CreatedBy", claimApplicationMaster.CreatedBy);
                    if (claimApplicationMaster.TotalAmount==0)
                        cmd.Parameters.AddWithValue("@TotalAmount", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@TotalAmount", claimApplicationMaster.TotalAmount);
                    if (claimApplicationMaster.Status==0)
                        cmd.Parameters.AddWithValue("@Status", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Status", claimApplicationMaster.Status);
                    cmd.Parameters.AddWithValue("@ClaimDateTime", claimApplicationMaster.ClaimDateTime);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            claimApplicationMaster.ClaimApplicationId = Convert.ToInt32(dr["ClaimId"].ToString());
                            claimApplicationMaster.ClaimApplicationNumber = dr["ClaimNo"].ToString();
                        }
                    }
                    con.Close();
                }
            }
            return claimApplicationMaster;
        }
        public static DataTable ClaimApplicationMaster_GetAll(Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimApplicationMaster_GetAll";

                        if (ClaimApplicationMaster.ClaimApplicationId == 0)
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApplicationMaster.ClaimApplicationId);
                        if (ClaimApplicationMaster.EmployeeId == 0)
                            cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeId", ClaimApplicationMaster.EmployeeId);
                        if (ClaimApplicationMaster.PeriodFrom == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", ClaimApplicationMaster.PeriodFrom);
                        if (ClaimApplicationMaster.PeriodTo == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", ClaimApplicationMaster.PeriodTo);

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
        public static int ClaimApplicationMaster_Delete(Entity.ClaimManagement.ClaimApplicationMaster objClaimApplicationMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimApplicationMaster_Delete";

                    cmd.Parameters.AddWithValue("@ClaimApplicationId", objClaimApplicationMaster);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }
        public static int ClaimApplicationDetails_Save(Entity.ClaimManagement.ClaimApplicationDetails ClaimApplicationDetails)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimDetails_Save";

                    cmd.Parameters.AddWithValue("@ClaimDetailsId", ClaimApplicationDetails.ClaimApplicationDetailId);
                    cmd.Parameters.AddWithValue("@ClaimId", ClaimApplicationDetails.ClaimApplicationId);
                    cmd.Parameters.AddWithValue("@ExpenseDate", ClaimApplicationDetails.ExpenseDate);
                    cmd.Parameters.AddWithValue("@Attachment", ClaimApplicationDetails.Attachment);
                    cmd.Parameters.AddWithValue("@CategoryId", ClaimApplicationDetails.CategoryId);
                    cmd.Parameters.AddWithValue("@Cost", ClaimApplicationDetails.Cost);
                    cmd.Parameters.AddWithValue("@Description", ClaimApplicationDetails.Description);
                    cmd.Parameters.AddWithValue("@Status", ClaimApplicationDetails.Status);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }
        public static DataTable ClaimApplicationDetails_GetAll(Entity.ClaimManagement.ClaimApplicationDetails ClaimApplicationDetails)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimApplicationDetails_GetAll";

                        if (ClaimApplicationDetails.ClaimApplicationId == 0)
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApplicationDetails.ClaimApplicationId);
                        if (ClaimApplicationDetails.ClaimApplicationDetailId == 0)
                            cmd.Parameters.AddWithValue("@ClaimApplicationDetailId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimApplicationDetailId", ClaimApplicationDetails.ClaimApplicationDetailId);

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
        public static int ClaimApplicationDetails_Delete(int ClaimApplicationDetailId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimApplicationDetails_Delete";

                    cmd.Parameters.AddWithValue("@ClaimApplicationDetailId", ClaimApplicationDetailId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }
        public static DataSet GetClaimApplicationDetails_ByClaimApplicationId(int ClaimApplicationId)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetClaimApplicationDetails_ByClaimApplicationId";
                        cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApplicationId);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }
        public static DataTable ClaimApplicationDetails_GetByDate(Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimApplicationDetails_GetByDate";

                        if (ClaimApplicationMaster.ClaimApplicationId == 0)
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApplicationMaster.ClaimApplicationId);
                        //if (ClaimApplicationMaster.RequestorId == 0)
                        //    cmd.Parameters.AddWithValue("@RequestorId", DBNull.Value);
                        //else
                        //    cmd.Parameters.AddWithValue("@RequestorId", ClaimApplicationMaster.RequestorId);
                        //if (string.IsNullOrEmpty(ClaimApplicationMaster.ClaimStatuses))
                        //    cmd.Parameters.AddWithValue("@ClaimStatuses", DBNull.Value);
                        //else
                        //    cmd.Parameters.AddWithValue("@ClaimStatuses", ClaimApplicationMaster.ClaimStatuses);
                        //if (ClaimApplicationMaster.FromClaimDate == DateTime.MinValue)
                        //    cmd.Parameters.AddWithValue("@FromClaimDate", DBNull.Value);
                        //else
                        //    cmd.Parameters.AddWithValue("@FromClaimDate", ClaimApplicationMaster.FromClaimDate);
                        //if (ClaimApplicationMaster.ToClaimDate == DateTime.MinValue)
                        //    cmd.Parameters.AddWithValue("@ToClaimDate", DBNull.Value);
                        //else
                        //    cmd.Parameters.AddWithValue("@ToClaimDate", ClaimApplicationMaster.ToClaimDate);

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
        public static DataSet ClaimApplication_GetAll(Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimApplication_GetAll";

                        if (ClaimApplicationMaster.ClaimApplicationId == 0)
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimApplicationId", ClaimApplicationMaster.ClaimApplicationId);
                        if (ClaimApplicationMaster.EmployeeId == 0)
                            cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeId", ClaimApplicationMaster.EmployeeId);
                        if (ClaimApplicationMaster.PeriodFrom == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", ClaimApplicationMaster.PeriodFrom);
                        if (ClaimApplicationMaster.PeriodTo == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", ClaimApplicationMaster.PeriodTo);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }
    }
}
