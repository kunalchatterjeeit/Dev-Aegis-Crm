using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.ClaimManagement
{
    public class ClaimStatus
    {
        public static int ClaimStatus_Save(Entity.ClaimManagement.ClaimStatus objClaimStatus)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimStatus_Save";
                    cmd.Parameters.AddWithValue("@ClaimStatusId", objClaimStatus.ClaimStatusId);
                    cmd.Parameters.AddWithValue("@ClaimStatusName", objClaimStatus.ClaimStatusName);
                    cmd.Parameters.AddWithValue("@ClaimStatusDescription", objClaimStatus.ClaimStatusDescription);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataTable ClaimStatus_GetAll()
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimStatus_GetAll";
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

        public static int ClaimStatus_Delete(Entity.ClaimManagement.ClaimStatus objClaimStatus)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimStatus_Delete";

                    cmd.Parameters.AddWithValue("@ClaimStatusId", objClaimStatus.ClaimStatusId);

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
