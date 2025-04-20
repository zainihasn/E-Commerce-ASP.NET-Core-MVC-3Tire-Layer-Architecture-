using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.DashboardServices.Base;

namespace BusinessLogicLayer.MainServices.DashboardServices
{
    public class TimeUnitProvider : ITimeUnitProvider
    {
        public DateTime GetStartOfUnit(DateTime referenceDate)
        {
            return referenceDate.Date; 
        }

        public DateTime GetEndOfUnit(DateTime referenceDate)
        {
            return referenceDate.Date.AddDays(1).AddTicks(-1);
        }

        public string GetUnitLabel()
        {
            return "يومي";
        }
    }

    public class MonthlyTimeUnitProvider : ITimeUnitProvider
    {
        public DateTime GetStartOfUnit(DateTime referenceDate)
        {
            return new DateTime(referenceDate.Year, referenceDate.Month, 1); 
        }

        public DateTime GetEndOfUnit(DateTime referenceDate)
        {
            return new DateTime(referenceDate.Year, referenceDate.Month, 1)
                .AddMonths(1)
                .AddDays(-1);
        }

        public string GetUnitLabel()
        {
            return "شهري";
        }
    }

    public class YearlyTimeUnitProvider : ITimeUnitProvider
    {
        public DateTime GetStartOfUnit(DateTime referenceDate)
        {
            return new DateTime(referenceDate.Year, 1, 1); 
        }

        public DateTime GetEndOfUnit(DateTime referenceDate)
        {
            return new DateTime(referenceDate.Year, 12, 31); 
        }

        public string GetUnitLabel()
        {
            return "سنوي";
        }
    }

}
