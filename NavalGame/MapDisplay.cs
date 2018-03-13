using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace NavalGame
{
    public class MapDisplay : PictureBox
    {
        Game _Game;
        MoveType MoveMode;
        PointF _CameraPosition;
        int _CameraScale;
        Point? DragFrom;
        Point? ClickPoint;
        private Order? _CurrentOrder;
        private List<Point> _PossibleMoves;
        private List<Point> _PossibleLightShots;
        private List<Point> _PossibleHeavyShots;
        private Unit _LastSelectedUnit;
        Bitmap[] CachedTiles;
        OrdersDisplay _OrdersDisplay;
        ToolTip _ToolTip;

        public Game Game
        {
            get
            {
                return _Game;
            }

            set
            {
                if (value != _Game)
                {
                    if (_Game != null) _Game.Changed -= GameChanged;
                    _Game = value;
                    MoveMode = MoveType.Drag;
                    _CameraPosition = new PointF(value.Terrain.Width / 2, value.Terrain.Height / 2);
                    CameraScale = 50;
                    DragFrom = null;
                    _Game.Changed += GameChanged;
                }
            }
        }

        private void GameChanged()
        {
            if (Game.SelectedUnit != _LastSelectedUnit)
            {
                CurrentOrder = Order.Move;
                _PossibleMoves.Clear();
            }
            _LastSelectedUnit = Game.SelectedUnit;
            Invalidate();
            OrdersDisplay.GameChanged();
        }

        public OrdersDisplay OrdersDisplay
        {
            get
            {
                return _OrdersDisplay;
            }

            set
            {
                _OrdersDisplay = value;
            }
        }

        public int CameraScale
        {
            get
            {
                return _CameraScale;
            }

            set
            {
                int newScale = Math.Max(Math.Min(value, 1000), 30);

                if (newScale != _CameraPosition.X)
                {
                    _CameraScale = newScale;
                    Invalidate();
                }
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
                if (Game == null) return;
                float newPosX = Math.Max(Math.Min(value.X, Game.Terrain.Width), 0);
                float newPosY = Math.Max(Math.Min(value.Y, Game.Terrain.Height), 0);

                if (newPosX != _CameraPosition.X || newPosY != _CameraPosition.Y)
                {
                    _CameraPosition.X = newPosX;
                    _CameraPosition.Y = newPosY;
                    Invalidate();
                }
            }
        }

        public Order? CurrentOrder
        {
            get
            {
                return _CurrentOrder;
            }

            set
            {
                _CurrentOrder = value;
                Invalidate();
            }
        }

        public MapDisplay()
        {
            _ToolTip = new ToolTip();
            CachedTiles = new Bitmap[16];
            CameraScale = 20;
            _CurrentOrder = null;
            _PossibleMoves = new List<Point>();
            _PossibleLightShots = new List<Point>();
            _PossibleHeavyShots = new List<Point>();
        }

        //private void RunCamera(object sender, EventArgs e)
        //{
        ////        if (Cursor == Cursors.PanEast && CameraPosition.X + CameraSize.X < Game.Terrain.Width) CameraPosition.X++;
        ////        else if (Cursor == Cursors.PanSouth && CameraPosition.Y + CameraSize.Y < Game.Terrain.Height) CameraPosition.Y++;
        ////        else if (Cursor == Cursors.PanWest && CameraPosition.X > 0) CameraPosition.X--;
        ////        if (Cursor == Cursors.PanNorth && CameraPosition.Y > 0) CameraPosition.Y--;

        ////        if (Cursor == Cursors.PanNW && CameraPosition.X > 0 && CameraPosition.Y > 0)
        ////        {
        ////            CameraPosition.X--;
        ////            CameraPosition.Y--;
        ////        }
        ////        if (Cursor == Cursors.PanSE && CameraPosition.X + CameraSize.X < Game.Terrain.Width && CameraPosition.Y + CameraSize.Y < Game.Terrain.Height)
        ////        {
        ////            CameraPosition.X++;
        ////            CameraPosition.Y++;
        ////        }
        ////        if (Cursor == Cursors.PanNE && CameraPosition.X + CameraSize.X < Game.Terrain.Width && CameraPosition.Y > 0)
        ////        {
        ////            CameraPosition.X++;
        ////            CameraPosition.Y--;
        ////        }
        ////        if (Cursor == Cursors.PanSW && CameraPosition.X > 0 && CameraPosition.Y + CameraSize.Y < Game.Terrain.Height)
        ////        {
        ////            CameraPosition.X--;
        ////            CameraPosition.Y++;
        ////        }

        ////    #region Drag
        ////    #endregion

        ////    #region Buttons
        ////    #endregion

        //    Invalidate();
        //}

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Game == null) return;

            var counter = Bitmaps.Get("Data\\Counter.png");
            var selected = Bitmaps.Get("Data\\Selected.png");
            var highlight = Bitmaps.Get("Data\\Highlight.png");
            var fogOfWar = Bitmaps.Get("Data\\FogOfWar.png");
            if (CachedTiles[0] == null || CachedTiles[0].Height != CameraScale)
            {
                var terrainTextures = Bitmaps.Get("Data\\TerrainTextures.png");
                for (int i = 0; i < 16; ++i)
                {
                    if (CachedTiles[i] != null) CachedTiles[i].Dispose();
                    CachedTiles[i] = new Bitmap(CameraScale, CameraScale);
                    var g = Graphics.FromImage(CachedTiles[i]);
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    var srcRect = GetSrcRectangle((i & 1) != 0, (i & 2) != 0, (i & 4) != 0, (i & 8) != 0);
                    g.DrawImage(terrainTextures, new Rectangle(0, 0, CachedTiles[i].Width, CachedTiles[i].Height), srcRect, GraphicsUnit.Pixel);
                    g.Dispose();
                }
            }

            pe.Graphics.Clear(Color.Black);
            PointF m1 = DisplayToMap(new Point(0, 0));
            PointF m2 = DisplayToMap(new Point(Width, Height));

            int m1x = (int)Math.Floor(m1.X);
            int m1y = (int)Math.Floor(m1.Y);
            int m2x = (int)Math.Ceiling(m2.X);
            int m2y = (int)Math.Ceiling(m2.Y);

            m1x = Math.Max(m1x, 0);
            m1y = Math.Max(m1y, 0);
            m2x = Math.Min(m2x, Game.Terrain.Width);
            m2y = Math.Min(m2y, Game.Terrain.Height);
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            pe.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            _PossibleMoves.Clear();
            _PossibleLightShots.Clear();
            _PossibleHeavyShots.Clear();
            for (int x = m1x; x < m2x; x++)
            {
                for (int y = m1y; y < m2y; y++)
                {
                    // Calculate tile index
                    int tileIndex = 0;
                    TerrainType t = Game.Terrain.Get(x, y);
                    bool topLeft = IsQuadrantLand(t, Game.Terrain.Get(x - 1, y), Game.Terrain.Get(x - 1, y - 1), Game.Terrain.Get(x, y - 1));
                    bool topRight = IsQuadrantLand(t, Game.Terrain.Get(x, y - 1), Game.Terrain.Get(x + 1, y - 1), Game.Terrain.Get(x + 1, y));
                    bool bottomRight = IsQuadrantLand(t, Game.Terrain.Get(x + 1, y), Game.Terrain.Get(x + 1, y + 1), Game.Terrain.Get(x, y + 1));
                    bool bottomLeft = IsQuadrantLand(t, Game.Terrain.Get(x, y + 1), Game.Terrain.Get(x - 1, y + 1), Game.Terrain.Get(x - 1, y));
                    if (topLeft) tileIndex += 1;
                    if (topRight) tileIndex += 2;
                    if (bottomLeft) tileIndex += 4;
                    if (bottomRight) tileIndex += 8;

                    // Draw tile
                    Point p = MapToDisplay(new PointF(x, y));
                    pe.Graphics.DrawImage(CachedTiles[tileIndex], p.X, p.Y);
                    if (Game.CurrentPlayer == null) pe.Graphics.DrawImage(fogOfWar, new Rectangle(p, CachedTiles[0].Size));
                    else if (!Game.CurrentPlayer.IsTileVisible(new Point(x, y))) pe.Graphics.DrawImage(fogOfWar, new Rectangle(p, CachedTiles[0].Size));
                    pe.Graphics.DrawRectangle(Pens.Black, new Rectangle(p, CachedTiles[0].Size));

                    // Draw Move Range
                    if (CurrentOrder == Order.Move && Game.SelectedUnit != null)
                    {
                        if (Game.Terrain.Get(x, y, TerrainType.Land) == TerrainType.Sea)
                        {
                            if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) <= Game.SelectedUnit.MovesLeft)
                            {
                                pe.Graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));

                                if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) < 2)
                                {
                                    pe.Graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));
                                    _PossibleMoves.Add(new Point(x, y));
                                }
                            }
                        }
                    }

                    // Draw Light Artillery Range
                    if (CurrentOrder == Order.LightArtillery && Game.SelectedUnit != null)
                    {
                        if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) <= Game.SelectedUnit.LightRange)
                        {
                            pe.Graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));
                            _PossibleLightShots.Add(new Point(x, y));
                        }
                    }

                    // Draw Heavy Artillery Range
                    if (CurrentOrder == Order.HeavyArtillery && Game.SelectedUnit != null)
                    {
                        if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) <= Game.SelectedUnit.HeavyRange)
                        {
                            pe.Graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));
                            _PossibleHeavyShots.Add(new Point(x, y));
                        }
                    }
                }

                // Draw Units
                if (Game.CurrentPlayer != null)
                {
                    foreach (Unit unit in Game.Units)
                    {
                        if (Game.CurrentPlayer.IsTileVisible(unit.Position))
                        {
                            Point displayPos = MapToDisplay(unit.Position);
                            if (displayPos.X > -CameraScale || displayPos.Y > -CameraScale)
                            {
                                if (displayPos.X < Width + CameraScale || displayPos.Y < Height + CameraScale)
                                {
                                    pe.Graphics.DrawImage(counter, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                    pe.Graphics.DrawImage(Bitmaps.Get(unit.Bitmap), new Rectangle(new Point(displayPos.X + CameraScale / 8, displayPos.Y + CameraScale / 8), new Size((CameraScale / 8) * 6, (CameraScale / 8) * 6)));
                                    if (Game.SelectedUnit == unit) pe.Graphics.DrawImage(selected, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                }
                            }
                        }
                    }
                }
            }
            OrdersDisplay.Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (MoveMode == MoveType.Drag) DragFrom = e.Location;
            ClickPoint = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ClickPoint == null) return;
            if (PointDifference((Point)ClickPoint, e.Location) < 2)
            {
                // Click
                bool l = true;
                bool a = true;
                PointF mapClickPosition = DisplayToMap(e.Location);
                if (Game.SelectedUnit != null)
                {
                    // Move
                    if (CurrentOrder == Order.Move)
                    {
                        if (_PossibleMoves.Contains(Point.Truncate(mapClickPosition)))
                        {
                            Game.SelectedUnit.Position = Point.Truncate(mapClickPosition);
                            a = false;
                        }
                    }

                    // Light Artillery
                    else if (CurrentOrder == Order.LightArtillery)
                    {
                        if (_PossibleLightShots.Contains(Point.Truncate(mapClickPosition)))
                        {
                            Game.LightArtillery(Point.Truncate(mapClickPosition), Game.SelectedUnit);
                            a = false;
                        }
                    }

                    // Heavy Artillery
                    else if (CurrentOrder == Order.HeavyArtillery)
                    {
                        if (_PossibleHeavyShots.Contains(Point.Truncate(mapClickPosition)))
                        {
                            Game.HeavyArtillery(Point.Truncate(mapClickPosition), Game.SelectedUnit);
                            a = false;
                        }
                    }
                }

                // Selection
                if (a)
                {
                    if (Game.CurrentPlayer != null)
                    {
                        foreach (Unit unit in Game.CurrentPlayer.Units)
                        {
                            PointF k = DisplayToMap(e.Location);
                            Point j = new Point((int)Math.Floor(k.X), (int)Math.Floor(k.Y));
                            if (unit.Position == j)
                            {
                                Game.SelectedUnit = unit;
                                l = false;
                            }
                        }
                    }
                    if (l) Game.SelectedUnit = null;
                }
            }
            // Drag
            DragFrom = null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Focus();

            #region Pan
            if (MoveMode == MoveType.Pan)
            {
                if (e.X <= 50)
                {
                    if (e.Y <= 50)
                    {
                        Cursor = Cursors.PanNW;
                        return;
                    }
                    if (e.Y >= Size.Height - 50)
                    {
                        Cursor = Cursors.PanSW;
                        return;
                    }
                    Cursor = Cursors.PanWest;
                    return;
                }
                if (e.X >= Size.Width - 50)
                {
                    if (e.Y <= 50)
                    {
                        Cursor = Cursors.PanNE;
                        return;
                    }
                    if (e.Y >= Size.Height - 50)
                    {
                        Cursor = Cursors.PanSE;
                        return;
                    }
                    Cursor = Cursors.PanEast;
                    return;
                }
                if (e.Y <= 50)
                {
                    Cursor = Cursors.PanNorth;
                    return;
                }
                if (e.Y >= Size.Height - 50)
                {
                    Cursor = Cursors.PanSouth;
                    return;
                }
                Cursor = DefaultCursor;
            }
            #endregion

            #region Drag
            if (MoveMode == MoveType.Drag && DragFrom != null)
            {
                PointF cameraPosition = CameraPosition;
                cameraPosition.X += (DragFrom.Value.X - e.Location.X) / (float)CameraScale;
                cameraPosition.Y += (DragFrom.Value.Y - e.Location.Y) / (float)CameraScale;
                CameraPosition = cameraPosition;
                DragFrom = new Point(e.Location.X, e.Location.Y);
            }
            #endregion
        }

        protected override Cursor DefaultCursor
        {
            get
            {
                if (MoveMode == MoveType.Pan) return Cursors.NoMove2D;
                else return Cursors.Hand;
            }

        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            CameraScale = (int)Math.Round(Math.Pow(1.2, e.Delta / 120) * CameraScale);
        }

        static Terrain StringToTerrain(int width, string plan)
        {
            Terrain terrain = new Terrain(width, plan.Length / width);

            for (int y = 0; y < plan.Length / width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (plan[y * width + x] == '1') terrain.Set(x, y, TerrainType.Land);
                }
            }

            return terrain;
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

        public static float PointDifference(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }

    public static class Bitmaps
    {
        static Dictionary<string, Bitmap> _Cache = new Dictionary<string, Bitmap>();

        public static Bitmap Get(string filename)
        {
            filename = System.IO.Path.GetFullPath(filename.ToLower());
            Bitmap result;
            if (!_Cache.TryGetValue(filename, out result))
            {
                result = (Bitmap)Image.FromFile(filename);
                _Cache.Add(filename, result);
            }
            return result;
        }
    }
}

