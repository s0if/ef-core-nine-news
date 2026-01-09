using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Interface
{
    public interface ICustomerService
    {
       Task<IEnumerable<CustomerWithCount>> GetUSCustomers();
        Task ExecuteUpdateAddress(Address newAddress);
        Task ComplexTypesGroupByAsync();

    }
}
