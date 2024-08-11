using System.Linq.Expressions;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;

public class EventService
{
    private readonly IRepository<Event> _dbEvent;

    public EventService(IRepository<Event> dbEvent)
    {
    
        _dbEvent = dbEvent;
    }

   
}
