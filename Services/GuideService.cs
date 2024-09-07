using System.Linq.Expressions;
using AutoMapper;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;
using Solution.dotnet_ids.Models.DTO;

namespace Solution.dotnet_ids.Services
{
    public class GuideService
    {
        private readonly IRepository<Guide> _dbGuide;
        private readonly IMapper _mapper;

        public GuideService(IRepository<Guide> dbGuide, IMapper mapper)
        {
            _dbGuide = dbGuide;
            _mapper = mapper;
        }

        public async Task<Guide> GetGuideByIdAsync(int ID, bool tracked, string? navigation = null)
        {
            Guide? guide = await _dbGuide.GetAsync(g => g.ID == ID, tracked, navigation) ?? throw new Exception("Guide not found!");
            return guide;
        }

        public async Task UpdateGuideAsync(Guide guide)
        {
            await _dbGuide.UpdateAsync(guide);
        }

        public async Task<Guide> AddGuideAsync(GuideCreateDTO guideCreateDTO)
        {
            var guide = _mapper.Map<Guide>(guideCreateDTO);
            await _dbGuide.CreateAsync(guide);
            return guide;
        }

        public async Task<Guide> UpdateGuideAsync(int ID, GuideUpdateDTO guideUpdate)
        {
            var guide = await _dbGuide.GetAsync(g => g.ID == ID) ?? throw new Exception("Guide not found!");
            _mapper.Map(guideUpdate, guide);
            await _dbGuide.UpdateAsync(guide);
            return guide;
        }

        public async Task<Guide> DeleteGuideAsync(int ID)
        {
            var guide = await _dbGuide.GetAsync(g => g.ID == ID, true) ?? throw new Exception("Guide not found!");
            await _dbGuide.DeleteAsync(guide);
            return guide;
        }

    }
}