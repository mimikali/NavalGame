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
                    if (_Game != null)
                    {
                        _Game.Changed -= GameChanged;
                        _Game.Sinking -= Sinking;
                        _Game.SubmarineDetected -= SubmarineDetected;
                        _Game.PlayerChanged -= PlayerChanged;
                    }
                    _Game = value;
                    _MoveMode = MoveType.Drag;
                    _CameraPosition = new PointF(value.Terrain.Width / 2, value.Terrain.Height / 2);
                    CameraScale = 50;
                    _DragFrom = null;
                    _Game.Changed += GameChanged;
                    _Game.Sinking += Sinking;
                    _Game.SubmarineDetected += SubmarineDetected;
                    _Game.PlayerChanged += PlayerChanged;
                }
            }
        }

        private void GameChanged()
        {
            if (Game.SelectedUnit != null && Game.SelectedUnit != _LastSelectedUnit)
            {
                if (Game.SelectedUnit.Player == Game.CurrentPlayer && CurrentOrder != Order.Move)
                {
                    if (Game.SelectedUnit != null && Game.SelectedUnit.Type.Abilities.Contains(Order.Move))
                    {
                        CurrentOrder = Order.Move;
                    }
                }
            }
            _LastSelectedUnit = Game.SelectedUnit;

            if (CurrentOrder == Order.SearchMines)
            {
                _ToolTip.Show(Game.SearchMines(Game.SelectedUnit).ToString() + " mine(s) found.", this, MapToDisplay(Game.SelectedUnit.Position), 2000);
                CurrentOrder = null;
            }

            Invalidate();
            OrdersDisplay.GameChanged();
        }

        private void Sinking()
        {
            PlaySound("Data\\Sinking.wav");
        }

        private void SubmarineDetected()
        {
            PlaySound("Data\\SonarPing.wav");
        }

        private void PlayerChanged()
        {
            CameraScale = 0;
            CameraPosition = new Point(Game.Terrain.Width / 2, Game.Terrain.Height / 2);
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
                int newScale = Math.Max(Math.Min(value, 150), 15);

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
                if (Game != null) Game.FireChangedEvent();
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
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if (Game == null) return;

            var USACounter = Bitmaps.Get("Data\\USACounter.png");
            var germanyCounter = Bitmaps.Get("Data\\GermanyCounter.png");
            var japanCounter = Bitmaps.Get("Data\\JapanCounter.png");
            var englandCounter = Bitmaps.Get("Data\\EnglandCounter.png");
            var neutralCounter = Bitmaps.Get("Data\\NeutralCounter.png");
            var selected = Bitmaps.Get("Data\\Selected.png");

            DrawMap(pe.Graphics);

            // Draw Mines

            foreach (Mine mine in Game.Mines)
            {
                Point displayPos = MapToDisplay(mine.Position);

                if (mine.IsVisible || Game.CurrentPlayer != null && Game.CurrentPlayer.Faction == mine.Faction)
                {
                    if (displayPos.X > -CameraScale || displayPos.Y > -CameraScale)
                    {
                        if (displayPos.X < Width + CameraScale || displayPos.Y < Height + CameraScale)
                        {
                            Rectangle counterRect = new Rectangle(displayPos, new Size(CameraScale, CameraScale));
                            counterRect.Inflate(0 - counterRect.Width / 5, 0 - counterRect.Height / 5);

                            switch (mine.Faction)
                            {
                                case Faction.USA:
                                    pe.Graphics.DrawImage(USACounter, counterRect);
                                    break;

                                case Faction.Germany:
                                    pe.Graphics.DrawImage(germanyCounter, counterRect);
                                    break;

                                case Faction.Japan:
                                    pe.Graphics.DrawImage(japanCounter, counterRect);
                                    break;

                                case Faction.England:
                                    pe.Graphics.DrawImage(englandCounter, counterRect);
                                    break;

                                case Faction.Neutral:
                                    pe.Graphics.DrawImage(neutralCounter, counterRect);
                                    break;
                            }

                            pe.Graphics.DrawImage(Bitmaps.Get("Data\\Mine.png"), counterRect);
                        }
                    }
                }
            }

            // Draw Units
            if (Game.CurrentPlayer != null)
            {
                foreach (Unit unit in Game.Units)
                {
                    if (Game.IsUnitVisibleForPlayer(Game.CurrentPlayer, unit) || unit.Type.AlwaysVisible)
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

                                    case Faction.Neutral:
                                        pe.Graphics.DrawImage(neutralCounter, new Rectangle(displayPos, new Size(CameraScale, CameraScale)));
                                        break;
                                }
                                pe.Graphics.DrawImage(unit.Type.Bitmap, new Rectangle(new Point(displayPos.X, displayPos.Y), new Size(CameraScale, CameraScale)));
                                RectangleF stringRectangle = new RectangleF(new Point(displayPos.X, displayPos.Y), new Size(CameraScale, CameraScale));
                                stringRectangle.Inflate(-0.1f * CameraScale, -0.1f * CameraScale);
                                StringFormat bottomStringFormat = new StringFormat()
                                {
                                    Alignment = StringAlignment.Center,
                                    LineAlignment = StringAlignment.Far
                                };
                                Font nameFont = GetFontFromSize(stringRectangle.Width, unit.Name, 0.1f * CameraScale, 0.16f * CameraScale, pe.Graphics);
                                pe.Graphics.DrawString(unit.Name, nameFont, Brushes.Black, stringRectangle, bottomStringFormat);
                                if (!string.IsNullOrEmpty(unit.Information))
                                {
                                    StringFormat topStringFormat = new StringFormat()
                                    {
                                        Alignment = StringAlignment.Center,
                                        LineAlignment = StringAlignment.Near,
                                    };
                                    Font infoFont = GetFontFromSize(stringRectangle.Width, unit.Information, 0.1f * CameraScale, nameFont.Size, pe.Graphics);
                                    pe.Graphics.DrawString(unit.Information, infoFont, Brushes.Black, stringRectangle, topStringFormat);
                                }
                                if (Game.SelectedUnit == unit)
                                {
                                    Rectangle rectangle = new Rectangle(displayPos, new Size(CameraScale, CameraScale));
                                    rectangle.Inflate((int)Math.Round(CameraScale / 16f), (int)Math.Round(CameraScale / 16f));
                                    pe.Graphics.DrawImage(selected, rectangle);
                                }
                                if (unit.MovesLeft >= 1)
                                {
                                    Rectangle rectangle = new Rectangle(displayPos.X, displayPos.Y, CameraScale / 8, CameraScale / 8);
                                    pe.Graphics.FillEllipse(Brushes.White, rectangle);
                                    pe.Graphics.DrawEllipse(Pens.Black, rectangle);
                                }
                            }
                        }
                    }
                }
            }
            OrdersDisplay.Invalidate();
        }

        static Font GetFontFromSize(float space, string text, float minFontSize, float maxFontSize, Graphics graphics)
        {
            Font font = new Font("Tahoma", 10);
            float width = graphics.MeasureString(text, font).Width;
            float size = Math.Max(Math.Min(space / width * font.Size, maxFontSize), minFontSize);
            return new Font("Tahoma", size);
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
            if (PointDifference((Point)_ClickPoint, e.Location) < 8)
            {
                // Click
                bool selectionPossible = true;
                Point mapClickPosition = Point.Truncate(DisplayToMap(e.Location));
                if (Game.SelectedUnit != null)
                {
                    // Move
                    if (CurrentOrder == Order.Move)
                    {
                        if (Game.GetPossibleMoves(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int result = Game.SelectedUnit.Move(mapClickPosition);

                            if (result >= 0)
                            {
                                PlaySound("Data\\Sailing.wav");
                            }
                            if (result > 0)
                            {
                                PlaySound("Data\\TorpedoHit.wav");
                            }
                            selectionPossible = false;
                        }
                    }

                    // Light Artillery
                    else if (CurrentOrder == Order.LightArtillery)
                    {
                        if (Game.GetPossibleLightShots(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int hit = Game.LightArtillery(mapClickPosition, Game.SelectedUnit);
                            if (hit == 0)
                            {
                                PlaySound("Data\\LightArtilleryMiss.wav");
                                _ToolTip.Show("Miss", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);

                            }
                            else
                            {
                                PlaySound("Data\\LightArtilleryHit.wav");
                                _ToolTip.Show("Hit, damage " + Math.Min(hit, 100).ToString() + "%", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                            }
                            selectionPossible = false;
                            CurrentOrder = null;
                        }
                    }

                    // Heavy Artillery
                    else if (CurrentOrder == Order.HeavyArtillery)
                    {
                        if (Game.GetPossibleHeavyShots(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int hit = Game.HeavyArtillery(mapClickPosition, Game.SelectedUnit);
                            if (hit == 0)
                            {
                                PlaySound("Data\\HeavyArtilleryMiss.wav");
                                _ToolTip.Show("Miss", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);

                            }
                            else
                            {
                                PlaySound("Data\\HeavyArtilleryHit.wav");
                                _ToolTip.Show("Hit, damage " + Math.Min(hit, 100).ToString() + "%", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                            }
                            selectionPossible = false;
                            CurrentOrder = null;
                        }
                    }

                    // Repair
                    else if (CurrentOrder == Order.Repair)
                    {
                        if (Game.GetPossibleRepairs(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int repairs = Game.Repair(mapClickPosition, Game.SelectedUnit);
                            _ToolTip.Show("Repaired " + repairs.ToString() + "%", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Build
                    else if (CurrentOrder == Order.Build)
                    {
                        if (Game.GetPossibleBuilds(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            new BuildForm(Game, Game.SelectedUnit, mapClickPosition).ShowDialog();
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Load
                    else if (CurrentOrder == Order.Load)
                    {
                        if (Game.GetPossibleLoads(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int load = Game.Load(mapClickPosition, Game.SelectedUnit);
                            _ToolTip.Show("Loaded " + load.ToString() + " war materials", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                            PlaySound("Data\\Cargo.wav");
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Unload
                    else if (CurrentOrder == Order.Unload)
                    {
                        if (Game.GetPossibleUnloads(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int unload = Game.Unload(mapClickPosition, Game.SelectedUnit);
                            _ToolTip.Show("Unloaded " + unload.ToString() + " war materials", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                            PlaySound("Data\\Cargo.wav");
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Torpedo
                    else if (CurrentOrder == Order.Torpedo)
                    {
                        if (Game.GetPossibleTorpedoes(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int damage = Game.Torpedo(mapClickPosition, Game.SelectedUnit);
                            if (damage != 0)
                            {
                                _ToolTip.Show("Hit, damage " + Math.Min(damage, 100).ToString() + "%", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                                PlaySound("Data\\TorpedoLaunchHit.wav");
                            }
                            else
                            {
                                _ToolTip.Show("Miss", this, MapToDisplay(new PointF(mapClickPosition.X + 0.5f, mapClickPosition.Y + 0.5f)), 2000);
                                PlaySound("Data\\TorpedoLaunch.wav");
                            }
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Load Torpedoes
                    else if (CurrentOrder == Order.LoadTorpedoes)
                    {
                        if (Game.GetPossibleTorpedoLoads(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            if (MessageBox.Show("Topping up on torpedoes costs 1 war material.", "Load Torpedoes", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                            {
                                if (Game.LoadTorpedoes(mapClickPosition, Game.SelectedUnit) == 1)
                                {
                                    _ToolTip.Show("Torpedoes loaded.", this, e.Location, 2000);
                                    PlaySound("Data\\Cargo.wav");
                                }
                                else _ToolTip.Show("Loading failed.", this, e.Location, 2000);
                            }
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Depth Charge
                    else if (CurrentOrder == Order.DepthCharge)
                    {
                        if (Game.GetPossibleDepthCharges(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int hit = Game.DepthCharge(mapClickPosition, Game.SelectedUnit);

                            if (hit > 0)
                            {
                                _ToolTip.Show("Hit, damage " + Math.Min(hit, 100).ToString("0") + "%", this, e.Location, 2000);
                                PlaySound("Data\\DepthChargeHit.wav");
                            }

                            else
                            {
                                _ToolTip.Show("Miss", this, e.Location, 2000);
                                PlaySound("Data\\DepthChargeMiss.wav");
                            }
                        }
                    }

                    // Install Battery
                    else if (CurrentOrder == Order.InstallBattery)
                    {
                        if (Game.GetPossibleBatteryInstallations(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int installation = Game.InstallBattery(mapClickPosition, Game.SelectedUnit);

                            if (installation != 0) _ToolTip.Show("Succesfully installed battery.", this, e.Location, 2000);
                            else _ToolTip.Show("Failed to install battery.", this, e.Location, 2000);
                        }
                    }

                    // Capture
                    else if (CurrentOrder == Order.Capture)
                    {
                        if (Game.GetPossibleCaptures(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int capture = Game.Capture(mapClickPosition, Game.SelectedUnit);

                            if (capture != 0) _ToolTip.Show("Successfully captured the target.", this, e.Location, 2000);
                            else _ToolTip.Show("Failed to capture the target.", this, e.Location, 2000);
                        }
                    }

                    // Mine
                    else if (CurrentOrder == Order.Mine)
                    {
                        if (Game.GetPossibleMines(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            int mine = Game.Mine(mapClickPosition, Game.SelectedUnit);

                            if (mine != 0) _ToolTip.Show("Successfully layed minefield.", this, e.Location, 2000);
                            else _ToolTip.Show("Failed to lay minefield.", this, e.Location, 2000);
                        }
                    }

                    // Load Mines
                    else if (CurrentOrder == Order.LoadMines)
                    {
                        if (Game.GetPossibleMineLoads(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            if (MessageBox.Show("Topping up on mines costs 2 war materials.", "Load Mines", MessageBoxButtons.OKCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.OK)
                            {
                                if (Game.LoadMines(mapClickPosition, Game.SelectedUnit) == 1)
                                {
                                    _ToolTip.Show("Mines loaded.", this, e.Location, 2000);
                                    PlaySound("Data\\Cargo.wav");
                                }
                                else _ToolTip.Show("Loading failed.", this, e.Location, 2000);
                            }
                        }
                        selectionPossible = false;
                        CurrentOrder = null;
                    }

                    // Sweep
                    else if (CurrentOrder == Order.Sweep)
                    {
                        if (Game.GetPossibleSweeps(Game.SelectedUnit).Contains(mapClickPosition))
                        {
                            if (Game.Sweep(mapClickPosition, Game.SelectedUnit) == 1)
                            {
                                _ToolTip.Show("Minefield removed.", this, e.Location, 2000);
                            }
                            else
                            {
                                _ToolTip.Show("Failed to sweep.", this, e.Location, 2000);
                            }
                        }
                    }
                }

                // Selection
                if (selectionPossible)
                {
                    if (Game.CurrentPlayer != null && Game.CurrentPlayer.Faction != Faction.Neutral)
                    {
                        Game.SelectedUnit = null;
                        CurrentOrder = null;

                        Unit unit = Game.GetUnitAt(mapClickPosition);
                        if (unit != null && Game.IsUnitVisibleForPlayer(Game.CurrentPlayer, unit))
                        {
                            Game.SelectedUnit = unit;
                        }
                    }
                }
            }
            // Drag
            _DragFrom = null;
            Game.FireChangedEvent();
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
            CameraScale = (int)Math.Round(Math.Pow(1.3, e.Delta / 120) * CameraScale);
        }

        private void DrawMap(Graphics graphics)
        {

            HashSet<Point> range = new HashSet<Point>();

            if (Game.SelectedUnit != null && Game.SelectedUnit.Player == Game.CurrentPlayer)
            {
                switch (CurrentOrder)
                {
                    case Order.Move:
                        range = Game.GetMoveRange(Game.SelectedUnit);
                        break;
                    case Order.LightArtillery:
                        range = Game.GetLightArtilleryRange(Game.SelectedUnit);
                        break;
                    case Order.HeavyArtillery:
                        range = Game.GetHeavyArtilleryRange(Game.SelectedUnit);
                        break;
                    case Order.Repair:
                        range = Game.GetPossibleRepairs(Game.SelectedUnit);
                        break;
                    case Order.Build:
                        range = Game.GetPossibleBuilds(Game.SelectedUnit);
                        break;
                    case Order.Load:
                        range = Game.GetPossibleLoads(Game.SelectedUnit);
                        break;
                    case Order.Unload:
                        range = Game.GetPossibleUnloads(Game.SelectedUnit);
                        break;
                    case Order.Torpedo:
                        range = Game.GetTorpedoRange(Game.SelectedUnit);
                        break;
                    case Order.LoadTorpedoes:
                        range = Game.GetPossibleTorpedoLoads(Game.SelectedUnit);
                        break;
                    case Order.DepthCharge:
                        range = Game.GetDepthChargeRange(Game.SelectedUnit);
                        break;
                    case Order.InstallBattery:
                        range = Game.GetPossibleBatteryInstallations(Game.SelectedUnit);
                        break;
                    case Order.Capture:
                        range = Game.GetPossibleCaptures(Game.SelectedUnit);
                        break;
                    case Order.Mine:
                        range = Game.GetPossibleMines(Game.SelectedUnit);
                        break;
                    case Order.LoadMines:
                        range = Game.GetPossibleMineLoads(Game.SelectedUnit);
                        break;
                    case Order.Sweep:
                        range = Game.GetPossibleSweeps(Game.SelectedUnit);
                        break;
                }
            }

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

                if (range.Contains(p))
                {
                    result |= _RangesLayerId;
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

        public static Terrain StringToTerrain(int width, string plan)
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

