using PaulasCadenza.HabboNetwork;
using PaulasCadenza.HabboNetwork.IO;

namespace PaulasCadenza.CommObjects.WriteCommObjects
{
	public sealed class WCOSearchRooms : CommWriteObject
	{
		private readonly string _searchCode, _filtering;

		public WCOSearchRooms(string searchCode, string filtering)
		{
			_searchCode = searchCode;
			_filtering = filtering;
		}

		public override ushort SendType => 2428;

		public override void Serialize(CommWriter writer)
		{
			writer.WriteString(_searchCode);
			writer.WriteString(_filtering);
		}
	}
}
