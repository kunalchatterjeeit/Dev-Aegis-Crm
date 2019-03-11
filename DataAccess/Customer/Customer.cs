using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccess.Common;

namespace DataAccess.Customer
{
    public class Customer
    {
        public static long Save(Entity.Customer.Customer customer)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_Customer_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerMasterId", customer.CustomerMasterId);
                    cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerType", customer.CustomerType);
                    cmd.Parameters.AddWithValue("@ReferenceName", customer.ReferenceName);
                    cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                    cmd.Parameters.AddWithValue("@PhoneNo", customer.PhoneNo);
                    cmd.Parameters.AddWithValue("@EmailId", customer.EmailId);
                    cmd.Parameters.AddWithValue("@pAddress", customer.PAddress);
                    cmd.Parameters.AddWithValue("@pStreet", customer.PStreet);
                    cmd.Parameters.AddWithValue("@Pin", customer.Pin);
                    cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                    cmd.Parameters.AddWithValue("@CompanyMasterId_FK", customer.CompanyMasterId_FK);
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataSet GetAllCustomer(Entity.Customer.Customer customer)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_Customer_GetAll";
                        cmd.Parameters.AddWithValue("@CompanyId", customer.CompanyMasterId_FK);
                        cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        cmd.Parameters.AddWithValue("@EmailId", customer.EmailId);
                        cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        cmd.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
                        cmd.InsertPaging(customer, customer.CustomerMasterId);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }

        public static DataTable Customer_Customer_GetByAssignEngineerId(Entity.Customer.Customer customer)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_Customer_GetByAssignEngineerId";
                        if (string.IsNullOrEmpty(customer.CustomerName))
                            cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        if (string.IsNullOrEmpty(customer.EmailId))
                            cmd.Parameters.AddWithValue("@EmailId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmailId", customer.EmailId);
                        if (string.IsNullOrEmpty(customer.MobileNo))
                            cmd.Parameters.AddWithValue("@MobileNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        if (string.IsNullOrEmpty(customer.CustomerCode))
                            cmd.Parameters.AddWithValue("@CustomerCode", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
                        if (customer.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", customer.AssignEngineer);
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

        public static DataSet Customer_CustomerMaster_GetByAssignEngineerIdWithPaging(Entity.Customer.Customer customer)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_CustomerMaster_GetByAssignEngineerIdWithPaging";
                        if (string.IsNullOrEmpty(customer.CustomerName))
                            cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        if (string.IsNullOrEmpty(customer.EmailId))
                            cmd.Parameters.AddWithValue("@EmailId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmailId", customer.EmailId);
                        if (string.IsNullOrEmpty(customer.MobileNo))
                            cmd.Parameters.AddWithValue("@MobileNo", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                        if (string.IsNullOrEmpty(customer.CustomerCode))
                            cmd.Parameters.AddWithValue("@CustomerCode", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerCode", customer.CustomerCode);
                        if (customer.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignEngineer", customer.AssignEngineer);
                        cmd.InsertPaging(customer, customer.CustomerMasterId);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }

        public static DataTable FetchCustomerDetailsById(Entity.Customer.Customer ObjElCustomer)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_Customer_GetById";
                        cmd.Parameters.AddWithValue("@CustomerMasterId", ObjElCustomer.CustomerMasterId);
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

        public static int DeleteCustomer(Entity.Customer.Customer ObjElCustomer)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_Customer_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerMasterId", ObjElCustomer.CustomerMasterId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static string CustomerPurchase_Save(Entity.Customer.Customer customer)
        {
            string retValue = string.Empty;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerPurchase_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerPurchaseId", customer.CustomerPurchaseId);
                    cmd.Parameters.AddWithValue("@CustomerMasterId_FK", customer.CustomerMasterId);
                    cmd.Parameters.AddWithValue("@ProductMasterId_FK", customer.Productid);
                    cmd.Parameters.AddWithValue("@ProductSerialNo", customer.SerialNo);
                    cmd.Parameters.Add("@MachineId", SqlDbType.VarChar, 50, customer.MachineId);
                    cmd.Parameters["@MachineId"].Direction = ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@UserId", customer.UserId);
                    cmd.Parameters.AddWithValue("@CompanyMasterId_FK", customer.CompanyMasterId_FK);
                    cmd.Parameters.AddWithValue("@ContactPerson", customer.ContactPerson);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);
                    cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                    if (customer.InstallationDate == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@InstallationDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@InstallationDate", customer.InstallationDate);
                    if (customer.AssignEngineer == 0)
                        cmd.Parameters.AddWithValue("@AssignEngineer", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@AssignEngineer", customer.AssignEngineer);
                    if (string.IsNullOrEmpty(customer.Stamp))
                        cmd.Parameters.AddWithValue("@Stamp", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Stamp", customer.Stamp);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    cmd.ExecuteNonQuery();
                    retValue = cmd.Parameters["@MachineId"].Value.ToString();
                    con.Close();
                }
            }
            return retValue;
        }

        public static DataTable CustomerPurchase_GetByCustomerId(int customerid)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Customer_CustomerPurchase_GetByCustomerId";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", customerid);
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

        public static DataTable Customer_CustomerPurchase_GetByCustomerId_ForTonner(int customerid)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Customer_CustomerPurchase_GetByCustomerId_ForTonner";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CustomerId", customerid);
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

        public static Entity.Customer.Customer CustomerPurchase_GetByCustomerPurchaseId(int customerpurchaseid)
        {
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Customer_CustomerPurchase_GetByCustomerPurchaseId";
                    cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerpurchaseid);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            customer.CustomerPurchaseId = customerpurchaseid;
                            customer.CustomerMasterId = (dr["CustomerMasterId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["CustomerMasterId_FK"].ToString());
                            customer.BrandId = (dr["BrandId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["BrandId_FK"].ToString());
                            customer.Productid = (dr["ProductMasterId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["ProductMasterId_FK"].ToString());
                            customer.SerialNo = (dr["ProductSerialNo"] == DBNull.Value) ? string.Empty : dr["ProductSerialNo"].ToString();
                            customer.MachineId = (dr["MachineId"] == DBNull.Value) ? string.Empty : dr["MachineId"].ToString();
                            customer.ContactPerson = (dr["ContactPerson"] == DBNull.Value) ? string.Empty : dr["ContactPerson"].ToString();
                            customer.Address = (dr["Address"] == DBNull.Value) ? string.Empty : dr["Address"].ToString();
                            customer.PhoneNo = (dr["Phone"] == DBNull.Value) ? string.Empty : dr["Phone"].ToString();
                            customer.MobileNo = (dr["MobileNo"] == DBNull.Value) ? string.Empty : dr["MobileNo"].ToString();
                            customer.AssignEngineer = (dr["AssignEngineer"] == DBNull.Value) ? 0 : int.Parse(dr["AssignEngineer"].ToString());
                            customer.InstallationDate = (dr["InstallationDate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr["InstallationDate"].ToString());
                            customer.A3BWMeterReading = (dr["A3BWLastMeterReading"] == DBNull.Value) ? 0 : Int64.Parse(dr["A3BWLastMeterReading"].ToString());
                            customer.A3CLMeterReading = (dr["A3CLLastMeterReading"] == DBNull.Value) ? 0 : Int64.Parse(dr["A3CLLastMeterReading"].ToString());
                            customer.A4BWMeterReading = (dr["A4BWLastMeterReading"] == DBNull.Value) ? 0 : Int64.Parse(dr["A4BWLastMeterReading"].ToString());
                            customer.A4CLMeterReading = (dr["A4CLLastMeterReading"] == DBNull.Value) ? 0 : Int64.Parse(dr["A4CLLastMeterReading"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return customer;
        }

        public static int CustomerPurchase_DeleteByCustomerPurchaseId(long customerpurchaseid)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerPurchase_DeleteByCustomerPurchaseId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerPurchaseId", customerpurchaseid);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }

        public static int Customer_CustomerPurchaseAssignEngineer_Save(long customerpurchaseid, int assignedEngineerId)
        {
            int i = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerPurchaseAssignEngineer_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerPurchaseid", customerpurchaseid);
                    cmd.Parameters.AddWithValue("@AssignedEngineerId", assignedEngineerId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return i;
        }

        public static DataTable CustomerAuthentication(string emailId, string mobileNo)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_CustomerAuthentication";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailId", emailId);
                        cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
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

        public static DataSet Customer_CustomerPurchase_GetAll(Entity.Customer.Customer customer)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Customer_CustomerPurchase_GetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (string.IsNullOrEmpty(customer.CustomerName))
                            cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                        if (string.IsNullOrEmpty(customer.SerialNo))
                            cmd.Parameters.AddWithValue("@ProductSerialNumber", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@ProductSerialNumber", customer.SerialNo);
                        if (customer.AssignEngineer == 0)
                            cmd.Parameters.AddWithValue("@AssignedEngineerId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AssignedEngineerId", customer.AssignEngineer);
                        cmd.InsertPaging(customer, customer.CustomerMasterId);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ds);
                        }
                        con.Close();
                    }
                }
                return ds;
            }
        }
    }
}
