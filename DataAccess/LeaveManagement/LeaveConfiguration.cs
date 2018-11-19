using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.LeaveManagement;

namespace DataAccess.LeaveManagement
{
   public class LeaveConfiguration
    {
        public static int LeaveConfigurations_Save(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
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
                    cmd.Parameters.AddWithValue("@CarryForwardCount", objLeaveManagement.CarryForwardCount);
                    cmd.Parameters.AddWithValue("@Encashable", objLeaveManagement.Encashable);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;

        }

        public static DataTable LeaveConfigurations_ById(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_HR_EmployeeMaster_ById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeMasterId", objLeaveManagement.LeaveConfigId);
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

        public static DataTable LeaveConfigurations_GetAll(Entity.LeaveManagement.LeaveConfiguration lmLeaveConfig)
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
        public static int LeaveConfigurations_Delete(int leaveConfigId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveConfig_Delete";

                    cmd.Parameters.AddWithValue("@LeaveConfigId", leaveConfigId);

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
