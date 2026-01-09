using EFCoreNews.Data;
using EFCoreNews.Interface;
using EFCoreNews.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreNews.Services
{
    public class PersonService: IPersonService
    {
        private readonly NewsDbContext _dbContext;

        public PersonService(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task GetHierarchyId()
        {
            var john = await _dbContext.Persons.SingleAsync(p => p.Name == "John");

            var child1 = new Person(HierarchyId.Parse(john.PathFromPatriarch, 1), "Doe");
            var child2 = new Person(HierarchyId.Parse(john.PathFromPatriarch, 2), "Smith");
            var child1b = new Person(HierarchyId.Parse(john.PathFromPatriarch, 1, 5), "Johnson");
        }
    }
}
