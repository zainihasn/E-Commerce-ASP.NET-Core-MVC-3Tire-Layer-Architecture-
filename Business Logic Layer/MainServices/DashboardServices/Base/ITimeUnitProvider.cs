using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.MainServices.DashboardServices.Base
{
    public interface ITimeUnitProvider
    {
        DateTime GetStartOfUnit(DateTime referenceDate); 
        DateTime GetEndOfUnit(DateTime referenceDate);  
        string GetUnitLabel();                         
    }

}
