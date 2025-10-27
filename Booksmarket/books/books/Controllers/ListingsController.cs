using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        public static List<Listings> listing = new List<Listings>
        {
            new Listings{ListingId=416,UserId=45,BookId=1,ActionType="sale",Price=50,DatePosted=DateTime.Now,IsActiv=true},
            new Listings{ListingId=417,UserId=46,BookId=2,ActionType="delivery",Price=0,DatePosted=DateTime.Now,IsActiv=true},
            new Listings{ListingId=417,UserId=47,BookId=3,ActionType="sale",Price=2,DatePosted=DateTime.Now,IsActiv=true}
        };
        [HttpGet]
        public IActionResult GetAllListings()
        {
            return Ok(listing.Where(a => a.IsActiv).ToList());
        }
        [HttpGet("byUser/{UserId}")]
        public IActionResult GetListingsByUser(int userId)
        {
            var userListing = listing.Where(a => a.UserId == userId && a.IsActiv).ToList();
            if (userListing.Any())
                return NotFound("no ads found for this user");
            return Ok(userListing);

        }
        [HttpGet("byPrice")]
        public IActionResult GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var results = listing.Where(a => a.IsActiv && a.Price >= minPrice && a.Price <= maxPrice).ToList();
            if (!results.Any())
                return NotFound("no ads found in the requested price range");
            return Ok(results);
        }
        [HttpPost]
        public IActionResult CreadeListing([FromBody] Listings newlistings)
        {
            newlistings.ListingId = listing.Any() ? listing.Max(a => a.ListingId) + 1 : 1;
            newlistings.DatePosted = DateTime.Now;
            newlistings.IsActiv = true;
            listing.Add(newlistings);
            return Ok(newlistings);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateListing(int id, [FromBody] Listings UpdateListing)
        {
            var listingu = listing.FirstOrDefault(u => u.ListingId == id);
            if (listingu == null)
                return NotFound("ads is not found");
           listingu.ActionType=UpdateListing.ActionType;
            listingu.Price = UpdateListing.Price;
            listingu.IsActiv =UpdateListing.IsActiv;
            listingu.BookId = UpdateListing.BookId;
            listingu.UserId = UpdateListing.UserId;
            return Ok(listingu);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteListing(int id)
        {
            var listingdel=listing.FirstOrDefault(u => u.ListingId == id);
            if (listingdel == null)
                return NotFound(" ads is not found ");
            listingdel.IsActiv = false;
            return Ok($"The ads {listingdel.ListingId} succeessfully disabled");
        }

    }
}
