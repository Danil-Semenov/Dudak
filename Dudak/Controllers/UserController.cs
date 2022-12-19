using Application.Interface;
using DB.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Durak.Controllers
{
    [ApiController]
    [Route("/api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRequestService _userRequestService;
        public UserController(IUserRequestService userRequestService)
        {
            _userRequestService = userRequestService;
        }

        [HttpPost("create")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateProfileAsync(UserDto profile)
        {
            try
            {
                var result = await _userRequestService.CreateProfileAsync(profile);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                //Request.Body.Position = 0;
                //var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                //await _requestService.SetLogAsync(Request.Path.Value, rawRequestBody, ex.Message);
                return BadRequest(new { status = 400, error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> EditProfileAsync(UserDto profile, int id)
        {
            try
            {
                var result = await _userRequestService.EditProfileAsync(profile,id);
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                //Request.Body.Position = 0;
                //var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                //await _requestService.SetLogAsync(Request.Path.Value, rawRequestBody, ex.Message);
                return BadRequest(new { status = 400, error = ex.Message });
            }
        }

        [HttpGet("guest")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetGuestProfileAsync()
        {
            try
            {
                var result = await _userRequestService.GetGuestProfileAsync();
                return Ok(new { result = result });
            }
            catch (Exception ex)
            {
                //Request.Body.Position = 0;
                //var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                //await _requestService.SetLogAsync(Request.Path.Value, rawRequestBody, ex.Message);
                return BadRequest(new { status = 400, error = ex.Message });
            }
        }
    }
}
