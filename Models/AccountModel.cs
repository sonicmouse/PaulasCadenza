using Newtonsoft.Json;
using PaulasCadenza.Utilities;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PaulasCadenza.Models
{
	public class AccountModel : IEquatable<AccountModel>
	{
		public string Email { get; set; }
		public Uri Hotel { get; set; }
		public byte[] Entropy { get; set; }
		public byte[] Password { get; set; }

		[JsonIgnore]
		public string PlainTextPassword
		{
			get
			{
				return Encoding.UTF8.GetString(
					ProtectedData.Unprotect(Password,
					Entropy, DataProtectionScope.CurrentUser));
			}
			set
			{
				Entropy = PRNG.Instance.NextBytes(new byte[32]);
				Password = ProtectedData.Protect(
					Encoding.UTF8.GetBytes(value),
					Entropy, DataProtectionScope.CurrentUser);
			}
		}

		public bool Equals(AccountModel other) =>
			(other != null) && Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase) &&
				Hotel.ToString().Equals(other.Hotel.ToString(), StringComparison.OrdinalIgnoreCase);

		public override int GetHashCode()
		{
			var fin = 0U;
			byte[] hash;
			using (var md5 = MD5.Create())
			{
				hash = md5.ComputeHash(
					Encoding.UTF8.GetBytes($"{Email.ToLower()}{Hotel.ToString().ToLower()}"));
			}
			for (var i = 0; i < hash.Length; ++i)
			{
				fin ^= (uint)(hash[i] << (i * 8 % 32));
			}
			return unchecked((int)~fin);
		}

		public override bool Equals(object obj) =>
			Equals(obj as AccountModel);

		public override string ToString() =>
			$"{Email} ({Hotel})";
	}
}
