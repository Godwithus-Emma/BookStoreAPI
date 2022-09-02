using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public interface IBookRepository
    {
       Task<List<BookModel>> GetAllBookAsync();
        Task<BookModel> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookModel bookModel);
        Task UpdateBookAsync(int id, BookModel bookModel); 
        Task UpdateBookPatchAsync(int id, JsonPatchDocument bookModel);
        Task DeleteBookAsync(int id);
    }
}
