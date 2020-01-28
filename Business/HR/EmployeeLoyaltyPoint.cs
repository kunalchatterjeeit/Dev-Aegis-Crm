
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.HR
{
    public class EmployeeLoyaltyPoint
    {
        public EmployeeLoyaltyPoint()
        { }
        public DataTable EmployeeLoyaltyPoint_GetAll(string month, int year)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.EmployeeLoyaltyPoint_GetAll(month, year);
        }
        public int EmployeeLoyaltyPoint_Save(Entity.HR.EmployeeLoyaltyPoint employeeLoyaltyPoint)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.EmployeeLoyaltyPoint_Save(employeeLoyaltyPoint);
        }
        public int EmployeeLoyaltyPoint_Delete(long loyaltyid)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.EmployeeLoyaltyPoint_Delete(loyaltyid);
        }
        public DataTable IndividualLoyalityPoint_ByEmployeeId(int employeeId)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.IndividualLoyalityPoint_ByEmployeeId(employeeId);
        }
        public int CalculateLoyalityPointFromJanuary(DataTable dtEmployeePoint)
        {
            int totalPoint = 0;
            //April
            var filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Apr" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //May
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "May" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //June
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jun" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //July
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jul" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //August
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Aug" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //September
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Sep" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //October
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Oct" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //November
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Nov" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //December
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Dec" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //January
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jan" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //February
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Feb" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //March
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Mar" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            return totalPoint;
        }
        public int CalculateLoyalityPointBeforeJanuary(DataTable dtEmployeePoint)
        {
            int totalPoint = 0;
            //April
            var filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Apr" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //May
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "May" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //June
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jun" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //July
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jul" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //August
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Aug" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //September
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Sep" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //October
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Oct" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //November
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Nov" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            //December
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Dec" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            totalPoint += filteredPoint.Any() ? int.Parse(filteredPoint.FirstOrDefault()["Point"].ToString()) : 0;
            return totalPoint;
        }
        public DataTable LoyalityPointFromJanuary(DataTable dtEmployeePoint)
        {
            DataTable dtList = dtEmployeePoint.Clone();
            //April
            var filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Apr" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //May
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "May" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //June
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jun" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //July
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jul" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //August
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Aug" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //September
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Sep" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //October
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Oct" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //November
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Nov" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //December
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Dec" &&
                          row["Year"].ToString() == (DateTime.Now.Year - 1).ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //January
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jan" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //February
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Feb" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //March
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Mar" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            return dtList;
        }
        public DataTable LoyalityPointBeforeJanuary(DataTable dtEmployeePoint)
        {
            DataTable dtList = dtEmployeePoint.Clone();
            //April
            var filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Apr" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //May
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "May" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //June
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jun" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //July
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Jul" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //August
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Aug" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //September
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Sep" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //October
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Oct" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //November
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Nov" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            //December
            filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Month"].ToString() == "Dec" &&
                          row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
                dtList.ImportRow(filteredPoint.FirstOrDefault());
            return dtList;
        }
    }
}
