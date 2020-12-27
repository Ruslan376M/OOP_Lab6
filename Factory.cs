using System.IO;

namespace Лабораторная_работа__8
{
    public static class Factory
    {
        public static GraphicObject createObject(ref StreamReader reader)
        {
            string name = reader.ReadLine();
            GraphicObject obj = null;
            switch(name)
            {
                case "Line":
                    obj = new Line();
                    break;
                case "Rectangle":
                    obj = new Rectangle();
                    break;
                case "Ellipse":
                    obj = new Ellipse();
                    break;
                case "Compound":
                    obj = new Compound();
                    break;
            }
            obj.load(ref reader);
            return obj;
        }
    }
}
