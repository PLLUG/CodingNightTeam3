using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoadRunner.Models
{
    public enum ProgressStuckReason
    {
        None,
        PhraseNotParsed,
        AddressMissing,
        MoreThanOneCityFound
    }

    public class HandleRequestResult
    {
        public ProgressStuckReason StuckReason { get; set; }
        public string TextToDisplay { get; set; }

    }
}