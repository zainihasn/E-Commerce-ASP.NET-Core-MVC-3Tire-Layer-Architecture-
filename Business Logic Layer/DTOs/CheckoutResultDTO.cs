

namespace BusinessLogicLayer.DTOs
{
    public class CheckoutResultDTO
    {
        public bool IsSuccess { get; set; }
        public string InvoiceHtml { get; set; }
        public static CheckoutResultDTO Success(string html) => new CheckoutResultDTO { IsSuccess = true, InvoiceHtml = html };
        public static CheckoutResultDTO Failed() => new CheckoutResultDTO { IsSuccess = false };
    }

}
