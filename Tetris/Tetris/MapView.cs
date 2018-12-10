using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Tetris
{
    public class MapView : SKCanvasView
    {
        public static readonly int ColCount = 12;
        public static readonly int RowCount = 35;
        private SKColor[,] _grid;
        private const int _blockSize = 40;

        public Shape Shape { get; private set; }
        private const int ScoreOfEachLevel = 100;
        public int Level { get; private set; } = 1;
        public int Scores { get; private set; } = 0;

        public MapView()
        {
            WidthRequest = _blockSize * ColCount;
            HeightRequest = _blockSize * RowCount;
            _grid = new SKColor[ColCount, RowCount];
            Shape = Shape.CreateShape(new SKPointI(new Random().Next(Map.ColCount - Shape.PointsCount), 0));
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {

            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint()
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill
            };

            //map
            for (int i = 0; i < Map.ColCount; i++)
            {
                for (int j = 0; j < Map.RowCount; j++)
                {

                    if (_grid[i, j] != SKColor.Empty)
                    {
                        paint.Color = _grid[i, j];
                        canvas.DrawRect(i * _blockSize - 1, j * _blockSize - 1, _blockSize - 2, _blockSize - 2, paint);
                    }

                }
            }

            //shape
            if (Shape != null)
            {
                foreach (var p in Shape.Points)
                {
                    paint.Color = Shape.Color;
                    canvas.DrawRect(p.X * _blockSize - 1, p.Y * _blockSize - 1, _blockSize - 2, _blockSize - 2, paint);
                }
            }
        }

        public void MoveDown()
        {
            if (Shape == null) return;

            var newPoints = (from p in Shape.Points select new SKPointI(p.X, p.Y + 1)).ToArray();
            if (CheckBoundary(newPoints))
            {
                Shape.Points = newPoints;
            }
        }

        public void MoveLeft()
        {
            if (Shape == null) return;

            var newPoints = (from p in Shape.Points select new SKPointI(p.X - 1, p.Y)).ToArray();
            if (CheckBoundary(newPoints))
            {
                Shape.Points = newPoints;
            }
        }

        public void MoveRight()
        {
            if (Shape == null) return;

            var newPoints = (from p in Shape.Points select new SKPointI(p.X + 1, p.Y)).ToArray();
            if (CheckBoundary(newPoints))
            {
                Shape.Points = newPoints;
            }

        }

        public void Rotate()
        {
            if (Shape == null) return;

            var newPoints = (from p in Shape.Points select new SKPointI(p.X, p.Y)).ToArray();
            //x' = x•cos(α) – y•sin(α)
            //y' = x•sin(α) + y•cos(α)
            if (Shape.CenterIndex >= 0)
            {
                for (int i = 0; i < newPoints.Length; i++)
                {
                    if (i != Shape.CenterIndex)
                    {
                        int x = newPoints[i].X - Shape.Points[Shape.CenterIndex].X;
                        int y = newPoints[i].Y - Shape.Points[Shape.CenterIndex].Y;

                        newPoints[i].X = -y + Shape.Points[Shape.CenterIndex].X;
                        newPoints[i].Y = x + Shape.Points[Shape.CenterIndex].Y;
                    }
                }
            }
            if (CheckBoundary(newPoints))
            {
                Shape.Points = newPoints;
            }

        }

        private bool CheckBoundary(SKPointI[] points)
        {
            // left bound
            if ((from p in points select p.X).Min() < 0)
            {
                return false;
            }

            // right bound
            if ((from p in points select p.X).Max() > Map.ColCount - 1)
            {
                return false;
            }

            // bottom and overlayed
            foreach (var p in points)
            {
                if (p.Y > Map.RowCount - 1 || _grid[p.X, p.Y] != SKColor.Empty)
                {
                    UpdateGrid(Shape);
                    Shape = Shape.CreateShape(new SKPointI(new Random().Next(Map.ColCount - Shape.PointsCount), 0));
                    return false;
                }
            }

            return true;
        }

        private void UpdateGrid(Shape shape)
        {
            foreach (var p in shape.Points)
            {
                _grid[p.X, p.Y] = shape.Color;
            }


            var newGrid = new SKColor[ColCount, RowCount];
            for (int i = RowCount - 1; i >= 0; i--)
            {
                int j = 0;
                for (j = 0; j < ColCount; j++)
                {
                    if (_grid[i, j] == SKColors.Empty) break;
                }

                if (j == ColCount)
                {
                    Scores += 10;
                    // level up
                    if (Scores % ScoreOfEachLevel == 0)
                    {
                        Level += 1;
                    }
                }
                else
                {
                    for (j = 0; j < ColCount; j++)
                    {
                        newGrid[i, j] = _grid[i, j];
                    }
                }
            }

        }
    }
}
