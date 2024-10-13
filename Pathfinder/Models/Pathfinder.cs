namespace PathfinderApi.Models
{
    /// <summary>
    /// This static class is used to find paths between countries in North America.
    /// </summary>
    public static class Pathfinder
    {
        // todo: move out to configuration
        private static readonly Dictionary<string, List<string>> AdjacencyList = new()
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

        private static readonly string InitialCountry = "USA";

        /// <summary>
        /// Returns a list of strings representing the countries that must be passed through to reach a destination.
        /// </summary>
        /// <param name="destination">The three-character code of a North American country to be reached.</param>
        /// <exception cref="ArgumentNullException">Exception thrown for empty/whitespace input.</exception>
        /// <exception cref="ArgumentException">Exception thrown for an invalid country code input.</exception>
        public static List<string> FindPath(string destination)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace((destination)))
            {
                throw new ArgumentNullException("destination country code cannot be null or empty");
            }

            if (!AdjacencyList.ContainsKey(destination))
            {
                throw new ArgumentException($"Invalid country code {destination}");
            }

            // BFS Search for desired country
            List<string> traversedCountries = new();
            HashSet<string> visitedCountries = new();

            // In our BFS queue, we will store the current country and the countries visited up to that point.
            Queue<(string, List<string>)> bfsQueue = new();
            bfsQueue.Enqueue((InitialCountry, []));

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
                foreach (var adjacentCountry in AdjacencyList[currentCountry])
                {
                    // Add each neighboring country to our queue to search (unless we have already visited it)
                    if (!visitedCountries.Contains(adjacentCountry))
                    {
                        bfsQueue.Enqueue((adjacentCountry, [.. currentPath, currentCountry]));
                    }
                }
            }

            // If we have reached this point, the BFS finished without finding our destination country.
            throw new InvalidOperationException($"Path could not be found for country code {destination}"); // todo: use a logger
        }
    }
}
