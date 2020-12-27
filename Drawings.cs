using System;
using System.Drawing;
using System.IO;

namespace Лабораторная_работа__7
{
    public abstract class GraphicObject
    {
        public int x { get; protected set; }
        public int y { get; protected set; }
        public int width { get; protected set; }
        public int height { get; protected set; }
        public bool isSelected { get; protected set; }
        protected Color color;
        protected Color selectedColor = Color.Red;
        protected int thickness = 5;

        protected GraphicObject() { }

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

        public virtual void setColor(Color color)
        {
            this.color = color;
        }

        public virtual void move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public virtual void select()
        {
            isSelected = true;
        }

        public virtual void deselect()
        {
            isSelected = false;
        }

        public virtual void changeSize(int diffWidth, int diffHeight)
        {
            width += diffWidth;
            height += diffHeight;
        }

        public virtual void correct()
        {
            if (className() == "Line")
                return;
            if (width < 0 || height < 0)
            {
                x = Math.Min(x, x + width);
                y = Math.Min(y, y + height);
                width = Math.Abs(width);
                height = Math.Abs(height);
            }
        }

        public virtual string save()
        {
            string text = "";
            text += className() + '\n';
            text += x.ToString() + '\n';
            text += y.ToString() + '\n';
            text += width.ToString() + '\n';
            text += height.ToString() + '\n';
            text += false.ToString() + '\n';
            text += color.ToArgb().ToString() + '\n';
            return text;
        }

        public virtual void load(ref StreamReader reader)
        {
            x = int.Parse(reader.ReadLine());
            y = int.Parse(reader.ReadLine());
            width = int.Parse(reader.ReadLine());
            height = int.Parse(reader.ReadLine());
            isSelected = bool.Parse(reader.ReadLine());
            int t = int.Parse(reader.ReadLine());
            color = Color.FromArgb(t);
        }
    }

    public class Line : GraphicObject
    {
        public Line() { }
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
        public Rectangle() { }
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
        public Ellipse() { }
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

    public class Compound : GraphicObject
    {
        public Storage<GraphicObject> group;

        public Compound()
        {
            group = new Storage<GraphicObject>();
        }

        public void add(ref GraphicObject obj)
        {
            if (group.getSize() != 0)
            {
                int x1 = Math.Min(x, obj.x);
                int y1 = Math.Min(y, obj.y);
                int x2 = Math.Max(x + width, obj.x + obj.width);
                int y2 = Math.Max(y + height, obj.y + obj.height);
                x = x1;
                y = y1;
                width = x2 - x1;
                height = y2 - y1;
            }
            else
            {
                x = obj.x;
                y = obj.y;
                width = obj.width;
                height = obj.height;
            }
            group.add(ref obj);
        }

        public override string className()
        {
            return "Compound";
        }

        public override bool belongsTo(int x, int y)
        {
            for (group.setFirst(); !group.eol(); group.next())
                if (group.getCurrent().belongsTo(x, y))
                    return true;
            return false;
        }

        public override void draw(ref Graphics g)
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().draw(ref g);
        }

        public override void setColor(Color color)
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().setColor(color);
        }

        public override void move(int x, int y)
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().move(x, y);
            this.x += x;
            this.y += y;
        }

        public override void select()
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().select();
            isSelected = true;
        }

        public override void deselect()
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().deselect();
            isSelected = false;
        }

        public override void changeSize(int diffWidth, int diffHeight)
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().changeSize(diffWidth, diffHeight);
            width += diffWidth;
            height += diffHeight;
        }

        public override void correct()
        {
            for (group.setFirst(); !group.eol(); group.next())
                group.getCurrent().correct();
        }

        public override string save()
        {
            string text = "";
            text += className() + '\n';
            text += group.getSize().ToString() + '\n';
            text += x.ToString() + '\n';
            text += y.ToString() + '\n';
            text += width.ToString() + '\n';
            text += height.ToString() + '\n';
            text += false.ToString() + '\n';
            text += color.ToArgb().ToString() + '\n';
            for (group.setFirst(); !group.eol(); group.next())
                text += group.getCurrent().save();
            return text;
        }

        public override void load(ref StreamReader reader)
        {
            int n = int.Parse(reader.ReadLine());
            x = int.Parse(reader.ReadLine());
            y = int.Parse(reader.ReadLine());
            width = int.Parse(reader.ReadLine());
            height = int.Parse(reader.ReadLine());
            isSelected = bool.Parse(reader.ReadLine());
            color = Color.FromArgb(int.Parse(reader.ReadLine()));
            for (int i = 0; i < n; i++)
            {
                GraphicObject obj = Factory.createObject(ref reader);
                group.add(ref obj);
            }
        }
    }
}
