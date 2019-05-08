using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class EmployeeLoyaltyPoint
    {
        public EmployeeLoyaltyPoint()
        { }

        public static DataTable EmployeeLoyaltyPoint_GetAll(string month, int year)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_EmployeeLoyaltyPoint_GetAll";
                        cmd.Parameters.AddWithValue("@Month", month);
                        cmd.Parameters.AddWithValue("@Year", year);
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

        public static int EmployeeLoyaltyPoint_Save(Entity.HR.EmployeeLoyaltyPoint employeeLoyaltyPoint)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeLoyaltyPoint_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoyaltyId", employeeLoyaltyPoint.LoyaltyId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeLoyaltyPoint.EmployeeId);
                    cmd.Parameters.AddWithValue("@LoyaltyPointReasonId", employeeLoyaltyPoint.LoyaltyPointReasonId);
                    cmd.Parameters.AddWithValue("@Point", employeeLoyaltyPoint.Point);
                    cmd.Parameters.AddWithValue("@Month", employeeLoyaltyPoint.Month);
                    cmd.Parameters.AddWithValue("@Year", employeeLoyaltyPoint.Year);
                    cmd.Parameters.AddWithValue("@Note", employeeLoyaltyPoint.Note);
                    cmd.Parameters.AddWithValue("@CreatedBy", employeeLoyaltyPoint.CreatedBy);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int EmployeeLoyaltyPoint_Delete(long loyaltyid)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeLoyaltyPoint_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LoyaltyId", loyaltyid);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable IndividualLoyalityPoint_ByEmployeeId(int employeeId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_IndividualLoyalityPoint_ByEmployeeId";
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
                return dt;
            }
        }
    }
}
