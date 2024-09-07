using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Data;
using Dotnetids.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Signers;
using Solution.dotnet_ids.Repository.IRepository;

namespace dotnet_ids.Repository
{

    public class GuideRepository : Repository<Guide>, IGuideRepository
    {
        private readonly AppDbContext _db;

        public GuideRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Guide>> GetGuidesNotInEventAsync(int eventId)
    {
        var guidesNotInEvent = await _db.Guides
            .Where(g => !g.Events.Any(e => e.ID == eventId))
            .ToListAsync();

        return guidesNotInEvent;
    }


    }
}

