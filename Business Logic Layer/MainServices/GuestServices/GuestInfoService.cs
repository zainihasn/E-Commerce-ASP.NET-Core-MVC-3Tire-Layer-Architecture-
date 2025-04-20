
using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.GusetServices.Base;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.MainServices.GusetServices
{
    public class GuestInfoService : IGuestInfoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GuestInfoService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> CreateGuestInfoAsync(GuestInfoDTO guestInfoDTO)
        {
            var guest = _mapper.Map<GuestInfo>(guestInfoDTO);
            await _unitOfWork.GuestInfo.AddOneAsync(guest);
            return guest.GuestId;
        }
        public async Task<bool> UpdateGuestInfoAsync(int guestId, GuestInfoDTO guestInfoDTO)
        {
            var guest = await _unitOfWork.GuestInfo.FindByIdAsync(guestId);
            if (guest == null)
                return false;

            var user = _httpContextAccessor.HttpContext?.User;

            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userName = user.Identity.Name;

                var registeredGuestDTO = await GetGuestInfoByUserNameAsync(userName);

                if (registeredGuestDTO != null)
                {

                    guest.FullName = registeredGuestDTO.FullName;
                    guest.Email = registeredGuestDTO.Email;
                    guest.PhoneNumber = registeredGuestDTO.PhoneNumber;
                    guest.Address = registeredGuestDTO.Address;
                }
                return true;
            }
            else
            {
                guest.FullName = guestInfoDTO.FullName;
                guest.Email = guestInfoDTO.Email;
                guest.PhoneNumber = guestInfoDTO.PhoneNumber;
                guest.Address = guestInfoDTO.Address;
            }

            await _unitOfWork.GuestInfo.EditOneAsync(guest);

            return true;
        }


        public async Task<int> CreateGuestInfoAndReturnIdAsync()
        {

            var user = _httpContextAccessor.HttpContext?.User;

            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userName = user.Identity.Name;

                var registeredGuestDTO = await GetGuestInfoByUserNameAsync(userName);
                return registeredGuestDTO.GuestId;
            }
            else
            {
                var guest = new GuestInfo
                {
                    UserName = "guest_" + Guid.NewGuid().ToString("N").Substring(0, 8),
                    FullName = "",
                    Email = "",
                    PhoneNumber = "",
                    Address = ""
                };
                await _unitOfWork.GuestInfo.AddOneAsync(guest);
                return guest.GuestId;
            }
        }

        public async Task<GuestInfoDTO> GetGuestInfoByUserNameAsync(string userName)
        {
            var guest = await _unitOfWork.ProImaRep.FindOneAsync(g => g.UserName == userName);
            return _mapper.Map<GuestInfoDTO>(guest);
        }



        public async Task<GuestInfoDTO> GetGuestInfoAsync(int guestId)
        {
            var guest = await _unitOfWork.GuestInfo.FindByIdAsync(guestId);
            return _mapper.Map<GuestInfoDTO>(guest);
        }
        public async Task<GuestInfoDTO> GetUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user != null && user.Identity != null && user.Identity.IsAuthenticated)
            {
                var userName = user.Identity.Name;

                var registeredGuestDTO = await GetGuestInfoByUserNameAsync(userName);
                return registeredGuestDTO;
            }
            
            return null;
        }
        



        
    }
}
