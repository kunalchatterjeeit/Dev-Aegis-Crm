﻿using System;
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
                            cmd.Parameters.Add("@FromDate", fromDate);                        
                        }
                        else 
                        {
                            cmd.Parameters.Add("@FromDate", DBNull.Value);
                        }

                        if (toDate != DateTime.MinValue)
                        {
                            cmd.Parameters.Add("@ToDate", toDate);
                        }
                        else
                        {
                            cmd.Parameters.Add("@ToDate", DBNull.Value);
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
                        cmd.Parameters.Add("@InventoryId", inventoryId);
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