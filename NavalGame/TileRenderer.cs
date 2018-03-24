using System;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading.Tasks;

namespace NavalGame
{
    public class TileRenderer
    {
        public enum LayerLayout
        {
            Corners,
            Sides
        }

        class Pixmap
        {
            [StructLayout(LayoutKind.Explicit)]
            public struct Pixel
            {
                [FieldOffset(0)]
                public byte R;
                [FieldOffset(1)]
                public byte G;
                [FieldOffset(2)]
                public byte B;
                [FieldOffset(3)]
                public byte A;
                [FieldOffset(0)]
                public int Value;
            }

            public GCHandle Handle;
            public Bitmap Bitmap;
            public Pixel[] Data;
            public int Width, Height;

            public Pixmap(int width, int height)
            {
                Width = width;
                Height = height;
                Data = new Pixel[width * height];
                Handle = GCHandle.Alloc(Data, GCHandleType.Pinned);
                Bitmap = new Bitmap(Width, Height, 4 * Width, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Handle.AddrOfPinnedObject());
            }

            ~Pixmap()
            {
                Bitmap.Dispose();
                Handle.Free();
            }

            public Pixmap(Bitmap bitmap) : this(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height))
            {
            }

            public Pixmap(Bitmap bitmap, Rectangle rectangle) : this(rectangle.Width, rectangle.Height)
            {
                var bd = bitmap.LockBits(rectangle, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                unsafe
                {
                    byte* ptr = (byte*)bd.Scan0;
                    for (int y = rectangle.Top; y < rectangle.Bottom; ++y)
                    {
                        for (int x = rectangle.Left; x < rectangle.Right; ++x)
                        {
                            int v = *(int*)(ptr + 4 * x + y * bd.Stride);
                            Data[x + y * Width] = new Pixel { Value = v };
                        }
                    }
                }
                bitmap.UnlockBits(bd);
            }

            public void Clear(Color color)
            {
                Pixel p = new Pixel { A = color.A, R = color.R, G = color.G, B = color.B };
                for (int i = 0; i < Data.Length; ++i)
                    Data[i] = p;
            }

            public static void Draw(Pixmap source, Pixmap target, Rectangle sourceRectangle, int x, int y)
            {
                int bx = Math.Max(-x, 0);
                int by = Math.Max(-y, 0);
                int w = Math.Min(sourceRectangle.Width, target.Width - x);
                int h = Math.Min(sourceRectangle.Height, target.Height - y);
                for (int ly = by; ly < h; ++ly)
                {
                    int sy = ly + sourceRectangle.Y;
                    int ty = ly + y;
                    int srow = sy * source.Width;
                    int trow = ty * target.Width;
                    for (int lx = bx; lx < w; ++lx)
                    {
                        int sx = lx + sourceRectangle.X;
                        int tx = lx + x;
                        int si = sx + srow;
                        int ti = tx + trow;
                        Pixel sp = source.Data[si];
                        if (sp.A == 255)
                        {
                            target.Data[ti] = sp;
                        }
                        else if (sp.A > 0)
                        {
                            Pixel tp = target.Data[ti];
                            byte tr = (byte)((sp.R * sp.A + tp.R * (255 - sp.A)) / 255);
                            byte tg = (byte)((sp.G * sp.A + tp.G * (255 - sp.A)) / 255);
                            byte tb = (byte)((sp.B * sp.A + tp.B * (255 - sp.A)) / 255);
                            target.Data[ti] = new Pixel { R = tr, G = tg, B = tb, A = 255 };
                        }
                    }
                }
            }

            public static void Draw2(Pixmap source, Pixmap wrapSource, Pixmap target, Rectangle sourceRectangle, Point wrapOrigin, Point targetOrigin)
            {
                int bx = Math.Max(-targetOrigin.X, 0);
                int by = Math.Max(-targetOrigin.Y, 0);
                int w = Math.Min(sourceRectangle.Width, target.Width - targetOrigin.X);
                int h = Math.Min(sourceRectangle.Height, target.Height - targetOrigin.Y);
                for (int ly = by; ly < h; ++ly)
                {
                    int sy = ly + sourceRectangle.Y;
                    int ty = ly + targetOrigin.Y;
                    int wy = (ly + wrapOrigin.Y) % wrapSource.Height;
                    int srow = sy * source.Width;
                    int trow = ty * target.Width;
                    int wrow = wy * wrapSource.Width;
                    for (int lx = bx; lx < w; ++lx)
                    {
                        int sx = lx + sourceRectangle.X;
                        int tx = lx + targetOrigin.X;
                        int wx = (lx + wrapOrigin.X) % wrapSource.Width;
                        int si = sx + srow;
                        int ti = tx + trow;
                        int wi = wx + wrow;
                        Pixel sp = source.Data[si];
                        Pixel wp = wrapSource.Data[wi];
                        Pixel p = new Pixel
                        {
                            A = (byte)Math.Min(sp.A * wp.A / 192, 255),
                            R = (byte)Math.Min(sp.R * wp.R / 192, 255),
                            G = (byte)Math.Min(sp.G * wp.G / 192, 255),
                            B = (byte)Math.Min(sp.B * wp.B / 192, 255),
                        };
                        if (p.A == 255)
                        {
                            target.Data[ti] = p;
                        }
                        else if (p.A > 0)
                        {
                            Pixel tp = target.Data[ti];
                            target.Data[ti] = new Pixel
                            {
                                R = (byte)((p.R * p.A + tp.R * (255 - p.A)) / 255),
                                G = (byte)((p.G * p.A + tp.G * (255 - p.A)) / 255),
                                B = (byte)((p.B * p.A + tp.B * (255 - p.A)) / 255),
                                A = 255
                            };
                        }
                    }
                }
            }
        }

        class Layer
        {
            const int _SidesLayoutWidth = 4;
            const int _SidesLayoutHeight = 4;
            static int[] _SidesLayout = new int[_SidesLayoutWidth * _SidesLayoutHeight]
            {
                8, 12, 13, 9,
                10, 14, 15, 11,
                2, 6, 7, 3,
                0, 4, 5, 1
            };

            const int _CornersLayoutWidth = 6;
            const int _CornersLayoutHeight = 8;
            static int[] _CornersLayout = new int[_CornersLayoutWidth * _CornersLayoutHeight]
            {
                8, 12, 12, 12, 12, 4,
                10, 7, 11, 7, 11, 5,
                2, 9, 14, 13, 6, 1,
                0, 10, 15, 15, 5, 0,
                0, 10, 15, 15, 5, 0,
                8, 6, 11, 7, 9, 4,
                10, 13, 14, 13, 14, 5,
                2, 3, 3, 3, 3, 1
            };

            public readonly LayerLayout Layout;
            public readonly int BitmapTileSize;
            public readonly Bitmap TilesBitmap;
            public readonly Bitmap WrapBitmap;
            public readonly int Mask;
            public int ConnectionsMask;
            public Pixmap TilesPixmap;
            public Pixmap WrapPixmap;
            List<Point>[] _CodeToTileCoords = new List<Point>[16];

            public Layer(int mask, LayerLayout layout, Bitmap tilesBitmap, Bitmap wrapBitmap)
            {
                Mask = mask;
                ConnectionsMask = mask;
                Layout = layout;
                TilesBitmap = tilesBitmap;
                WrapBitmap = wrapBitmap;

                for (int i = 0; i < _CodeToTileCoords.Length; ++i)
                    _CodeToTileCoords[i] = new List<Point>();
                if (Layout == LayerLayout.Sides)
                {
                    BitmapTileSize = TilesBitmap.Height / _SidesLayoutHeight;
                    if (TilesBitmap.Height != _SidesLayoutHeight * BitmapTileSize || TilesBitmap.Width % (_SidesLayoutWidth * BitmapTileSize) != 0)
                        throw new InvalidOperationException("Invalid size of tiles bitmap.");
                    for (int y = 0; y < _SidesLayoutHeight; ++y)
                    {
                        for (int x = 0; x < TilesBitmap.Width / BitmapTileSize; ++x)
                        {
                            int code = _SidesLayout[(x % _SidesLayoutWidth) + y * _SidesLayoutWidth];
                            _CodeToTileCoords[code].Add(new Point(x, y));
                        }
                    }
                }
                else
                {
                    BitmapTileSize = TilesBitmap.Height / _CornersLayoutHeight;
                    if (TilesBitmap.Height != _CornersLayoutHeight * BitmapTileSize || TilesBitmap.Width % (_CornersLayoutWidth * BitmapTileSize) != 0)
                        throw new InvalidOperationException("Invalid size of tiles bitmap.");
                    for (int y = 0; y < _CornersLayoutHeight; ++y)
                    {
                        for (int x = 0; x < TilesBitmap.Width / BitmapTileSize; ++x)
                        {
                            int code = _CornersLayout[(x % _CornersLayoutWidth) + y * _CornersLayoutWidth];
                            _CodeToTileCoords[code].Add(new Point(x, y));
                        }
                    }
                }
            }

            public Point GetTileCoords(int code, int seed)
            {
                var list = _CodeToTileCoords[code];
                int i = (ushort)Hash((uint)seed) % list.Count;
                return list[i];
            }

            public void CreatePixmaps(int tileSize)
            {
                if (Layout == LayerLayout.Sides)
                {
                    using (var resizedTilesBitmap = ResizeBitmap(TilesBitmap, tileSize * TilesBitmap.Width / BitmapTileSize, tileSize * _SidesLayoutHeight))
                    using (var resizedWrapBitmap = WrapBitmap != null ? ResizeBitmap(WrapBitmap, tileSize * WrapBitmap.Width / BitmapTileSize, tileSize * WrapBitmap.Height / BitmapTileSize) : null)
                    {
                        TilesPixmap = new Pixmap(resizedTilesBitmap);
                        WrapPixmap = WrapBitmap != null ? new Pixmap(resizedWrapBitmap) : null;
                    }
                }
                else
                {
                    using (var resizedTilesBitmap = ResizeBitmap(TilesBitmap, tileSize * TilesBitmap.Width / BitmapTileSize, tileSize * _CornersLayoutHeight))
                    using (var resizedWrapBitmap = WrapBitmap != null ? ResizeBitmap(WrapBitmap, tileSize * WrapBitmap.Width / BitmapTileSize, tileSize * WrapBitmap.Height / BitmapTileSize) : null)
                    {
                        TilesPixmap = new Pixmap(resizedTilesBitmap);
                        WrapPixmap = WrapBitmap != null ? new Pixmap(resizedWrapBitmap) : null;
                    }
                }
            }

            private static uint Hash(uint key)
            {
                key = ~key + (key << 15);
                key = key ^ (key >> 12);
                key = key + (key << 2);
                key = key ^ (key >> 4);
                key = key * 2057;
                key = key ^ (key >> 16);
                return key;
            }
        }

        int _TileSize;
        int _PixmapsTileSize;
        List<Layer> _Layers = new List<Layer>();
        Pixmap _Pixmap;
        int[] _Terrain;

        public int TileSize
        {
            get
            {
                return _TileSize;
            }

            set
            {
                _TileSize = value;
            }
        }
        public int LayersCount => _Layers.Count;

        public TileRenderer(int tileSize)
        {
            if (tileSize <= 0)
                throw new InvalidOperationException("Invalid tilesize.");
            _TileSize = tileSize;
        }

        public int AddLayer(LayerLayout layout, Bitmap tilesBitmap, Bitmap wrapBitmap)
        {
            int index = _Layers.Count;
            if (index >= 32)
                throw new InvalidOperationException("Too many layers.");
            int mask = 1 << index;
            _Layers.Add(new Layer(mask, layout, tilesBitmap, wrapBitmap));
            return mask;
        }

        public void AddLayerConnection(int layerId1, int layerId2)
        {
            var layer1 = _Layers.FirstOrDefault(l => l.Mask == layerId1);
            var layer2 = _Layers.FirstOrDefault(l => l.Mask == layerId2);
            if (layer1 == null || layer2 == null)
                throw new InvalidOperationException("Invalid layer id.");
            layer1.ConnectionsMask |= layerId2;
            layer2.ConnectionsMask |= layerId1;
        }

        public void DrawTiles(Graphics g, Point p, Rectangle terrainRectangle, Func<Point, int> terrain)
        {
            // Calculate and clip screen coordinates of terrain
            var sx1 = Math.Max(p.X + terrainRectangle.Left * TileSize, (int)g.ClipBounds.Left);
            var sy1 = Math.Max(p.Y + terrainRectangle.Top * TileSize, (int)g.ClipBounds.Top);
            var sx2 = Math.Min(p.X + terrainRectangle.Right * TileSize, (int)g.ClipBounds.Right);
            var sy2 = Math.Min(p.Y + terrainRectangle.Bottom * TileSize, (int)g.ClipBounds.Bottom);
            if (sx1 >= sx2 || sy1 >= sy2) return;

            // Calculate clipped terrain coordinates
            var tx1 = (sx1 - p.X) / TileSize;
            var ty1 = (sy1 - p.Y) / TileSize;
            var tx2 = (sx2 - p.X) / TileSize;
            var ty2 = (sy2 - p.Y) / TileSize;

            // Prepare pixmap; pixmap covers screen coordinates (sx1,sy1) to (sx2,sy2)
            if (_Pixmap == null || _Pixmap.Width != sx2 - sx1 || _Pixmap.Height != sy2 - sy1)
                _Pixmap = new Pixmap(sx2 - sx1, sy2 - sy1);

            // Prepare layers pixmaps
            if (_PixmapsTileSize != TileSize)
            {
                foreach (var layer in _Layers)
                    layer.CreatePixmaps(TileSize);
                _PixmapsTileSize = TileSize;
            }

            // Prepare terrain cache
            int originX = tx1 - 1;
            int originY = ty1 - 1;
            int sizeX = tx2 - tx1 + 2 + 2;
            int sizeY = ty2 - ty1 + 2 + 2;
            if (_Terrain == null || _Terrain.Length != sizeX * sizeY)
            {
                _Terrain = new int[sizeX * sizeY];
            }
            for (int y = 0; y < sizeY; ++y)
            {
                for (int x = 0; x < sizeX; ++x)
                {
                    _Terrain[x + y * sizeX] = terrain(new Point(x + originX, y + originY));
                }
            }

            // Draw all layers
            foreach (var layer in _Layers)
            {
                if (layer.Layout == LayerLayout.Corners)
                {
                    // For all terrain locations to draw
                    Parallel.For(ty1, ty2 + 2, ty =>
                    // for (int ty = ty1; ty < ty2 + 2; ++ty)
                    {
                        for (int tx = tx1; tx < tx2 + 2; ++tx)
                        {
                            // Get pixmap position of terrain location
                            int px = tx * TileSize + p.X - sx1 - TileSize / 2;
                            int py = ty * TileSize + p.Y - sy1 - TileSize / 2;

                            // Get layers masks of surrounding terrain locations (corners)
                            int cmask1 = _Terrain[(tx - originX - 1) + (ty - originY - 1) * sizeX];
                            int cmask2 = _Terrain[(tx - originX) + (ty - originY - 1) * sizeX];
                            int cmask3 = _Terrain[(tx - originX - 1) + (ty - originY) * sizeX];
                            int cmask4 = _Terrain[(tx - originX) + (ty - originY) * sizeX];

                            // Prepare seed for selecting random variations of tiles
                            int seed = tx | (ty << 16);

                            // Draw tile
                            int code = 0;
                            if ((cmask1 & layer.ConnectionsMask) != 0) code += 1;
                            if ((cmask2 & layer.ConnectionsMask) != 0) code += 2;
                            if ((cmask3 & layer.ConnectionsMask) != 0) code += 4;
                            if ((cmask4 & layer.ConnectionsMask) != 0) code += 8;
                            if (code != 0)
                            {
                                Point coords = layer.GetTileCoords(code, seed);
                                var srcRect = new Rectangle(TileSize * coords.X, TileSize * coords.Y, TileSize, TileSize);
                                if (layer.WrapPixmap != null)
                                    Pixmap.Draw2(layer.TilesPixmap, layer.WrapPixmap, _Pixmap, srcRect, new Point(tx * TileSize, ty * TileSize), new Point(px, py));
                                else
                                    Pixmap.Draw(layer.TilesPixmap, _Pixmap, srcRect, px, py);
                            }
                        }
                    });
                }
                else
                {
                    // For all terrain locations to draw
                    Parallel.For(ty1, ty2 + 1, ty =>
                    //for (int ty = ty1; ty < ty2 + 1; ++ty)
                    {
                        for (int tx = tx1; tx < tx2 + 1; ++tx)
                        {
                            // If location contains this layer
                            int mask = terrain(new Point(tx, ty));
                            if ((mask & layer.Mask) != 0)
                            {
                                // Get pixmap position of terrain location
                                int px = tx * TileSize + p.X - sx1;
                                int py = ty * TileSize + p.Y - sy1;

                                // Get layers masks of surrounding terrain locations (sides)
                                //int lmask = terrain(new Point(tx - 1, ty));
                                //int umask = terrain(new Point(tx, ty - 1));
                                //int rmask = terrain(new Point(tx + 1, ty));
                                //int dmask = terrain(new Point(tx, ty + 1));
                                int lmask = _Terrain[(tx - originX - 1) + (ty - originY) * sizeX];
                                int umask = _Terrain[(tx - originX) + (ty - originY - 1) * sizeX];
                                int rmask = _Terrain[(tx - originX + 1) + (ty - originY) * sizeX];
                                int dmask = _Terrain[(tx - originX) + (ty - originY + 1) * sizeX];

                                // Prepare seed for selecting random variations of tiles
                                int seed = tx | (ty << 16);

                                // Draw tile
                                int code = 0;
                                if ((lmask & layer.ConnectionsMask) != 0) code += 1;
                                if ((umask & layer.ConnectionsMask) != 0) code += 2;
                                if ((rmask & layer.ConnectionsMask) != 0) code += 4;
                                if ((dmask & layer.ConnectionsMask) != 0) code += 8;
                                Point coords = layer.GetTileCoords(code, seed);
                                var srcRect = new Rectangle(TileSize * coords.X, TileSize * coords.Y, TileSize, TileSize);
                                if (layer.WrapPixmap != null)
                                    Pixmap.Draw2(layer.TilesPixmap, layer.WrapPixmap, _Pixmap, srcRect, new Point(tx * TileSize, ty * TileSize), new Point(px, py));
                                else
                                    Pixmap.Draw(layer.TilesPixmap, _Pixmap, srcRect, px, py);
                            }
                        }
                    });
                }
            }

            // Copy pixmap to graphics
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            g.DrawImageUnscaled(_Pixmap.Bitmap, new Point(sx1, sy1));
        }

        private static Bitmap ResizeBitmap(Bitmap source, int width, int height)
        {
            Bitmap target = new Bitmap(width, height);
            using (var g = Graphics.FromImage(target))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                g.DrawImage(source, new Rectangle(0, 0, width, height));
            }
            return target;
        }
    }
}
