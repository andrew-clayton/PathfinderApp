using Microsoft.Extensions.Options;
using Pathfinder.Models;
using PathfinderApi.Controllers;

namespace Pathfinder_UnitTests
{
    public class PathfinderSetup
    {
        public static PathfinderController GetController()
        {
            PathfinderService pathfinderSvc = GetPathfinderSvc();
            return new PathfinderController(pathfinderSvc);
        }

        /// <summary>
        /// xUnit tests can use an IEnumerable to provide MemberData for a Theory.
        /// This is data containing the destination, and the path that should be returned.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> GetPathTestData()
        {
            yield return new object[] { "PAN", new List<string> { "USA", "MEX", "GTM", "HND", "NIC", "CRI", "PAN" } };
            yield return new object[] { "BLZ", new List<string> { "USA", "MEX", "BLZ" } };
            yield return new object[] { "CAN", new List<string> { "USA", "CAN" } };
        }

        public static IOptions<PathfinderConfiguration> GetConfig()
        {
            string initialCountry = "USA";
            Dictionary<string, List<string>> adjacencyList = new()
            {
                ["CAN"] = ["USA"],
                ["USA"] = ["CAN", "MEX"],
                ["MEX"] = ["USA", "GTM", "BLZ"],
                ["BLZ"] = ["MEX", "GTM"],
                ["GTM"] = ["MEX", "BLZ", "SLV", "HND"],
                ["SLV"] = ["GTM", "HND"],
                ["HND"] = ["GTM", "SLV", "NIC"],
                ["NIC"] = ["HND", "CRI"],
                ["CRI"] = ["NIC", "PAN"],
                ["PAN"] = ["CRI"]
            };

            var config = new PathfinderConfiguration
            {
                InitialCountry = initialCountry,
                AdjacencyList = adjacencyList
            };
            return Options.Create(config);
        }

        public static PathfinderService GetPathfinderSvc()
        {
            var config = GetConfig();
            return new PathfinderService(config);
        }
    }
}
