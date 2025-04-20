using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.MainServices.GusetServices.Base;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.MainServices.GusetServices
{

    public class CookieService : ICookieService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetGuestIdFromCookie()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext.Request.Cookies.TryGetValue("GuestId", out string guestId))
            {
                return guestId;
            }
            return null;
        }

        public void SetGuestIdInCookie(string guestId)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.Response.Cookies.Append("GuestId", guestId, new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(7),
                HttpOnly = true
            });
        }
    }
}
