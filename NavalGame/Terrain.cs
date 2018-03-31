using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace NavalGame
{
    public class Terrain
    {
        int _width;
        int _height;
        TerrainType[] _cells;

        public int Width
        {
            get
            {
                return _width;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
        }

        public Terrain(int width, int height)
        {
            _cells = new TerrainType[width * height];
            _width = width;
            _height = height;
        }

        public Terrain(Bitmap map)
        {
            _width = map.Width;
            _height = map.Height;
            _cells = new TerrainType[_width * _height];

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    Color pixel = map.GetPixel(x, y);

                    if (pixel == Color.FromArgb(0, 0, 255))
                    {
                        Set(x, y, TerrainType.Sea);
                    }
                    else
                    {
                        Set(x, y, TerrainType.Land);
                    }

                }
            }
        }

        public TerrainType Get(int x, int y, TerrainType defaultTerrain = TerrainType.Sea)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return defaultTerrain;
            return _cells[x + y * Width];
        }

        public void Set(int x, int y, TerrainType terrainType)
        {
            _cells[x + y * Width] = terrainType;
        }

        public static XElement Save(Terrain terrain)
        {
            XElement terrainNode = new XElement("Terrain");

            terrainNode.SetAttributeValue("Width", terrain.Width);
            terrainNode.SetAttributeValue("Height", terrain.Height);

            terrainNode.Value = "";
            foreach (TerrainType cell in terrain._cells)
            {
                if (cell == TerrainType.Land) terrainNode.Value += "1";
                else terrainNode.Value += "0";
            }

            return terrainNode;
        }

        public static Terrain Load(XElement terrainNode)
        {
            int width = XmlUtils.GetAttributeValue<int>(terrainNode, "Width");
            int height = XmlUtils.GetAttributeValue<int>(terrainNode, "Height");

            Terrain terrain = new Terrain(width, height);

            for(int i = 0; i < terrain._cells.Length; i++)
            {
                terrain._cells[i] = terrainNode.Value[i] == '1' ? TerrainType.Land : TerrainType.Sea;
            }

            return terrain;
        }
    }
}

