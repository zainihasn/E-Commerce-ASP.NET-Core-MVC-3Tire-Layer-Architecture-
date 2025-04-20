using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.DashboardServices.Base
{
    public interface IDataFilteringService
    {
        (List<DailyCountDTO> Orders, List<DailyCountDTO> Sales, List<DailyCountDTO> Units)
            FilterAllData(List<DailyCountDTO> orders, List<DailyCountDTO> sales, List<DailyCountDTO> units,
                          DateTime startDate, DateTime endDate);
    }

}
