using EFCoreNews.Data;
using EFCoreNews.Interface;
using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Utils
{
    public class SeedData: ISeedData
    {
        private readonly NewsDbContext _dbContext;

        public SeedData(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task  SeedCustomerData()
        {
            if (! _dbContext.Customers.Any())
            {
                _dbContext.Customers.AddRange(
                    new Customer
                    {
                        Name = "John Doe",
                        Phone = "555-1234",
                        Region = "USA",
                        CustomerAddress = new Address("123 Main St", 123, "New York", "USA", "10001")
                    },
                    new Customer
                    {
                        Name = "Jane Smith",
                        Phone = "555-5678",
                        Region = "USA",
                        CustomerAddress = new Address("456 Elm St", 456, "Los Angeles", "USA", "90001")
                    },
                    new Customer
                    {
                        Name = "Mike Johnson",
                        Phone = "555-7890",
                        Region = "USA",
                        CustomerAddress = new Address("789 Oak Ave", 789, "Chicago", "USA", "60601")
                    },
                    new Customer
                    {
                        Name = "Maria Johnson",
                        Phone = "555-3311",
                        Region = "USA",
                        CustomerAddress = new Address("789 Oak Ave", 789, "Chicago", "USA", "60601")
                    }
                );
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SeedPerson()
        {
            // Define variables to hold constant values.
            string defaultHierarchyPath = "/4/1/3/1/";
            string defaultPersonName = "John";
            // Parse the hierarchy path once into a variable to keep the constructor call clean and readable.

            var path = HierarchyId.Parse(defaultHierarchyPath);

            await _dbContext.Persons.AddAsync(new Person(path, defaultPersonName));
            await _dbContext.SaveChangesAsync();
        }
    }
}
