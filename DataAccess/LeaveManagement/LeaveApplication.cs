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
    public class LeaveApplication
    {

        public static Entity.LeaveManagement.LeaveApplicationMaster LeaveApplicationMaster_Save(Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApplicationMaster_Save";

                    cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApplicationMaster.LeaveApplicationId);
                    if (string.IsNullOrEmpty(leaveApplicationMaster.LeaveApplicationNumber))
                        cmd.Parameters.AddWithValue("@LeaveApplicationNumber", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@LeaveApplicationNumber", leaveApplicationMaster.LeaveApplicationNumber);
                    if (leaveApplicationMaster.RequestorId == 0)
                        cmd.Parameters.AddWithValue("@RequestorId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@RequestorId", leaveApplicationMaster.RequestorId);
                    if (leaveApplicationMaster.LeaveTypeId == 0)
                        cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@LeaveTypeId", leaveApplicationMaster.LeaveTypeId);
                    if (leaveApplicationMaster.LeaveAccumulationTypeId == 0)
                        cmd.Parameters.AddWithValue("@LeaveAccumulationTypeId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@LeaveAccumulationTypeId", leaveApplicationMaster.LeaveAccumulationTypeId);
                    if (leaveApplicationMaster.FromDate == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@FromDate", leaveApplicationMaster.FromDate);
                    if (leaveApplicationMaster.ToDate == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ToDate", leaveApplicationMaster.ToDate);
                    if (leaveApplicationMaster.LeaveStatusId == 0)
                        cmd.Parameters.AddWithValue("@LeaveStatusId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@LeaveStatusId", leaveApplicationMaster.LeaveStatusId);
                    if (string.IsNullOrEmpty(leaveApplicationMaster.Reason))
                        cmd.Parameters.AddWithValue("@Reason", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Reason", leaveApplicationMaster.Reason);
                    if (string.IsNullOrEmpty(leaveApplicationMaster.Attachment))
                        cmd.Parameters.AddWithValue("@Attachment", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Attachment", leaveApplicationMaster.Attachment);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            leaveApplicationMaster.LeaveApplicationId = Convert.ToInt32(dr["LeaveApplicationId"].ToString());
                            leaveApplicationMaster.LeaveApplicationNumber = dr["LeaveApplicationNumber"].ToString();
                        }
                    }
                    con.Close();
                }
            }
            return leaveApplicationMaster;
        }
        public static DataTable LeaveApplicationMaster_GetAll(Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveApplicationMaster_GetAll";

                        if (leaveApplicationMaster.LeaveApplicationId == 0)
                            cmd.Parameters.AddWithValue("@LeaveApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApplicationMaster.LeaveApplicationId);
                        if (leaveApplicationMaster.RequestorId == 0)
                            cmd.Parameters.AddWithValue("@RequestorId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestorId", leaveApplicationMaster.RequestorId);
                        if (leaveApplicationMaster.LeaveTypeId == 0)
                            cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveTypeId", leaveApplicationMaster.LeaveTypeId);
                        if (leaveApplicationMaster.LeaveAccumulationTypeId == 0)
                            cmd.Parameters.AddWithValue("@LeaveAccumulationTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveAccumulationTypeId", leaveApplicationMaster.LeaveAccumulationTypeId);
                        if (leaveApplicationMaster.FromLeaveStartDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromLeaveStartDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromLeaveStartDate", leaveApplicationMaster.FromLeaveStartDate);
                        if (leaveApplicationMaster.ToLeaveStartDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToLeaveStartDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToLeaveStartDate", leaveApplicationMaster.ToLeaveStartDate);
                        if (leaveApplicationMaster.FromApplyDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromApplyDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromApplyDate", leaveApplicationMaster.FromApplyDate);
                        if (leaveApplicationMaster.ToApplyDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToApplyDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToApplyDate", leaveApplicationMaster.ToApplyDate);
                        if (leaveApplicationMaster.LeaveStatusId == 0)
                            cmd.Parameters.AddWithValue("@LeaveStatusId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveStatusId", leaveApplicationMaster.LeaveStatusId);

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
        public static int LeaveApplicationMaster_Delete(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApplicationMaster_Delete";

                    cmd.Parameters.AddWithValue("@LeaveApplicationId", objLeaveApplicationMaster);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }
        public static int LeaveApplicationDetails_Save(Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApplicationDetails_Save";

                    cmd.Parameters.AddWithValue("@LeaveApplicationDetailId", leaveApplicationDetails.LeaveApplicationDetailId);
                    cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApplicationDetails.LeaveApplicationId);
                    cmd.Parameters.AddWithValue("@LeaveDate", leaveApplicationDetails.LeaveDate);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }
        public static DataTable LeaveApplicationDetails_GetAll(Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveApplicationDetails_GetAll";

                        if (leaveApplicationDetails.LeaveApplicationId == 0)
                            cmd.Parameters.AddWithValue("@LeaveApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApplicationDetails.LeaveApplicationId);
                        if (leaveApplicationDetails.LeaveApplicationDetailId == 0)
                            cmd.Parameters.AddWithValue("@LeaveApplicationDetailId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveApplicationDetailId", leaveApplicationDetails.LeaveApplicationDetailId);

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
        public static int LeaveApplicationDetails_Delete(int leaveApplicationDetailId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApplicationDetails_Delete";

                    cmd.Parameters.AddWithValue("@LeaveApplicationDetailId", leaveApplicationDetailId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }
        public static DataSet GetLeaveApplicationDetails_ByLeaveApplicationId(int leaveApplicationId)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetLeaveApplicationDetails_ByLeaveApplicationId";
                        cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApplicationId);
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
        public static DataTable LeaveApplicationDetails_GetByDate(Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveApplicationDetails_GetByDate";

                        if (leaveApplicationMaster.LeaveApplicationId == 0)
                            cmd.Parameters.AddWithValue("@LeaveApplicationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveApplicationId", leaveApplicationMaster.LeaveApplicationId);
                        if (leaveApplicationMaster.RequestorId == 0)
                            cmd.Parameters.AddWithValue("@RequestorId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@RequestorId", leaveApplicationMaster.RequestorId);
                        if (leaveApplicationMaster.LeaveTypeId == 0)
                            cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveTypeId", leaveApplicationMaster.LeaveTypeId);
                        if (string.IsNullOrEmpty(leaveApplicationMaster.LeaveStatuses))
                            cmd.Parameters.AddWithValue("@LeaveStatuses", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveStatuses", leaveApplicationMaster.LeaveStatuses);
                        if (leaveApplicationMaster.FromLeaveDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromLeaveDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromLeaveDate", leaveApplicationMaster.FromLeaveDate);
                        if (leaveApplicationMaster.ToLeaveDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToLeaveDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToLeaveDate", leaveApplicationMaster.ToLeaveDate);

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
        public static DataSet LeaveApplication_GetAll(Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveApplication_GetAll";
                        if (leaveApplicationMaster.LeaveTypeId == 0)
                            cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveTypeId", leaveApplicationMaster.LeaveTypeId);
                        if (leaveApplicationMaster.RequestorId == 0)
                            cmd.Parameters.AddWithValue("@EmployeeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeId", leaveApplicationMaster.RequestorId);
                        if (leaveApplicationMaster.FromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@FromDate", leaveApplicationMaster.FromDate);
                        if (leaveApplicationMaster.ToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ToDate", leaveApplicationMaster.ToDate);
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
