using System.Linq.Expressions;
using AutoMapper;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;
using Solution.dotnet_ids.Models.DTO;

namespace Solution.dotnet_ids.Services
{
public class EventService
{
    private readonly IRepository<Event> _dbEvent;
    private readonly IMapper _mapper;


    public EventService(IRepository<Event> dbEvent, IMapper mapper)
    {

        _dbEvent = dbEvent;
        _mapper = mapper;
    }

    public async Task<Event> AddEventAsync(EventCreateDTO eventCreateDTO)
    {
        var e = _mapper.Map<Event>(eventCreateDTO);
        await _dbEvent.CreateAsync(e);
        return e;
    }

    public async Task<Event> UpdateEventAsync(int ID , EventUpdateDTO eventUpdate)
    {
        var e = await _dbEvent.GetAsync(e=> e.ID == ID) ?? throw new Exception("Event not found!");
        _mapper.Map(eventUpdate , e);
        await _dbEvent.UpdateAsync(e);
        return e;
    }
    
    public async Task<Event> DeleteEventAsync(int ID)
    {
        var e = await _dbEvent.GetAsync(e => e.ID == ID , true) ?? throw new Exception("Event not found!") ;
        await _dbEvent.DeleteAsync(e);
        return e;
    }
    
    public async Task<Event> GetEventByIdAsync(int ID , bool tracked , string ?navigation = null)
    {
        Event ? e = await _dbEvent.GetAsync(g => g.ID == ID , tracked , navigation) ?? throw new Exception("Event not found!");
        return e;
    }

    // public async Task<Event?> GetEventAsync(Expression<Func<Event,bool>>? filter = null)
    // {
    //     Event? e = await _dbEvent.GetAsync(filter , false);
    //     return e;
    // }
    
    // public async Task<List<Event>> GetAllEventsAsync()
    // {
    //     List<Event> events = await _dbEvent.GetAllAsync();
    //     return events;
    // }

   

}
}