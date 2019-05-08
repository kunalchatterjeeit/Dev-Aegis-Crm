using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace DataAccess.Purchase
{
    public class Purchase
    {
        public Purchase()
        { }

        public static int Purchase_Save(Entity.Purchase.Purchase purchase)
        {
            int purchaseId = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Purchase_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PurchaseId", purchase.PurchaseId).Direction = ParameterDirection.InputOutput;
                    if (purchase.PurchaseOrderNo != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@PurchaseOrderNo", purchase.PurchaseOrderNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurchaseOrderNo", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@VendorId", purchase.VendorId);
                    if (purchase.InvoiceNo != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@InvoiceNo", purchase.InvoiceNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceNo", DBNull.Value);
                    }
                    if (purchase.InvoiceDate != DateTime.MinValue)
                    {
                        cmd.Parameters.AddWithValue("@InvoiceDate", purchase.InvoiceDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceDate", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@PurchaseDate", purchase.PurchaseDate);
                    cmd.Parameters.AddWithValue("@BillAmount", purchase.BillAmount);
                    cmd.Parameters.AddWithValue("@PaymentAmount", purchase.PaymentAmount);
                    cmd.Parameters.AddWithValue("@CreatedBy", purchase.CreatedBy);
                    cmd.Parameters.AddWithValue("@Error", string.Empty).Direction = ParameterDirection.InputOutput;

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    purchaseId = Convert.ToInt32(cmd.Parameters["@PurchaseId"].Value);
                    con.Close();
                }
            }
            return purchaseId;
        }

        public static int PurchaseDetails_Save(Entity.Purchase.Purchase purchase)
        {
            int rowsAffacted = 0;

            foreach (Entity.Purchase.PurchaseDetails purchasedetails in purchase.PurchaseDetailsCollection)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_PurchaseDetails_Save";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PurchaseDetailsId", purchasedetails.PurchaseDetailsId);
                        cmd.Parameters.AddWithValue("@PurchaseId", purchasedetails.PurchaseId);
                        cmd.Parameters.AddWithValue("@ItemId", purchasedetails.ItemId);
                        cmd.Parameters.AddWithValue("@ItemType", purchasedetails.ItemType);
                        cmd.Parameters.AddWithValue("@ItemQty", purchasedetails.ItemQty);
                        cmd.Parameters.AddWithValue("@ItemRate", purchasedetails.ItemRate);
                        cmd.Parameters.AddWithValue("@GST", purchasedetails.GST);
                        cmd.Parameters.AddWithValue("@Discount", purchasedetails.Discount);
                        cmd.Parameters.AddWithValue("@HSNCode", purchasedetails.HsnCode);
                        cmd.Parameters.AddWithValue("@UOM", purchasedetails.Uom);
                        cmd.Parameters.AddWithValue("@EmployeeId", purchase.CreatedBy);
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

        public static DataTable Purchase_GetAll(Entity.Purchase.Purchase purchase)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Purchase_GetAll";
                        if (!string.IsNullOrEmpty(purchase.PurchaseOrderNo))
                        {
                            cmd.Parameters.Add("@PurchaseOrderNo", purchase.PurchaseOrderNo);
                        }
                        else
                        {
                            cmd.Parameters.Add("@PurchaseOrderNo", DBNull.Value);
                        }

                        if (purchase.VendorId > 0)
                        {
                            cmd.Parameters.Add("@VendorId", purchase.VendorId);
                        }
                        else
                        {
                            cmd.Parameters.Add("@VendorId", DBNull.Value);
                        }

                        if (!string.IsNullOrEmpty(purchase.InvoiceNo))
                        {
                            cmd.Parameters.Add("@InvoiceNo", purchase.InvoiceNo);
                        }
                        else
                        {
                            cmd.Parameters.Add("@InvoiceNo", DBNull.Value);
                        }

                        if (purchase.InvoiceFromDate != DateTime.MinValue)
                        {
                            cmd.Parameters.Add("@InvoiceFromDate", purchase.InvoiceFromDate);
                        }
                        else
                        {
                            cmd.Parameters.Add("@InvoiceFromDate", DBNull.Value);
                        }

                        if (purchase.InvoiceToDate != DateTime.MinValue)
                        {
                            cmd.Parameters.Add("@InvoiceToDate", purchase.InvoiceToDate);
                        }
                        else
                        {
                            cmd.Parameters.Add("@InvoiceToDate", DBNull.Value);
                        }

                        if (purchase.PurchaseFromDate != DateTime.MinValue)
                        {
                            cmd.Parameters.Add("@FromDate", purchase.PurchaseFromDate);
                        }
                        else
                        {
                            cmd.Parameters.Add("@FromDate", DBNull.Value);
                        }

                        if (purchase.PurchaseToDate != DateTime.MinValue)
                        {
                            cmd.Parameters.Add("@ToDate", purchase.PurchaseToDate);
                        }
                        else
                        {
                            cmd.Parameters.Add("@ToDate", DBNull.Value);
                        }

                        if (purchase.CreatedBy > 0)
                        {
                            cmd.Parameters.Add("@CreatedBy", purchase.CreatedBy);
                        }
                        else
                        {
                            cmd.Parameters.Add("@CreatedBy", DBNull.Value);
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

        public static DataTable PurchaseDetails_GetByPurchaseId(Int64 purchaseId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_PurchaseDetails_GetByPurchaseId";
                        cmd.Parameters.AddWithValue("@PurchaseId", purchaseId);
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

        public static int Purchase_Delete(int cityid)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@PurchaseId", cityid);
                    cmd.Parameters.AddWithValue("@Error", cityid);
                    cmd.Parameters["@Error"].Direction = ParameterDirection.Output;

                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Purchase_Delete";
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                    string error = cmd.Parameters["@Error"].Value.ToString();
                }
            }
            return rowsAffacted;
        }
    }
}

