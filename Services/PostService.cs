using EFCoreNews.Data;
using EFCoreNews.Interface;
using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Services
{
    public class PostService : IPostService
    {
        private readonly NewsDbContext _dbContext;
        public PostService(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Post>> GetPostsWithAuthors()
        {
            var postsWithAuthors = await _dbContext
                .Posts
                .Where(p => p.Authors.Any()).ToListAsync();

            // ToListAsync never returns null; it returns an empty list when no records are found.

            //return postsWithAuthors ?? new List<Post>();

            return postsWithAuthors ;
        }

        public async Task<int> GetRecentPostQuantity()
        {
            int recentPostQuantity = await _dbContext.Posts
                .Where(p => p.CreatedDate >= DateTime.UtcNow)
                .Take(6)
                .CountAsync();

            return recentPostQuantity;
        }
        public async Task<List<Post>> GetPostsWithAuthorsUsingCount()
        {
            var postsWithAuthors = await _dbContext.Posts
                .Where(b => b.Authors.Count > 0)
                .ToListAsync();
            return postsWithAuthors;
        }
        public async Task<object> NewOrderOperators()
        {
            var orderedPostsWithAuthors = await _dbContext.Posts
                .Order()
                .Select(x => new
                {
                    x.Title,
                    OrderedAuthors = x.Authors.OrderDescending().ToList(),
                    OrderedAuthorName = x.Authors.Select(xx => xx.Name).Order().ToList()
                })
                .ToListAsync();

            var orderedByPostsWithAuthors = await _dbContext.Posts
                .OrderBy(x => x.Title)
                .Select(x => new
                {
                    x.Title,
                    OrderedAuthors = x.Authors.OrderByDescending(a => a.Name).ToList(),
                    OrderedAuthorName = x.Authors.Select(a => a.Name).OrderBy(n => n).ToList()
                })
                .ToListAsync();

            return orderedPostsWithAuthors;
        }
    }
}
