using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class LoyaltyPointReasonMaster
    {
        public LoyaltyPointReasonMaster()
        { }

        public static int Save(Entity.HR.LoyaltyPointReasonMaster loyaltyPointReasonMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_LoyaltyPointReason_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LoyaltyPointReasonId", loyaltyPointReasonMaster.LoyaltyPointReasonId);
                    cmd.Parameters.AddWithValue("@DesignationId", loyaltyPointReasonMaster.DesignationId);
                    cmd.Parameters.AddWithValue("@Reason", loyaltyPointReasonMaster.Reason);
                    cmd.Parameters.AddWithValue("@Description", loyaltyPointReasonMaster.Description);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable GetAll(Entity.HR.LoyaltyPointReasonMaster loyaltyPointReasonMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_LoyaltyPointReason_GetAll";
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

        public static Entity.HR.LoyaltyPointReasonMaster GetById(int loyaltyPointReasonId)
        {
            Entity.HR.LoyaltyPointReasonMaster loyaltyPointReasonMaster = new Entity.HR.LoyaltyPointReasonMaster();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@LoyaltyPointReasonId", loyaltyPointReasonId);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LoyaltyPointReason_GetById";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            loyaltyPointReasonMaster.LoyaltyPointReasonId = loyaltyPointReasonId;
                            loyaltyPointReasonMaster.Reason = dr["Reason"].ToString();
                            loyaltyPointReasonMaster.Description = dr["Description"].ToString();
                            loyaltyPointReasonMaster.DesignationId = int.Parse(dr["DesignationMasterId"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return loyaltyPointReasonMaster;
        }

        public static int Delete(int loyaltyPointReasonId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@LoyaltyPointReasonId", loyaltyPointReasonId);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_LoyaltyPointReason_Delete";
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
