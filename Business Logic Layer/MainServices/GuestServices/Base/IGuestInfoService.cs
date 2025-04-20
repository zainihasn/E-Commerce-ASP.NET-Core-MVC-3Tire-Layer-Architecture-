using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.GusetServices.Base
{
    public interface IGuestInfoService
    {
        Task<int> CreateGuestInfoAsync(GuestInfoDTO guestInfoDTO);
        Task<bool> UpdateGuestInfoAsync(int guestId, GuestInfoDTO guestInfoDTO);
        Task<GuestInfoDTO> GetGuestInfoAsync(int guestId);
        Task<int> CreateGuestInfoAndReturnIdAsync();
        Task<GuestInfoDTO> GetGuestInfoByUserNameAsync(string userName);
        Task<GuestInfoDTO> GetUser();
    }
}
