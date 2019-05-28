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
                    catch(Exception ex)
                    {
                    }
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int HR_PasswordReset_Save(Entity.HR.EmployeeMaster employeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_PasswordReset_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeMasterId", employeeMaster.EmployeeMasterId);
                    cmd.Parameters.AddWithValue("@Password", employeeMaster.Password);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
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
                            user.UserId = (dr["EmployeeMasterId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["EmployeeMasterId"].ToString());
                            user.EmployeeCode = (dr["EmployeeCode"] == DBNull.Value) ? "" : dr["EmployeeCode"].ToString();
                            user.Password = (dr["Password"] == DBNull.Value) ? "" : dr["Password"].ToString();
                            user.Roles = (dr["Roles"] == DBNull.Value) ? "" : dr["Roles"].ToString();
                            user.EmployeeName = (dr["EmployeeName"] == DBNull.Value) ? "" : dr["EmployeeName"].ToString();
                            user.IsActive = (dr["IsActive"] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsActive"].ToString());
                            user.IsLoginActive = (dr["IsLoginActive"] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsLoginActive"].ToString());
                            user.IsPasswordChangeRequired = (dr["IsPasswordChangeRequired"] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsPasswordChangeRequired"].ToString());
                            user.Image = (dr["Image"] == DBNull.Value) ? "" : dr["Image"].ToString();
                            user.GenderId = (dr["GenderId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["GenderId"].ToString());
                        }

                        con.Close();
                        return user;
                    }

                    con.Close();
                    return null;
                }
            }
        }

        public static Entity.HR.EmployeeMaster AutoAuthenticateUserByDevice(string deviceId)
        {
            Entity.HR.EmployeeMaster user = new Entity.HR.EmployeeMaster();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "GetUserNameByDevice";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DeviceId", deviceId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            user.UserId = (dr["EmployeeMasterId"] == DBNull.Value) ? 0 : Convert.ToInt32(dr["EmployeeMasterId"].ToString());
                            user.EmployeeCode = (dr["EmployeeCode"] == DBNull.Value) ? "" : dr["EmployeeCode"].ToString();
                            user.Roles = (dr["Roles"] == DBNull.Value) ? "" : dr["Roles"].ToString();
                            user.EmployeeName = (dr["EmployeeName"] == DBNull.Value) ? "" : dr["EmployeeName"].ToString();
                            user.IsActive = (dr["IsActive"] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsActive"].ToString());
                            user.IsLoginActive = (dr["IsLoginActive"] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsLoginActive"].ToString());
                            user.IsPasswordChangeRequired = (dr["IsPasswordChangeRequired"] == DBNull.Value) ? false : Convert.ToBoolean(dr["IsPasswordChangeRequired"].ToString());
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

        public static int EmployeeLeave_Update(Entity.HR.EmployeeMaster employeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeLeave_Update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeMaster.EmployeeMasterId);
                    cmd.Parameters.AddWithValue("@LeaveStatus", employeeMaster.LeaveActive);

                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int LinkedDevices_Save(int employeeId, string deviceId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_LinkedDevices_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                    cmd.Parameters.AddWithValue("@DeviceId", deviceId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable LinkedDevices_GetByUserId(int userId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "usp_HR_LinkedDevices_GetByUserId";
                        cmd.Parameters.AddWithValue("@EmployeeId", userId);
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

        public static int LiknedDevices_Delete(int linkedDeviceId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_LiknedDevices_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LinkedDeviceId", linkedDeviceId);

                    cmd.CommandType = CommandType.StoredProcedure;
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Employee_Update(Entity.HR.EmployeeMaster employeeMaster)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Employee_Update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeMasterId", employeeMaster.EmployeeMasterId);
                    if (employeeMaster.Image == "")
                        cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Image", employeeMaster.Image);
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

