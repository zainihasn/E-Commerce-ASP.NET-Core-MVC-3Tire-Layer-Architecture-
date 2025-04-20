using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.GusetServices.Base;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.ProductsServices
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly IGuestInfoService _guestInfoService;
        public ProductReviewService(IUnitOfWork unit, IMapper mapper, IGuestInfoService guestInfoService)
        {
            _unit = unit;
            _mapper = mapper;
            _guestInfoService = guestInfoService;
        }

        public async Task AddReviewAsync(ProductReviewDTO dto) => await _unit.ProductReview.AddOneAsync(_mapper.Map<ProductReview>(dto));

        public async Task<List<ProductReviewDTO>> GetReviewsWithUserAsync(int productId)
        {
            var data = await _unit.ProImaRep.GetProductReviewsWithUserAsync(productId);
            return _mapper.Map<List<ProductReviewDTO>>(data);
        }


        public async Task<ProductReviewDTO> CreateReviewAsync(ProductReviewDTO reviewDto)
        {
            var guest = await _guestInfoService.GetUser();
            reviewDto.GuestId = guest.GuestId;
            reviewDto.CreatedDate = DateTime.Now;
            return reviewDto;
        }
    }
}
