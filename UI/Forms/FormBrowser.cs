using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using PaulasCadenza.BaseUI.Forms;
using PaulasCadenza.Data;
using PaulasCadenza.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaulasCadenza.UI.Forms
{
	public partial class FormBrowser : FormCadenza
	{
		public FlashVarsModel FlashVars { get; private set; }
		public FigureData FigureData { get; private set; }

		private readonly ChromiumWebBrowser _browser;
		private readonly string _username, _password;
		private readonly IRequestContext _reqConext;

		public FormBrowser(Uri hotelUrl, string username, string password)
		{
			_username = username;
			_password = password;

			_reqConext = new RequestContextBuilder().
				WithCachePath(CEFSettings.GetUserCacheDirectory(_username).FullName).Create();

			_browser = new ChromiumWebBrowser(hotelUrl.ToString(), _reqConext);
			_browser.FrameLoadEnd += OnFrameLoadEnd;

			InitializeComponent();

			Controls.Add(_browser);
		}

		private async void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
		{
			var uri = new Uri(e.Url);
			if (uri.PathAndQuery.StartsWith("/hotel", StringComparison.OrdinalIgnoreCase))
			{
				// this is NOT task-async
				_browser.ExecuteScriptAsync(
					$"document.getElementsByName('email')[0].value='{_username}';" +
					$"document.getElementsByName('password')[0].value='{_password}';" +
					 "document.getElementsByClassName('login-form__button')[0].click();");
			}
			else if (uri.PathAndQuery.StartsWith("/client/hh", StringComparison.OrdinalIgnoreCase))
			{
				var src = await e.Frame.GetSourceAsync();
				var doc = new HtmlAgilityPack.HtmlDocument();
				doc.LoadHtml(src);

				var nodes = doc.DocumentNode.SelectNodes("//script[@type='text/javascript' and contains(., 'var flashvars')]");
				if (nodes.Count > 0)
				{
					var script = nodes[0].InnerText;
					var indFlashVars = script.IndexOf("flashvars");
					var indAssign = script.IndexOf('=', indFlashVars);
					if (indAssign != -1)
					{
						FlashVars = JsonConvert.DeserializeObject<FlashVarsModel>(script.Substring(indAssign + 1).Trim().Trim(';'));

						var hl = await GetStringResourceFromUrlAsync(_browser.GetMainFrame(), FlashVars.AvatarEditorPromoHabbos);
						var figures = await GetStringResourceFromUrlAsync(_browser.GetMainFrame(), FlashVars.ExternalFigurePartListTxt);
						FigureData = new FigureData(figures, hl);

						Invoke(new Action(() => DialogResult = DialogResult.OK));
					}
				}
			}
		}

		private static async Task<string> GetStringResourceFromUrlAsync(IFrame frm, Uri uri)
		{
			var script = (await Resources.GetResourceAsStringAsync(
				"PaulasCadenza.Resources.Scripts.HttpReq.js").ConfigureAwait(false)).
				Replace("%%URL%%", uri.ToString());
			var ret = await frm.EvaluateScriptAsPromiseAsync(script).ConfigureAwait(false);
			return ret.Result as string;
		}

		private void FormBrowser_FormClosing(object sender, FormClosingEventArgs e)
		{
			_reqConext?.Dispose();
			_browser.Dispose();
		}
	}
}
