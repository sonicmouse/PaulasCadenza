using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.ReadCommObjects
{
	public sealed class RCOSelfAvatarUpdated : CommReadObject
	{
		public string Avatar { get; private set; }
		public bool IsMale { get; private set; }

		public override ushort SendType => 1250;

		public override void Deserialize(CommReader reader)
		{
			Avatar = reader.ReadString();
			IsMale = reader.ReadString().ToUpper() == "M";
		}
	}
}
