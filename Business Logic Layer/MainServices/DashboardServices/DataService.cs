using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.DashboardServices.Base;
using DataAccessLayer.Repository.Base;
using AutoMapper;

namespace BusinessLogicLayer.MainServices.DashboardServices
{
    

    public class DataService : IDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DailyCountDTO>> GetDailyOrdersAsync() => _mapper.Map<List<DailyCountDTO>>(await _unitOfWork.Dashboard.GetDailyOrderCountsAsync());
      
        public async Task<List<DailyCountDTO>> GetDailySalesAsync()
        {
            var dailySales = await _unitOfWork.Dashboard.GetDailySalesStatsAsync(oi => oi.Quantity * (decimal)oi.Price);
            return _mapper.Map<List<DailyCountDTO>>(dailySales);
        }

        public async Task<List<DailyCountDTO>> GetDailyUnitsAsync()
        {
            var dailyUnits = await _unitOfWork.Dashboard.GetDailyUnitsSoldStatsAsync(oi => oi.Quantity);
            return _mapper.Map<List<DailyCountDTO>>(dailyUnits);
        }
    }


}
