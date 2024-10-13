namespace Pathfinder.Models
{
    public class PathfinderConfiguration
    {
        public string InitialCountry { get; set; }
        public Dictionary<string, List<string>> AdjacencyList { get; set; }

        public PathfinderConfiguration(string initialCountry, Dictionary<string, List<string>> adjacencyList)
        {
            InitialCountry = initialCountry;
            AdjacencyList = adjacencyList;
        }
    }
}
