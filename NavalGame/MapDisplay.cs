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
    public class MapDisplay : PictureBox
    {
        private Game _Game;
        private MoveType _MoveMode; //ToDo: delete
        private PointF _CameraPosition;
        private int _CameraScale;
        private Point? _DragFrom;
        private Point? _ClickPoint;
        private SoundPlayer _SoundPlayer;
        private Order? _CurrentOrder;
        private List<Point> _PossibleMoves;
        private List<Point> _PossibleLightShots;
        private List<Point> _PossibleHeavyShots;
        private Unit _LastSelectedUnit;
        private Bitmap[] CachedTiles;
        private OrdersDisplay _OrdersDisplay;
        private ToolTip _ToolTip;
        private TileRenderer _TileRenderer;
        private int _OceanLayerId;
        private int _LandLayerId;
        private int _FogOfWarLayerId;
        private int _RangesLayerId;

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
                    _MoveMode = MoveType.Drag;
                    _CameraPosition = new PointF(value.Terrain.Width / 2, value.Terrain.Height / 2);
                    CameraScale = 50;
                    _DragFrom = null;
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
                int newScale = Math.Max(Math.Min(value, 200), 10);

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
            _TileRenderer = new TileRenderer(32);
            _OceanLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\Ocean.png"), Bitmaps.Get("Data\\SeaWrap.png"));
            _LandLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\Land.png"), null);
            _FogOfWarLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\FogOfWar.png"), Bitmaps.Get("Data\\FogOfWarWrap.png"));
            _RangesLayerId = _TileRenderer.AddLayer(TileRenderer.LayerLayout.Corners, Bitmaps.Get("Data\\Ranges.png"), null);
            _SoundPlayer = new SoundPlayer();
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

            var USACounter = Bitmaps.Get("Data\\USACounter.png");
            var germanyCounter = Bitmaps.Get("Data\\GermanyCounter.png");
            var japanCounter = Bitmaps.Get("Data\\JapanCounter.png");
            var englandCounter = Bitmaps.Get("Data\\EnglandCounter.png");
            var selected = Bitmaps.Get("Data\\Selected.png");

            DrawMap2(pe.Graphics);

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
                                switch (unit.Player.Faction)
                                {
                                    case Faction.USA:
                                        pe.Graphics.DrawImage(USACounter, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                        break;

                                    case Faction.Germany:
                                        pe.Graphics.DrawImage(germanyCounter, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                        break;

                                    case Faction.Japan:
                                        pe.Graphics.DrawImage(japanCounter, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                        break;

                                    case Faction.England:
                                        pe.Graphics.DrawImage(englandCounter, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                        break;
                                }
                                pe.Graphics.DrawImage(Bitmaps.Get(unit.Bitmap), new Rectangle(new Point(displayPos.X, displayPos.Y), new Size(CameraScale, CameraScale)));
                                RectangleF stringRectangle = new RectangleF(new Point(displayPos.X, displayPos.Y), new Size(CameraScale, CameraScale));
                                stringRectangle.Inflate(-0.05f * CameraScale, -0.05f * CameraScale);
                                StringFormat stringFormat = new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Far
                                };
                                pe.Graphics.DrawString(unit.Name, new Font("Tahoma", CameraScale * 0.15f), Brushes.Black, stringRectangle, stringFormat);
                                if (Game.SelectedUnit == unit) pe.Graphics.DrawImage(selected, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
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
            if (_MoveMode == MoveType.Drag) _DragFrom = e.Location;
            _ClickPoint = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_ClickPoint == null) return;
            if (PointDifference((Point)_ClickPoint, e.Location) < 2)
            {
                // Click
                bool l = true;
                bool a = true;
                Point mapClickPosition = Point.Truncate(DisplayToMap(e.Location));
                if (Game.SelectedUnit != null)
                {
                    // Move
                    if (CurrentOrder == Order.Move)
                    {
                        if (_PossibleMoves.Contains(mapClickPosition))
                        {
                            Game.SelectedUnit.Position = mapClickPosition;
                            PlaySound("Data\\Sailing.wav");
                            a = false;
                        }
                    }

                    // Light Artillery
                    else if (CurrentOrder == Order.LightArtillery)
                    {
                        if (_PossibleLightShots.Contains(mapClickPosition))
                        {
                            int hit = Game.LightArtillery(mapClickPosition, Game.SelectedUnit);
                            if (hit == 0)
                            {
                                PlaySound("Data\\LightArtilleryMiss.wav");
                                _ToolTip.IsBalloon = true;
                                _ToolTip.Show("Miss", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);

                            }
                            else
                            {
                                PlaySound("Data\\LightArtilleryHit.wav");
                                _ToolTip.IsBalloon = true;
                                _ToolTip.Show("Hit " + hit.ToString() + "%", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                            }
                            a = false;
                        }
                    }

                    // Heavy Artillery
                    else if (CurrentOrder == Order.HeavyArtillery)
                    {
                        if (_PossibleHeavyShots.Contains(mapClickPosition))
                        {
                            int hit = Game.HeavyArtillery(mapClickPosition, Game.SelectedUnit);
                            if (hit == 0)
                            {
                                PlaySound("Data\\HeavyArtilleryMiss.wav");
                                _ToolTip.IsBalloon = true;
                                _ToolTip.Show("Miss", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);

                            }
                            else
                            {
                                PlaySound("Data\\HeavyArtilleryHit.wav");
                                _ToolTip.IsBalloon = true;
                                _ToolTip.Show("Hit " + hit.ToString() + "%", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                            }
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
            _DragFrom = null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Focus();

            #region Pan
            if (_MoveMode == MoveType.Pan)
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
            if (_MoveMode == MoveType.Drag && _DragFrom != null)
            {
                PointF cameraPosition = CameraPosition;
                cameraPosition.X += (_DragFrom.Value.X - e.Location.X) / (float)CameraScale;
                cameraPosition.Y += (_DragFrom.Value.Y - e.Location.Y) / (float)CameraScale;
                CameraPosition = cameraPosition;
                _DragFrom = new Point(e.Location.X, e.Location.Y);
            }
            #endregion
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            //if (Game.SelectedUnit == null) return;
            //Unit unit = Game.SelectedUnit;
            //Point mapPos = Point.Truncate(DisplayToMap(Cursor.Position));
            //switch (CurrentOrder)
            //{
            //    case null:
            //        return;

            //    case Order.Move:
            //        if (_PossibleMoves.Contains(mapPos))
            //        {
            //            float baseFuelCost = PointDifference(unit.Position, mapPos);
            //            float fuelCost = (float)(baseFuelCost + (baseFuelCost * 0.5) * (unit.Speed - unit.MovesLeft));
            //            _ToolTip.IsBalloon = true;
            //            _ToolTip.Show("Moving here costs " + Math.Round(fuelCost, 2).ToString() + " fuel.", this, 3000);
            //        }
            //        break;

            //    case Order.LightArtillery:
            //        if (_PossibleLightShots.Contains(mapPos))
            //        {

            //        }
            //        break;
            //}
            //DisplayToMap(Cursor.Position);
        }

        protected override Cursor DefaultCursor
        {
            get
            {
                if (_MoveMode == MoveType.Pan) return Cursors.NoMove2D;
                else return Cursors.Hand;
            }

        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            CameraScale = (int)Math.Round(Math.Pow(1.2, e.Delta / 120) * CameraScale);
        }

        private void DrawMap(Graphics graphics)
        {
            var highlight = Bitmaps.Get("Data\\Highlight.png");
            var fogOfWar = Bitmaps.Get("Data\\FogOfWar.png");

            _PossibleMoves.Clear();
            _PossibleLightShots.Clear();
            _PossibleHeavyShots.Clear();

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

            graphics.Clear(Color.Black);
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
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

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
                    graphics.DrawImage(CachedTiles[tileIndex], p.X, p.Y);
                    if (Game.CurrentPlayer == null) graphics.DrawImage(fogOfWar, new Rectangle(p, CachedTiles[0].Size));
                    else if (!Game.CurrentPlayer.IsTileVisible(new Point(x, y))) graphics.DrawImage(fogOfWar, new Rectangle(p, CachedTiles[0].Size));
                    graphics.DrawRectangle(Pens.Black, new Rectangle(p, CachedTiles[0].Size));

                    // Draw Move Range
                    if (CurrentOrder == Order.Move && Game.SelectedUnit != null)
                    {
                        if (Game.Terrain.Get(x, y, TerrainType.Land) == TerrainType.Sea)
                        {
                            if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) <= Game.SelectedUnit.MovesLeft)
                            {
                                graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));

                                if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) < 2)
                                {
                                    graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));
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
                            graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));
                            if (Game.SelectedUnit.LightShotsLeft >= 1)
                            {
                                _PossibleLightShots.Add(new Point(x, y));
                            }
                        }
                    }

                    // Draw Heavy Artillery Range
                    if (CurrentOrder == Order.HeavyArtillery && Game.SelectedUnit != null)
                    {
                        if (PointDifference(Game.SelectedUnit.Position, new Point(x, y)) <= Game.SelectedUnit.HeavyRange)
                        {
                            graphics.DrawImage(highlight, new Rectangle(p, CachedTiles[0].Size));
                            if (Game.SelectedUnit.HeavyShotsLeft >= 1)
                            {
                                _PossibleHeavyShots.Add(new Point(x, y));
                            }
                        }
                    }
                }
            }
        }

        private void DrawMap2(Graphics graphics)
        {
            _TileRenderer.TileSize = CameraScale;
            _TileRenderer.DrawTiles(graphics, MapToDisplay(new Point(0, 0)), new Rectangle(0, 0, Game.Terrain.Width, Game.Terrain.Height), p =>
            {
                p.X = Math.Min(Math.Max(p.X, 0), Game.Terrain.Width - 1);
                p.Y = Math.Min(Math.Max(p.Y, 0), Game.Terrain.Height - 1);
                TerrainType t = Game.Terrain.Get(p.X, p.Y, TerrainType.Sea);
                int result = 0;
                if (Game.CurrentPlayer == null || !Game.CurrentPlayer.IsTileVisible(p))
                {
                    result |= _FogOfWarLayerId;
                }

                if (Game.SelectedUnit != null)
                {
                    switch (CurrentOrder)
                    {
                        case null: break;
                        case Order.Move:
                            if (PointDifference(Game.SelectedUnit.Position, p) <= Game.SelectedUnit.MovesLeft)
                            {
                                result |= _RangesLayerId;
                                if (PointDifference(Game.SelectedUnit.Position, p) < 2) _PossibleMoves.Add(p);
                            }
                            break;
                        case Order.LightArtillery:
                            if (PointDifference(Game.SelectedUnit.Position, p) <= Game.SelectedUnit.LightRange)
                            {
                                result |= _RangesLayerId;
                                _PossibleLightShots.Add(p);
                            }
                            break;
                        case Order.HeavyArtillery:
                            if (PointDifference(Game.SelectedUnit.Position, p) <= Game.SelectedUnit.HeavyRange)
                            {
                                result |= _RangesLayerId;
                                _PossibleHeavyShots.Add(p);
                            }
                            break;
                    }
                }
                switch(t)
                {
                    case TerrainType.Sea: result |= _OceanLayerId; break;
                    case TerrainType.Land: result |= _OceanLayerId | _LandLayerId; break;
                    default: throw new Exception();
                }
                return result;
            });

            Pen pen = new Pen(Color.FromArgb(64, 0, 0, 0));
            for (int x = 0; x < Game.Terrain.Width; x++)
            {
                graphics.DrawLine(pen, MapToDisplay(new Point(x, 0)), MapToDisplay(new Point(x, Game.Terrain.Height)));
            }
            for (int y = 0; y < Game.Terrain.Height; y++)
            {
                graphics.DrawLine(pen, MapToDisplay(new Point(0, y)), MapToDisplay(new Point(Game.Terrain.Width, y)));
            }
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

        public void PlaySound(string fileName)
        {
            _SoundPlayer.SoundLocation = fileName;
            _SoundPlayer.Play();
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

