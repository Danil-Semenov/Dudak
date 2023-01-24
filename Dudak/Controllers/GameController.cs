using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Threading.Channels;
using Application;
using Core.Player;
using System.Linq;

namespace Durak.Controllers
{
    [ApiController]
    [Route("/api/v1/game")]
    public class GameController : ControllerBase
    {
        [HttpGet("{id}/add")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> AddAsync(int id, string channel, int port)
        {
            try
            {
                var room = GemeCore.Rooms.FirstOrDefault(r => r.Id == id);
                room.Port = port;
                room.Host = channel;
                return Ok();
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
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var room = GemeCore.Rooms.FirstOrDefault(r => r.Id == id);
                return Ok(new { result = new { Host = room.Host, Port = room.Port } });
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
