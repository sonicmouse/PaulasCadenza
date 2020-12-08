namespace PaulasCadenza.HabboNetwork.Models
{
	public interface ICommReadObjectDelegate
	{
		CommReadObject DeriveCommReadObject(ushort sendType);
	}
}
