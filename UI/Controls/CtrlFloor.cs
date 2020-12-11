using PaulasCadenza.BaseUI.Controls;
using PaulasCadenza.CommObjects.ReadCommObjects;
using PaulasCadenza.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace PaulasCadenza.UI.Controls
{
	public partial class CtrlFloor : CadenzaControl
	{
		private delegate void HandleTileDelegate(RectangleF rect, int height, Point ptOriginal);

		public sealed class TileClickedEventArgs : EventArgs
		{
			public int X { get; set; }
			public int Y { get; set; }
		}

		public event EventHandler<TileClickedEventArgs> TileClicked;

		public RCOHeightMap HeightMap
		{
			get => _heightMap;
			set
			{
				_heightMap = value;
				Invalidate();
			}
		}

		private float TileDim =>
			Math.Min(Size.Width / (float)HeightMap.Width, Size.Height / (float)HeightMap.Height);
		private PointF TileOffset => new PointF(
			Size.Width / 2.0f - TileDim * HeightMap.Width / 2.0f,
			Size.Height / 2.0f - TileDim * HeightMap.Height / 2.0f);

		private RCOHeightMap _heightMap;
		private RectangleF? _mouseOverTile;
		private readonly IReadOnlyList<SolidBrush> _gradient;
		private readonly HatchBrush _bgBrush =
			new HatchBrush(HatchStyle.SmallCheckerBoard, Color.White, Color.LightGray);
		private readonly SolidBrush _mouseBrush = new SolidBrush(Color.Black);

		public CtrlFloor()
		{
			InitializeComponent();
			_gradient = ColorGradient.Create(
				Color.FromArgb(0xef, 0x91, 0x10),
				Color.FromArgb(0x10, 0x6e, 0xef),
				(int)RCOHeightMap.MaxHeight).Select(x => new SolidBrush(x)).ToList();
		}

		private void DeriveUITiles(HandleTileDelegate @delegate)
		{
			if (HeightMap == null) { return; }

			var si = TileDim;
			var os = TileOffset;

			for (var x = 0; x < HeightMap.Width; ++x)
			{
				for (var y = 0; y < HeightMap.Height; ++y)
				{
					var v = (int)HeightMap.GetHeightAtPoint(x, y);
					@delegate(new RectangleF(os.X + x * si, os.Y + y * si, si, si), v, new Point(x, y));
				}
			}
		}

		private void CtrlFloor_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.PageUnit = GraphicsUnit.Pixel;
			e.Graphics.SmoothingMode = SmoothingMode.None;

			e.Graphics.FillRectangle(_bgBrush, new Rectangle(0, 0, Size.Width, Size.Height));

			DeriveUITiles((rect, height, ptOrig) =>
			{
				if (height >= 0)
				{
					e.Graphics.FillRectangle(_gradient[height], rect);
				}
			});

			if(_mouseOverTile.HasValue)
			{
				e.Graphics.FillRectangle(_mouseBrush, _mouseOverTile.Value);
			}
		}

		private void CtrlFloor_Click(object sender, EventArgs e)
		{
			if (e is MouseEventArgs args)
			{
				DeriveUITiles((rect, height, ptOrig) =>
				{
					if ((height >= 0) && rect.Contains(new Point(args.X, args.Y)))
					{
						TileClicked?.Invoke(this, new TileClickedEventArgs { X = ptOrig.X, Y = ptOrig.Y });
					}
				});
			}
		}

		private void CtrlFloor_MouseMove(object sender, MouseEventArgs e)
		{
			RectangleF? rectOver = null;
			DeriveUITiles((rect, height, ptOrig) =>
			{
				if ((height >= 0) && rect.Contains(new Point(e.X, e.Y)))
				{
					rectOver = rect;
				}
			});

			if (_mouseOverTile.HasValue ^ rectOver.HasValue)
			{
				_mouseOverTile = rectOver;
				Cursor = _mouseOverTile.HasValue ? Cursors.Hand : Cursors.Default;
				Invalidate();
			}
			else if (rectOver.HasValue)
			{
				if (_mouseOverTile.Value != rectOver.Value)
				{
					_mouseOverTile = rectOver;
					Cursor = Cursors.Hand;
					Invalidate();
				}
			}
		}

		private void CtrlFloor_MouseLeave(object sender, EventArgs e)
		{
			if (_mouseOverTile.HasValue)
			{
				_mouseOverTile = null;
				Cursor = Cursors.Default;
				Invalidate();
			}
		}
	}
}
