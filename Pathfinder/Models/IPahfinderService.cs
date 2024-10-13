namespace PathfinderApi.Models
{
    public interface IPathfinderService
    {
        public List<string> FindPath(string destination);
    }
}
