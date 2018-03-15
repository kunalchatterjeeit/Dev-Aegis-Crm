using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.Inventory
{
    public class SpareMaster
    {
        public SpareMaster()
        { }

        public static int Save(Entity.Inventory.SpareMaster spareMaster)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_SpareMaster_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SpareId", spareMaster.SpareId);
                    cmd.Parameters.AddWithValue("@SpareName", spareMaster.SpareName);
                    cmd.Parameters.AddWithValue("@SpareType", spareMaster.SpareType);
                    cmd.Parameters.AddWithValue("@Description", spareMaster.Description);
                    cmd.Parameters.AddWithValue("@Yield", spareMaster.Yield);
                    cmd.Parameters.AddWithValue("@IsTonner", spareMaster.isTonner);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }

        public static DataTable GetAll(Entity.Inventory.SpareMaster spareMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Inventory_SpareMaster_GetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (spareMaster.SpareName == "")
                            cmd.Parameters.AddWithValue("@SpareName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@SpareName", spareMaster.SpareName);
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

        public static Entity.Inventory.SpareMaster GetById(int SpareMasterId)
        {
            Entity.Inventory.SpareMaster spareMaster = new Entity.Inventory.SpareMaster();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_SpareMaster_GetBySpareId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SpareId", SpareMasterId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            spareMaster.SpareId = SpareMasterId;
                            spareMaster.SpareName = (dr["SpareName"] == DBNull.Value) ? string.Empty : dr["SpareName"].ToString();
                            spareMaster.SpareType = (dr["SpareType"] == DBNull.Value) ? 0 : int.Parse(dr["SpareType"].ToString());
                            spareMaster.Description = (dr["Description"] == DBNull.Value) ? string.Empty : dr["Description"].ToString();
                            spareMaster.Yield = (dr["Yield"] == DBNull.Value) ? 0 : int.Parse(dr["Yield"].ToString());
                            spareMaster.isTonner = (dr["IsTonner"] == DBNull.Value) ? false : bool.Parse(dr["IsTonner"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return spareMaster;
        }

        public static int Delete(int SpareMasterId)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_SpareMaster_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SpareId", SpareMasterId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }
    }
}
