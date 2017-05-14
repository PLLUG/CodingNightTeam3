using RoadRunner.APIAI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunner.APIAI
{
    public interface IRecognitionService
    {
        APIAIRequestResult TryRecogniseAddress(string message);
    }
}
