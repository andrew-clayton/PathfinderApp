using PathfinderApi.Controllers;
using PathfinderApi.Models;

namespace Pathfinder_UnitTests
{
    public class PathfinderSetup
    {
        public static PathfinderController GetController()
        {
            PathfinderService pathfinderSvc = new();
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
    }
}
