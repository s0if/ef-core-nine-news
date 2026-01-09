using EFCoreNews.Data;
using EFCoreNews.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace EFCoreNews.Services
{
    public class PartyEventService : IPartyEventService
    {
        private readonly NewsDbContext _dbContext;
        public PartyEventService(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task ProcessReadOnlyPrimitiveCollections()
        {
            var today = DateTime.Today;

            var pastEvents = await _dbContext.PartyEvents
                .Where(e => e.CreatedDate < today)
                .Select(p => new
                {
                    Id = Guid.NewGuid(),
                    Name = p.Name,
                    Count = p.EventDays.Count(d => p.EventDays.Contains(d)),
                    TotalCount = p.EventDays.Count
                })
                .ToListAsync();

            var initialList = new List<int> { 1, 2, 3 };

            var readWriteCollection = new List<int>(initialList);
            var readOnlyCollection = new ReadOnlyCollection<int>(initialList);

            readWriteCollection.Add(4);

            //This generates an error
            //readOnlyCollection.Add(4);
        }
    }
}
