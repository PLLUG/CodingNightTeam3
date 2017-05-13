namespace RoadRunner.GoogleAPIIntegration
{
    public interface IPathRequestHandler
    {
        object GetBestPath(string destination);
    }
}