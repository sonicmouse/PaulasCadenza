using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PaulasCadenza.Data
{
	public static class DadJokes
	{
		private sealed class JokeModel
		{
			[JsonProperty("joke")]
			public string Joke { get; set; }
		}

		public static async Task<string> GetAsync()
		{
			using(var msg = new HttpRequestMessage(HttpMethod.Get, new Uri("https://icanhazdadjoke.com/")))
			{
				msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				using (var req = await GlobalHttpClient.Instance.SendAsync(msg))
				{
					req.EnsureSuccessStatusCode();

					using (var s = await req.Content.ReadAsStreamAsync())
					using (var sr = new StreamReader(s))
					using (var jtr = new JsonTextReader(sr))
					{
						return new JsonSerializer().Deserialize<JokeModel>(jtr).Joke;
					}
				}
			}
		}
	}
}
