using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Inventory
{
    public class Inventory
    {
        public Inventory()
        { }

        public static int Inventory_Save(Entity.Inventory.Inventory inventory)
        {
            int rowsAffacted = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DataSet dsInventory = new DataSet())
                    {
                        dsInventory.Tables.Add(inventory.InventoryDetails);
                        cmd.Parameters.AddWithValue("@InventoryDetails", dsInventory.GetXml());
                    }
                    cmd.Parameters.AddWithValue("@Error", string.Empty).Direction = ParameterDirection.InputOutput;

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted += cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return rowsAffacted;
        }

        public static DataTable Inventory_GetAll(DateTime fromDate, DateTime toDate)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Inventory_GetAll";

                        if (fromDate != DateTime.MinValue)
                        {
                            cmd.Parameters.AddWithValue("@FromDate", fromDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
                        }

                        if (toDate != DateTime.MinValue)
                        {
                            cmd.Parameters.AddWithValue("@ToDate", toDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
                        }

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

        public static DataTable Inventory_Transaction_GetByInventoryId(Int64 inventoryId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Inventory_Transaction_GetByInventoryId";
                        cmd.Parameters.AddWithValue("@InventoryId", inventoryId);
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

        public static DataTable Inventory_GetApprovedInventorySpareByServiceBookId(long serviceBookId, AssetLocation assetLocation, ItemType itemType)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Inventory_GetApprovedInventorySpareByServiceBookId";
                        if (serviceBookId == 0)
                            cmd.Parameters.AddWithValue("@ServiceBookId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ServiceBookId", serviceBookId);
                        cmd.Parameters.AddWithValue("@AssetLocationId", (int)assetLocation);
                        cmd.Parameters.AddWithValue("@ItemType", (int)itemType);
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

        public static DataTable Inventory_GetInventoryItem(AssetLocation assetLocation, ItemType itemType, string itemName)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Inventory_GetInventoryItem";
                        cmd.Parameters.AddWithValue("@AssetLocationId", (int)assetLocation);
                        cmd.Parameters.AddWithValue("@ItemType", (int)itemType);
                        cmd.Parameters.AddWithValue("@ItemName", itemName);
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

        public static int Inventory_Movement(Entity.Inventory.Inventory inventory)
        {
            int rowsAffacted = 0;

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_Movement";
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (DataRow dr in inventory.InventoryDetails.Rows)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@AssetId", dr["AssetId"].ToString());
                        cmd.Parameters.AddWithValue("@AssetLocationId", dr["AssetLocationId"].ToString());
                        cmd.Parameters.AddWithValue("@CustomerId", dr["CustomerId"].ToString());
                        cmd.Parameters.AddWithValue("@SaleChallanId", dr["SaleChallanId"].ToString());
                        cmd.Parameters.AddWithValue("@EmployeeId", dr["EmployeeId"].ToString());

                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        rowsAffacted += cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

            return rowsAffacted;
        }
    }
}
