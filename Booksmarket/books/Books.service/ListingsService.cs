using books;
using Books.core.Entities;
using Books.core.Repositories;
using Books.core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Books.service
{
    public class ListingsService: IListingsService
    {
        private readonly IListingsRepository _ListingsRepository;
        public ListingsService(IListingsRepository ListingsRepository)
        {
            _ListingsRepository = ListingsRepository;
        }
        public async Task< List<Listings>> GetAllListings()
        {
            return await _ListingsRepository.GetAllListings();
        }
        public async Task< List<Listings>> GetListingsByUser(int userId)
        {
            return await _ListingsRepository.GetListingsByUser(userId);
        }
        public async Task< List<Listings>> GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return await _ListingsRepository.GetListingsByPriceRange( minPrice,  maxPrice);
        }
        public async Task< Listings> CreateListing(Listings newListings)
        {
            var listings =await _ListingsRepository.CreateListing(newListings);
          await  _ListingsRepository.save();
            return listings;
        }
        public async Task<string> SaveImageAsync(int id, int currentUserId, IFormFile file)
        {
            // כאן ה-Service קורא ללוגיקה שכתבת ב-Repository
            return await _ListingsRepository.SaveImageAsync(id, currentUserId, file);
        }
        public async Task< Listings > UpdateListing(int id, Listings UpdateListing)
        {
            var listings = await _ListingsRepository.UpdateListing(id, UpdateListing);
            if (listings != null)
            {
               await _ListingsRepository.save();
            }
            return listings ;
        }
        public async Task < Listings> ToggleListingStatus(int id)
        {
            var listings = await _ListingsRepository.ToggleListingStatus(id);
            if (listings != null)
            {
               await _ListingsRepository.save();
               
            }
            return listings;
        }
    }
}
