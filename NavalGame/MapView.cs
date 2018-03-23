using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Media;

namespace NavalGame
{
    public class MapView : PictureBox
    {
        TileRenderer _TileRenderer;
        int _OceanLayerId;
        int _LandLayerId;
        int _FogOfWarLayerId;
        int _RangesLayerId;
        int _CameraScale;
        PointF _CameraPosition;
        Terrain _Terrain;


        public MapView()
        {
            CameraScale = 50;
            CameraPosition = new PointF(0, 0);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawMap2(pe.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        public int CameraScale
        {
            get
            {
                return _CameraScale;
            }

            set
            {
                _CameraScale = value;
            }
        }

        public PointF CameraPosition
        {
            get
            {
                return _CameraPosition;
            }

            set
            {
                _CameraPosition = value;
            }
        }

        public Terrain Terrain
        {
            get
            {
                return _Terrain;
            }

            set
            {
                _Terrain = value;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            CameraScale = (int)Math.Round(Math.Pow(1.2, e.Delta / 120) * CameraScale);
        }

        private void DrawMap2(Graphics graphics)
        {
            if (Terrain == null)
            {
                if (Image != null)
                {
                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
 
                    graphics.DrawImage(Image, new Rectangle(0, 0, Width, Height), new Rectangle(0, 0, 128, 128), GraphicsUnit.Pixel);
                }
                return;
            }

            if (_TileRenderer == null)
            {
                _TileRenderer = new TileRenderer(32);
                _OceanLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\Ocean.png"), Bitmaps.Get("Data\\SeaWrap.png"));
                _LandLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\Land.png"), null);
                _FogOfWarLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\FogOfWar.png"), Bitmaps.Get("Data\\FogOfWarWrap.png"));
                _RangesLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\Ranges.png"), null);
            }

            CameraScale = Height / Terrain.Height;
            CameraPosition = new Point(Terrain.Width / 2, Terrain.Height / 2);

            _TileRenderer.TileSize = CameraScale;
            _TileRenderer.DrawTiles(graphics, MapToDisplay(new Point(0, 0)), new Rectangle(0, 0, Terrain.Width, Terrain.Height), p =>
            {
                p.X = Math.Min(Math.Max(p.X, 0), Terrain.Width - 1);
                p.Y = Math.Min(Math.Max(p.Y, 0), Terrain.Height - 1);
                TerrainType t = Terrain.Get(p.X, p.Y, TerrainType.Sea);
                int result = 0;
                switch(t)
                {
                    case TerrainType.Sea: result |= _OceanLayerId; break;
                    case TerrainType.Land: result |= _OceanLayerId | _LandLayerId; break;
                    default: throw new Exception();
                }
                return result;
            });

            Pen pen = new Pen(Color.FromArgb(64, 0, 0, 0));
            for (int x = 0; x < Terrain.Width; x++)
            {
                graphics.DrawLine(pen, MapToDisplay(new Point(x, 0)), MapToDisplay(new Point(x, Terrain.Height)));
            }
            for (int y = 0; y < Terrain.Height; y++)
            {
                graphics.DrawLine(pen, MapToDisplay(new Point(0, y)), MapToDisplay(new Point(Terrain.Width, y)));
            }
        }

        Point MapToDisplay(PointF mapPosition)
        {
            Point DisplayPosition = new Point();
            DisplayPosition.X = (int)((mapPosition.X - CameraPosition.X) * CameraScale + Width / 2);
            DisplayPosition.Y = (int)((mapPosition.Y - CameraPosition.Y) * CameraScale + Height / 2);
            return DisplayPosition;
        }

        PointF DisplayToMap(Point displayPosition)
        {
            PointF MapPosition = new PointF();
            MapPosition.X = (displayPosition.X - Width / 2) / (float)CameraScale + CameraPosition.X;
            MapPosition.Y = (displayPosition.Y - Height / 2) / (float)CameraScale + CameraPosition.Y;
            return MapPosition;
        }

        static Rectangle GetSrcRectangle(bool topLeft, bool topRight, bool bottomLeft, bool bottomRight)
        {
            int src = 64;

            if (topLeft && topRight && bottomRight && bottomLeft) return new Rectangle(src, src, src, src);
            if (topLeft && topRight && bottomRight && !bottomLeft) return new Rectangle(src * 3, src * 3, src, src);
            if (topLeft && topRight && !bottomRight && bottomLeft) return new Rectangle(src * 2, src * 3, src, src);
            if (topLeft && topRight && !bottomRight && !bottomLeft) return new Rectangle(src, src * 2, src, src);

            if (topLeft && !topRight && bottomRight && bottomLeft) return new Rectangle(0, src * 3, src, src);
            if (topLeft && !topRight && bottomRight && !bottomLeft) return new Rectangle(src * 3, src, src, src);
            if (topLeft && !topRight && !bottomRight && bottomLeft) return new Rectangle(src * 2, src, src, src);
            if (topLeft && !topRight && !bottomRight && !bottomLeft) return new Rectangle(src * 2, src * 2, src, src);

            if (!topLeft && topRight && bottomRight && bottomLeft) return new Rectangle(src, src * 3, src, src);
            if (!topLeft && topRight && bottomRight && !bottomLeft) return new Rectangle(0, src, src, src);
            if (!topLeft && topRight && !bottomRight && bottomLeft) return new Rectangle(src * 3, 0, src, src);
            if (!topLeft && topRight && !bottomRight && !bottomLeft) return new Rectangle(0, src * 2, src, src);

            if (!topLeft && !topRight && bottomRight && bottomLeft) return new Rectangle(src, 0, src, src);
            if (!topLeft && !topRight && bottomRight && !bottomLeft) return new Rectangle(0, 0, src, src);
            if (!topLeft && !topRight && !bottomRight && bottomLeft) return new Rectangle(src * 2, 0, src, src);
            if (!topLeft && !topRight && !bottomRight && !bottomLeft) return new Rectangle(src * 3, src * 2, src, src);

            else return new Rectangle(src * 3, src * 2, src, src);
        }

        static bool IsQuadrantLand(TerrainType t0, TerrainType t1, TerrainType t2, TerrainType t3)
        {
            byte landTiles = 0;
            if (t0 == TerrainType.Sea) return false;
            if (t1 == TerrainType.Land) landTiles++;
            if (t2 == TerrainType.Land) landTiles++;
            if (t3 == TerrainType.Land) landTiles++;
            if (landTiles > 2) return true;
            return false;
        }
    }
}

