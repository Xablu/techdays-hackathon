using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpeechToText
{
    public class Api
    {
        static string AUTH_URL = "https://api.cognitive.microsoft.com/sts/v1.0";
        static string AUTH_KEY = "3fb5915cfedb44cb919b52a13f8825de";
        static string DICTATION_URL = "https://speech.platform.bing.com/speech/recognition/dictation/cognitiveservices/v1?language=en-US";

        public Api()
        {
        }

		public static async Task<string> FetchTokenAsync()
		{
			using (var client = new HttpClient())
			{
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AUTH_KEY);
                UriBuilder uriBuilder = new UriBuilder(AUTH_URL);
				uriBuilder.Path += "/issueToken";

				var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
				return await result.Content.ReadAsStringAsync();
			}
		}
    }
}
