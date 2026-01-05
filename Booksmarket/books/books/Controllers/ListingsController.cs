using AutoMapper;
using Books.core.DTO;
using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IListingsService _service;
        private readonly IMapper _mapper;
        public ListingsController(IListingsService service , IMapper mapper)
        {
           _service = service;
            _mapper= mapper;
        }

        [HttpGet]
        public IActionResult GetAllListings()
        {
            var listing= _service.GetAllListings();
            return Ok(_mapper.Map<List<ListingsDTO>>(listing));
        }
        [HttpGet("byUser/{UserId}")]
        public IActionResult GetListingsByUser(int UserId)
        {
            var result = _service.GetListingsByUser(UserId);
            if (result==null||!result.Any())
                return NotFound("no ads found for this user");
            var resultDto = _mapper.Map<IEnumerable<ListingsDTO>>(result);
            return Ok(resultDto);

        }
        [HttpGet("byPrice")]
        public IActionResult GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var results = _service.GetListingsByPriceRange(minPrice, maxPrice);
            if (!results.Any())
                return NotFound("no ads found in the requested price range");
            var resultDto = _mapper.Map<IEnumerable<ListingsDTO>>(results);
            return Ok(resultDto);
        }
        [HttpPost]
        public IActionResult CreadeListing([FromBody] ListingsDTO newListingDto)
        {

            var listingEntity = _mapper.Map<Listings>(newListingDto);
            var result = _service.CreateListing(listingEntity);

            // החזרת האובייקט החדש שנוצר כ-DTO
            return Ok(_mapper.Map<ListingsDTO>(result));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateListing(int id, [FromBody] PutListingsDTO UpdateListing)
        {
            var listingEntity = _mapper.Map<Listings>(UpdateListing);
            var result = _service.UpdateListing(id, listingEntity);
            if (result == null)
                return NotFound("ads is not found");
            var finalDto = _mapper.Map<ListingsDTO>(result);
            return Ok(finalDto);

        }
        [HttpPut("{id}/disable")]
        public IActionResult DeleteListing(int id)
        {
            var result = _service.DeleteListing(id);
            if (result == null)
                return NotFound(new { Error = $"Listing with ID {id} not found" });
            var resultDto = _mapper.Map<DeactivateListingsDTO>(result);

            // החזרת אובייקט אנונימי הכולל גם הודעה וגם את הנתונים
            return Ok(new
            {
                Message = "Listing successfully disabled",
                ListingId = resultDto.ListingId
            });
        }



    }
}
