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
    [Route("/api/v1/room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRequestService _roomRequestService;
        public RoomController(IRoomRequestService roomRequestService)
        {
            _roomRequestService = roomRequestService;
        }

        [HttpPost("create")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateRoomAsync(RoomDto room)
        {
            try
            {
                var result = await _roomRequestService.CreateRoomAsync(room);
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

        [HttpPost("all")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateRoomAsync(RulesDto rules)
        {
            try
            {
                var result = await _roomRequestService.GetAllRoomsAsync(rules);
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

        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRoomByIDAsync(int id)
        {
            try
            {
                var result = await _roomRequestService.GetRoomByIDAsync(id);
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
