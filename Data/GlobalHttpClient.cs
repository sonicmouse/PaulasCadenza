using System.Net.Http;

namespace PaulasCadenza.Data
{
	public static class GlobalHttpClient
	{
		public static HttpClient Instance { get; } = new HttpClient();
	}
}
