using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.Service.Base
{
    public interface IGuestService
    {
        string GetGuestId();
        Task<int> CreateGuestInfoAndReturnIdAsync(GuestInfoDTO guestInfoDTO);
        Task<int> EnsureGuestIdInSessionAsync(HttpContext httpContext);
        Task<bool> UpdateGuestInfoAsync(int guestId, GuestInfoDTO guestInfoDTO);
        Task<GuestInfoDTO> GetGuestInfoDTOAsync(int guestId);
         Task<GuestInfoDTO> GetGuestInfoByUserNameAsync(string userName);
        string? GetGuestUserNameFromCookie();
        Task<GuestInfoDTO> GetById(int GuestId);
       Task<GuestInfoDTO> getUserFullName();
    }
}
