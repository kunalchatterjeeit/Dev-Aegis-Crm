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
   public class LeaveDesignation
    {
        public static int LeaveDesignationConfig_Save(Entity.LeaveManagement.LeaveDesignation objLeaveDesignation)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveDesignationConfig_Save";

                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", objLeaveDesignation.LeaveDesignationConfigId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", objLeaveDesignation.LeaveTypeId);
                    cmd.Parameters.AddWithValue("@DesignationId", objLeaveDesignation.DesignationId);
                    cmd.Parameters.AddWithValue("@LeaveCount", objLeaveDesignation.LeaveCount);
                    // cmd.Parameters.AddWithValue("@CreateDate", Leave.CreateDate);
                    cmd.Parameters.AddWithValue("@Active", objLeaveDesignation.Active);

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
        public static int LeaveDesignationConfig_Delete(Entity.LeaveManagement.LeaveDesignation objLeaveDesignation)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveDesignationConfig_Delete";

                    cmd.Parameters.AddWithValue("@LeaveDesignationConfigId", objLeaveDesignation);

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
