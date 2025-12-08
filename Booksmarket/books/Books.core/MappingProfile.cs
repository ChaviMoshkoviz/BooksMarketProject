using AutoMapper;
using Books.core.DTO;
using Books.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Books.core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BooksDTO>().ReverseMap();
            CreateMap<Listings,ListingsDTO>().ReverseMap();
            CreateMap<Users,UsersDTO>().ReverseMap();
            CreateMap<Users,PostUserDTO>().ReverseMap();
            CreateMap<Book, PostBooksDTO>().ReverseMap();
            CreateMap<Listings, PostListingsDTO>().ReverseMap();

        }
    }
}
