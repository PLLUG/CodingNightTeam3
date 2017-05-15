using Google.Maps.Direction;
using Google.Maps.Geocoding;
using RoadRunner.GoogleAPIIntegration.Models;
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

        public PathRequestResponce GetBestPath(string destination, string travelMode)
        {
            var request = new Google.Maps.Direction.DirectionRequest();

            string parsedOrigin = GetExactParsedLocation(string.Join(", ", new string[] { "Universytetska street", CITY }));
            string parsedDestination = GetExactParsedLocation(string.Join(", ", new string[] { destination, CITY }));

            request.Origin = new Google.Maps.Location(parsedOrigin);
            request.Destination = new Google.Maps.Location(parsedDestination);
            request.Mode = ParseTravelMode(travelMode);
            request.Sensor = false;
            request.Language = "en";
            request.Region = "UA";

            var response = new DirectionService().GetResponse(request);
            return new PathRequestResponce
            {
                PathInsructions = ParseRoute(response.Routes[0].Legs[0].Steps)
            };
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

        private Google.Maps.TravelMode ParseTravelMode(string travelMode)
        {
            switch (travelMode)
            {
                case "on foot":
                    return Google.Maps.TravelMode.walking;
                case "by car":
                    return Google.Maps.TravelMode.driving;
                case "by bus":
                    return Google.Maps.TravelMode.transit;
                case "by bicycle":
                    return Google.Maps.TravelMode.bicycling;
                default:
                    return Google.Maps.TravelMode.driving;
            }
        }

        private string ParseRoute(DirectionStep[] steps)
        {
            String result = "";
            foreach (var directionStep in steps)
            {
                result += directionStep.HtmlInstructions.Replace("<b>", "").Replace("</b>", "").Replace("<div style=\"font-size:0.9em\">", ", ").Replace("</div>", "") + ". " + " --> ";
            }
            return result;
        }
    }
}
