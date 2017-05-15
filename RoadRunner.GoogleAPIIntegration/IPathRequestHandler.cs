using RoadRunner.GoogleAPIIntegration.Models;

namespace RoadRunner.GoogleAPIIntegration
{
    public interface IPathRequestHandler
    {
        PathRequestResponce GetBestPath(string destination, string travelMode);
    }
}