using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Inventory
{
    public class StoreMaster
    {
        public StoreMaster()
        { }

        public static int Save(Entity.Inventory.StoreMaster storeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_StoreMaster_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StoreId", storeMaster.StoreId);
                    cmd.Parameters.AddWithValue("@StoreName", storeMaster.StoreName);
                    cmd.Parameters.AddWithValue("@ContactPerson", storeMaster.ContactPerson);
                    cmd.Parameters.AddWithValue("@Location", storeMaster.Location);
                    cmd.Parameters.AddWithValue("@Phone", storeMaster.Phone);
                    cmd.Parameters.AddWithValue("@IsActive", storeMaster.IsActive);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable GetAll()
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Inventory_StoreMaster_GetAll";
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

        public static Entity.Inventory.StoreMaster GetById(int brandId)
        {
            Entity.Inventory.StoreMaster StoreMaster = new Entity.Inventory.StoreMaster();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@StoreId", brandId);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Inventory_StoreMaster_GetById";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            StoreMaster.StoreId = brandId;
                            StoreMaster.StoreName = dr["StoreName"].ToString();
                            StoreMaster.Location = dr["Location"].ToString();
                            StoreMaster.Phone = dr["Phone"].ToString();
                            StoreMaster.ContactPerson = Convert.ToInt32(dr["ContactPerson"].ToString());
                            StoreMaster.IsActive = Convert.ToBoolean(dr["IsActive"].ToString());
                        }
                    }
                    con.Close();
                    return StoreMaster;
                }
            }
        }

        public static int Delete(int storeId)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@StoreId", storeId);

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Inventory_StoreMaster_Delete";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                    con.Close();
                    return retValue;
                }
            }
        }
    }
}
