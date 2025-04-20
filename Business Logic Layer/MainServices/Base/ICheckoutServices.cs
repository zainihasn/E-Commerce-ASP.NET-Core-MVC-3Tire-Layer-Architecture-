using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLogicLayer.MainServices.Base
{
    public interface ICheckoutServices
    {
        Task<CheckoutResultDTO> ProcessCheckoutAsync(PaymentDTO Payment, GuestInfoDTO GuestInfoDTO, ClaimsPrincipal user, HttpContext httpContext, string CardToken);
    }
}
