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

    public async Task<Event> AddEventAsync(Event e)
    {
        await _dbEvent.CreateAsync(e);
        return e;
    }
    
    public async Task<Event> DeleteEventAsync(Event e)
    {
        await _dbEvent.DeleteAsync(e);
        return e;
    }
    
    public async Task<Event> UpdateEventAsync(Event e)
    {
        await _dbEvent.UpdateAsync(e);
        return e;
    }

    public async Task<Event?> GetEvent(Expression<Func<Event,bool>>? filter = null , bool tracked = true)
    {
        Event? e = await _dbEvent.GetAsync(filter , tracked);
        return e;
    }
    
    public async Task<List<Event>> GetAllEvents(Expression<Func<Event,bool>>? filter = null)
    {
        List<Event> events = await _dbEvent.GetAllAsync(filter);
        return events;
    }
}
