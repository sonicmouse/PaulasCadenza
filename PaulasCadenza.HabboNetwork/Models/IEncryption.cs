namespace PaulasCadenza.HabboNetwork.Models
{
	public interface IEncryption
	{
		byte[] Encrypt(byte[] arr);
		byte[] Decrypt(byte[] arr);
	}
}
