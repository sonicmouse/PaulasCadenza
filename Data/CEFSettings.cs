using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PaulasCadenza.Data
{
	public static class CEFSettings
	{
		public static DirectoryInfo GlobalCacheDirectory =>
			new DirectoryInfo(Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
					@"PaulasCadenza\Cache"));

		public static DirectoryInfo GetUserCacheDirectory(string username)
		{
			
			using (var md5 = MD5.Create())
			{
				var hash = string.Concat(
					md5.ComputeHash(
						Encoding.UTF8.GetBytes(
							username.Trim().ToLower())).
							Select(b => b.ToString("x2")));
				return new DirectoryInfo(Path.Combine(
					GlobalCacheDirectory.FullName, "users", hash));
			}
		}

		public static void SetupGlobalSettings()
		{
			Cef.EnableHighDPISupport();

			var settings = new CefSettings
			{
				CachePath = GlobalCacheDirectory.FullName
			};

			settings.CefCommandLineArgs.Add("enable-npapi", "1");
			settings.CefCommandLineArgs.Add("enable-system-flash", "1");

			Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
		}

		public static void TearDownGlobalSettings()
		{
			Cef.Shutdown();
		}
	}
}
