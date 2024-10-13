using Microsoft.AspNetCore.Mvc;
using PathfinderApi.Models;

namespace PathfinderApi.Controllers
{
    [Route("/Countries")]
    [ApiController]
    public class PathfinderController(IPathfinderService pathfinderService) : ControllerBase
    {
        private readonly IPathfinderService _pathfinderSvc = pathfinderService;
        

        // We have our endpoint for request/response for the country codes
        [HttpGet("Path/{countryCode}")]
        public IActionResult FindPath(string countryCode)
        {
            // Check request validity
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                return BadRequest($"Invalid country code {countryCode}.");
            }

            // Execute request logic
            List<string> result;
            try
            {
                result = _pathfinderSvc.FindPath(countryCode);
            }
            catch (ArgumentException e)
            {
                return BadRequest($"Invalid request for country {countryCode}");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Encountered error: {e}");
            }

            // Return response
            return Ok(result);
        }
    }
}
