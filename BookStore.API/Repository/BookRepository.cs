using AutoMapper;
using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;   

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BookModel>> GetAllBookAsync()
        { 
            var result = await _context.Books.ToListAsync();    
            return _mapper.Map<List<BookModel>>(result);
            //var result = await _context.Books.Select(x => new BookModel()
            //    {
            //        Id = x.Id,
            //        Description = x.Description,
            //        Title = x.Title
            //    }).ToListAsync();
            //return result;
        }


        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var result = await _context.Books.FindAsync(id);
            return _mapper.Map<BookModel>(result);
            //var result = await _context.Books.Where(x => x.Id==id).Select(x => new BookModel()
            //{
            //    Id=x.Id,
            //    Description=x.Description,
            //    Title = x.Title
            //}).FirstOrDefaultAsync();
            //return result;
        }

        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            //var book = new Books()
            //{
            //    Title = bookModel.Title,
            //    Description = bookModel.Description,
            //};
            var book = _mapper.Map<Books>(bookModel);   
             await _context.Books.AddAsync(book);    
            await _context.SaveChangesAsync();

            return book.Id;
        }

        public async Task UpdateBookAsync(int id, BookModel bookModel)
        {
            var book = _mapper.Map<Books>(bookModel);
            book.Id = id;
            //var book = await _context.Books.FindAsync(id);
            //book.Description = bookModel.Description;
            //book.Title = bookModel.Title;

            _context.Books.Update(book);  
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateBookPatchAsync(int id, JsonPatchDocument bookModel)
        {
            var book = await _context.Books.FindAsync(id);
            if(book != null)
            {
                bookModel.ApplyTo(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            //var book = _context.Books.Where(x => x.Id==id).FirstOrDefault();
            var book = new Books() { Id= id };  
            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
        }
    }
}
