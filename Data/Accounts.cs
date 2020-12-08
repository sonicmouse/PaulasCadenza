using Microsoft.Win32;
using Newtonsoft.Json;
using PaulasCadenza.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaulasCadenza.Data
{
	public static class Accounts
	{
		private static readonly string KeyPath = @"Software\PaulasCredenza";
		private static readonly string KeyName = "accounts";

		public static void AddAccount(AccountModel account)
		{
			var accts = GetAccounts().Where(x => !x.Equals(account)).ToList();
			accts.Add(account);
			SaveAccounts(accts);
		}

		public static void RemoveAccount(AccountModel account)
		{
			SaveAccounts(GetAccounts().Where(x => !x.Equals(account)));
		}

		public static IEnumerable<AccountModel> GetAccounts()
		{
			using (var bk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
			using (var key = bk.CreateSubKey(KeyPath, false))
			{
				var s = key.GetValue(KeyName) as string;
				if(!string.IsNullOrEmpty(s))
				{
					return JsonConvert.DeserializeObject<AccountModel[]>(s);
				}
			}
			return Array.Empty<AccountModel>();
		}

		public static void SaveAccounts(IEnumerable<AccountModel> accounts)
		{
			using (var bk = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
			using (var key = bk.CreateSubKey(KeyPath, true))
			{
				key.SetValue(KeyName, JsonConvert.SerializeObject(accounts));
			}
		}
	}
}
