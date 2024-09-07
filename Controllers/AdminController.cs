using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.dotnet_ids.Models;
using Solution.dotnet_ids.Models.DTO;
using Solution.dotnet_ids.Models.Request;
using Solution.dotnet_ids.Services;

namespace Solution.dotnet_ids.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly GuideService _guideService;
        private readonly EventService _eventService;

        private readonly ApiResponse _response;

        public AdminController(AdminService adminService, GuideService guideService, EventService eventService, MemberService memberService)
        {
            _adminService = adminService;
            _response = new();
            _guideService = guideService;
            _eventService = eventService;
        }

        //- TODO login
        //TODO CRUD guides
        //TODO CRUD events --> associated guides + members

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> loginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {

                var assertLogin = await _adminService.AdminLoginAsync(loginRequestDTO);

                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = assertLogin;
                return Ok(_response);

            }, _response, this);
        }

        [HttpPost("addEventFromGuide")]
        public async Task<IActionResult> addEventFromGuide(int guideID, int eventID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                await _adminService.addEventToGuide(guideID, eventID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }

        [HttpDelete("removeEventFromGuide")]
        public async Task<IActionResult> removeEventFromGuide(int guideID, int eventID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                await _adminService.removeEventFromGuide(guideID, eventID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }

        [HttpDelete("removeEventFromMember")]
        public async Task<IActionResult> removeEventFromMember(int memberID, int eventID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                await _adminService.removeEventFromMember(memberID, eventID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }


        // Add |-----------------------------------
        [HttpPost("addEvent")]
        public async Task<IActionResult> addEvent(EventCreateDTO createDTO)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _eventService.AddEventAsync(createDTO);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }

        [HttpPost("addGuide")]
        public async Task<IActionResult> addGuide(GuideCreateDTO createDTO)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _guideService.AddGuideAsync(createDTO);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);
        }

        [HttpPost("addAdmin")]
        public async Task<IActionResult> addAdmin(AdminCreateDTO createDTO)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _adminService.AddAdminAsync(createDTO);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);
        }

        // Delete |-----------------------------------
        [HttpDelete("removeEvent")]
        public async Task<IActionResult> removeEvent(int ID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _eventService.DeleteEventAsync(ID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }

        
        [HttpDelete("removeAdmin")]
        public async Task<IActionResult> removeAdmin(int ID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _adminService.DeleteAdminAsync(ID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }, _response, this);
        }

        [HttpDelete("removeGuide")]
        public async Task<IActionResult> removeGuide(int ID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _guideService.DeleteGuideAsync(ID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }, _response, this);
        }

        //Update |-----------------------------------

        [HttpPut("UpdateEvent")]
        public async Task<IActionResult> updateEvent(int ID, EventUpdateDTO eventUpdate)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _eventService.UpdateEventAsync(ID, eventUpdate);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }


        [HttpPut("UpdateAdmin")]
        public async Task<IActionResult> updateAdmin(int ID, AdminUpdateDTO adminUpdate)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _adminService.UpdateAdminAsync(ID, adminUpdate);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }, _response, this);
        }

        [HttpPut("UpdateGuide")]
        public async Task<IActionResult> updateGuide(int ID, GuideUpdateDTO guideUpdate)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                _response.Result = await _guideService.UpdateGuideAsync(ID, guideUpdate);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }, _response, this);
        }


    }
}