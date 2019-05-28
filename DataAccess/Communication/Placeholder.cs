using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Communication
{
    public class Placeholder
    {
        public Placeholder() { }

        public static DataTable Placeholder_GetByTypeId(int placeholderTypeId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Communication_Placeholder_GetByTypeId";
                        cmd.Parameters.AddWithValue("@PlaceholderTypeId", placeholderTypeId);
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
