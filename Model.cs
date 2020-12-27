using System;
using System.Drawing;
using System.IO;

namespace Лабораторная_работа__7
{
    public class Model
    {
        public Graphics g; // Предоставляет методы для рисования
        public Bitmap image; // Изображение, на котором происходят изменения
        private Color color = Color.Black; // Цвет новых объектов

        private Storage<GraphicObject> storage; // Хранилище всех объектов
        private Storage<GraphicObject> selectedStorage; // Хранилище выделенных объектов
        public GraphicObject temp; // Временный объект, находящийся в процессе создания

        private int creationMode; // Тип создаваемой фигуры (Line = 0, Rectangle = 1, Ellipse = 2)
        public int velocity = 5; // Скорость изменения, передвижения. Поделить на 5
        public bool creatingObject; // ЛКМ нажата и не отпущена - режим создания объекта

        public bool ctrlIsPressed;
        public bool shiftIsPressed;
        public bool wIsPressed;
        public bool aIsPressed;
        public bool sIsPressed;
        public bool dIsPressed;
        
        public delegate void Refresh();
        public Refresh refresh; // Функция обновления картинки в интерфейсе

        public Model()
        {
            storage = new Storage<GraphicObject>();
            selectedStorage = new Storage<GraphicObject>();
            
            image = new Bitmap(1920, 1080);
            g = Graphics.FromImage(image);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // Смягчение изображения
        }

        public void setMode(int mode) // Выбрать объект для создания
        {
            creationMode = mode;
        }

        public void setColor(Color color) // Устанавливает цвет новых объектов и изменяет цвет выделенных
        {
            this.color = color;
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
                selectedStorage.getCurrent().setColor(color);
        }

        public void redrawAll() // Перерисовка изображения
        {
            g.Clear(Color.White);
            storage.setFirst();
            for (int i = 0; i < storage.getSize(); i++, storage.next())
                storage.getCurrent().draw(ref g);
            refresh();
        }

        private void deselectAll() // Снимаем выделение всех объектов
        {
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
                selectedStorage.getCurrent().deselect();
            selectedStorage = new Storage<GraphicObject>();
        }

        public void doTheRightThing(int x, int y) // В зависимости от исходных данных выполняет необходимые действия
        {
            if(checkAndSelect(x,y)) // Если попали по элементу - выделение
                redrawAll();
            else // Если попали по пустому месту - создание
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
            temp.changeSize(x - temp.x - temp.width, y - temp.y - temp.height);
            redrawAll();
            temp.draw(ref g);
        }

        // Функция ищет объекты, которым принадлежит заданная точка.
        // Если ctrl не зажат, хранилище выбранных очищается.
        // Если точка принадлежит объекту,
        // Объект добавляется в хранилище выбранных.
        // Функция вернёт true, если точка принадлежит объекту,
        // И false, если кликнули на пустое место.
        private bool checkAndSelect(int x,int y) 
        {
            bool found = false;
            for (storage.setFirst(); !storage.eol(); storage.next())
                if (storage.getCurrent().belongsTo(x, y))
                {
                    temp = storage.getCurrent();
                    if (ctrlIsPressed) // Если зажат ctrl
                    {
                        if (temp.isSelected) // А объект уже выделен
                        {
                            temp.deselect(); // Выделение снимается
                            selectedStorage.checkAndSetCurrent(temp);
                            selectedStorage.del();
                        }
                        else
                        {
                            temp.select(); // Иначе объект выделяется
                            selectedStorage.add(ref temp);
                        }
                    }
                    else
                    {
                        bool state = temp.isSelected; // Запоминаем состояние объекта
                        int n = selectedStorage.getSize(); // Запоминаем количество выделенных объектов
                        deselectAll(); // Снимаем выделение всех объектов
                        if (state == false || n > 1) // Объект выделяется, если до этого он не был выделен или был в группе
                        {
                            temp.select();
                            selectedStorage.add(ref storage.getCurrent());
                        }
                    }
                    found = true;
                }
            return found;
        }

        public void add() // Добавление объекта в хранилище
        {
            if (Math.Abs(temp.width) < 10 && Math.Abs(temp.height) < 10) // Если объект очень мал, не создаём его
            {
                redrawAll();
                return; 
            }
            temp.correct();
            storage.add(ref temp);
        }

        public void correctSelected() // Корректировка всех выделенных объектов
        {
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                temp.correct();
            }
        }

        public void delete() // Удаление выделенных объектов
        {
            for (storage.setFirst(); !storage.eol();)
                if (storage.getCurrent().isSelected)
                    storage.del();
                else
                    storage.next();
            selectedStorage = new Storage<GraphicObject>();
            redrawAll();
        }

        public void move() // Двигает выделенные объекты
        {
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
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                int x1 = x;
                int y1 = y;
                if (x != 0)
                {
                    if (temp.x + x < 0 || temp.x + temp.width + x < 0)
                    {
                        x1 = 0;
                        velocity = 5; 
                    }
                }
                if (y != 0)
                {
                    if (temp.y + y < 0 || temp.y + temp.height + y < 0)
                    {
                        y1 = 0;
                        velocity = 5;
                    }
                }
                if (x1 != 0 || y1 != 0)
                    temp.move(x1, y1);
            }
            redrawAll();
        }

        public void changeSize() // Изменяет размер выделенных объектов
        {
            if (selectedStorage.getSize() == 0)
                return;
            velocity++;
            int currentVelocity = velocity / 5;
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                int width = 0;
                int height = 0;
                if (wIsPressed)
                    if (temp.y - currentVelocity >= 0 && temp.y + temp.height - currentVelocity >= 0)
                        height -= currentVelocity;
                if (aIsPressed)
                    if (temp.x - currentVelocity >= 0 && temp.x + temp.width - currentVelocity >= 0)
                        width -= currentVelocity;
                if (sIsPressed)
                    if (temp.y + currentVelocity >= 0 && temp.y + temp.height + currentVelocity >= 0)
                        height += currentVelocity;
                if (dIsPressed)
                    if (temp.x + currentVelocity >= 0 && temp.x + temp.width + currentVelocity >= 0)
                        width += currentVelocity;
                temp.changeSize(width, height);
            }
            redrawAll();
        }

        public void group()
        {
            selectedStorage = new Storage<GraphicObject>();
            GraphicObject group = new Compound();
            for (storage.setFirst(); !storage.eol();)
                if (storage.getCurrent().isSelected)
                {
                    ((Compound)group).add(ref storage.getCurrent());
                    storage.del();
                }
                else
                    storage.next();
            storage.add(ref group);
            selectedStorage.add(ref group);
        }

        public void ungroup()
        {
            selectedStorage = new Storage<GraphicObject>();
            for (storage.setFirst(); !storage.eol();)
                if (storage.getCurrent().isSelected && storage.getCurrent().className() == "Compound")
                {
                    Compound temp = (Compound)storage.getCurrent();
                    for (temp.group.setFirst(); !temp.group.eol(); temp.group.next())
                    {
                        storage.add(ref temp.group.getCurrent());
                        selectedStorage.add(ref temp.group.getCurrent());
                    }
                    storage.del();
                }
                else
                    storage.next();
        }

        public void save()
        {
            StreamWriter writer = new StreamWriter("saveFile", false);
            writer.WriteLine(storage.getSize());
            for (storage.setFirst(); !storage.eol(); storage.next())
                writer.Write(storage.getCurrent().save());
            writer.Close();
        }

        public void load()
        {
            clear();
            StreamReader reader = new StreamReader("saveFile");
            int n = int.Parse(reader.ReadLine());
            for (int i = 0; i < n; i++)
            {
                temp = Factory.createObject(ref reader);
                storage.add(ref temp); 
            }
            reader.Close();
            redrawAll();
        }

        public void clear()
        {
            temp = null;
            storage = new Storage<GraphicObject>();
            selectedStorage = new Storage<GraphicObject>();
            GC.Collect();
            redrawAll();
        }
    }
}
