using RoadRunner.APIAI;
using RoadRunner.GoogleAPIIntegration;
using RoadRunner.GoogleAPIIntegration.Models;
using RoadRunner.Models;
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

        public HandleRequestResult HandleRequest(string message)
        {
            HandleRequestResult result = new HandleRequestResult();
            PathRequestResponce pathResponce = new PathRequestResponce();

            var recognitionResponce = _recognitionService.TryRecogniseAddress(message);

            switch (recognitionResponce.RecognitionStatus)
            {
                case APIAI.Models.RecognitionStatus.Valid:
                    pathResponce = _pathRequestHandler.GetBestPath(recognitionResponce.Address);
                    break;
                case APIAI.Models.RecognitionStatus.Invalid:
                    result.StuckReason = ProgressStuckReason.PhraseNotParsed;
                    result.TextToDisplay = recognitionResponce.QuestionToAsk;
                    return result;
                case APIAI.Models.RecognitionStatus.AddressMissing:
                    result.StuckReason = ProgressStuckReason.AddressMissing;
                    result.TextToDisplay = recognitionResponce.QuestionToAsk;
                    return result;
            }

            result.TextToDisplay = pathResponce.PathInsructions;
            return result;
        }
    }
}