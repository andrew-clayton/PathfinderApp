namespace Pathfinder.Models
{
    public class PathfinderConfiguration
    {
        public string InitialCountry { get; set; }
        public Dictionary<string, List<string>> AdjacencyList { get; set; }
    }
}
