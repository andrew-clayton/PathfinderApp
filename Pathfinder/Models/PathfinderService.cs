using Microsoft.Extensions.Options;

namespace Pathfinder.Models
{
    /// <summary>
    /// This class is used to find paths between countries in North America. The configuration is pulled from appsettings.json.
    /// </summary>
    /// <param name="config">This configuration determines what the adjacency list for countries are, and the starting point for a path.</param>
    public class PathfinderService(IOptions<PathfinderConfiguration> config) : IPathfinderService
    {
        /// <summary>
        /// This is an adjacency list, with countries as keys, and bordering countries as values.
        /// This is how we can traverse through various countries to build our path.
        /// </summary>
        private readonly Dictionary<string, List<string>> _adjacencyList = config.Value.AdjacencyList; // todo: add xml comments

        /// <summary>
        /// This is the starting point for any given path.
        /// </summary>
        private readonly string _initialCountry = config.Value.InitialCountry;

        /// <summary>
        /// Returns a list of strings representing the countries that must be passed through to reach a destination.
        /// </summary>
        /// <param name="destination">3-character string of a North American country code to reach (ex: CAN).</param>
        /// <exception cref="ArgumentNullException">Exception thrown for empty/whitespace input.</exception>
        /// <exception cref="ArgumentException">Exception thrown for an invalid country code input.</exception>
        public List<string> FindPath(string destination)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace((destination)))
            {
                throw new ArgumentNullException("destination country code cannot be null or empty");
            }

            if (!_adjacencyList.ContainsKey(destination))
            {
                throw new ArgumentException($"Invalid country code {destination}");
            }

            // Normalize input to the capital letters we expect in our dictionary
            destination = destination.ToUpper();

            // BFS Search for desired country
            List<string> traversedCountries = new();
            HashSet<string> visitedCountries = new();

            // In our BFS queue, we will store the current country and the countries visited up to that point.
            Queue<(string, List<string>)> bfsQueue = new();
            bfsQueue.Enqueue((_initialCountry, []));

            // BFS
            while (bfsQueue.Count > 0)
            {
                var (currentCountry, currentPath) = bfsQueue.Dequeue();

                if (currentCountry == destination)
                {
                    return [.. currentPath, destination];
                }

                // We have not reached the country we were looking for
                visitedCountries.Add(currentCountry);
                foreach (var adjacentCountry in _adjacencyList[currentCountry])
                {
                    // Add each neighboring country to our queue to search (unless we have already visited it)
                    if (!visitedCountries.Contains(adjacentCountry))
                    {
                        bfsQueue.Enqueue((adjacentCountry, [.. currentPath, currentCountry]));
                    }
                }
            }

            // If we have reached this point, the BFS finished without finding our destination country.
            throw new InvalidOperationException($"Path could not be found for country code {destination}");
        }
    }
}
