using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entity;
using System.Configuration;

namespace DataAccess.Common
{
    public class City
    {
        public City()
        { }

        public static int Save(Entity.Common.City city)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Common_City_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CityId", city.CityId);
                    cmd.Parameters.AddWithValue("@DistrictId", city.DistrictId);
                    cmd.Parameters.AddWithValue("@CityName", city.CityName);
                    cmd.Parameters.AddWithValue("@STD", city.STD);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable GetAll(Entity.Common.City city)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Common_City_GetAll";
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

        public static Entity.Common.City GetById(int cityid)
        {
            Entity.Common.City city = new Entity.Common.City();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@CityId", cityid);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Common_City_GetById";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            city.CityId = cityid;
                            city.CityName = dr["CityName"].ToString();
                            city.STD = (dr["STD"] == DBNull.Value) ? "" : dr["STD"].ToString();
                            city.StateId = int.Parse(dr["StateId_FK"].ToString());
                            city.DistrictId = int.Parse(dr["DistrictId"].ToString());
                            city.CountryId = int.Parse(dr["CountryId"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return city;
        }

        public static int Delete(int cityid)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@CityId", cityid);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Common_City_Delete";
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
