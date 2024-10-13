using Microsoft.AspNetCore.Mvc;
using Pathfinder.Models;

namespace PathfinderApi.Controllers
{
    /// <summary>
    /// This is a controller for our Pathfinder API.
    /// </summary>
    /// <param name="pathfinderService"></param>
    [Route("/Countries")]
    [ApiController]
    public class PathfinderController(IPathfinderService pathfinderService) : ControllerBase
    {
        private readonly IPathfinderService _pathfinderSvc = pathfinderService;


        /// <summary>
        /// This method is to map a trip to the specified destination, from a default starting point.
        /// </summary>
        /// <param name="countryCode">3-character string of a North American country code to reach (ex: CAN).</param>
        /// <returns>A list of strings representing the countries that must be passed through to reach the destination, in that order.</returns>
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
