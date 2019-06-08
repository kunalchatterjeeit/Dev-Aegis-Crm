using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.HR
{
    public class EmployeeMaster
    {

        public int Employee_Save(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.Employee_Save(ObjElEmployeeMaster);
        }

        public int HR_PasswordReset_Save(Entity.HR.EmployeeMaster employeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.HR_PasswordReset_Save(employeeMaster);
        }

        public int EmployeeDelete(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.EmployeeDelete(ObjElEmployeeMaster);
        }

        public System.Data.DataTable EmployeeMaster_GetAll(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.EmployeeMaster_GetAll(ObjElEmployeeMaster);
        }

        public DataTable EmployeeMaster_ById(Entity.HR.EmployeeMaster employeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.EmployeeMaster_ById(employeeMaster);
        }

        public System.Data.DataTable City_GetAll()
        {
            return DataAccess.HR.EmployeeMaster.City_GetAll();
        }

        public System.Data.DataTable DesignationMaster_GetAll(Entity.HR.EmployeeMaster employeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.DesignationMaster_GetAll(employeeMaster);
        }

        public int DeleteEmployee(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.DeleteEmployee(ObjElEmployeeMaster);
        }

        public System.Data.SqlClient.SqlDataReader ViewEmployeeById(Entity.HR.EmployeeMaster ObjElEmployeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.ViewEmployeeById(ObjElEmployeeMaster);
        }

        public Entity.HR.EmployeeMaster AuthenticateUser(string UserName)
        {
            return DataAccess.HR.EmployeeMaster.AuthenticateUser(UserName);
        }

        public Entity.HR.EmployeeMaster AutoAuthenticateUserByDevice(string deviceId)
        {
            return DataAccess.HR.EmployeeMaster.AutoAuthenticateUserByDevice(deviceId);
        }

        public int Login_Save(Entity.Common.Auth auth)
        {
            return DataAccess.HR.EmployeeMaster.Login_Save(auth);
        }

        public int EmployeeLeave_Update(Entity.HR.EmployeeMaster employeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.EmployeeLeave_Update(employeeMaster);
        }

        public int LinkedDevices_Save(int employeeId, string deviceId)
        {
            return DataAccess.HR.EmployeeMaster.LinkedDevices_Save(employeeId, deviceId);
        }

        public DataTable LinkedDevices_GetByUserId(int userId)
        {
            return DataAccess.HR.EmployeeMaster.LinkedDevices_GetByUserId(userId);
        }

        public DataTable ValidateForgotPassword(string userName, string emailId)
        {
            return DataAccess.HR.EmployeeMaster.ValidateForgotPassword(userName, emailId);
        }

        public int LiknedDevices_Delete(int linkedDeviceId)
        {
            return DataAccess.HR.EmployeeMaster.LiknedDevices_Delete(linkedDeviceId);
        }

        public int Employee_Update(Entity.HR.EmployeeMaster employeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.Employee_Update(employeeMaster);
        }
    }
}
