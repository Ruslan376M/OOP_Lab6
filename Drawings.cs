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
        public bool isSelected = false;
        public Color color;
        public int thickness = 5;

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
    }

    public class Line : GraphicObject // В Line width и height становятся x2 и y2
    {
        public Line(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override string className()
        {
            return "Line";
        }

        public override bool belongsTo(int x, int y)
        {
            double tgA = (double)(height) / (width) ;
            int minX = Math.Min(this.x, this.x + width);
            int maxX = Math.Max(this.x, this.x + width);
            int minY = Math.Min(this.y, this.y + height);
            int maxY = Math.Max(this.y, this.y + height);
            double d = Math.Abs(tgA * (this.x - x) + y - this.y) / Math.Sqrt(tgA * tgA + 1);
            if (d <= thickness && minX <= x && x <= maxX && minY <= y && y <= maxY)
                return true;
            return false;
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
    }

    public class Painter
    {
        private Graphics g;
        private Color selectedColor;

        public Painter(Graphics g, Color selectedColor)
        {
            this.g = g;
            this.selectedColor = selectedColor;
        }

        public void drawObject(GraphicObject obj)
        {
            Pen pen;
            if (obj.isSelected)
            {
                pen = new Pen(selectedColor, obj.thickness);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            }
            else
                pen = new Pen(obj.color, obj.thickness);
            string className = obj.className();
            switch(className)
            {
                case "Line":
                    g.DrawLine(pen, obj.x, obj.y, obj.x + obj.width, obj.y + obj.height);
                    break;
                case "Rectangle":
                    {
                        int x1 = obj.width + obj.x;
                        int y1 = obj.height + obj.y;
                        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                            Math.Min(obj.x, x1),
                            Math.Min(obj.y, y1),
                            Math.Abs(obj.width),
                            Math.Abs(obj.height));
                        g.DrawRectangle(pen, rect);
                    }
                    break;
                case "Ellipse":
                    g.DrawEllipse(pen, obj.x, obj.y, obj.width, obj.height);
                    break;
            }
        }
    }
}
