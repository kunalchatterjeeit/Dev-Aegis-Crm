using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.LeaveManagement
{
    public static class LeaveAccountBalance
    {
        public static int LeaveAccountBalance_Save(int employeeId, int leaveTypeId)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LeaveAccountBalance_Save";
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataTable LeaveAccountBalance_ByEmployeeId(int employeeId, int leaveTypeId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LeaveAccountBalance_ByEmployeeId";
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        cmd.Parameters.AddWithValue("@LeaveTypeId", leaveTypeId);

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

        public static int LeaveAccontBalance_Adjust(Entity.LeaveManagement.LeaveAccountBalance leaveAccountBalance)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_LeaveAccontBalance_Adjust";
                    cmd.Parameters.AddWithValue("@EmployeeId", leaveAccountBalance.EmployeeId);
                    cmd.Parameters.AddWithValue("@LeaveTypeId", leaveAccountBalance.LeaveTypeId);
                    cmd.Parameters.AddWithValue("@Amount", leaveAccountBalance.Amount);
                    cmd.Parameters.AddWithValue("@Reason", leaveAccountBalance.Reason);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

    }
}
