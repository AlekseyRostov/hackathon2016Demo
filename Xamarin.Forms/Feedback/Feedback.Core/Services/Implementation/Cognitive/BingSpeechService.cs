using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Feedback.Core.Services.Cognitive;
using Feedback.Core.Services.Implementation.Cognitive.Entities;
using Newtonsoft.Json;
using PCLStorage;

namespace Feedback.Core.Services.Implementation.Cognitive
{
    public class BingSpeechService : ISpeechService
    {
        private const string SpeechApiUri = "https://speech.platform.bing.com/recognize";
        private readonly ICognitiveServiceAuthenticator _authenticator;
        private readonly IDeviceService _deviceService;

        public BingSpeechService(ICognitiveServiceAuthenticator authenticator, IDeviceService deviceService)
        {
            _authenticator = authenticator;
            _deviceService = deviceService;
        }

        public async Task<string> SpeechToTextAsync(string recordingPath, string locale)
        {
            if(!_authenticator.HasValidToken)
            {
                await _authenticator.AuthenticateAsync(Constants.BingSpeechApiKey);
            }
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authenticator.Token}");
                var path = AddParameters(SpeechApiUri, locale);
                var recording = await FileSystem.Current.GetFileFromPathAsync(recordingPath);
                using(var fileStream = await recording.OpenAsync(FileAccess.Read))
                {
                    var streamContent = new StreamContent(fileStream);
                    streamContent.Headers.Add("Content-Type", $@"audio/wav; codec=""audio/pcm""; samplerate={_deviceService.AudioSampleRate}");
                    var result = await client.PostAsync(path, streamContent);
                    var response = await result.Content.ReadAsStringAsync();
                    if(result.StatusCode != HttpStatusCode.OK) throw new WebRequestException(result.StatusCode, response);
                    var speechResponse = JsonConvert.DeserializeObject<SpeechResponse>(response);
                    return speechResponse.Results?.FirstOrDefault()?.Lexical;
                }
            }
        }

        private string AddParameters(string requestUri, string locale)
        {
            requestUri += "?scenarios=ulm"; // websearch is the other main option.
            requestUri += "&appid=D4D52672-91D7-4C74-8AD8-42B1D98141A5"; // You must use this ID.
            requestUri += $"&locale={locale}";
            requestUri += "&version=3.0";
            requestUri += "&format=json";
            requestUri += "&instanceid=565D69FF-E928-4B7E-87DA-9A750B96D9E3";
            requestUri += $"&requestid={Guid.NewGuid()}";
            requestUri += $@"&device.os={_deviceService.OperatingSystem}";
            return requestUri;
        }
    }
}