using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Interface
{
    public interface IPostService
    {
       Task<List<Post>> GetPostsWithAuthors();
       Task<int> GetRecentPostQuantity();
       Task<List<Post>> GetPostsWithAuthorsUsingCount();
       Task<object> NewOrderOperators();
        


    }
}
