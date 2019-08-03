using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.HR;

namespace DataAccess.ClaimManagement
{
    public class ClaimDesignationWiseConfiguration
    {
        public ClaimDesignationWiseConfiguration()
        {

        }

        public static int ClaimDesignationConfig_Save(Entity.ClaimManagement.ClaimDesignationWiseConfiguration ClaimDesignationWiseConfiguration)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimDesignationConfig_Save";

                    cmd.Parameters.AddWithValue("@ClaimDesignationConfigId", ClaimDesignationWiseConfiguration.ClaimDesignationConfigId);
                    cmd.Parameters.AddWithValue("@ClaimCategoryId", ClaimDesignationWiseConfiguration.ClaimCategoryId);
                    cmd.Parameters.AddWithValue("@DesignationId", ClaimDesignationWiseConfiguration.DesignationId);
                    cmd.Parameters.AddWithValue("@Limit", ClaimDesignationWiseConfiguration.Limit);
                    cmd.Parameters.AddWithValue("@FollowupInterval", ClaimDesignationWiseConfiguration.FollowupInterval);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return retValue;
        }

        public static int ClaimDesignationConfig_Delete(int ClaimDesignationWiseConfigurationId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_HR_ClaimDesignationConfig_Delete";

                    cmd.Parameters.AddWithValue("@ClaimDesignationConfigId", ClaimDesignationWiseConfigurationId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable ClaimDesignationConfig_GetById(int ClaimDesignationWiseConfigurationId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_HR_ClaimDesignationConfig_GetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ClaimDesignationConfigId", ClaimDesignationWiseConfigurationId);
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

        public static DataTable ClaimDesignationConfig_GetAll(Entity.ClaimManagement.ClaimDesignationWiseConfiguration ClaimDesignationWiseConfiguration)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_ClaimDesignationConfig_GetAll";
                        if (ClaimDesignationWiseConfiguration.ClaimCategoryId == 0)
                            cmd.Parameters.AddWithValue("@ClaimCategoryId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ClaimCategoryId", ClaimDesignationWiseConfiguration.ClaimCategoryId);
                        if (ClaimDesignationWiseConfiguration.DesignationId == 0)
                            cmd.Parameters.AddWithValue("@DesignationId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@DesignationId", ClaimDesignationWiseConfiguration.DesignationId);
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
