using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Sale
{
    public class SaleChallan
    {
        public SaleChallan()
        { }

        public static int SaleChallan_Save(Entity.Sale.SaleChallan saleChallan)
        {
            int purchaseId = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Sale_Challan_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SaleChallanId", saleChallan.SaleChallanId).Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.AddWithValue("@CustomerMasterId", saleChallan.CustomerMasterId);

                    if (saleChallan.Note != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@Note", saleChallan.Note);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Note", DBNull.Value);
                    }
                    if (saleChallan.ChallanNo != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@ChallanNo", saleChallan.ChallanNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ChallanNo", DBNull.Value);
                    }
                    if (saleChallan.OrderNo != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@OrderNo", saleChallan.OrderNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OrderNo", DBNull.Value);
                    }
                    if (saleChallan.OrderDate != DateTime.MinValue)
                    {
                        cmd.Parameters.AddWithValue("@OrderDate", saleChallan.OrderDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@OrderDate", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@ChallanType", saleChallan.CallanTypeId);
                    cmd.Parameters.AddWithValue("@CreatedBy", saleChallan.CreatedBy);
                    cmd.Parameters.AddWithValue("@Error", string.Empty).Direction = ParameterDirection.InputOutput;

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    purchaseId = Convert.ToInt32(cmd.Parameters["@SaleChallanId"].Value);
                    con.Close();

                    if (!string.IsNullOrEmpty(cmd.Parameters["@Error"].Value.ToString()))
                    {
                        throw new Exception(cmd.Parameters["@Error"].Value.ToString());
                    }
                }
            }
            return purchaseId;
        }

        public static int Sale_ChallanDetails_Save(Entity.Sale.SaleChallan saleChallan)
        {
            int rowsAffacted = 0;

            foreach (Entity.Sale.SaleChallanDetails saleChallanDetails in saleChallan.SaleChallanDetailsCollection)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Sale_ChallanDetails_Save";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SaleChallanDetailsId", saleChallanDetails.SaleChallanDetailsId);
                        cmd.Parameters.AddWithValue("@SaleChallanId", saleChallanDetails.SaleChallanId);
                        cmd.Parameters.AddWithValue("@ItemId", saleChallanDetails.ItemId);
                        cmd.Parameters.AddWithValue("@ItemType", saleChallanDetails.ItemType);
                        cmd.Parameters.AddWithValue("@ItemQty", saleChallanDetails.ItemQty);
                        cmd.Parameters.AddWithValue("@ItemRate", saleChallanDetails.ItemRate);
                        cmd.Parameters.AddWithValue("@GST", saleChallanDetails.GST);
                        cmd.Parameters.AddWithValue("@HSNCode", saleChallanDetails.HsnCode);
                        cmd.Parameters.AddWithValue("@UOM", saleChallanDetails.Uom);
                        cmd.Parameters.AddWithValue("@Error", string.Empty).Direction = ParameterDirection.InputOutput;

                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        rowsAffacted += cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return rowsAffacted;
        }

        public static DataTable Sale_Challan_GetAll(Entity.Sale.SaleChallan saleChallan)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Sale_Challan_GetAll";
                        if (!string.IsNullOrEmpty(saleChallan.CustomerName))
                        {
                            cmd.Parameters.AddWithValue("@CustomerName", saleChallan.CustomerName);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);
                        }
                        if (!string.IsNullOrEmpty(saleChallan.ChallanNo))
                        {
                            cmd.Parameters.AddWithValue("@ChallanNo", saleChallan.ChallanNo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ChallanNo", DBNull.Value);
                        }
                        if (!string.IsNullOrEmpty(saleChallan.OrderNo))
                        {
                            cmd.Parameters.AddWithValue("@OrderNo", saleChallan.OrderNo);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@OrderNo", DBNull.Value);
                        }
                        if (saleChallan.OrderFromDate != DateTime.MinValue)
                        {
                            cmd.Parameters.AddWithValue("@OrderFromDate", saleChallan.OrderFromDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@OrderFromDate", DBNull.Value);
                        }
                        if (saleChallan.OrderToDate != DateTime.MinValue)
                        {
                            cmd.Parameters.AddWithValue("@OrderToDate", saleChallan.OrderToDate);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@OrderToDate", DBNull.Value);
                        }

                        if (saleChallan.CreatedBy > 0)
                        {
                            cmd.Parameters.AddWithValue("@CreatedBy", saleChallan.CreatedBy);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CreatedBy", DBNull.Value);
                        }
                        if (saleChallan.ItemId > 0)
                        {
                            cmd.Parameters.AddWithValue("@ItemId", saleChallan.ItemId);
                            cmd.Parameters.AddWithValue("@ItemType", (int)saleChallan.itemType);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ItemId", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ItemType", DBNull.Value);
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

        public static DataTable Sale_Challan_GetById(int saleChallanId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Sale_Challan_GetById";
                        cmd.Parameters.AddWithValue("@SaleChallanId", saleChallanId);

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

        public static DataTable SaleChallanDetails_GetBySaleChallanId(long saleChallanId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_SaleChallanDetails_GetBySaleChallanId";
                        cmd.Parameters.AddWithValue("@SaleChallanId", saleChallanId);
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

        public static int Sale_Challan_Delete(int saleChallanId)
        {
            int response = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Sale_Challan_Delete";
                    cmd.Parameters.AddWithValue("@SaleChallanId", saleChallanId);
                    cmd.Parameters.AddWithValue("@Error", string.Empty).Direction = ParameterDirection.InputOutput;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    response = cmd.ExecuteNonQuery();
                    string error = cmd.Parameters["@Error"].Value.ToString();
                    if (!string.IsNullOrEmpty(error))
                        throw new Exception(error);
                    con.Close();
                }
                return response;
            }
        }
    }
}
