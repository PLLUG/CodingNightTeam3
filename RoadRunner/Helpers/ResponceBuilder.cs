using RoadRunner.APIAI;
using RoadRunner.GoogleAPIIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadRunner.Helpers
{
    public class ResponceBuilder
    {
        private IRecognitionService _recognitionService;
        private IPathRequestHandler _pathRequestHandler;

        public ResponceBuilder()
        {
            _recognitionService = new RecognitionService();
            _pathRequestHandler = new PathRequestHandler();
        }

        public string HandleRequest(string message)
        {
            var recognitionResponce = _recognitionService.TryRecogniseAddress(message);

            if (recognitionResponce == null)
                return "Didn't get it";

            return _pathRequestHandler.GetBestPath(recognitionResponce.To);
        }
    }
}