using PaulasCadenza.HabboDHM;
using PaulasCadenza.HabboNetwork.Models;

namespace PaulasCadenza.Data
{
	public sealed class RC4Encryption : IEncryption
	{
		private readonly RC4 _rc4;

		public RC4Encryption(byte[] key)
		{
			_rc4 = new RC4(key);
		}

		public byte[] Decrypt(byte[] arr) =>
			_rc4.EnDecrypt(arr);

		public byte[] Encrypt(byte[] arr) =>
			_rc4.EnDecrypt(arr);
	}
}
