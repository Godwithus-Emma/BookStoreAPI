using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;

namespace BookStore.API.Utilities
{
    public class BookMapper :Profile
    {
        public BookMapper()
        {
            CreateMap<Books, BookModel>().ReverseMap();
        }
    }
}
