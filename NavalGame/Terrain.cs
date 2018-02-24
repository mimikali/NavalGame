using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public TerrainType Get(int x, int y, TerrainType defaultTerrain = TerrainType.Sea)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height) return defaultTerrain;
            return _cells[x + y * Width];
        }

        public void Set(int x, int y, TerrainType terrainType)
        {
            _cells[x + y * Width] = terrainType;
        }
    }
}

