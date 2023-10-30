using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayEd.Core.Services;
using PayEd.Data.Dto;
using System;
using System.Threading.Tasks;

namespace PayEd.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamsController : ControllerBase
    {
        private readonly IStreamRepository _stream;

        public StreamsController(IStreamRepository stream)
        {
            _stream = stream;
        }

        [HttpGet("get-all-streams")]
        public async Task<IActionResult> GetIncomeStreams()
        {
            var streams = await _stream.GetStreamsAsync();
            if (streams.Suceeded)
                return Ok(streams);
            return BadRequest(streams);
        }

        [HttpGet("get-stream/{streamID}")]
        public async Task<IActionResult> GetIncomeStream(Guid streamID)
        {
            var stream = await _stream.GetStreamAsync(streamID);
            if (stream == null)
                return NotFound();
            return Ok(stream);
        }

        [HttpPost("create-a-stream")]
        public async Task<IActionResult> CreateIncomeStream(Guid userId, [FromBody] StreamDto income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Inputs in Fields");
            }
            var streamID = await _stream.CreateStreamAsync(userId, income);
            return CreatedAtAction("GetIncomeStream", new { streamID }, streamID);
        }

        [HttpPut("modify-stream/{streamID}")]
        public async Task<IActionResult> UpdateIncomeStream(Guid streamID, [FromBody] StreamDto incomeStreamDto)
        {
            var updatedStream = await _stream.UpdateStreamAsync(streamID, incomeStreamDto);
            if (updatedStream == null)
                return NotFound();
            return Ok(updatedStream);
        }

        [HttpDelete("delete-stream/{streamID}")]
        public async Task<IActionResult> DeleteIncomeStream(Guid streamID)
        {
            var deleted = await _stream.DeleteStreamAsync(streamID);
            if (deleted.Suceeded)
                return Ok(deleted);
            return BadRequest(deleted);
        }
    }
}
