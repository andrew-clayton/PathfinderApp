namespace PathfinderApi.Models
{
    /// <summary>
    /// This is an interface for the PathfinderService, which could be implemented in different ways.
    /// </summary>
    public interface IPathfinderService
    {
        /// <summary>
        /// FindPath is to map a trip to the specified destination, from a default starting point.
        /// </summary>
        /// <param name="destination">3-character string of a North American country code to reach (ex: CAN).</param>
        /// <returns>A list of strings representing the countries that must be passed through to reach the destination, in that order.</returns>
        List<string> FindPath(string destination);
    }
}
