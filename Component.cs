using System;
using System.Drawing;

namespace Лабораторная_работа__N
{
    public abstract class GraphicObject
    {
        protected int x;
        protected int y;
        protected int width;
        protected int height;
        public bool isSelected { get; }
        protected Color color;
        protected Color selectedColor = Color.Red;
        protected int thickness = 5;

        public GraphicObject(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public abstract string className();

        public abstract bool belongsTo(int x, int y);

        public abstract void draw(ref Graphics g);

        public virtual void move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
    }

    public class Line : GraphicObject
    {
        public Line(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override string className()
        {
            return "Line";
        }

        public override bool belongsTo(int x, int y)
        {
            double tgA = (double)(height) / (width);
            int minX = Math.Min(this.x, this.x + width);
            int maxX = Math.Max(this.x, this.x + width);
            int minY = Math.Min(this.y, this.y + height);
            int maxY = Math.Max(this.y, this.y + height);
            double d = Math.Abs(tgA * (this.x - x) + y - this.y) / Math.Sqrt(tgA * tgA + 1);
            if (d <= thickness && minX <= x && x <= maxX && minY <= y && y <= maxY)
                return true;
            return false;
        }

        public override void draw(ref Graphics g)
        {
            Pen pen;
            if (isSelected)
            {
                pen = new Pen(selectedColor, thickness);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
                pen = new Pen(color, thickness);
            g.DrawLine(pen, x, y, x + width, y + height);
        }
    }

    public class Rectangle : GraphicObject
    {
        public Rectangle(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override string className()
        {
            return "Rectangle";
        }

        public override bool belongsTo(int x, int y)
        {
            if (this.x - thickness <= x && x <= this.x + width + thickness)
                if (this.y - thickness <= y && y <= this.y + height + thickness)
                {
                    int result = 0;
                    if (Math.Abs(x - this.x) <= thickness)
                        result++;
                    if (Math.Abs(x - this.x - width) <= thickness)
                        result++;
                    if (Math.Abs(y - this.y) <= thickness)
                        result++;
                    if (Math.Abs(y - this.y - height) <= thickness)
                        result++;
                    if (result == 1)
                        return true;
                }
            return false;
        }

        public override void draw(ref Graphics g)
        {
            Pen pen;
            if (isSelected)
            {
                pen = new Pen(selectedColor, thickness);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
                pen = new Pen(color, thickness);

            int x1 = width + x;
            int y1 = height + y;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                Math.Min(x, x1),
                Math.Min(y, y1),
                Math.Abs(width),
                Math.Abs(height));
            g.DrawRectangle(pen, rect);
        }
    }

    public class Ellipse : GraphicObject
    {
        public Ellipse(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override string className()
        {
            return "Ellipse";
        }

        public override bool belongsTo(int x, int y)
        {
            int a = width / 2;
            int b = height / 2;
            int centerX = this.x + a;
            int centerY = this.y + b;
            int lowR = (a - 3) * (b - 3);
            int highR = (a + 3) * (b + 3);
            int temp = b * b * (x - centerX) * (x - centerX) + a * a * (y - centerY) * (y - centerY);
            return lowR * lowR <= temp && temp <= highR * highR;
        }

        public override void draw(ref Graphics g)
        {
            Pen pen;
            if (isSelected)
            {
                pen = new Pen(selectedColor, thickness);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
                pen = new Pen(color, thickness);
            g.DrawEllipse(pen, x, y, width, height);
        }
    }
}
