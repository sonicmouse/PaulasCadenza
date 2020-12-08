using Newtonsoft.Json;
using System;

namespace PaulasCadenza.Models
{
	public sealed class FlashVarsModel
	{
		[JsonProperty("external.texts.txt")]
		public Uri ExternalTextsTxt { get; set; }

		[JsonProperty("connection.info.port")]
		public string ConnectionInfoPort { get; set; }

		[JsonProperty("furnidata.load.url")]
		public Uri FurniDataLoadUrl { get; set; }

		[JsonProperty("external.variables.txt")]
		public Uri ExternalVariablesTxt { get; set; }

		[JsonProperty("client.allow.cross.domain")]
		public string ClientAllowCrossDomain { get; set; }

		[JsonProperty("url.prefix")]
		public string UrlPrefix { get; set; }

		[JsonProperty("external.override.texts.txt")]
		public Uri ExternalOverrideTextsTxt { get; set; }

		[JsonProperty("supersonic_custom_css")]
		public Uri SuperSonicCustomCss { get; set; }

		[JsonProperty("external.figurepartlist.txt")]
		public Uri ExternalFigurePartListTxt { get; set; }

		[JsonProperty("flash.client.origin")]
		public string FlashClientOrigin { get; set; }

		[JsonProperty("client.starting")]
		public string ClientStarting { get; set; }

		[JsonProperty("processlog.enabled")]
		public string ProcesslogEnabled { get; set; }

		[JsonProperty("has.identity")]
		public string HasIdentity { get; set; }

		[JsonProperty("avatareditor.promohabbos")]
		public Uri AvatarEditorPromoHabbos { get; set; }

		[JsonProperty("productdata.load.url")]
		public Uri ProductDataLoadUrl { get; set; }

		[JsonProperty("client.starting.revolving")]
		public string ClientStartingRevolving { get; set; }

		[JsonProperty("external.override.variables.txt")]
		public Uri ExternalOverrideVariablesTxt { get; set; }

		[JsonProperty("spaweb")]
		public string SpaWeb { get; set; }

		[JsonProperty("supersonic_application_key")]
		public string SuperSonicApplicationKey { get; set; }

		[JsonProperty("connection.info.host")]
		public string ConnectionInfoHost { get; set; }

		[JsonProperty("sso.ticket")]
		public string SSOTicket { get; set; }

		[JsonProperty("client.notify.cross.domain")]
		public string ClientNotifyCrossDomain { get; set; }

		[JsonProperty("account_id")]
		public string AccountId { get; set; }

		[JsonProperty("flash.client.url")]
		public string FlashClientUrl { get; set; }

		[JsonProperty("unique_habbo_id")]
		public string UniqueHabboId { get; set; }
	}
}
