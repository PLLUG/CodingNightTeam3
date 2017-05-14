using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunner.APIAI.Models
{
    public enum RecognitionStatus
    {
        Valid,
        Invalid,
        AddressMissing
    }

    public enum Actions
    {
        None,
        AddressSave,
        CitySave
    }

    public class APIAIRequestResult
    { 
        public RecognitionStatus RecognitionStatus { get; set; }
        public Actions Action { get; set; }
        public bool ActionIncomplete { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string QuestionToAsk { get; set; }
    }
}
