using books;
using Books.core.Entities;
using Books.core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.data
{
    public class ListingsRepository: IListingsService
    {
        private readonly DataContext _context;
        public ListingsRepository(DataContext context)
        {
            _context = context;
        }

        public List<Listings> GetAllListings()
        {
            return _context.listing.Where(a => a.IsActiv).ToList();
        }
        public List<Listings> GetListingsByUser(int userId)
        {
            return _context.listing.Where(a => a.UserId == userId && a.IsActiv).ToList();
        }
        public List<Listings> GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _context.listing.Where(a => a.IsActiv && a.Price >= minPrice && a.Price <= maxPrice).ToList();
        }
        public Listings CreateListing(Listings newListings)
        {
            newListings.ListingId = _context.listing.Any() ? _context.listing.Max(a => a.ListingId) + 1 : 1;
            newListings.DatePosted = DateTime.Now;
            newListings.IsActiv = true;
            _context.listing.Add(newListings);
            return newListings;
        }
        public Listings UpdateListing(int id, Listings UpdateListing)
        {
            var listing = _context.listing.FirstOrDefault(u => u.ListingId == id);
            if (listing != null)
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
        public Listings DeleteListing(int id)
        {
            var listing = _context.listing.FirstOrDefault(u => u.ListingId == id);
            if (listing != null)
            {
                return null;
            }
            listing.IsActiv = false;
            return listing;
        }

    }
}
