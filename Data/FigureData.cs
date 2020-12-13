using PaulasCadenza.Models;
using PaulasCadenza.Utilities;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PaulasCadenza.Data
{
	public sealed class FigureData
	{
		private readonly FigureDataModel.FigureData _figureData;
		private readonly FigureDataHotLooksModel.Habbos _figureDataHL;

		public FigureData(string xmlFigureData, string xmlFigureDataHotLooks)
		{
			var serializer = new XmlSerializer(typeof(FigureDataModel.FigureData));

			if(!string.IsNullOrEmpty(xmlFigureData))
			{
				using (var sr = new StringReader(xmlFigureData))
				{
					_figureData = (FigureDataModel.FigureData)serializer.Deserialize(sr);
				}
			}

			serializer = new XmlSerializer(typeof(FigureDataHotLooksModel.Habbos));

			if(!string.IsNullOrEmpty(xmlFigureDataHotLooks))
			{
				using (var sr = new StringReader(xmlFigureDataHotLooks))
				{
					_figureDataHL = (FigureDataHotLooksModel.Habbos)serializer.Deserialize(sr);
				}
			}
		}

		private string GetRandomPart(string part, bool male)
		{
			var sb = new StringBuilder();

			var partSetType = _figureData.Sets.SetTypes.Single(x => x.Type == part);
			var partSets = partSetType.Sets.Where(x => !x.Sellable && x.Selectable &&
				(x.Club == 0) && ((x.Gender == "U") || (x.Gender == (male ? "M" : "F")))).ToArray();

			var randomPartSet = partSets[PRNG.Instance.Next(partSets.Length)];
			sb.Append($"{part}-{randomPartSet.Id}");
			if (randomPartSet.Colorable)
			{
				var palettes = _figureData.Colors.Palettes.Where(x => x.Id == partSetType.PaletteId).
					Single().Colors.Where(x => (x.Club == 0) && x.Selectable).ToArray();
				sb.Append($"-{palettes[PRNG.Instance.Next(palettes.Length)].Id}");
			}

			return sb.ToString();
		}

		public string GetRandomFigure(out bool isMaleGender)
		{
			isMaleGender = Convert.ToBoolean(PRNG.Instance.Next(2));
			var isMale = isMaleGender;
			return string.Join(".",
				_figureData.Sets.SetTypes.Select(x => GetRandomPart(x.Type, isMale)));
		}

		public string GetRandomHotLookFigure(out bool isMaleGender)
		{
			var hl = _figureDataHL.Habbo[PRNG.Instance.Next(_figureDataHL.Habbo.Count)];
			isMaleGender = hl.Gender.ToLower() == "m";
			return hl.Figure;
		}
	}
}
