using Books.core.Entities;
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
        private readonly ListingsService _service;
        public ListingsController(ListingsService service)
        {
           _service = service;
        }

        [HttpGet]
        public IActionResult GetAllListings()
        {
            return Ok(_service.GetAllListings());
        }
        [HttpGet("byUser/{UserId}")]
        public IActionResult GetListingsByUser(int userId)
        {
            var result = _service.GetListingsByUser(userId);
            if (result.Any())
                return NotFound("no ads found for this user");
            return Ok(result);

        }
        [HttpGet("byPrice")]
        public IActionResult GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var results = _service.GetListingsByPriceRange(minPrice, maxPrice);
            if (!results.Any())
                return NotFound("no ads found in the requested price range");
            return Ok(results);
        }
        [HttpPost]
        public IActionResult CreadeListing([FromBody] Listings newlistings)
        {
          
            return Ok(_service.CreateListing(newlistings));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateListing(int id, [FromBody] Listings UpdateListing)
        {
            var result = _service.UpdateListing(id, UpdateListing);
            if (result == null)
                return NotFound("ads is not found");
           
            return Ok(result);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteListing(int id)
        {
            var result=_service.DeleteListing(id);
            if (result == null)
                return NotFound(" ads is not found ");
           
            return Ok($"The ads {result.ListingId} succeessfully disabled");
        }

    }
}
