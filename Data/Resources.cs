using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace PaulasCadenza.Data
{
	public static class Resources
	{
		public static Task<string> GetResourceAsStringAsync(string resName)
		{
			using(var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resName))
			using(var sr = new StreamReader(s))
			{
				return sr.ReadToEndAsync();
			}
		}
	}
}
