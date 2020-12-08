using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaulasCadenza.Models
{
	public sealed class FigureDataModel
	{
		[XmlRoot(ElementName = "color")]
		public sealed class Color
		{
			[XmlAttribute(AttributeName = "id", DataType = "int")]
			public int Id { get; set; }

			[XmlAttribute(AttributeName = "index", DataType = "int")]
			public int Index { get; set; }

			[XmlAttribute(AttributeName = "club", DataType = "int")]
			public int Club { get; set; }

			[XmlAttribute(AttributeName = "selectable", DataType = "boolean")]
			public bool Selectable { get; set; }

			[XmlText]
			public string Text { get; set; }
		}

		[XmlRoot(ElementName = "palette")]
		public sealed class Palette
		{
			[XmlElement(ElementName = "color")]
			public List<Color> Colors { get; set; }

			[XmlAttribute(AttributeName = "id", DataType = "int")]
			public int Id { get; set; }
		}

		[XmlRoot(ElementName = "colors")]
		public sealed class Colors
		{
			[XmlElement(ElementName = "palette")]
			public List<Palette> Palettes { get; set; }
		}

		[XmlRoot(ElementName = "part")]
		public sealed class Part
		{
			[XmlAttribute(AttributeName = "id", DataType = "int")]
			public int Id { get; set; }

			[XmlAttribute(AttributeName = "type")]
			public string Type { get; set; }

			[XmlAttribute(AttributeName = "colorable", DataType = "boolean")]
			public bool Colorable { get; set; }

			[XmlAttribute(AttributeName = "index", DataType = "int")]
			public int Index { get; set; }

			[XmlAttribute(AttributeName = "colorindex", DataType = "int")]
			public int ColorIndex { get; set; }
		}

		[XmlRoot(ElementName = "set")]
		public sealed class Set
		{
			[XmlElement(ElementName = "part")]
			public List<Part> Parts { get; set; }

			[XmlAttribute(AttributeName = "id", DataType = "int")]
			public int Id { get; set; }

			[XmlAttribute(AttributeName = "gender")]
			public string Gender { get; set; }

			[XmlAttribute(AttributeName = "club", DataType = "int")]
			public int Club { get; set; }

			[XmlAttribute(AttributeName = "colorable", DataType = "boolean")]
			public bool Colorable { get; set; }

			[XmlAttribute(AttributeName = "selectable", DataType = "boolean")]
			public bool Selectable { get; set; }

			[XmlAttribute(AttributeName = "preselectable", DataType = "boolean")]
			public bool Preselectable { get; set; }

			[XmlAttribute(AttributeName = "sellable", DataType = "boolean")]
			public bool Sellable { get; set; }

			[XmlElement(ElementName = "hiddenlayers")]
			public HiddenLayers HiddenLayers { get; set; }
		}

		[XmlRoot(ElementName = "settype")]
		public sealed class SetType
		{
			[XmlElement(ElementName = "set")]
			public List<Set> Sets { get; set; }

			[XmlAttribute(AttributeName = "type")]
			public string Type { get; set; }

			[XmlAttribute(AttributeName = "paletteid", DataType = "int")]
			public int PaletteId { get; set; }

			[XmlAttribute(AttributeName = "mand_m_0", DataType = "boolean")]
			public bool Mand_m_0 { get; set; }

			[XmlAttribute(AttributeName = "mand_f_0", DataType = "boolean")]
			public bool Mand_f_0 { get; set; }

			[XmlAttribute(AttributeName = "mand_m_1", DataType = "boolean")]
			public bool Mand_m_1 { get; set; }

			[XmlAttribute(AttributeName = "mand_f_1", DataType = "boolean")]
			public bool Mand_f_1 { get; set; }
		}

		[XmlRoot(ElementName = "layer")]
		public sealed class Layer
		{
			[XmlAttribute(AttributeName = "parttype")]
			public string PartType { get; set; }
		}

		[XmlRoot(ElementName = "hiddenlayers")]
		public sealed class HiddenLayers
		{
			[XmlElement(ElementName = "layer")]
			public List<Layer> Layers { get; set; }
		}

		[XmlRoot(ElementName = "sets")]
		public sealed class Sets
		{
			[XmlElement(ElementName = "settype")]
			public List<SetType> SetTypes { get; set; }
		}

		[XmlRoot(ElementName = "figuredata")]
		public sealed class FigureData
		{
			[XmlElement(ElementName = "colors")]
			public Colors Colors { get; set; }

			[XmlElement(ElementName = "sets")]
			public Sets Sets { get; set; }
		}
	}
}
