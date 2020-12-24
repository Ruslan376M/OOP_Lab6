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
        private Graphics g;
        public Bitmap image;
        private Painter painter;
        private Storage<GraphicObject> storage;
        private Storage<GraphicObject> selectedStorage;
        private int creationMode;
        private Color color;
        public bool ctrlIsPressed;
        public bool altIsPressed;
        public bool wIsPressed;
        public bool aIsPressed;
        public bool sIsPressed;
        public bool dIsPressed;
        public bool mouseIsPressed;

        public Model()
        {
            storage = new Storage<GraphicObject>();
            selectedStorage = new Storage<GraphicObject>();
            image = new Bitmap(1920, 1080);
            g = Graphics.FromImage(image);
            painter = new Painter(g);
        }

        public void setMode(int mode)
        {
            creationMode = mode;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public void doTheRightThing(int x, int y)
        {
            if(check(x,y))
            {

            }
        }

        private bool check(int x,int y)
        {
            bool found = false;
            storage.setFirst();
            for (int i = 0; i < storage.getSize(); i++, storage.next())
                if (storage.getCurrent().belongsTo(x, y))
                { 
                    selectedStorage.add(storage.getCurrent());
                    found = true;
                }
            return found;
        }

        public void add(int x, int y, int width, int height, Color color)
        {

        }

        public void move()
        {

        }

        public void delete()
        {

        }
    }
}
