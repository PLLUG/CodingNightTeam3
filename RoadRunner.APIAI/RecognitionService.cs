using ApiAiSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoadRunner.APIAI.Models;
using ApiAiSDK.Model;

namespace RoadRunner.APIAI
{
    public class RecognitionService : IRecognitionService
    {
        private const string ADDRESS_PARAMETER_KEY = "address";

        private APIAIProvider _provider;

        public RecognitionService()
        {
            _provider = new APIAIProvider();
        }

        public AddressResponce TryRecogniseAddress(string message)
        {
            AIResponse responce = _provider.GetConfiguredProvider().TextRequest(message);

            if (!ValidateResponce(responce))
                return null;

            return new AddressResponce
            {
                To = responce.Result.Parameters.FirstOrDefault(p => p.Key == ADDRESS_PARAMETER_KEY).Value.ToString()
            };
        }

        private bool ValidateResponce(AIResponse responce)
        {
            var parameters = responce.Result.Parameters;

            if (!parameters.ContainsKey(ADDRESS_PARAMETER_KEY))
                return false;

            return true;
        }
    }
}
