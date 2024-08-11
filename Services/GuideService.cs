using System.Linq.Expressions;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;

public class GuideService
{
    private readonly IRepository<Guide> _dbGuide;

    public GuideService(IRepository<Guide> dbGuide)
    {
        _dbGuide = dbGuide;
    }

    public async Task<Guide> AddGuideAsync(Guide guide)
    {
        await _dbGuide.CreateAsync(guide);
        return guide;
    }
    
    public async Task<Guide> DeleteGuideAsync(Guide guide)
    {
        await _dbGuide.DeleteAsync(guide);
        return guide;
    }
    
    public async Task<Guide> UpdateGuideAsync(Guide guide)
    {
        await _dbGuide.UpdateAsync(guide);
        return guide;
    }

    public async Task<Guide?> GetGuide(Expression<Func<Guide,bool>>? filter = null , bool tracked = true)
    {
        Guide? guide = await _dbGuide.GetAsync(filter , tracked);
        return guide;
    }
    
    public async Task<List<Guide>> GetAllGuides(Expression<Func<Guide,bool>>? filter = null)
    {
        List<Guide> guides = await _dbGuide.GetAllAsync(filter);
        return guides;
    }
}
