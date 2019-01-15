using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class Holiday
    {
        public Holiday()
        {

        }

        public static DataTable Holiday_GetAll(Entity.HR.Holiday holiday)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Holiday_GetAll";
                        if (holiday.HolidayProfileId == 0)
                            cmd.Parameters.AddWithValue("@HolidayProfileId", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@HolidayProfileId", holiday.HolidayProfileId);
                        if (holiday.HolidayYear == 0)
                            cmd.Parameters.AddWithValue("@HolidayYear", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@HolidayYear", holiday.HolidayYear);
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

        public static DataTable Holiday_GetById(int holidayId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Holiday_GetById";
                        cmd.Parameters.AddWithValue("@HolidayId", holidayId);
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

        public static int Holiday_Save(Entity.HR.Holiday holiday)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Holiday_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HolidayId", holiday.HolidayId);
                    cmd.Parameters.AddWithValue("@HolidayProfileId", holiday.HolidayProfileId);
                    cmd.Parameters.AddWithValue("@HolidayName", holiday.HolidayName);
                    cmd.Parameters.AddWithValue("@HolidayDate", holiday.HolidayDate);
                    cmd.Parameters.AddWithValue("@HolidayDescription", holiday.HolidayDescription);
                    cmd.Parameters.AddWithValue("@Show", holiday.Show);
                    cmd.Parameters.AddWithValue("@CreatedBy", holiday.CreatedBy);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Holiday_Delete(int holidayId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Holiday_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HolidayId", holidayId);
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
