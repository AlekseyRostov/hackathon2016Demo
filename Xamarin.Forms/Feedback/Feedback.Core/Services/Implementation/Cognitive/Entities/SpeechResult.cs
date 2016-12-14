using Newtonsoft.Json;

namespace Feedback.Core.Services.Implementation.Cognitive.Entities
{
    [JsonObject("result")]
    public class SpeechResult
    {
        public string Scenario { get; set; }
        public string Name { get; set; }
        public string Lexical { get; set; }
        public double Confidence { get; set; }
    }
}