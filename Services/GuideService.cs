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

   
}
