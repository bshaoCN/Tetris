using System;
using SkiaSharp;

namespace Tetris
{
    public class Map
    {
        public static readonly int ColCount = 12;
        public static readonly int RowCount = 30;
        private SKColor[,] _grid;

        public Map()
        {
            _grid = new SKColor[ColCount, RowCount];
        }

        public SKColor this[int i, int j]
        {
            get
            {
                if (i < 0 || i > ColCount - 1 || j < 0 || j > RowCount - 1)
                {
                    throw new IndexOutOfRangeException();
                }
                return _grid[i, j];
            }
            set
            {
                if (i < 0 || i > ColCount - 1 || j < 0 || j > RowCount - 1)
                {
                    throw new IndexOutOfRangeException();
                }
                _grid[i, j] = value;
            }
        }

        public void Update(Shape shape)
        {
            foreach (var p in shape.Points)
            {
                _grid[p.X, p.Y] = shape.Color;
            }
        }
    }
}
