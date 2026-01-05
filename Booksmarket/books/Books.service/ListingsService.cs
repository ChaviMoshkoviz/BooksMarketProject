using books;
using Books.core.Entities;
using Books.core.Repositories;
using Books.core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.service
{
    public class ListingsService: IListingsService
    {
        private readonly IListingsRepository _ListingsRepository;
        public ListingsService(IListingsRepository ListingsRepository)
        {
            _ListingsRepository = ListingsRepository;
        }
        public List<Listings> GetAllListings()
        {
            return _ListingsRepository.GetAllListings();
        }
        public List<Listings> GetListingsByUser(int userId)
        {
            return _ListingsRepository.GetListingsByUser(userId);
        }
        public List<Listings> GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _ListingsRepository.GetListingsByPriceRange( minPrice,  maxPrice);
        }
        public Listings CreateListing(Listings newListings)
        {var listings = _ListingsRepository.CreateListing(newListings);
            _ListingsRepository.save();
            return listings;
        }
        public Listings UpdateListing(int id, Listings UpdateListing)
        {
            var listings = _ListingsRepository.UpdateListing(id, UpdateListing);
            if (listings != null)
            {
                _ListingsRepository.save();
            }
            return listings ;
        }
        public Listings DeleteListing(int id)
        {
            var listings = _ListingsRepository.DeleteListing(id);
            if (listings != null)
            {
                _ListingsRepository.save();
            }
            return listings;
        }
    }
}
