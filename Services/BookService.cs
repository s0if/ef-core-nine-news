using EFCoreNews.Data;
using EFCoreNews.DTOs;
using EFCoreNews.Interface;
using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Services
{
    public class BookService:IBookService
    {
        private readonly NewsDbContext _dbContext;

        public BookService(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BookDTO>> GetBooksStartsWithR()
        {
            var books = await _dbContext.Books
                .Where(b => b.PartitionKey == "someValue" && b.Title.StartsWith("R"))
                .ToListAsync();

            var bookById = await _dbContext.Books
                .Where(b => b.PartitionKey == "someValue" && b.Id == 1)
                .SingleAsync();

            return books
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Title = b.Title,
                    PartitionKey = b.PartitionKey,
                    Category = b.Category,
                    SessionId = b.SessionId,
                    TenantId = b.TenantId,
                    UserId= b.UserId
                })
                .ToList();
        }
    }
}
