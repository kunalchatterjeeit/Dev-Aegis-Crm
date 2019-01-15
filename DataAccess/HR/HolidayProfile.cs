using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class HolidayProfile
    {
        public HolidayProfile()
        {

        }

        public static DataTable HolidayProfile_GetAll(Entity.HR.HolidayProfile holidayProfile)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_HolidayProfile_GetAll";
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

        public static DataTable HolidayProfile_GetById(int holidayProfileId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_HolidayProfile_GetById";
                        cmd.Parameters.AddWithValue("@HolidayProfileId", holidayProfileId);
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

        public static int HolidayProfile_Save(Entity.HR.HolidayProfile holidayProfile)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_HolidayProfile_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HolidayProfileId", holidayProfile.HolidayProfileId);
                    cmd.Parameters.AddWithValue("@HolidayProfileName", holidayProfile.HolidayProfileName);
                    cmd.Parameters.AddWithValue("@HolidayProfileDescription", holidayProfile.HolidayProfileDescription);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int HolidayProfile_Delete(int holidayProfileId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_HolidayProfile_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HolidayProfileId", holidayProfileId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int EmployeeHolidayProfileMapping_Save(Entity.HR.EmployeeHolidayProfileMapping employeeHolidayProfileMapping)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeHolidayProfileMapping_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeHolidayProfileMappingId", employeeHolidayProfileMapping.EmployeeHolidayProfileMappingId);
                    cmd.Parameters.AddWithValue("@HolidayProfileId", employeeHolidayProfileMapping.HolidayProfileId);
                    cmd.Parameters.AddWithValue("@EmployeeId", employeeHolidayProfileMapping.EmployeeId);
                    cmd.Parameters.AddWithValue("@Active", employeeHolidayProfileMapping.Active);
                    cmd.Parameters.AddWithValue("@CreatedBy", employeeHolidayProfileMapping.CreatedBy);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable EmployeeHolidayProfileMapping_GetById(int employeeHolidayProfileMappingId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_EmployeeHolidayProfileMapping_GetById";
                        cmd.Parameters.AddWithValue("@EmployeeHolidayProfileMappingId", employeeHolidayProfileMappingId);
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

        public static int EmployeeHolidayProfileMapping_Delete(int employeeHolidayProfileMappingId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_EmployeeHolidayProfileMapping_delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmployeeHolidayProfileMappingId", employeeHolidayProfileMappingId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static DataTable EmployeeHolidayProfileMapping_GetAll(Entity.HR.EmployeeHolidayProfileMapping employeeHolidayProfileMapping)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_EmployeeHolidayProfileMapping_GetAll";
                        if (employeeHolidayProfileMapping.HolidayProfileId == 0)
                            cmd.Parameters.AddWithValue("@HolidayProfileId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@HolidayProfileId", employeeHolidayProfileMapping.HolidayProfileId);
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
    }
}
