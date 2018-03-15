using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Entity;
using System.Configuration;

namespace DataAccess.Inventory
{
    public class ProductMaster
    {
        public ProductMaster()
        { }

        public static int Save(Entity.Inventory.ProductMaster productMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_Product_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductMasterId", productMaster.ProductMasterId);
                    cmd.Parameters.AddWithValue("@BrandId_FK", productMaster.BrandId);
                    cmd.Parameters.AddWithValue("@ProductCode", productMaster.ProductCode);
                    cmd.Parameters.AddWithValue("@ProductName", productMaster.ProductName);
                    cmd.Parameters.AddWithValue("@ProductSpecification", productMaster.ProductSpecification);
                    cmd.Parameters.AddWithValue("@MachineLife", productMaster.MachineLife);
                    cmd.Parameters.AddWithValue("@UserId", productMaster.UserId);
                    cmd.Parameters.AddWithValue("@MCBF", productMaster.MCBF);
                    cmd.Parameters.AddWithValue("@MTBF", productMaster.MTBF);
                    cmd.Parameters.AddWithValue("@CompanyMasterId", productMaster.CompanyMasterId);
                    cmd.Parameters.AddWithValue("@ProductCategoryId", productMaster.ProductCategoryId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable GetAll(Entity.Inventory.ProductMaster productMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Inventory_Product_GetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CompanyId", productMaster.CompanyMasterId);
                        if (productMaster.BrandId == 0)
                            cmd.Parameters.AddWithValue("@BrandId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@BrandId", productMaster.BrandId);
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

        public static Entity.Inventory.ProductMaster GetById(int ProductMasterId)
        {
            Entity.Inventory.ProductMaster productMaster = new Entity.Inventory.ProductMaster();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_Product_GetById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductMasterId", ProductMasterId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            productMaster.ProductMasterId = ProductMasterId;
                            productMaster.ProductCode = (dr["ProductCode"] == DBNull.Value) ? string.Empty : dr["ProductCode"].ToString();
                            productMaster.ProductName = (dr["ProductName"] == DBNull.Value) ? string.Empty : dr["ProductName"].ToString();
                            productMaster.ProductSpecification = (dr["ProductSpecification"] == DBNull.Value) ? string.Empty : dr["ProductSpecification"].ToString();
                            productMaster.MachineLife = (dr["MachineLife"] == DBNull.Value) ? 0 : int.Parse(dr["MachineLife"].ToString());
                            productMaster.BrandId = (dr["BrandId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["BrandId_FK"].ToString());
                            productMaster.MCBF = (dr["MCBF"] == DBNull.Value) ? 0 : int.Parse(dr["MCBF"].ToString());
                            productMaster.MTBF = (dr["MTBF"] == DBNull.Value) ? 0 : int.Parse(dr["MTBF"].ToString());
                            productMaster.ProductCategoryId = (dr["ProductCategoryId"] == DBNull.Value) ? 0 : int.Parse(dr["ProductCategoryId"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return productMaster;
        }

        public static int Delete(int ProductMasterId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_Product_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductMasterId", ProductMasterId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        //Product Spare mapping
        public static int ProductSpareMapping_Save(Entity.Inventory.ProductMaster productmaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_ProductSpareMapping_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(productmaster.dtMapping);
                    cmd.Parameters.AddWithValue("@ProductId", productmaster.ProductMasterId);
                    cmd.Parameters.AddWithValue("@MappingDetails", ds.GetXml());
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable ProductSpareMapping_GetById(int productid)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Inventory_ProductSpareMapping_GetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productid);
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

        public static int ProductSpareMapping_Delete(int ProductSpareMappingId)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_ProductSpareMapping_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductSpareMappingId", ProductSpareMappingId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }
        //Product Consumable Mapping
        public static int ProductConsumableMapping_Save(Entity.Inventory.ProductMaster productmaster)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_ProductConsumableMapping_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(productmaster.dtMapping);
                    cmd.Parameters.AddWithValue("@ProductId", productmaster.ProductMasterId);
                    cmd.Parameters.AddWithValue("@MappingDetails", ds.GetXml());
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }

        public static DataTable ProductConsumableMapping_GetById(int productid)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Inventory_ProductConsumableMapping_GetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProductId", productid);
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

        public static int ProductConsumableMapping_Delete(int ProductConsumableMappingId)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Inventory_ProductConsumableMapping_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductConsumableMappingId", ProductConsumableMappingId);

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
