using System;
namespace std
{
    public interface Shapes
    {
        double Area { get; }
        string Info { get; }
        bool Valid();
    }
    public class Triangle : Shapes
    {
        public Triangle(double a, double b, double c)
        {
            double[] newEdges = new double[3] { a, b, c };
            this.Edges = newEdges;
        }
        public double[] Edges { get; set; } = new double[3];
        public string Info
        {
            get => $"Triangle:a={Edges[0]},b={Edges[1]},c={Edges[2]}.";
        }
        public double Area
        {
            get
            {
                if (!Valid())
                {
                    throw new InvalidOperationException("Invalid.");
                }
                double p = (Edges[0] + Edges[1] + Edges[2]) / 2;
                return Math.Sqrt(p * (p - Edges[0]) * (p - Edges[1]) * (p - Edges[2]));
            }
        }
        public bool Valid()
        {
            double a = Edges[0], b = Edges[1], c = Edges[2];
            return a > 0 && b > 0 && c > 0 && a + b > c && b + c > a && a + c > b;
        }
    }
    public class Rectangle : Shapes
    {
        public Rectangle(double length, double width)
        {
            this.Length = length;
            this.Width = width;
        }
        public double Length { get; set; }
        public double Width { get; set; }
        public string Info => $"Rectangle:length={Length},width={Width}.";
        public double Area
        {
            get
            {
                if (!Valid())
                {
                    throw new InvalidOperationException("Invalid.");
                }
                return Length * Width;
            }
        }
        public bool Valid()
        {
            return Length > 0 && Width > 0;
        }
    }
    public class Square : Shapes
    {
        public Square(double side)
        {
            Side = side;
        }
        public double Side { get; set; }
        public string Info => $"Square:side={Side}.";
        public double Area
        {
            get
            {
                if (!Valid())
                {
                    throw new InvalidOperationException("Invalid.");
                }
                return Side * Side;
            }
        }
        public bool Valid()
        {
            return Side > 0;
        }
    }
    public class ShapeFactory
    {
        public static Shapes CreateShape()
        {
            Shapes result = null;
            while (result == null)
            {
                Random random = new Random();
                int type = random.Next(0, 3);
                int[] edges = new int[type + 1];
                for (int i = 0; i < edges.Length; i++)
                {
                    edges[i] = random.Next(200);
                }
                switch (type)
                {
                    case 0:
                        result = new Square(edges[0]);
                        break;
                    case 1:
                        result = new Rectangle(edges[0], edges[1]);
                        break;
                    case 2:
                        result = new Triangle(edges[0], edges[1], edges[2]);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid shape type.");
                }
                if (result.Valid() == false)
                {
                    result = null;
                }
            }
            return result;
        }
    }
    public class S1
    {
        public static void Main()
        {
            Shapes[] shape = new Shapes[10];
            for (int i = 0; i < shape.Length; i++)
            {
                shape[i] = ShapeFactory.CreateShape();
            }
            for(int i = 0; i < shape.Length; i++)
            {
                Console.WriteLine($"{shape[i].Info}, area={shape[i].Area}");

            }
            double total = shape.Sum(s => s.Area);
            Console.WriteLine($"total={total}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
