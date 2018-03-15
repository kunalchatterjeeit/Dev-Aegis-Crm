using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.Customer
{
    public class CustomerAddress
    {
        public int SaveCustomerAddress(Entity.Customer.CustomerAddress ObjElCustomerAddress)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerAddress_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerAddressId", ObjElCustomerAddress.CustomerAddressId);
                    cmd.Parameters.AddWithValue("@tAddress", ObjElCustomerAddress.TAddress);
                    cmd.Parameters.AddWithValue("@tStreet", ObjElCustomerAddress.TStreet);
                    cmd.Parameters.AddWithValue("@CustomerMasterId_FK", ObjElCustomerAddress.CustomerMasterId_FK);
                    cmd.Parameters.AddWithValue("UserId", ObjElCustomerAddress.UserId);
                    cmd.Parameters.AddWithValue("CompanyMasterId_FK", ObjElCustomerAddress.CompanyMasterId_FK);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public DataTable GetAllAddress(Entity.Customer.CustomerAddress ObjElCustomerAddress)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_CustomerAddress_GetAll";
                        cmd.Parameters.AddWithValue("@CustomerMasterId_FK", ObjElCustomerAddress.CustomerMasterId_FK);
                        cmd.Parameters.AddWithValue("CompanyMasterId_FK", ObjElCustomerAddress.CompanyMasterId_FK);
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

        public DataTable FetchCustomerAddressDetailsById(Entity.Customer.CustomerAddress ObjElCustomerAddress)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_CustomerAddress_GeyById";
                        cmd.Parameters.AddWithValue("@CustomerAddressId", ObjElCustomerAddress.CustomerAddressId);
                        //cmd.Parameters.AddWithValue("CompanyMasterId_FK", ObjElCustomerAddress.CompanyMasterId_FK);
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

        public int DeleteCustomerAddressById(Entity.Customer.CustomerAddress ObjElCustomerAddress)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerAddress_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerAddressId", ObjElCustomerAddress.CustomerAddressId);
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
