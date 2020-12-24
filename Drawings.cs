using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_работа__6
{
    public abstract class GraphicObject
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public Brush color;
        protected int thickness = 3;

        public GraphicObject(int x, int y, int width, int height, Brush color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;
        }

        public abstract string className();

        public abstract bool belongsTo(int x, int y);
    }

    public class Line : GraphicObject // В Line width и height становятся x2 и y2
    {
        public Line(int x, int y, int width, int height, Brush color)
            : base(x, y, width, height, color) { }

        public override string className()
        {
            return "Line";
        }

        public override bool belongsTo(int x, int y)
        {
            // this.x = x1, this.y = y1, width = x2, height = y2 
            double tgA = (double)(height - this.y) / (width - this.x);
            double e = (y - this.y - tgA * (x - this.x)) / thickness;
            if ((-1 - tgA) <= e && e <= (1 + tgA))
                return true;
            return false;
        }
    }

    public class Rectangle : GraphicObject
    {
        public Rectangle(int x, int y, int width, int height, Brush color)
            : base(x, y, width, height, color) { }

        public override string className()
        {
            return "Rectangle";
        }

        public override bool belongsTo(int x, int y)
        {
            bool a = Math.Abs(x - this.x) <= thickness;
            bool b = Math.Abs(x - this.x - width) <= thickness;
            bool c = Math.Abs(y - this.y) <= thickness;
            bool d = Math.Abs(y - this.y - height) <= thickness;
            return (a || b) && (c || d);
        }
    }

    public class Ellipse : GraphicObject
    {
        public Ellipse(int x, int y, int width, int height, Brush color)
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
    }

    public class Painter
    {
        private Graphics g;

        public Painter(Graphics g)
        {
            this.g = g;
        }

        public void drawLine(GraphicObject line)
        {
            Pen pen = new Pen(line.color);
            g.DrawLine(pen, line.x, line.y, line.width, line.height);
        }

        public void drawRectangle(GraphicObject rectangle)
        {
            Pen pen = new Pen(rectangle.color);
            g.DrawRectangle(pen, rectangle.x, rectangle.y, rectangle.width, rectangle.height);
        }

        public void drawEllipse(GraphicObject ellipse)
        {
            Pen pen = new Pen(ellipse.color);
            g.DrawEllipse(pen, ellipse.x, ellipse.y, ellipse.width, ellipse.height);
        }
    }
}
