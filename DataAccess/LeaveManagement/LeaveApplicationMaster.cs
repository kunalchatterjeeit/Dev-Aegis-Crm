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
    public class LeaveApplicationMaster
    {
        public static int LeaveDesignationConfig_Save(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveDesignationConfig_Save";

                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", objLeaveManagement.LeaveDesignationConfigId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", objLeaveManagement.LeaveTypeId);
                    cmd.Parameters.AddWithValue("@DesignationId", objLeaveManagement.DesignationId);
                    cmd.Parameters.AddWithValue("@LeaveCount", objLeaveManagement.LeaveCount);
                    //  cmd.Parameters.AddWithValue("@CreateDate", Leave.CreateDate);
                    cmd.Parameters.AddWithValue("@Active", objLeaveManagement.Active);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataTable LeaveDesignationConfig_GetAll()
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveDesignationConfig_GetAll";
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
        public static int LeaveDesignationConfig_Delete(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveDesignationConfig_Delete";

                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", objLeaveManagement);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int LeaveApplicationMaster_Save(Entity.LeaveManagement.LeaveManagement Leave)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApplicationMaster_Save";

                    cmd.Parameters.AddWithValue("@LeaveApplicationId", Leave.LeaveApplicationId);
                    cmd.Parameters.AddWithValue("@LeaveApplicationNumber", Leave.LeaveApplicationNumber);
                    cmd.Parameters.AddWithValue("@RequestorId", Leave.RequestorId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", Leave.LeaveTypeId);
                    cmd.Parameters.AddWithValue("@LeaveAccumulationTypeId", Leave.LeaveAccumulationTypeId);
                    cmd.Parameters.AddWithValue("@FromDate", Leave.FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", Leave.ToDate);
                    cmd.Parameters.AddWithValue("@LeaveStatusId", Leave.LeaveStatusId);
                    cmd.Parameters.AddWithValue("@Reason", Leave.Reason);
                    cmd.Parameters.AddWithValue("@Attachment", Leave.Attachment);


                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }
        public static int LeaveApplicationMaster_GetAll(Entity.LeaveManagement.LeaveManagement objleaveapplicationmaster)
        {
            throw new NotImplementedException();
        }

        public static DataTable LeaveApplicationMaster_GetAll()
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
        public static int LeaveApplicationMaster_Delete(Entity.LeaveManagement.LeaveManagement objleaveapplicationmaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveApplicationMaster_Delete";

                    cmd.Parameters.AddWithValue("@LeaveApplicationId", objleaveapplicationmaster);

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
