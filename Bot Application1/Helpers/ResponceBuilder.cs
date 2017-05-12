using RoadRunner.APIAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadRunner.Helpers
{
    public class ResponceBuilder
    {
        private IRecognitionService _recognitionService;

        public ResponceBuilder()
        {
            _recognitionService = new RecognitionService();
        }

        public string HandleRequest(string message)
        {
            var recognitionResponce = _recognitionService.TryRecogniseAddress(message);

            return recognitionResponce.To;
        }
    }
}