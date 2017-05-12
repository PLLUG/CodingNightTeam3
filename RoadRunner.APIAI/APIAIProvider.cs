using ApiAiSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunner.APIAI
{
    internal class APIAIProvider
    {
        private const string ACCESS_TOKEN = "c17986e091524c84b889ffdea6b3cd66";
        private readonly SupportedLanguage SUPPORTED_LANGUAGE = SupportedLanguage.English;
        private ApiAi _apiAI;

        public APIAIProvider(string accessToken = ACCESS_TOKEN, SupportedLanguage supportedLanguage = null)
        {
            this._apiAI = InitializeAPIAIProvider(accessToken, supportedLanguage ?? SUPPORTED_LANGUAGE);
        }

        public ApiAi GetConfiguredProvider()
        {
            if (this._apiAI == null)
                throw new InvalidProgramException("Underlying provider was not initialized");

            return this._apiAI;
        }

        private ApiAi InitializeAPIAIProvider(string accessToken, SupportedLanguage supportedLanguage)
        {
            AIConfiguration aiConfig = new AIConfiguration(accessToken, supportedLanguage ?? SUPPORTED_LANGUAGE);
            return new ApiAi(aiConfig);
        }
    }
}
