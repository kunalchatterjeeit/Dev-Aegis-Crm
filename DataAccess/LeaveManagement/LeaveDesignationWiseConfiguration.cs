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
    public class LeaveDesignationWiseConfiguration
    {
        public static int LeaveDesignationConfig_Save(Entity.LeaveManagement.LeaveDesignationWiseConfiguration leaveDesignationWiseConfiguration)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveDesignationConfig_Save";

                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", leaveDesignationWiseConfiguration.LeaveDesignationConfigId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", leaveDesignationWiseConfiguration.LeaveTypeId);
                    cmd.Parameters.AddWithValue("@DesignationId", leaveDesignationWiseConfiguration.DesignationId);
                    cmd.Parameters.AddWithValue("@LeaveCount", leaveDesignationWiseConfiguration.LeaveCount);
                    cmd.Parameters.AddWithValue("@CarryForwardCount", leaveDesignationWiseConfiguration.CarryForwardCount);
                    cmd.Parameters.AddWithValue("@MinApplyDays", leaveDesignationWiseConfiguration.MinApplyDays);
                    cmd.Parameters.AddWithValue("@MaxApplyDays", leaveDesignationWiseConfiguration.MaxApplyDays);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int LeaveDesignationConfig_Delete(int leaveDesignationWiseConfigurationId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveDesignationConfig_Delete";

                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", leaveDesignationWiseConfigurationId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable LeaveDesignationConfig_GetById(int leaveDesignationWiseConfigurationId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_HR_LeaveDesignationConfig_GetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", leaveDesignationWiseConfigurationId);
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

        public static DataTable LeaveDesignationConfig_GetAll(Entity.LeaveManagement.LeaveDesignationWiseConfiguration leaveDesignationWiseConfiguration)
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
                        if (leaveDesignationWiseConfiguration.LeaveTypeId == 0)
                            cmd.Parameters.AddWithValue("@LeaveTypeId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@LeaveTypeId", leaveDesignationWiseConfiguration.LeaveTypeId);
                        if (leaveDesignationWiseConfiguration.DesignationId == 0)
                            cmd.Parameters.AddWithValue("@DesignationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DesignationId", leaveDesignationWiseConfiguration.DesignationId);
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
