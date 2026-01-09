using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Interface
{
    public interface IProductService
    {
        Task<int> GetNewProducts();
       
    }
}
