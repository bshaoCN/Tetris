using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Tetris
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                mapView.MoveDown();
                mapView.InvalidateSurface();
                return true;
            });

        }

        //private void mainCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        //{

        //    SKImageInfo info = e.Info;
        //    SKSurface surface = e.Surface;
        //    SKCanvas canvas = surface.Canvas;

        //    canvas.Clear();

        //    SKPaint paint = new SKPaint()
        //    {
        //        IsAntialias = true,
        //        Style = SKPaintStyle.Fill
        //    };

        //    //map
        //    for (int i = 0; i < Map.ColCount; i++)
        //    {
        //        for (int j = 0; j < Map.RowCount; j++)
        //        {

        //            if (_map[i, j] != SKColor.Empty)
        //            {
        //                paint.Color = _map[i, j];
        //                canvas.DrawRect(i * BlockSize - 1, j * BlockSize - 1, BlockSize - 2, BlockSize - 2, paint);
        //            }

        //        }
        //    }

        //    //shape
        //    foreach (var p in _shape.Points)
        //    {
        //        paint.Color = _shape.Color;
        //        canvas.DrawRect(p.X * BlockSize - 1, p.Y * BlockSize - 1, BlockSize - 2, BlockSize - 2, paint);
        //    }
        //}

        //private void MoveDown()
        //{
        //    var newPoints = from p in _shape.Points select new SKPointI(p.X, p.Y + 1);

        //    foreach (var p in newPoints)
        //    {
        //        if (p.Y > Map.RowCount - 1 || _map[p.X, p.Y] != SKColor.Empty)
        //        {
        //            _map.Update(_shape);
        //            _shape = Shape.CreateShape(new SKPointI(new Random().Next(Map.ColCount - Shape.PointsCount), 0));
        //            return;
        //        }
        //    }

        //    _shape.Points = newPoints.ToArray();
        //}

        //private void MoveLeft()
        //{
        //    var newPoints = from p in _shape.Points select new SKPointI(p.X - 1, p.Y);

        //    // left bound
        //    if ((from p in newPoints select p.X).Min() < 0)
        //    {
        //        return;
        //    }

        //    // overlayed
        //    foreach (var p in newPoints)
        //    {
        //        if (_map[p.X, p.Y] != SKColor.Empty)
        //        {
        //            _map.Update(_shape);
        //            _shape = Shape.CreateShape(new SKPointI(new Random().Next(Map.ColCount - Shape.PointsCount), 0));
        //            return;
        //        }
        //    }

        //    _shape.Points = newPoints.ToArray();
        //}

        //private void MoveRight()
        //{
        //    var newPoints = from p in _shape.Points select new SKPointI(p.X + 1, p.Y);

        //    // right bound
        //    if ((from p in newPoints select p.X).Max() > Map.ColCount - 1)
        //    {
        //        return;
        //    }

        //    // overlayed
        //    foreach (var p in newPoints)
        //    {
        //        if (_map[p.X, p.Y] != SKColor.Empty)
        //        {
        //            _map.Update(_shape);
        //            _shape = Shape.CreateShape(new SKPointI(new Random().Next(Map.ColCount - Shape.PointsCount), 0));
        //            return;
        //        }
        //    }

        //    _shape.Points = newPoints.ToArray();
        //}

        //private void Rotate()
        //{
        //    //x' = x•cos(α) – y•sin(α)
        //    //y' = x•sin(α) + y•cos(α)
        //    if (_shape.CenterIndex >= 0)
        //    {
        //        for (int i = 0; i < _shape.Points.Length; i++)
        //        {
        //            if (i != _shape.CenterIndex)
        //            {
        //                int x = _shape.Points[i].X - _shape.Points[_shape.CenterIndex].X;
        //                int y = _shape.Points[i].Y - _shape.Points[_shape.CenterIndex].Y;

        //                _shape.Points[i].X = -y + _shape.Points[_shape.CenterIndex].X;
        //                _shape.Points[i].Y = x + _shape.Points[_shape.CenterIndex].Y;
        //            }
        //        }
        //    }
        //}

        //private void show(Shape shape)
        //{
        //    string s = string.Empty;
        //    foreach (var p in shape.Points)
        //    {
        //        s += p.ToString();
        //    }
        //    this.DisplayAlert("111", s, "111");
        //}

        //private void mainCanvasView_Tapped(object sender, EventArgs e)
        //{

        //    _shape = Shape.CreateShape(new SKPointI(7, 5));
        //}

        private void SwipeGestureRecognizer_LeftSwiped(object sender, SwipedEventArgs e)
        {
            mapView.MoveLeft();
        }

        private void SwipeGestureRecognizer_RightSwiped(object sender, SwipedEventArgs e)
        {
            mapView.MoveRight();
        }

        private void SwipeGestureRecognizer_UpSwiped(object sender, SwipedEventArgs e)
        {
            mapView.Rotate();
        }

        private void nextShape_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SKPaint paint = new SKPaint()
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = mapView.Shape.Color
            };

            //shape
            int size = 5;
            if (mapView.Shape != null)
            {
                foreach (var p in mapView.Shape.Points)
                {
                    canvas.DrawRect(p.X * size - 1, p.Y * size - 1, size - 2, size - 2, paint);
                }
            }
        }
    }
}
