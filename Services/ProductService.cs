using EFCoreNews.Data;
using EFCoreNews.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Services
{
    public class ProductService: IProductService
    {
        private readonly NewsDbContext _dbContext;

        public ProductService(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetNewProducts()
        {
            var newProducts = await _dbContext.Products
              .Select(b => !(b.Id > 10 ? false : true))
              .ToListAsync();

            return newProducts.Count;
        }
    }
}
