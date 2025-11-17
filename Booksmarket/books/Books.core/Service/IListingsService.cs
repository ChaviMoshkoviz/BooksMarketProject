using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Service
{
    public interface IListingsService
    {
        List<Listings> GetAllListings();
        List<Listings> GetListingsByUser(int userId);
        List<Listings> GetListingsByPriceRange(decimal minPrice, decimal maxPrice);
        Listings CreateListing(Listings newListings);
        Listings UpdateListing(int id, Listings UpdateListing);
        Listings DeleteListing(int id);
    }
}
