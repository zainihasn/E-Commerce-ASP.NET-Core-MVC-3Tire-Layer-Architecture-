using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.DashboardServices.Base;
using DataAccessLayer.Repository.Base;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.MainServices.DashboardServices
{
    public class SummaryService : ISummaryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public SummaryService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<(int AvailableProducts, int TotalProducts, int TotalUsers)> GetSummaryDataAsync()
        {
            int available = await _unitOfWork.Dashboard.CountAsync(p => p.IsActive);
            int total = await _unitOfWork.Dashboard.CountProductAsync();
            int users = _userManager.Users.Count();
            return (available, total, users);
        }
    }
}
