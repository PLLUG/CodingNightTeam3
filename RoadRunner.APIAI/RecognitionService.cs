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
        private const string GEOCITY_PARAMETER_KEY = "geo_city";

        private APIAIProvider _provider;

        public RecognitionService()
        {
            _provider = new APIAIProvider();
        }

        public APIAIRequestResult TryRecogniseAddress(string message)
        {
            AIResponse responce = _provider.GetConfiguredProvider().TextRequest(message);
            APIAIRequestResult requestResult = new APIAIRequestResult();

            requestResult = FulfillAction(requestResult, responce);
            
            if (!requestResult.ActionIncomplete)
            {
                requestResult.RecognitionStatus = RecognitionStatus.Valid;

                if (requestResult.Action == Actions.AddressSave)
                    requestResult.Address = responce.Result.Parameters.FirstOrDefault(p => p.Key == ADDRESS_PARAMETER_KEY).Value.ToString();
                else if (requestResult.Action == Actions.CitySave)
                    requestResult.City = responce.Result.Parameters.FirstOrDefault(p => p.Key == GEOCITY_PARAMETER_KEY).Value.ToString();
                else
                {
                    requestResult.QuestionToAsk = responce.Result.Fulfillment.Speech;
                    requestResult.RecognitionStatus = RecognitionStatus.Invalid;
                }
                    
            } else
            {
                requestResult.RecognitionStatus = RecognitionStatus.AddressMissing;
                requestResult.QuestionToAsk = responce.Result.Fulfillment.Speech;
            }

            return requestResult;
        }

        private static APIAIRequestResult FulfillAction(APIAIRequestResult requestResult, AIResponse responce)
        {
            requestResult.ActionIncomplete = responce.Result.ActionIncomplete;

            switch (responce.Result.Action)
            {
                case "save.address":
                    requestResult.Action = Actions.AddressSave;
                    return requestResult;
                case "save.city":
                    requestResult.Action = Actions.CitySave;
                    return requestResult;
                default:
                    requestResult.Action = Actions.None;
                    return requestResult;
            }
        }
    }
}
