using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Entity.HR;

namespace DataAccess.HR
{
    public class EmployeeMaster
    {
        public static int EmployeeSave(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Employee_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeMasterId", ObjElEmployeeMaster.EmployeeMasterId);
                    cmd.Parameters.AddWithValue("@EmployeeMasterName", ObjElEmployeeMaster.EmployeeName);
                    if (ObjElEmployeeMaster.Image == "")
                        cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Image", ObjElEmployeeMaster.Image);
                    cmd.Parameters.AddWithValue("@GenderId", ObjElEmployeeMaster.GenderId);
                    cmd.Parameters.AddWithValue("@DOB", ObjElEmployeeMaster.DOB);
                    cmd.Parameters.AddWithValue("@MaritalStatus", ObjElEmployeeMaster.MaritalStatus);
                    if (ObjElEmployeeMaster.DOM == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@DOM", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@DOM", ObjElEmployeeMaster.DOM);
                    cmd.Parameters.AddWithValue("@ReligionId_FK", ObjElEmployeeMaster.ReligionId_FK);
                    cmd.Parameters.AddWithValue("@BloodGroup", ObjElEmployeeMaster.BloodGroup);
                    cmd.Parameters.AddWithValue("@PersonalMobileNo", ObjElEmployeeMaster.PersonalMobileNo);
                    cmd.Parameters.AddWithValue("@OfficeMobileNo", ObjElEmployeeMaster.OfficeMobileNo);
                    cmd.Parameters.AddWithValue("@PersonalEmailId", ObjElEmployeeMaster.PersonalEmailId);
                    cmd.Parameters.AddWithValue("@OfficeEmailId", ObjElEmployeeMaster.OfficeEmailId);
                    if (ObjElEmployeeMaster.ReferenceEmployeeId == 0)
                        cmd.Parameters.AddWithValue("@ReferenceEmployeeId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ReferenceEmployeeId", ObjElEmployeeMaster.ReferenceEmployeeId);
                    cmd.Parameters.AddWithValue("@pAddress", ObjElEmployeeMaster.PAddress);
                    cmd.Parameters.AddWithValue("@pCityId", ObjElEmployeeMaster.PCityId_FK);
                    cmd.Parameters.AddWithValue("@pPIN", ObjElEmployeeMaster.PPIN);
                    cmd.Parameters.AddWithValue("@UserId", ObjElEmployeeMaster.UserId);
                    cmd.Parameters.AddWithValue("@EmployeeJobId", ObjElEmployeeMaster.EmployeeJobId);
                    cmd.Parameters.AddWithValue("@DesignationMasterId_FK", ObjElEmployeeMaster.DesignationMasterId_FK);
                    if (ObjElEmployeeMaster.DOJ == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@DOJ", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@DOJ", ObjElEmployeeMaster.DOJ);
                    cmd.Parameters.AddWithValue("@JobTypeId", ObjElEmployeeMaster.ReferenceEmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeCode", ObjElEmployeeMaster.EmployeeCode);
                    cmd.Parameters.AddWithValue("@Password", ObjElEmployeeMaster.Password);
                    if (ObjElEmployeeMaster.TAddress == "")
                        cmd.Parameters.AddWithValue("@tAddress", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tAddress", ObjElEmployeeMaster.TAddress);
                    if (ObjElEmployeeMaster.TCityId_FK == 0)
                        cmd.Parameters.AddWithValue("@tCityId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tCityId", ObjElEmployeeMaster.TCityId_FK);
                    if (ObjElEmployeeMaster.TPIN == "")
                        cmd.Parameters.AddWithValue("@tPIN", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tPIN", ObjElEmployeeMaster.TPIN);
                    cmd.Parameters.AddWithValue("@CompanyId_FK", ObjElEmployeeMaster.CompanyId_FK);

                    cmd.Parameters.AddWithValue("@RoleId", ObjElEmployeeMaster.RoleId);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    try
                    {
                        using (DataTable dt = new DataTable())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }
                            rowsAffacted = (ObjElEmployeeMaster.EmployeeMasterId > 0) ?
                                ObjElEmployeeMaster.EmployeeMasterId : int.Parse(dt.Rows[0]["EmployeeMasterId"].ToString());
                        }
                    }
                    catch
                    {
                    }
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int LeaveDesignationConfig_Delete(LeaveMaster objElEmployeeMaster)
        {
            throw new NotImplementedException();
        }

        public static int EmployeeDelete(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Customer_Customer_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("", ObjElEmployeeMaster.EmployeeMasterId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable EmployeeMaster_GetAll(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_HR_EmployeeMaster_GetAll";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CompanyId_FK", ObjElEmployeeMaster.CompanyId_FK);
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

        public static DataTable EmployeeMaster_ById(Entity.HR.EmployeeMaster employeeMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_HR_EmployeeMaster_ById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeMasterId", employeeMaster.EmployeeMasterId);
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

        public static DataTable City_GetAll()
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_Common_City_GetAll";
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

        public static DataTable DesignationMaster_GetAll(Entity.HR.EmployeeMaster employeeMaster)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_DesignationMaster_GetAll";
                        cmd.Parameters.AddWithValue("@DesignationName", DBNull.Value);
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

        public static int DeleteEmployee(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeMaster_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeMasterId", ObjElEmployeeMaster.EmployeeMasterId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static SqlDataReader ViewEmployeeById(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeMaster_ViewDetailById";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeMasterId", ObjElEmployeeMaster.EmployeeMasterId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    con.Close();
                    return dr;
                }
            }
        }

        public static Entity.HR.EmployeeMaster AuthenticateUser(string UserName)
        {
            Entity.HR.EmployeeMaster user = new Entity.HR.EmployeeMaster();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetUserNameAndPass";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeCode", UserName);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.UserId = (dr[0] == DBNull.Value) ? 0 : Convert.ToInt32(dr[0].ToString());
                            user.EmployeeCode = (dr[1] == DBNull.Value) ? "" : dr[1].ToString();
                            user.Password = (dr[2] == DBNull.Value) ? "" : dr[2].ToString();
                            user.Roles = (dr[3] == DBNull.Value) ? "" : dr[3].ToString();
                            user.EmployeeName = (dr[4] == DBNull.Value) ? "" : dr[4].ToString();
                        }

                        con.Close();
                        return user;
                    }

                    con.Close();
                    return null;
                }
            }
        }

        public static int Login_Save(Entity.Common.Auth auth)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_Login_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", auth.UserId);
                    cmd.Parameters.AddWithValue("@IP", auth.IP);
                    cmd.Parameters.AddWithValue("@Status", Enum.GetName(typeof(Entity.Common.LoginStatus), auth.Status));
                    cmd.Parameters.AddWithValue("@Client", auth.Client);
                    cmd.Parameters.AddWithValue("@FailedUserName", auth.FailedUserName);
                    cmd.Parameters.AddWithValue("@FailedPassword", auth.FailedPassword);

                    cmd.CommandType = CommandType.StoredProcedure;
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

