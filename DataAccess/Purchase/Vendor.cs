using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess.Purchase
{
    public class Vendor
    {
        public Vendor()
        { }

        public static int Save(Entity.Purchase.Vendor vendor)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "usp_Purchase_VendorMaster_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@VendorId", vendor.VendorId);
                    cmd.Parameters.AddWithValue("@VendorName", vendor.VendorName);
                    cmd.Parameters.AddWithValue("@Address", vendor.Address);
                    cmd.Parameters.AddWithValue("@CountryId", vendor.CountryId);
                    cmd.Parameters.AddWithValue("@StateId", vendor.StateId);
                    cmd.Parameters.AddWithValue("@DistrictId", vendor.DistrictId);
                    cmd.Parameters.AddWithValue("@CityId", vendor.CityId);
                    cmd.Parameters.AddWithValue("@PinId", vendor.PinId);
                    cmd.Parameters.AddWithValue("@Tan", vendor.Tan);
                    cmd.Parameters.AddWithValue("@StateCode", vendor.StateCode);
                    cmd.Parameters.AddWithValue("@Pan", vendor.Pan);
                    cmd.Parameters.AddWithValue("@CST", vendor.CST);
                    cmd.Parameters.AddWithValue("@PhoneNo", vendor.PhoneNo);
                    cmd.Parameters.AddWithValue("@MobileNo", vendor.MobileNo);
                    cmd.Parameters.AddWithValue("@Fax", vendor.Fax);
                    cmd.Parameters.AddWithValue("@MailId", vendor.MailId);
                    cmd.Parameters.AddWithValue("@UserId", vendor.UserId);
                    cmd.Parameters.AddWithValue("@CompanyId", vendor.CompanyId);
                    if (vendor.ActiveDate == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@ActiveDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ActiveDate", vendor.ActiveDate);
                    cmd.Parameters.AddWithValue("@ConcernedPerson", vendor.ConcernedPerson);
                    cmd.Parameters.AddWithValue("@Bank", vendor.BankName);
                    cmd.Parameters.AddWithValue("@BankBranch", vendor.BankBranch);
                    cmd.Parameters.AddWithValue("@ACNo", vendor.ACNo);
                    cmd.Parameters.AddWithValue("@IFSC", vendor.IFSC);
                    cmd.Parameters.AddWithValue("@Status", vendor.Status);
                    cmd.Parameters.AddWithValue("@GSTNo", vendor.GSTNo);

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    retValue = cmd.ExecuteNonQuery();
                }
            }
            return retValue;
        }

        public static DataTable GetAll(Entity.Purchase.Vendor vendor)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Purchase_VendorMaster_GetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (vendor.CompanyId == 0)
                            cmd.Parameters.AddWithValue("@CompanyId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@CompanyId", vendor.CompanyId);

                        cmd.CommandType = CommandType.StoredProcedure;
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

        public static Entity.Purchase.Vendor GetById(int vendorId)
        {
            Entity.Purchase.Vendor vendor = new Entity.Purchase.Vendor();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "usp_Purchase_VendorMaster_GetById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@VendorId", vendorId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            vendor.VendorId = vendorId;
                            vendor.VendorName = (dr["VendorMasterName"] == DBNull.Value) ? string.Empty : dr["VendorMasterName"].ToString();
                            vendor.Address = (dr["Address"] == DBNull.Value) ? string.Empty : dr["Address"].ToString();
                            vendor.CountryId = (dr["CountryId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["CountryId_FK"].ToString());
                            vendor.StateId = (dr["StateId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["StateId_FK"].ToString());
                            vendor.DistrictId = (dr["DistrictId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["DistrictId_FK"].ToString());
                            vendor.CityId = (dr["CityId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["CityId_FK"].ToString());
                            vendor.PinId = (dr["PinId_FK"] == DBNull.Value) ? 0 : int.Parse(dr["PinId_FK"].ToString());
                            vendor.Tan = (dr["Tan"] == DBNull.Value) ? string.Empty : dr["Tan"].ToString();
                            vendor.StateCode = (dr["StateCode"] == DBNull.Value) ? string.Empty : dr["StateCode"].ToString();
                            vendor.Pan = (dr["Pan"] == DBNull.Value) ? string.Empty : dr["Pan"].ToString();
                            vendor.CST = (dr["CST"] == DBNull.Value) ? string.Empty : dr["CST"].ToString();
                            vendor.PhoneNo = (dr["Phone"] == DBNull.Value) ? string.Empty : dr["Phone"].ToString();
                            vendor.MobileNo = (dr["Mobile"] == DBNull.Value) ? string.Empty : dr["Mobile"].ToString();
                            vendor.Fax = (dr["Fax"] == DBNull.Value) ? string.Empty : dr["Fax"].ToString();
                            vendor.MailId = (dr["MailID"] == DBNull.Value) ? string.Empty : dr["MailID"].ToString();
                            vendor.ActiveDate = (dr["ActiveDate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(dr["ActiveDate"].ToString());
                            vendor.Status = Convert.ToBoolean(dr["Status"].ToString());
                            vendor.ConcernedPerson = (dr["ConcernedPerson"] == DBNull.Value) ? "" : dr["ConcernedPerson"].ToString();
                            vendor.BankName = (dr["Bank"] == DBNull.Value) ? "" : dr["Bank"].ToString();
                            vendor.BankBranch = (dr["BankBranch"] == DBNull.Value) ? "" : dr["BankBranch"].ToString();
                            vendor.ACNo = (dr["ACNo"] == DBNull.Value) ? "" : dr["ACNo"].ToString();
                            vendor.IFSC = (dr["IFSC"] == DBNull.Value) ? "" : dr["IFSC"].ToString();
                            vendor.GSTNo = dr["GSTNo"].ToString();
                        }
                    }
                }
            }
            return vendor;
        }

        public static int Delete(int vendorId)
        {
            int retValue = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "usp_Purchase_VendorMaster_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VendorId", vendorId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    retValue = cmd.ExecuteNonQuery();
                }
            }
            return retValue;
        }
    }
}
