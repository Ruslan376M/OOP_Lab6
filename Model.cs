using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Лабораторная_работа__6
{
    public class Model
    {
        public Graphics g;
        public Bitmap image;
        private Painter painter;
        private Storage<GraphicObject> storage;
        private Storage<GraphicObject> selectedStorage;
        private Storage<Color> originColorStorage;
        private int creationMode;
        private Color color = Color.Black;
        public bool ctrlIsPressed;
        public bool altIsPressed;
        public bool wIsPressed;
        public bool aIsPressed;
        public bool sIsPressed;
        public bool dIsPressed;
        public bool mouseIsPressed;
        public bool creatingObject;
        public int velocity;

        public Model()
        {
            storage = new Storage<GraphicObject>();
            selectedStorage = new Storage<GraphicObject>();
            originColorStorage = new Storage<Color>();
            
            image = new Bitmap(1920, 1080);
            g = Graphics.FromImage(image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            painter = new Painter(g, Color.Red);
            velocity = 5;
        }

        public void setMode(int mode)
        {
            creationMode = mode;
        }

        public void setColor(Color color)
        {
            this.color = color;
            if(selectedStorage.getSize()!=0)
            {
                selectedStorage.setFirst();
                for (int i = 0; i < selectedStorage.getSize(); i++, selectedStorage.next())
                    selectedStorage.getCurrent().color = color;
            }
        }

        public GraphicObject temp;

        public void doTheRightThing(int x, int y)
        {
            if(check(x,y)) // Если попали по элементу, выбор
            {
                redrawAll();
            }
            else // Если попали по пустому месту, создание
            {
                deselectAll();
                creatingObject = true;
                if (creationMode == 0)
                    temp = new Line(x, y, 0, 0, color);
                else if (creationMode == 1)
                    temp = new Rectangle(x, y, 0, 0, color);
                else if (creationMode == 2)
                    temp = new Ellipse(x, y, 0, 0, color);
            }
        }

        public void drawOnline(int x, int y)
        {
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            g.Clear(Color.White);
            temp.width = x - temp.x;
            temp.height = y - temp.y;
            redrawAll();
            painter.drawObject(temp);
        }

        public void redrawAll()
        {
            g.Clear(Color.White);
            storage.setFirst();
            for (int i = 0; i < storage.getSize(); i++, storage.next())
                painter.drawObject(storage.getCurrent());
        }

        private bool check(int x,int y)
        {
            bool found = false;
            storage.setFirst();
            for (int i = 0; i < storage.getSize(); i++, storage.next())
                if (storage.getCurrent().belongsTo(x, y))
                {
                    temp = storage.getCurrent();

                    if (ctrlIsPressed)
                    {
                        if (temp.isSelected)
                        {
                            temp.isSelected = false;
                            selectedStorage.checkAndSetCurrent(temp);
                            selectedStorage.del();
                        }
                        else
                        {
                            temp.isSelected = true;
                            selectedStorage.add(temp);
                        }
                    }
                    else
                    {
                        deselectAll();
                        if (temp.isSelected == false)
                        {
                            selectedStorage.add(storage.getCurrent());
                            temp.isSelected = true;
                        }
                    }

                    found = true;
                }
            return found;
        }

        private void deselectAll() // Убираем все элементы из списка выбранных
        {
            selectedStorage.setFirst();
            for (int i = 0; i < selectedStorage.getSize(); i++, selectedStorage.next())
                selectedStorage.getCurrent().isSelected = false;
            selectedStorage = new Storage<GraphicObject>();
        }

        public void add()
        {
            if (Math.Abs(temp.width) < 10 && Math.Abs(temp.height) < 10)
            {
                redrawAll();
                return; 
            }
            if (temp.className() != "Line")
                if (temp.width < 0 || temp.height < 0)
                {
                    temp.x = Math.Min(temp.x, temp.x + temp.width);
                    temp.y = Math.Min(temp.y, temp.y + temp.height);
                    temp.width = Math.Abs(temp.width);
                    temp.height = Math.Abs(temp.height);
                }
            storage.add(temp);
        }
        public void correct()
        {
            selectedStorage.setFirst();
            for (int i = 0; i < selectedStorage.getSize(); i++, selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                if (temp.className() != "Line")
                    if (temp.width < 0 || temp.height < 0)
                    {
                        temp.x = Math.Min(temp.x, temp.x + temp.width);
                        temp.y = Math.Min(temp.y, temp.y + temp.height);
                        temp.width = Math.Abs(temp.width);
                        temp.height = Math.Abs(temp.height);
                    }
            }

        }
        public void move()
        {
            if (altIsPressed)
            { 
                changeSize();
                return ;
            }
            if (selectedStorage.getSize() == 0)
                return ;
            int x = 0;
            int y = 0;
            velocity++;
            if (wIsPressed)
                y--;
            if (aIsPressed)
                x--;
            if (sIsPressed)
                y++;
            if (dIsPressed)
                x++;
            x *= velocity / 5;
            y *= velocity / 5;
            selectedStorage.setFirst();
            for (int i = 0; i < selectedStorage.getSize(); i++, selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                if (temp.x + x >= 0 && temp.x + temp.width + x >= 0)
                    temp.x += x;
                if (temp.y + y >= 0 && temp.y + temp.height + y >= 0)
                    temp.y += y;
            }
            redrawAll();
        }

        public void changeSize()
        {
            if (selectedStorage.getSize() == 0)
                return;
            velocity++;
            int currentVelocity = velocity / 5;
            selectedStorage.setFirst();
            for (int i = 0; i < selectedStorage.getSize(); i++, selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                if (wIsPressed)
                    if (temp.y - currentVelocity >= 0 && temp.y + temp.height - currentVelocity >= 0)
                    { 
                        temp.height -= currentVelocity;
                    }
                if (aIsPressed)
                    if (temp.x - currentVelocity >= 0 && temp.x + temp.width - currentVelocity >= 0)
                    { 
                        temp.width -= currentVelocity;
                    }
                if (sIsPressed)
                    if (temp.y + currentVelocity >= 0 && temp.y + temp.height + currentVelocity >= 0)
                        temp.height += currentVelocity;
                if (dIsPressed)
                    if (temp.x + currentVelocity >= 0 && temp.x + temp.width + currentVelocity >= 0)
                        temp.width += currentVelocity;
            }
            redrawAll();
        }

        public void delete()
        {
            storage.setFirst();
            for (int i = 0; i < storage.getSize(); i++)
                if (storage.getCurrent().isSelected)
                {
                    storage.del();
                    i--;
                }
                else
                    storage.next();
            selectedStorage = new Storage<GraphicObject>();
            redrawAll();
        }
    }
}
