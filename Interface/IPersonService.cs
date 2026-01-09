using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Interface
{
    public interface IPersonService
    {
        Task GetHierarchyId();
        
    }
}
