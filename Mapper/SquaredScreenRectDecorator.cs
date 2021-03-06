﻿using System;
using System.Drawing;

namespace Utilities.Mapper
{
    public class SquaredScreenRectDecorator : MapperDecorator
    {
        //public override double ScreenWidth => Mapper.ScreenRight - Mapper.ScreenLeft;
        //public override double ScreenHeight => Mapper.ScreenBottom - Mapper.ScreenTop;
        public SquaredScreenRectDecorator(IScreenToCoordinateMapper mapper) : base(mapper)
        {
        }
        public override void SetScreenArea(double left, double right, double top, double bottom)
        {
            var area = FindSquareInArea(left, right, top, bottom);
            base.SetScreenArea(area.Left, area.Right, area.Top, area.Bottom);
        }
        //public static RectangleF FindBiggestSquareIn(RectangleF rect)
        //{
        //    PointF center = CenterPointOf(rect);
        //    var squareWidth = Math.Min(rect.Width, rect.Height);
        //    var leftTopPoint = new PointF(center.X - squareWidth / 2, center.Y - squareWidth / 2);
        //    return new RectangleF(leftTopPoint, new SizeF(squareWidth, squareWidth));
        //}

        public static PointF CenterPointOf(RectangleF rect) => new PointF(rect.Width / 2 + rect.Left, rect.Height / 2 + rect.Top);

        public static PointF CenterPoint(double left, double right, double top, double bottom) => new PointF(Math.Abs((float)left - (float)right) / 2, Math.Abs((float)top - (float)bottom) / 2);
        public static double MaximumSquareWidth(double left, double right, double top, double bottom) => Math.Min(Math.Abs(left - right), Math.Abs(top - bottom))/2;

        private Area FindSquareInArea(double left, double right, double top, double bottom)
        {
            var center = CenterPoint(left, right, top, bottom);
            var width = MaximumSquareWidth(left, right, top, bottom);
            bool isXIncreasing = right > left;
            bool isYIncreaseing = top > bottom;

            Area ret;

            if(isXIncreasing)
            {
                ret.Left = center.X - width;
                ret.Right = center.X + width;
            }
            else
            {
                ret.Left = center.X + width;
                ret.Right = center.X - width;
            }

            if(isYIncreaseing)
            {
                ret.Top = center.Y + width;
                ret.Bottom = center.Y - width;
            }
            else
            {
                ret.Top = center.Y - width;
                ret.Bottom = center.Y + width;
            }
            
            return ret;
        }

        private struct Area
        {
            public double Left;
            public double Right;
            public double Top;
            public double Bottom;
        }
    }
}
