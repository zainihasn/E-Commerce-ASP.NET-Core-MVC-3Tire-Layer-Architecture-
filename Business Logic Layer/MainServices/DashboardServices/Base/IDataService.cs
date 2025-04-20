using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;


namespace BusinessLogicLayer.MainServices.DashboardServices.Base
{
    public interface IDataService
    {
        Task<List<DailyCountDTO>> GetDailyOrdersAsync();
        Task<List<DailyCountDTO>> GetDailySalesAsync();
        Task<List<DailyCountDTO>> GetDailyUnitsAsync();
    }
}
