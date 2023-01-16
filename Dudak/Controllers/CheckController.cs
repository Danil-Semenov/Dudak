using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace Durak.Controllers
{
    [ApiController]
    [Route("/api/v1/check")]
    public class CheckController : ControllerBase
    {
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CheckByIDAsync(int id)
        {
            try
            {
                return Ok(new { result = id });
            }
            catch (Exception ex)
            {
                //Request.Body.Position = 0;
                //var rawRequestBody = await new StreamReader(Request.Body).ReadToEndAsync();
                //await _requestService.SetLogAsync(Request.Path.Value, rawRequestBody, ex.Message);
                return BadRequest(new { status = 400, error = ex.Message });
            }
        }

        [HttpGet("string")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> CheckByStringAsync()
        {
            try
            {
                return Ok(new { result = "Ok" });
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
