using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Feedback.Core.Services.Cognitive;

namespace Feedback.Core.Services.Implementation.Cognitive
{
    public class CognitiveServiceAuthenticator : ICognitiveServiceAuthenticator
    {
        public static readonly string AccessTokenUri = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";
        private DateTimeOffset? _tokenDate;

        public bool HasValidToken => _tokenDate != null && DateTimeOffset.Now.Subtract(_tokenDate.Value) < TimeSpan.FromMinutes(9);

        public string Token { get; private set; }

        public async Task AuthenticateAsync(string subscriptionKey)
        {
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                var result = await client.PostAsync(AccessTokenUri, null);
                var response = await result.Content.ReadAsStringAsync();
                if(result.StatusCode != HttpStatusCode.OK) throw new WebRequestException(result.StatusCode, response);
                Token = response;
                _tokenDate = DateTimeOffset.Now;
            }
        }
    }
}