using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.HR;

namespace DataAccess.HR
{
    public class LeaveManagement
    {
        public static int LeaveDesignationConfig_Save(Entity.HR.LeaveManagement objLeaveManagement)
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



        public static int LeaveDesignationConfig_GetAll(Entity.HR.LeaveManagement objLeaveManagement)
        {
            throw new NotImplementedException();
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
        public static int LeaveDesignationConfig_Delete(Entity.HR.LeaveManagement objLeaveManagement)
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

        public static int LeaveApplicationMaster_Save(Entity.HR.LeaveManagement Leave)
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
        public static int LeaveApplicationMaster_GetAll(Entity.HR.LeaveManagement objleaveapplicationmaster)
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
        public static int LeaveApplicationMaster_Delete(Entity.HR.LeaveManagement objleaveapplicationmaster)
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
        public static int LeaveConfigurations_Save(Entity.HR.LeaveManagement objLeaveManagement)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveConfig_Save";

                    cmd.Parameters.AddWithValue("@LeaveConfigId", objLeaveManagement.LeaveConfigId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", objLeaveManagement.LeaveTypeId);
                    cmd.Parameters.AddWithValue("@LeaveFrequency", objLeaveManagement.LeaveFrequency);
                    cmd.Parameters.AddWithValue("@LeaveAccureDate", objLeaveManagement.LeaveAccureDate);
                    cmd.Parameters.AddWithValue("CarryForwardCount", objLeaveManagement.CarryForwardCount);
                    cmd.Parameters.AddWithValue("@Encashable", objLeaveManagement.Encashable);
                    cmd.Parameters.AddWithValue("@CreatedDate", objLeaveManagement.CreatedDate);
                    cmd.Parameters.AddWithValue("@Active", objLeaveManagement.Active);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;

        }

        public static int LeaveConfigurations_GetAll(Entity.HR.LeaveManagement ObjbelLeaveConfig)
        {
            throw new NotImplementedException();
        }

        public static DataTable LeaveConfigurations_GetAll()
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveConfig_GetAll";
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
        public static int LeaveConfigurations_Delete(Entity.HR.LeaveManagement objleaveapplicationmaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveConfig_Delete";

                    cmd.Parameters.AddWithValue("@LeaveConfigId", objleaveapplicationmaster);

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


