using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaulasCadenza.Models
{
	public sealed class FigureDataHotLooksModel
	{
		[XmlRoot(ElementName = "habbo")]
		public sealed class Habbo
		{
			[XmlAttribute(AttributeName = "gender")]
			public string Gender { get; set; }
			[XmlAttribute(AttributeName = "figure")]
			public string Figure { get; set; }
			[XmlAttribute(AttributeName = "hash")]
			public string Hash { get; set; }
		}

		[XmlRoot(ElementName = "habbos")]
		public sealed class Habbos
		{
			[XmlElement(ElementName = "habbo")]
			public List<Habbo> Habbo { get; set; }

			[XmlAttribute(AttributeName = "url")]
			public string Url { get; set; }
		}
	}
}
