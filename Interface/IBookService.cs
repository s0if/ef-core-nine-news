using EFCoreNews.DTOs;
using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Interface
{
    public interface IBookService
    {
         Task<List<BookDTO>> GetBooksStartsWithR();
    }
}
