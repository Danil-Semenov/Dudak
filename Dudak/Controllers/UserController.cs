using Application.Interface;
using DB.DTO;
using Durak.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DB.Entities;

namespace Durak.Controllers
{
    [ApiController]
    [Route("/api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRequestService _userRequestService;
        private readonly IUserService _userService;
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

        [BasicAuth]
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

        
        /*
                                   ПЕРВАЯ РЕАЛИЗАЦИЯ 
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            
                var result = await _userService.GetUserByLoginAndPasswordAsync(login,password);
                if (result.Login == login && result.Password == password)
                {
                    return Ok(new { result = result });
                }

                return NotFound();
        }
        */

        /*
                                  ВТОРАЯ РЕАЛИЗАЦИЯ
         
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            
            var result = await _userService.GetUserByLoginAndPasswordAsync(login,password);


            return result == null ? NotFound() : Ok(result);
        }
        */
        
        /*
                                  ТРЕТЬЯ РЕАЛИЗАЦИЯ
          
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            
            var result = await _userService.GetUserByLoginAndPasswordAsync(login,password);
            if (result is null) return BadRequest();
            return Ok(new { result = result });
        }
        */
        
        /*
                                ЧЕТВЕРТАЯ РЕАЛИЗАЦИЯ
         
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            var result = await _userService.GetUserByLoginAndPasswordAsync(login,password);
            var tuple = (true, result);
            if (result != null)
            {
                return tuple;
            }
            return BadRequest(204);
        }
        */
        
        
    }
}
