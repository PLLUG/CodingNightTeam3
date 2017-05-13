using Google.Maps.Direction;
using Google.Maps.Geocoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunner.GoogleAPIIntegration
{
    public class PathRequestHandler : IPathRequestHandler
    {
        private const string CITY = "Lviv";

        public object GetBestPath(string destination)
        {
            var request = new Google.Maps.Direction.DirectionRequest();

            string parsedOrigin = GetExactParsedLocation(string.Join(", ", new string[] { "Universytetska street", CITY }));
            string parsedDestination = GetExactParsedLocation(string.Join(", ", new string[] { destination, CITY }));

            request.Origin = new Google.Maps.Location(parsedOrigin);
            request.Destination = new Google.Maps.Location(parsedDestination);
            request.Mode = Google.Maps.TravelMode.walking;
            request.Sensor = false;
            request.Language = "en";
            request.Region = "UA";

            var responce = new DirectionService().GetResponse(request);

            return new object();
        }

        private string GetExactParsedLocation(string location)
        {
            var request = new GeocodingRequest();
            request.Address = location;
            request.Sensor = false;

            var responce = new GeocodingService().GetResponse(request);

            if (responce.Results.Length > 0)
                return responce.Results[0].FormattedAddress;

            return location;
        }
    }
}
