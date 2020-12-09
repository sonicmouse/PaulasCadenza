using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOAvatar : CommWriteObject
	{
		private readonly bool _isMale;
		private readonly string _figureData;

		public WCOAvatar(bool male, string figureData)
		{
			_isMale = male;
			_figureData = figureData;
		}

		public override ushort SendType => 2156;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_isMale ? "M" : "F");
			writer.WriteString(_figureData);
		}
	}
}
