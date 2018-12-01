using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.Settings
{
    public static class UserSettings
    {
        public static void Save(Entity.Settings.UserSettings userSettings)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["addonsConstr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Appn_UserSettings_Save";
                    cmd.Parameters.AddWithValue("@UserId", userSettings.UserId);
                    cmd.Parameters.AddWithValue("@SettingsName", userSettings.SettingName);
                    cmd.Parameters.AddWithValue("@SettingValue", userSettings.SettingValue);
                    cmd.Parameters.AddWithValue("@IsActive", userSettings.IsActive);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public static DataSet GetByUserId(int userId)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["addonsConstr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Appn_UserSettings_GetByUserId";
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }
    }
}
