using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.GusetServices.Base;

namespace BusinessLogicLayer.MainServices.GusetServices
{

    public class GuestManagerService : IGuestManagerService
    {
        private readonly ICookieService _cookieService;

        public GuestManagerService(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public string GetGuestId()
        {
            var guestId = _cookieService.GetGuestIdFromCookie();
            if (string.IsNullOrEmpty(guestId))
            {
                guestId = Guid.NewGuid().ToString();
                _cookieService.SetGuestIdInCookie(guestId);
            }
            return guestId;
        }
    }
}
