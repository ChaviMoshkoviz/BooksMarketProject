using books;
using Books.core.Entities;
using Books.core.Repositories;
using Books.core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.data
{
    public class ListingsRepository: IListingsRepository
    {
        private readonly DataContext _context;
        public ListingsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task< List<Listings>> GetAllListings()
        {
            return await _context.listing.Where(a => a.IsActiv).ToListAsync();
        }
        public async Task <List<Listings>> GetListingsByUser(int userId)
        {
            return await _context.listing
                .Where(a => a.UserId == userId && a.IsActiv).ToListAsync();
        }
        public async Task< List<Listings>> GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _context.listing.Where(a => a.IsActiv && a.Price >= minPrice && a.Price <= maxPrice).ToListAsync();
        }
        public async Task < Listings> CreateListing(Listings newListings)
        {
          
            newListings.DatePosted = DateTime.Now;
            newListings.IsActiv = true;
            await _context.listing.AddAsync(newListings);
            return newListings;
        }
        public async Task< Listings >UpdateListing(int id, Listings UpdateListing)
        {
            var listing = await _context.listing.FirstOrDefaultAsync(u => u.ListingId == id);
            if (listing == null)
            {
                return null;
            }
            listing.ActionType = UpdateListing.ActionType;
            listing.Price = UpdateListing.Price;
            listing.IsActiv = UpdateListing.IsActiv;
            listing.BookId = UpdateListing.BookId;
            listing.UserId = UpdateListing.UserId;
            return listing;
        }
        public async Task < Listings >ToggleListingStatus(int id)
        {
            var listing = await _context.listing.FirstOrDefaultAsync(u => u.ListingId == id);
            if (listing != null)
            {
                listing.IsActiv = !listing.IsActiv;
            
            }
           
            return listing;
        }


        public async Task save()
        {
           await _context.SaveChangesAsync();
        }

    }
}
