using System;
using System.Drawing;

namespace Лабораторная_работа__6
{
    public class Model
    {
        public Graphics g; // Предоставляет методы для рисования
        public Bitmap image; // Изображение, на котором происходят изменения
        private Painter painter; // Класс, который знает, как должны выглядеть объекты
        private Color color = Color.Black; // Цвет новых объектов
        private Color selectedColor = Color.Red; // Цвет выделенных объектов

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
            painter = new Painter(g, selectedColor);
        }

        public void setMode(int mode) // Выбрать объект для создания
        {
            creationMode = mode;
        }

        public void setColor(Color color) // Устанавливает цвет новых объектов и изменяет цвет выделенных
        {
            this.color = color;
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
                selectedStorage.getCurrent().color = color;
        }

        public void redrawAll() // Перерисовка изображения
        {
            g.Clear(Color.White);
            storage.setFirst();
            for (int i = 0; i < storage.getSize(); i++, storage.next())
                painter.drawObject(storage.getCurrent());
            refresh();
        }

        private void deselectAll() // Снимаем выделение всех объектов
        {
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
                selectedStorage.getCurrent().isSelected = false;
            selectedStorage = new Storage<GraphicObject>();
        }

        public void doTheRightThing(int x, int y) // В зависимости от исходных данных выполняет необходимые действия
        {
            if(checkAndSelect(x,y)) // Если попали по элементу, выбор
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
                            temp.isSelected = false; // Выделение снимается
                            selectedStorage.checkAndSetCurrent(temp);
                            selectedStorage.del();
                        }
                        else
                        {
                            temp.isSelected = true; // Иначе объект выделяется
                            selectedStorage.add(temp);
                        }
                    }
                    else
                    {
                        bool state = temp.isSelected; // Запоминаем состояние объекта
                        int n = selectedStorage.getSize(); // Запоминаем количество выделенных объектов
                        deselectAll(); // Снимаем выделение всех объектов
                        if (state == false || n > 1) // Объект выделяется, если до этого он не был выделен или был в группе
                        {
                            temp.isSelected = true;
                            selectedStorage.add(storage.getCurrent());
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
            correctTemp();
            storage.add(temp);
        }

        public void correctTemp() // Для объектов Rectangle и Ellipse нужна корректировка
        {
            if (temp.className() != "Line") 
                if (temp.width < 0 || temp.height < 0)
                {
                    temp.x = Math.Min(temp.x, temp.x + temp.width);
                    temp.y = Math.Min(temp.y, temp.y + temp.height);
                    temp.width = Math.Abs(temp.width);
                    temp.height = Math.Abs(temp.height);
                }
        }

        public void correctSelected() // Корректировка всех выделенных объектов
        {
            for (selectedStorage.setFirst(); !selectedStorage.eol(); selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                correctTemp();
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
            for (int i = 0; i < selectedStorage.getSize(); i++, selectedStorage.next())
            {
                temp = selectedStorage.getCurrent();
                if (x != 0)
                {
                    if (temp.x + x >= 0 && temp.x + temp.width + x >= 0)
                        temp.x += x;
                    else
                        velocity = 5;
                }
                if (y != 0)
                {
                    if (temp.y + y >= 0 && temp.y + temp.height + y >= 0)
                        temp.y += y;
                    else 
                        velocity = 5;
                }
            }
            redrawAll();
        }

        public void changeSize() // Изменяет размер выделенных объектов
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
                        temp.height -= currentVelocity;
                if (aIsPressed)
                    if (temp.x - currentVelocity >= 0 && temp.x + temp.width - currentVelocity >= 0)
                        temp.width -= currentVelocity;
                if (sIsPressed)
                    if (temp.y + currentVelocity >= 0 && temp.y + temp.height + currentVelocity >= 0)
                        temp.height += currentVelocity;
                if (dIsPressed)
                    if (temp.x + currentVelocity >= 0 && temp.x + temp.width + currentVelocity >= 0)
                        temp.width += currentVelocity;
            }
            redrawAll();
        }
    }
}
