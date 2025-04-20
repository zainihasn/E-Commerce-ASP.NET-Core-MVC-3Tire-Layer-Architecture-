using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.Base
{
    public interface IDashboardService
    {

        Task<DashboardDTO> GetCustomRangeStatsAsync(DateTime startDate, DateTime endDate);
       
    }


}
