using PaulasCadenza.CommObjects;
using PaulasCadenza.Data;
using PaulasCadenza.HabboDHM;
using PaulasCadenza.HabboNetwork;
using PaulasCadenza.Utilities;
using System;
using System.Linq;

namespace PaulasCadenza.Models
{
	public sealed class CadenzaBotModel
	{
		public AccountModel Account { get; }
		public Communication Comm { get; }
		public FigureData Figure { get; }
		public FlashVarsModel FlashVars { get; }
		public DHM DHM { get; set; }
		public RoomUsers RoomUsers { get; private set; } = new RoomUsers();

		public CadenzaBotModel(AccountModel account, FigureData figureData, FlashVarsModel flashVars)
		{
			Account = account;
			Figure = figureData;
			FlashVars = flashVars;

			var ports = flashVars.ConnectionInfoPort.Split(',').Select(x => Convert.ToUInt16(x)).ToArray();
			Comm = new Communication(flashVars.ConnectionInfoHost,
				ports[PRNG.Instance.Next(ports.Length)], CommReadObjectsMap.Instance);
		}
	}
}
