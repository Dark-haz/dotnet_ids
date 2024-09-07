using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_ids.Repository;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Solution.dotnet_ids.Models.DTO;
using Solution.dotnet_ids.Repository.IRepository;

namespace Solution.dotnet_ids.Controllers
{
    [ApiController]
    [Route("api/get")]

    public class DataController : ControllerBase
    {
        private readonly IRepository<Admin> _dbAdmin ;
        private readonly IRepository<Event> _dbEvent ;
        private readonly IGuideRepository _dbGuide ;
        private readonly IMapper _mapper ;

        public DataController(IRepository<Admin> dbAdmin, IMapper mapper, IRepository<Event> dbEvent, IGuideRepository dbGuide)
        {
            _dbAdmin = dbAdmin;
            _mapper = mapper;
            _dbEvent = dbEvent;
            _dbGuide = dbGuide;
        }

        [HttpGet("admins")]
        [Authorize(Roles = "Admin")]
        public async Task<List<AdminDTO>> getAdmins()
        {
            return  _mapper.Map<List<AdminDTO>>(await _dbAdmin.GetAllAsync(null , false));
        }
        
        [HttpGet("events")]
        public async Task<List<EventDTO>> getEvents()
        {
            return  _mapper.Map<List<EventDTO>>(await _dbEvent.GetAllAsync(null , false ));
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet("eventMembers")]
        public async Task<ICollection<MemberDTO> ?> getEventMembers(int ID)
        {
            Event ? e = await _dbEvent.GetAsync(e => e.ID == ID, false, "Members") ?? throw new Exception("Event not found");
            return _mapper.Map<List<MemberDTO>>(e.Members);
        }
        
        [HttpGet("eventGuides")]
        public async Task<ICollection<GuideDTO> ?> getEventGuides(int ID)
        {
            Event ? e = await _dbEvent.GetAsync(e => e.ID == ID, false, "Guides") ?? throw new Exception("Event not found");
            return _mapper.Map<List<GuideDTO>>(e.Guides);
        }
        
        [HttpGet("eventAvailableGuides")]
        public async Task<ICollection<GuideDTO> ?> getEventAvailableGuides(int ID)
        {
            return _mapper.Map<ICollection<GuideDTO>>(await _dbGuide.GetGuidesNotInEventAsync(ID));
        }
        
        [HttpGet("guides")]
        public async Task<ICollection<GuideDTO> ?> getGuides()
        {
            return _mapper.Map<ICollection<GuideDTO>>(await _dbGuide.GetAllAsync(null , false));
        }
       
    }
}