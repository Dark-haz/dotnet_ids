using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;

namespace Solution.dotnet_ids.Repository.IRepository
{
        public interface IGuideRepository : IRepository<Guide>
        {
            Task<ICollection<Guide>> GetGuidesNotInEventAsync(int eventId);
        }

}