using EFCoreNews.Data;
using EFCoreNews.Interface;
using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EFCoreNews.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NewsDbContext _dbContext;

        public CustomerService(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<CustomerWithCount>> GetUSCustomers()
        {
            var usCustomers = _dbContext.Customers.Where(c => c.Region.Contains("USA"));
            //  calculate total count once instead of inside Select
            var TotalCounts = await usCustomers.CountAsync();
            //add OrderBy before Skip/ Take
            //Without ordering, pagination results can change between executions
            var Customers = await usCustomers
                .Where(c => c.Id > 1)
                .OrderBy(c => c.Id)
                .Skip(2).Take(10)
                .ToArrayAsync();

            return Customers
                .Select(c => new CustomerWithCount
                {
                    Customer = c,
                    TotalCount = TotalCounts
                });
        }
        public async Task ExecuteUpdateAddress(Address newAddress)
        {
            //Validation
          
            if (string.IsNullOrWhiteSpace(newAddress.Street))
                throw new ArgumentException("Street is required.", nameof(newAddress.Street));

            if (newAddress.Number <= 0)
                throw new ArgumentOutOfRangeException(nameof(newAddress.Number), "Number must be positive.");

            if (string.IsNullOrWhiteSpace(newAddress.City))
                throw new ArgumentException("City is required.", nameof(newAddress.City));

            if (string.IsNullOrWhiteSpace(newAddress.Country))
                throw new ArgumentException("Country is required.", nameof(newAddress.Country));

            if (string.IsNullOrWhiteSpace(newAddress.PostCode))
                throw new ArgumentException("PostCode is required.", nameof(newAddress.PostCode));

            await _dbContext.Customers
           .Where(e => e.Region == "USA")
           .ExecuteUpdateAsync(s => s.SetProperty(
               b => b.CustomerAddress, newAddress
               ));
            // No need to call SaveChangesAsync after ExecuteUpdateAsync
            // it executes immediately.

            //await _dbContext.SaveChangesAsync();
        }
        public async Task ComplexTypesGroupByAsync()
        {
            var groupedAddresses = await _dbContext.Customers
                .GroupBy(b => b.CustomerAddress)
                .Select(g => new { g.Key, Count = g.Count() })
                .ToListAsync();

            groupedAddresses.ForEach(g =>
            {
                Console.WriteLine($"Key: {g.Key} - Group: {g.Count}");
            });
        }
    }
}
