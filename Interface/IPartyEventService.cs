using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace EFCoreNews.Interface
{
    public interface IPartyEventService
    {
       Task ProcessReadOnlyPrimitiveCollections();
    }
}
