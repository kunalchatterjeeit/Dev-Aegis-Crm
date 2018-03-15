using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DataAccess.Customer
{
    public class CustomerContactDetails
    {
        public int SaveContactDeatails(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerContactDetails_Save";
                    cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@CustomerAddressId", ObjElCustomerContactDetails.CustomerAddressId);
                    //cmd.Parameters.AddWithValue("@tAddress", ObjElCustomerContactDetails.TAddress);
                    //cmd.Parameters.AddWithValue("@tStreet", ObjElCustomerContactDetails.TStreet);
                    cmd.Parameters.AddWithValue("@CustomerContactDetailsId", ObjElCustomerContactDetails.CustomerContactDetailsId);
                    cmd.Parameters.AddWithValue("@CustomerMasterId_FK", ObjElCustomerContactDetails.CustomerMasterId_FK);
                    cmd.Parameters.AddWithValue("@ContactPerson", ObjElCustomerContactDetails.ContactPerson);
                    cmd.Parameters.AddWithValue("@CPDesignation", ObjElCustomerContactDetails.CPDesignation);
                    cmd.Parameters.AddWithValue("@CPPhoneNo", ObjElCustomerContactDetails.CPPhoneNo);
                    cmd.Parameters.AddWithValue("UserId", ObjElCustomerContactDetails.UserId);
                    cmd.Parameters.AddWithValue("CompanyMasterId_FK", ObjElCustomerContactDetails.CompanyMasterId_FK);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public DataTable GetAllACustomerContactDetails(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_CustomerContactDetails_GetAll";
                        cmd.Parameters.AddWithValue("@CustomerMasterId_FK", ObjElCustomerContactDetails.CustomerMasterId_FK);
                        cmd.Parameters.AddWithValue("CompanyMasterId_FK", ObjElCustomerContactDetails.CompanyMasterId_FK);
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

        public DataTable FetchCustomerContactDetailsById(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_Customer_CustomerContactDetails_GetById";
                        cmd.Parameters.AddWithValue("@CustomerContactDetailsId", ObjElCustomerContactDetails.CustomerContactDetailsId);
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

        public int DeleteCustomerContactDetailsById(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_CustomerContactDetails_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerContactDetailsId", ObjElCustomerContactDetails.CustomerContactDetailsId);
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
