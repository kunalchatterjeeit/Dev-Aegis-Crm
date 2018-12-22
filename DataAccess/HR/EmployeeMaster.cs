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
        public static int Employee_Save(Entity.HR.EmployeeMaster employeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Employee_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeMasterId", employeeMaster.EmployeeMasterId);
                    cmd.Parameters.AddWithValue("@EmployeeMasterName", employeeMaster.EmployeeName);
                    if (employeeMaster.Image == "")
                        cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Image", employeeMaster.Image);
                    cmd.Parameters.AddWithValue("@GenderId", employeeMaster.GenderId);
                    cmd.Parameters.AddWithValue("@DOB", employeeMaster.DOB);
                    cmd.Parameters.AddWithValue("@MaritalStatus", employeeMaster.MaritalStatus);
                    if (employeeMaster.DOM == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@DOM", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@DOM", employeeMaster.DOM);
                    cmd.Parameters.AddWithValue("@ReligionId_FK", employeeMaster.ReligionId_FK);
                    cmd.Parameters.AddWithValue("@BloodGroup", employeeMaster.BloodGroup);
                    cmd.Parameters.AddWithValue("@PersonalMobileNo", employeeMaster.PersonalMobileNo);
                    cmd.Parameters.AddWithValue("@OfficeMobileNo", employeeMaster.OfficeMobileNo);
                    cmd.Parameters.AddWithValue("@PersonalEmailId", employeeMaster.PersonalEmailId);
                    cmd.Parameters.AddWithValue("@OfficeEmailId", employeeMaster.OfficeEmailId);
                    if (employeeMaster.ReferenceEmployeeId == 0)
                        cmd.Parameters.AddWithValue("@ReferenceEmployeeId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@ReferenceEmployeeId", employeeMaster.ReferenceEmployeeId);
                    cmd.Parameters.AddWithValue("@pAddress", employeeMaster.PAddress);
                    cmd.Parameters.AddWithValue("@pCityId", employeeMaster.PCityId_FK);
                    cmd.Parameters.AddWithValue("@pPIN", employeeMaster.PPIN);
                    cmd.Parameters.AddWithValue("@UserId", employeeMaster.UserId);
                    cmd.Parameters.AddWithValue("@EmployeeJobId", employeeMaster.EmployeeJobId);
                    cmd.Parameters.AddWithValue("@DesignationMasterId_FK", employeeMaster.DesignationMasterId_FK);
                    if (employeeMaster.DOJ == DateTime.MinValue)
                        cmd.Parameters.AddWithValue("@DOJ", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@DOJ", employeeMaster.DOJ);
                    cmd.Parameters.AddWithValue("@JobTypeId", employeeMaster.ReferenceEmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeMaster.EmployeeCode);
                    cmd.Parameters.AddWithValue("@Password", employeeMaster.Password);
                    if (employeeMaster.TAddress == "")
                        cmd.Parameters.AddWithValue("@tAddress", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tAddress", employeeMaster.TAddress);
                    if (employeeMaster.TCityId_FK == 0)
                        cmd.Parameters.AddWithValue("@tCityId", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tCityId", employeeMaster.TCityId_FK);
                    if (employeeMaster.TPIN == "")
                        cmd.Parameters.AddWithValue("@tPIN", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@tPIN", employeeMaster.TPIN);
                    cmd.Parameters.AddWithValue("@CompanyId_FK", employeeMaster.CompanyId_FK);

                    cmd.Parameters.AddWithValue("@RoleId", employeeMaster.RoleId);
                    cmd.Parameters.AddWithValue("@ReportingEmployeeId", employeeMaster.ReportingEmployeeId);

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
                            rowsAffacted = (employeeMaster.EmployeeMasterId > 0) ?
                                employeeMaster.EmployeeMasterId : int.Parse(dt.Rows[0]["EmployeeMasterId"].ToString());
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

        public static int EmployeeDelete(Entity.HR.EmployeeMaster objElEmployeeMaster)
        {
            throw new NotImplementedException();
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

