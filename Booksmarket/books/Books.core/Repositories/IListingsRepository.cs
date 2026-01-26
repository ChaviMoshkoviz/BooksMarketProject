using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.core.Repositories
{
    public interface IListingsRepository
    {
       
      Task<  List<Listings>> GetAllListings();
       Task< List<Listings>> GetListingsByUser(int userId);
       Task< List<Listings>> GetListingsByPriceRange(decimal minPrice, decimal maxPrice);
      Task<  Listings> CreateListing(Listings newListings);
       Task< Listings> UpdateListing(int id, Listings UpdateListing);
       Task< Listings >ToggleListingStatus(int id);
        Task save();
    }
}
