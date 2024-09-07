using Microsoft.AspNetCore.Mvc;
using Solution.dotnet_ids.Models;
using Solution.dotnet_ids.Models.DTO;
using Solution.dotnet_ids.Models.Request;
using Solution.dotnet_ids.Services;

namespace Solution.dotnet_ids.Controllers
{
    [ApiController]
    [Route("api/member")]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _memberService;
        private readonly ApiResponse _response;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
            _response = new();
        }


        [HttpPost("login")]
        public async Task<IActionResult> loginAsync(LoginRequestDTO loginRequestDTO)
        {
            var assertLogin = await _memberService.MemberLoginAsync(loginRequestDTO);

            if (assertLogin.User == null || string.IsNullOrEmpty(assertLogin.Token))
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSucess = false;
                _response.ErrorMessages.Add("Email or Password is incorrect");
                return BadRequest(_response);
            }

            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = assertLogin;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> registerAsync(MemberCreateDTO memberCreateDTO)
        {
            var assertLogin = await _memberService.MemberRegisterAsync(memberCreateDTO);

            if (assertLogin.User == null || string.IsNullOrEmpty(assertLogin.Token))
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSucess = false;
                _response.ErrorMessages.Add("Email already exists!");
                return BadRequest(_response);
            }

            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = assertLogin;
            return Ok(_response);
        }


        [HttpPost("addEventFromMember")]
        public async Task<IActionResult> addEventFromGuide(int memberID, int eventID)
        {
            return await WrapperService.ControllerWrapper(async () =>
            {
                await _memberService.addEventToMember(memberID, eventID);
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);

            }, _response, this);

        }

    }
}