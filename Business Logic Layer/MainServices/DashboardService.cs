using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.Base;
using BusinessLogicLayer.MainServices.DashboardServices.Base;
using BusinessLogicLayer.Service.Base;

namespace BusinessLogicLayer.MainServices
{
  
    public class DashboardService : IDashboardService
    {
        private readonly IDataService _dataRetrievalService;
        private readonly IChartDataService _chartDataService;
        private readonly ISummaryService _summaryDataService;
        private readonly IDashboardBuilderService _dashboardDTOBuilderService;
        private readonly IDataFilteringService _dataFilteringService;


        public DashboardService(
            IDataService dataRetrievalService,
            IChartDataService chartDataService,
            ISummaryService summaryDataService,
            IDashboardBuilderService dashboardDTOBuilderService,
            IDataFilteringService dataFilteringService
           )
        {
            _dataRetrievalService = dataRetrievalService;
            _chartDataService = chartDataService;
            _summaryDataService = summaryDataService;
            _dashboardDTOBuilderService = dashboardDTOBuilderService;
            _dataFilteringService = dataFilteringService;

        }

       
        public async Task<DashboardDTO> GetCustomRangeStatsAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _dataRetrievalService.GetDailyOrdersAsync();
            var sales = await _dataRetrievalService.GetDailySalesAsync();
            var units = await _dataRetrievalService.GetDailyUnitsAsync();

           
            var filteredData = _dataFilteringService.FilterAllData(orders, sales, units, startDate, endDate);

            var chartData = _chartDataService.GenerateChartData(filteredData, startDate, endDate);

            var summary = await _summaryDataService.GetSummaryDataAsync();

            return _dashboardDTOBuilderService.BuildDashboardDTO(chartData, summary);
        }

    }

}
    




