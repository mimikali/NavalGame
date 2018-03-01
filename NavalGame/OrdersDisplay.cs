using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NavalGame
{
    public class OrdersDisplay : PictureBox
    {
        MapDisplay _MapDisplay;
        public int InitialWidth;
        public int InitialHeight;
        int ButtonPressedIndex;
        List<Rectangle> ButtonLocations = new List<Rectangle>();

        public MapDisplay MapDisplay
        {
            get
            {
                return _MapDisplay;
            }

            set
            {
                _MapDisplay = value;
            }
        }

        public OrdersDisplay(MapDisplay mapDisplay)
        {
            _MapDisplay = mapDisplay;
            InitialWidth = Size.Width;
            InitialHeight = Size.Height;
            ButtonPressedIndex = -1;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            pe.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, 0, Width, Height));
            if (MapDisplay.Game == null) return;
            if (MapDisplay.Game.SelectedUnit != null)
            {
                ButtonLocations.Clear();
                for (int i = 0; i < MapDisplay.Game.SelectedUnit.Abilities.Count; i++)
                {
                    ButtonLocations.Add(new Rectangle((int)(Width * 0.1), (int)(Width * 0.2 + InitialHeight * 0.3 + i * Height * 0.1), (int)(Width * 0.8), (int)(InitialHeight * 0.1)));
                }
                pe.Graphics.DrawImage(Bitmaps.Get(MapDisplay.Game.SelectedUnit.Bitmap), new Rectangle((int)(Width * 0.1), (int)(Width * 0.1), (int)(Width * 0.8), (int)(InitialHeight * 0.3)));
                for (int i = 0; i < ButtonLocations.Count; i++)
                {
                    if (i == ButtonPressedIndex) pe.Graphics.DrawImage(Bitmaps.Get("Data\\ButtonPressed.png"), ButtonLocations[i]);
                    else pe.Graphics.DrawImage(Bitmaps.Get("Data\\Button.png"), ButtonLocations[i]);

                    pe.Graphics.DrawImage(Bitmaps.Get(MapDisplay.Game.GetOrderIconPath(MapDisplay.Game.SelectedUnit.Abilities[i])), ButtonLocations[i]);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            for (int i = 0; i < ButtonLocations.Count; i++)
            {
                if (e.X > ButtonLocations[i].X && e.X < ButtonLocations[i].X + ButtonLocations[i].Width)
                {
                    if (e.Y > ButtonLocations[i].Y && e.Y < ButtonLocations[i].Y + ButtonLocations[i].Height)
                    {
                        ButtonPressedIndex = i;
                        Invalidate();
                    }
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (ButtonPressedIndex != -1)
            {
                if (MapDisplay.Game.SelectedUnit.Abilities[ButtonPressedIndex] == Order.Move)
                {
                    MapDisplay.CurrentMove = NavalGame.Move.Wait;
                }
                ButtonPressedIndex = -1;
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            for (int i = 0; i < ButtonLocations.Count; i++)
            {
                if (e.X > ButtonLocations[i].X && e.X < ButtonLocations[i].X + ButtonLocations[i].Width)
                {
                    if (e.Y > ButtonLocations[i].Y && e.Y < ButtonLocations[i].Y + ButtonLocations[i].Height)
                    {
                        
                    }
                }
            }
        }
    }
}