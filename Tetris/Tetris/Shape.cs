using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

// https://en.wikipedia.org/wiki/Tetris
namespace Tetris
{
    public class Shape
    {
        public static readonly int PointsCount = 4;
        public SKPointI[] Points { get; set; }
        public int CenterIndex { get; protected set; } = -1;
        public SKColor Color { get; protected set; }

        public static Shape CreateShape(SKPointI point)
        {
            Shape shape;
            int i = new Random().Next(7);
            if (i == 0)
            {
                shape = new ShapeI(point);
            }
            else if (i == 1)
            {
                shape = new ShapeJ(point);
            }
            else if (i == 2)
            {
                shape = new ShapeL(point);
            }
            else if (i == 3)
            {
                shape = new ShapeO(point);
            }
            else if (i == 4)
            {
                shape = new ShapeS(point);
            }
            else if (i == 5)
            {
                shape = new ShapeT(point);
            }
            else if (i == 6)
            {
                shape = new ShapeZ(point);
            }
            else
            {
                shape = new ShapeJ(point);
            }
            return shape;
        }
    }

    //□■□□
    public class ShapeI : Shape
    {
        public ShapeI(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X+2,point.Y),
                new SKPointI(point.X+3,point.Y)
            };
            CenterIndex = 1;
            Color = SKColors.Red;
        }

    }

    //□□■
    //  □
    public class ShapeJ : Shape
    {
        public ShapeJ(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X+2,point.Y),
                new SKPointI(point.X+2,point.Y+1)
            };
            CenterIndex = 2;
            Color = SKColors.Magenta;
        }
    }

    //■□□
    //□
    public class ShapeL : Shape
    {
        public ShapeL(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X+2,point.Y),
                new SKPointI(point.X,point.Y+1)
            };
            CenterIndex = 0;
            Color = SKColors.Brown;
        }
    }

    //□□
    //□□
    public class ShapeO : Shape
    {
        public ShapeO(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X,point.Y+1),
                new SKPointI(point.X+1,point.Y+1)
            };
            Color = SKColors.Cyan;
        }
    }

    // □□
    //□■
    public class ShapeS : Shape
    {
        public ShapeS(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X-1,point.Y+1),
                new SKPointI(point.X,point.Y+1)
            };
            CenterIndex = 3;
            Color = SKColors.Green;
        }
    }

    //□■□
    // □
    public class ShapeT : Shape
    {
        public ShapeT(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X+2,point.Y+1),
                new SKPointI(point.X+1,point.Y+1)
            };
            CenterIndex = 1;
            Color = SKColors.Blue;
        }
    }

    //□□
    // ■□
    public class ShapeZ : Shape
    {
        public ShapeZ(SKPointI point)
        {
            Points = new SKPointI[]
            {
                new SKPointI(point.X,point.Y),
                new SKPointI(point.X+1,point.Y),
                new SKPointI(point.X+1,point.Y+1),
                new SKPointI(point.X+2,point.Y+1)
            };
            CenterIndex = 2;
            Color = SKColors.Teal;
        }
    }
}
