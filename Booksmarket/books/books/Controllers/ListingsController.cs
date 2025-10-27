using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private IDataContext _context;
        public ListingsController(IDataContext context)
        {
           _context = context;
        }

        [HttpGet]
        public IActionResult GetAllListings()
        {
            return Ok(_context.listing.Where(a => a.IsActiv).ToList());
        }
        [HttpGet("byUser/{UserId}")]
        public IActionResult GetListingsByUser(int userId)
        {
            var userListing = _context.listing.Where(a => a.UserId == userId && a.IsActiv).ToList();
            if (userListing.Any())
                return NotFound("no ads found for this user");
            return Ok(userListing);

        }
        [HttpGet("byPrice")]
        public IActionResult GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var results = _context.listing.Where(a => a.IsActiv && a.Price >= minPrice && a.Price <= maxPrice).ToList();
            if (!results.Any())
                return NotFound("no ads found in the requested price range");
            return Ok(results);
        }
        [HttpPost]
        public IActionResult CreadeListing([FromBody] Listings newlistings)
        {
            newlistings.ListingId = _context.listing.Any() ? _context.listing.Max(a => a.ListingId) + 1 : 1;
            newlistings.DatePosted = DateTime.Now;
            newlistings.IsActiv = true;
            _context.listing.Add(newlistings);
            return Ok(newlistings);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateListing(int id, [FromBody] Listings UpdateListing)
        {
            var listingu = _context.listing.FirstOrDefault(u => u.ListingId == id);
            if (listingu == null)
                return NotFound("ads is not found");
            listingu.ActionType = UpdateListing.ActionType;
            listingu.Price = UpdateListing.Price;
            listingu.IsActiv = UpdateListing.IsActiv;
            listingu.BookId = UpdateListing.BookId;
            listingu.UserId = UpdateListing.UserId;
            return Ok(listingu);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteListing(int id)
        {
            var listingdel = _context.listing.FirstOrDefault(u => u.ListingId == id);
            if (listingdel == null)
                return NotFound(" ads is not found ");
            listingdel.IsActiv = false;
            return Ok($"The ads {listingdel.ListingId} succeessfully disabled");
        }

    }
}
