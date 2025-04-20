using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using Microsoft.AspNetCore.Http;
using BusinessLogicLayer.MainServices.GusetServices.Base;

namespace BusinessLogicLayer.Service
{
    public class GuestService : IGuestService
    {
        private readonly IGuestManagerService _guestManagerService;
        private readonly IGuestInfoService _guestInfoService;
        private readonly ICookieService _cookieService;

        public GuestService(
            IGuestManagerService guestManagerService,
            IGuestInfoService guestInfoService,
            ICookieService cookieService)
        {
            _guestManagerService = guestManagerService;
            _guestInfoService = guestInfoService;
            _cookieService = cookieService;
        }

        public string GetGuestId() => _guestManagerService.GetGuestId();       

        public async Task<int> CreateGuestInfoAndReturnIdAsync(GuestInfoDTO guestInfoDTO) 
            => await _guestInfoService.CreateGuestInfoAsync(guestInfoDTO);

        public async Task<int> EnsureGuestIdInSessionAsync(HttpContext httpContext)
        {
            if (!httpContext.Session.TryGetValue("GuestId", out _))
            {
                int newGuestId = await _guestInfoService.CreateGuestInfoAndReturnIdAsync();
                httpContext.Session.SetInt32("GuestId", newGuestId);
                return newGuestId;
            }
            return httpContext.Session.GetInt32("GuestId").Value;
        }

  
        public async Task<bool> UpdateGuestInfoAsync(int guestId, GuestInfoDTO guestInfoDTO)
            => await _guestInfoService.UpdateGuestInfoAsync(guestId, guestInfoDTO);

        public async Task<GuestInfoDTO> GetGuestInfoDTOAsync(int guestId)
            => await _guestInfoService.GetGuestInfoAsync(guestId);

        public async Task<GuestInfoDTO> getUserFullName() => await _guestInfoService.GetUser();
        public string? GetGuestUserNameFromCookie() => _cookieService.GetGuestIdFromCookie();
        public async Task<GuestInfoDTO> GetById(int guestId) => await _guestInfoService.GetGuestInfoAsync(guestId);
        public async Task<GuestInfoDTO> GetGuestInfoByUserNameAsync(string userName) => await _guestInfoService.GetGuestInfoByUserNameAsync(userName);
        

        
    }

}

