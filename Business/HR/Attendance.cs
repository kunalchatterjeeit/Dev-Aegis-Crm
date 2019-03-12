using System;
using System.Data;

namespace Business.HR
{
    public class Attendance
    {
        public Attendance()
        {

        }
        public DataSet Attendance_GetAll(Entity.HR.Attendance attendance)
        {
            return DataAccess.HR.Attendance.Attendance_GetAll(attendance);
        }
        public DataTable Attendance_GetById(int attendanceId)
        {
            return DataAccess.HR.Attendance.Attendance_GetById(attendanceId);
        }
        public int Attendance_Save(Entity.HR.Attendance attendance)
        {
            return DataAccess.HR.Attendance.Attendance_Save(attendance);
        }
        public int Attendance_Delete(int attendanceId)
        {
            return DataAccess.HR.Attendance.Attendance_Delete(attendanceId);
        }
        public DataTable Attendance_GetByEmployeeId(int employeeId, DateTime attendanceDate)
        {
            return DataAccess.HR.Attendance.Attendance_GetByEmployeeId(employeeId, attendanceDate);
        }
    }
}
