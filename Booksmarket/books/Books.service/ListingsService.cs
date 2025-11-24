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
        {
            _ListingsRepository.save();
            return _ListingsRepository.CreateListing(newListings);
        }
        public Listings UpdateListing(int id, Listings UpdateListing)
        {
            _ListingsRepository.save();
            return _ListingsRepository.UpdateListing(id, UpdateListing);
        }
        public Listings DeleteListing(int id)
        {
            _ListingsRepository.save();
            return _ListingsRepository.DeleteListing(id);
        }
    }
}
