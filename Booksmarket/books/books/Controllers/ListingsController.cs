using AutoMapper;
using Books.core.DTO;
using Books.core.Entities;
using Books.core.Service;
using Books.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task< IActionResult> GetAllListings()
        {
            var listing= await _service.GetAllListings();
            return Ok(_mapper.Map<List<ListingsDTO>>(listing));
        }
        [HttpGet("byUser/{UserId}")]
        public async Task< IActionResult> GetListingsByUser(int UserId)
        {
            var result = await _service.GetListingsByUser(UserId);
            if (result==null||!result.Any())
                return NotFound("no ads found for this user");
            var resultDto = _mapper.Map<IEnumerable<ListingsDTO>>(result);
            return Ok(resultDto);

        }
        [HttpGet("byPrice")]
        public async Task< IActionResult> GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var results =await _service.GetListingsByPriceRange(minPrice, maxPrice);
            if (!results.Any())
                return NotFound("no ads found in the requested price range");
            var resultDto = _mapper.Map<IEnumerable<ListingsDTO>>(results);
            return Ok(resultDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreadeListing([FromBody] PostListingsDTO newListingDto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var listingEntity = _mapper.Map<Listings>(newListingDto);

            // אבטחה: קביעת ה-UserId לפי המשתמש המחובר באמת
            listingEntity.UserId = int.Parse(userIdClaim.Value);

            var result = await _service.CreateListing(listingEntity);
            return Ok(_mapper.Map<ListingsDTO>(result));
        }

        [Authorize] // רק משתמשים מחוברים יכולים להעלות תמונה
        [HttpPost("{id}/upload-image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            // 1. שליפת ה-ID של המשתמש המחובר מתוך ה-Token
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User not identified.");

            int currentUserId = int.Parse(userIdClaim.Value);

            try
            {
                // תיקון: הקריאה צריכה להיות ל-_service ולא ל-_listingRepository
                // וודאי שבממשק IListingsService ובמחלקה ListingsService קיימת הפונקציה הזו
                var imagePath = await _service.SaveImageAsync(id, currentUserId, file);
                return Ok(new { url = imagePath });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task< IActionResult> UpdateListing(int id, [FromBody] PutListingsDTO UpdateListing)
        {
            var listingEntity = _mapper.Map<Listings>(UpdateListing);
            var result = await _service.UpdateListing(id, listingEntity);
            if (result == null)
                return NotFound("ads is not found");
            var finalDto = _mapper.Map<ListingsDTO>(result);
            return Ok(finalDto);

        }
        [Authorize]
        [HttpPut("{id}/disable")]
        public async Task < IActionResult> ToggleListingStatus(int id)
        {
            var listings = await _service.GetAllListings();
            var listing = listings.FirstOrDefault(l => l.ListingId == id);
            if (listing == null)
                return NotFound(new { Error = $"Listing with ID {id} not found" });
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.IsInRole("Admin");
            // בדיקת בעלות
            if (listing.UserId.ToString() != currentUserId && !isAdmin)
            {
                return Forbid("You can only disable your own listings.");
            }
            var result = await _service.ToggleListingStatus(id);
            var resultDto = _mapper.Map<DeactivateListingsDTO>(result);

            // החזרת אובייקט אנונימי הכולל גם הודעה וגם את הנתונים
            return Ok(new
            {
                Message = result.IsActiv ? "Listing enabled" : "Listing disabled",
                ListingId = resultDto.ListingId,
                IsActive = result.IsActiv
            });
        }



    }
}
