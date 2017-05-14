namespace RoadRunner.GoogleAPIIntegration
{
    public interface IPathRequestHandler
    {
        string GetBestPath(string destination);
    }
}