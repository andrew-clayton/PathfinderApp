using PathfinderApi.Controllers;
using PathfinderApi.Models;

namespace Pathfinder_UnitTests
{
    public static class PathfinderSetup
    {
        public static PathfinderController GetController()
        {
            PathfinderService pathfinderSvc = new();
            return new PathfinderController(pathfinderSvc);
        }
    }
}
