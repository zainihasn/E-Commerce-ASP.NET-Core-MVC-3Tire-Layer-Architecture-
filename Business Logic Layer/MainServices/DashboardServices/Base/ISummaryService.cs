using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.MainServices.DashboardServices.Base
{
    public interface ISummaryService
    {
        Task<(int AvailableProducts, int TotalProducts, int TotalUsers)> GetSummaryDataAsync();
    }

}
