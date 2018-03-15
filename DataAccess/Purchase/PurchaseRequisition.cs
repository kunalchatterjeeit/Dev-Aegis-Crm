using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Purchase
{
    public class PurchaseRequisition
    {
        public PurchaseRequisition()
        { }

        public static Int64 PurchaseRequisition_Save(Entity.Purchase.PurchaseRequisition purchase)
        {
            Int64 purchaseId = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Purchase_RequisitionMaster_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PurchaseRequisitionId", purchase.PurchaseRequisitionId).Direction = ParameterDirection.InputOutput;
                    if (purchase.@PurchaseRequisitionNo != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRequisitionNo", purchase.@PurchaseRequisitionNo);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurchaseRequisitionNo", DBNull.Value);
                    }
                    if (purchase.RequisitionDate != DateTime.MinValue)
                    {
                        cmd.Parameters.AddWithValue("@RequisitionDate", purchase.RequisitionDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RequisitionDate", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@PurchaseDepartment", purchase.PurchaseDepartment);
                    cmd.Parameters.AddWithValue("@VendorId", purchase.VendorId);
                    if (purchase.WhenNeeded != DateTime.MinValue)
                    {
                        cmd.Parameters.AddWithValue("@WhenNeeded", purchase.WhenNeeded);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@WhenNeeded", DBNull.Value);
                    }
                    if (purchase.PurposeOfRequisition != string.Empty)
                    {
                        cmd.Parameters.AddWithValue("@PurposeOfRequisition", purchase.PurposeOfRequisition);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PurposeOfRequisition", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@CreatedBy", purchase.CreatedBy);
                    cmd.Parameters.AddWithValue("@Error", string.Empty).Direction = ParameterDirection.InputOutput;

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    purchaseId = Convert.ToInt64(cmd.Parameters["@PurchaseRequisitionId"].Value);
                    con.Close();
                }
            }
            return purchaseId;
        }

        public static int Purchase_RequisitionDetails_Save(Entity.Purchase.PurchaseRequisition purchase)
        {
            int rowsAffacted = 0;

            foreach (Entity.Purchase.PurchaseRequisitionDetails purchasedetails in purchase.PurchaseRequisitionDetailsCollection)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Purchase_RequisitionDetails_Save";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PurchaseRequisitionDetailsId", purchasedetails.PurchaseRequisitionDetailsId);
                        cmd.Parameters.AddWithValue("@PurchaseRequisitionId", purchasedetails.PurchaseRequisitionId);
                        cmd.Parameters.AddWithValue("@ItemId", purchasedetails.ItemId);
                        cmd.Parameters.AddWithValue("@ItemType", purchasedetails.ItemType);
                        cmd.Parameters.AddWithValue("@Quantity", purchasedetails.Quantity);
                        cmd.Parameters.AddWithValue("@UOM", purchasedetails.UOM);
                        cmd.Parameters.AddWithValue("@Description", purchasedetails.Description);
                        cmd.Parameters.AddWithValue("@ApprovalStatus", purchasedetails.ApprovalStatus);
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
                        cmd.Parameters.Add("@PurchaseId", purchaseId);
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
