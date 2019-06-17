using DataAccess.Common;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.HR
{
    public class Attendance
    {
        public Attendance()
        {

        }

        public static DataSet Attendance_GetAll(Entity.HR.Attendance attendance)
        {
            using (DataSet ds = new DataSet())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Attendance_GetAll";
                        if (string.IsNullOrEmpty(attendance.EmployeeName))
                            cmd.Parameters.AddWithValue("@EmployeeName", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@EmployeeName", attendance.EmployeeName);
                        if (attendance.AttendanceFromDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@AttendanceFromDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AttendanceFromDate", attendance.AttendanceFromDate);
                        if (attendance.AttendanceToDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@AttendanceToDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AttendanceToDate", attendance.AttendanceToDate);
                        cmd.InsertPaging(attendance, attendance.AttendanceId);
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

        public static DataTable Attendance_GetById(int attendanceId)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Attendance_GetById";
                        cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
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

        public static DataTable Attendance_GetByEmployeeId(int employeeId, DateTime attendanceDate)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_HR_Attendance_GetByEmployeeId";
                        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                        if (attendanceDate == DateTime.MinValue)
                            cmd.Parameters.AddWithValue("@AttendanceDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
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

        public static int Attendance_Save(Entity.HR.Attendance attendance)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Attendance_Save";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AttendanceId", attendance.AttendanceId);
                    cmd.Parameters.AddWithValue("@EmployeeId", attendance.EmployeeId);
                    cmd.Parameters.AddWithValue("@AttendanceDate", attendance.AttendanceDate);
                    cmd.Parameters.AddWithValue("@InDateTime", attendance.InDateTime);
                    cmd.Parameters.AddWithValue("@OutDateTime", attendance.OutDateTime);
                    cmd.Parameters.AddWithValue("@TotalHours", attendance.TotalHours);
                    cmd.Parameters.AddWithValue("@CreatedBy", attendance.CreatedBy);
                    cmd.Parameters.AddWithValue("@Latitude", attendance.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", attendance.Longitude);
                    cmd.Parameters.AddWithValue("@Source", attendance.Source);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Attendance_Delete(int attendanceId)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Attendance_Delete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Attendance_MarkLate(int attendanceId, bool isLate)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Attendance_MarkLate";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
                    cmd.Parameters.AddWithValue("@IsLate", isLate);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    rowsAffacted = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return rowsAffacted;
        }

        public static int Attendance_MarkHalfday(int attendanceId, bool isHalfday)
        {
            int rowsAffacted = 0;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_HR_Attendance_MarkHalfday";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AttendanceId", attendanceId);
                    cmd.Parameters.AddWithValue("@IsHalfDay", isHalfday);
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
