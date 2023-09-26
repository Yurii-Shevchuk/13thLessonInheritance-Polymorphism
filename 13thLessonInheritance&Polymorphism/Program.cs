using System.Reflection;

namespace _13thLessonInheritance_Polymorphism
{
    public class Shape
    {
        public Shape(string name)
        {
            Name = name;
        }
        public string Name { get; init;}

        public virtual float Perimeter => 0;

        public virtual float Area => 0;

        public virtual void DrawShapeToConsole()
        {
            Console.WriteLine("I do not know how to draw such a shape, sorry, here, take this instead:");
            Console.WriteLine(@"    /\_____/\
   /  o   o  \
  ( ==  ^  == )
   )         (
  (           )
 ( (  )   (  ) )
(__(__)___(__)__)");
        }


        public override string ToString()
            => $"{Name} has an area of {Area} sq. units and perimeter of {Perimeter} units";
    }

    public class Triangle : Shape
    {
        public Triangle(float height, float baseWidth, float sideB, float sideC)
            : base(nameof(Triangle))
        {
            Height = height;
            BaseWidth = baseWidth;
            SideB = sideB;
            SideC = sideC;
        }
        public float Height { get; init; }
        public float BaseWidth { get; init; }
        public float SideB { get; init; }
        public float SideC { get; init; }

        public override float Area => 0.5f * BaseWidth * Height;

        public override float Perimeter => BaseWidth + SideB + SideC;


    }



    public class RightTriangle : Triangle
    {
        public RightTriangle(float height, float baseWidth, float sideB, float sideC)
            : base(height, baseWidth,  sideB,  sideC) 
        {
            Name = nameof(RightTriangle);
        }
        public override void DrawShapeToConsole()
        {
            Console.Write("*");
            Console.WriteLine();

            for (int i = 0; i < Height - 2; i++) 
            {
                Console.Write("*");
                for(int j = 0; j < i; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("*");
                Console.WriteLine();
            }
            for (int i = 0; i < Height; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }

   public class EquilateralTriangle : Triangle
    {
        public EquilateralTriangle(float height, float baseWidth, float sideB, float sideC)
            : base(height, baseWidth, sideB, sideC)
        {
            Name = nameof(EquilateralTriangle);
            if(sideC != baseWidth || sideB != baseWidth)
            {
                Height = MathF.Round(0.5f * MathF.Sqrt(3) * baseWidth);
                SideB = baseWidth;
                SideC = baseWidth;
            }
        }

        public override float Area => MathF.Sqrt(3) * 0.25f * MathF.Pow(BaseWidth, 2);
        public override void DrawShapeToConsole()
        {
            for(int i = 0; i < (int)(BaseWidth/2); i++)
            {
                Console.Write(" ");
            }
            Console.Write("*");
            Console.WriteLine();
            for (int i = 0; i < (int)(BaseWidth / 2) - 1; i++)
            {
                for(int j = 0; j < (int)(BaseWidth / 2) - i -1; j++)
                {
                    Console.Write(" ");
                }
                Console.Write("*");
                for(int k = 0; k < 2*i +1;  k++)
                {
                    Console.Write(" ");
                }
                Console.Write("*");
                Console.WriteLine();
            }
            if(BaseWidth%2  == 0f) 
            {
                for (int i = 0; i < (int)BaseWidth+1; i++)
                {
                    Console.Write("*");
                }
            }
            else
            {
                for (int i = 0; i < (int)BaseWidth; i++)
                {
                    Console.Write("*");
                }
            }
            
        }
    }

    public class Circle : Shape
    {
        public Circle(float radius)
            : base(nameof(Circle))
        {
            Radius = radius;
        }

        public float Radius { get; }

        private readonly float Thickness = 0.5f;

        // IEEE 754 - float/double
        public override float Perimeter => 2 * MathF.PI * Radius;

        public override float Area => MathF.PI * Radius * Radius;

        public override void DrawShapeToConsole()
        {
            float rIn = Radius - Thickness, rOut = Radius + Thickness;

            for (float y = Radius; y >= -Radius; --y)
            {
                for (float x = -Radius; x < rOut; x += 0.5f)
                {
                    float value = x * x + y * y;
                    if (value >= rIn * rIn && value <= rOut * rOut)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle(float width, float height)
            : base(nameof(Rectangle))
        {
            Width = width;
            Height = height;
        }

        public float Width { get; }

        public float Height { get; }

        public override float Area => Width * Height;

        public override float Perimeter => 2 * (Width + Height);

        public override void DrawShapeToConsole()
        {
            for(int i = 0; i < Width; i++)
            {
                Console.Write("*");
                Console.Write(" ");
            }
            Console.WriteLine();
            for(int i = 1; i<Height-1; i ++)
            {
                Console.Write("*");
                for (int j = 1; j < Width*2-2; j ++)
                {
                    Console.Write(" ");
                }
                Console.Write("*");
                Console.WriteLine();
            }
            for (int i = 0; i < Width; i++)
            {
                Console.Write("*");
                Console.Write(" ");
            }
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Enter count of shapes: ");
            int count = int.Parse(Console.ReadLine());

            Shape[] shapes = new Shape[count];
            for (int i = 0; i < count; i++)
            {
                shapes[i] = ReadShape();
            }
            foreach(var shape in shapes)
            {
                Console.WriteLine(shape);
                DrawColoredShape(shape);
            }

        }

        static void DrawColoredShape(Shape shape)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = SetConsoleColor();
            shape.DrawShapeToConsole();
            Console.ForegroundColor = prev;
        }

        static ConsoleColor SetConsoleColor()
        {
           
            Random rand = new Random();
            int randomColor = rand.Next(0, 15);
            return (ConsoleColor)(MyConsoleColor)randomColor;

        }
        public enum MyConsoleColor
        {
            DarkBlue = ConsoleColor.DarkBlue,
            DarkGreen = ConsoleColor.DarkGreen,
            DarkCyan = ConsoleColor.DarkCyan,
            DarkRed = ConsoleColor.DarkRed,
            DarkMagenta = ConsoleColor.DarkMagenta,
            DarkYellow = ConsoleColor.DarkYellow,
            Gray = ConsoleColor.Gray,
            DarkGray = ConsoleColor.DarkGray,
            Blue = ConsoleColor.Blue,
            Green = ConsoleColor.Green,
            Cyan = ConsoleColor.Cyan,
            Red = ConsoleColor.Red,
            Magenta = ConsoleColor.Magenta,
            Yellow = ConsoleColor.Yellow,
            White = ConsoleColor.White
        }

        static Shape ReadShape()
        {
            Console.WriteLine("Choose a shape type: ");
            Console.WriteLine("1. Circle");
            Console.WriteLine("2. Rectangle");
            Console.WriteLine("3. Generic Triangle");
            Console.WriteLine("4. Right Triangle");
            Console.WriteLine("5. Equilateral Triangle");

        Read_Input:
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    Console.Write("Enter circle radius: ");
                    float radius = float.Parse(Console.ReadLine());
                    return new Circle(radius);

                case 2:
                    Console.Write("Enter rectangle width: ");
                    float width = float.Parse(Console.ReadLine());
                    Console.Write("Enter rectangle height: ");
                    float height = float.Parse(Console.ReadLine());
                    return new Rectangle(width, height);
                case 3:
                    Console.Write("Enter triangle height: ");
                    height = float.Parse(Console.ReadLine());
                    Console.Write("Enter triangle base (aka side A): ");
                    float baseWidth = float.Parse(Console.ReadLine());
                    Console.Write("Enter triangle side B: ");
                    float sideB = float.Parse(Console.ReadLine());
                    Console.Write("Enter triangle side C: ");
                    float sideC = float.Parse(Console.ReadLine());
                    return new Triangle(height, baseWidth, sideB, sideC);
                    
                case 4:
                    Console.Write("Enter right triangle height: ");
                    height = float.Parse(Console.ReadLine());
                    Console.Write("Enter right triangle base (aka side A): ");
                    baseWidth = float.Parse(Console.ReadLine());
                    Console.Write("Enter right triangle side B: ");
                    sideB = float.Parse(Console.ReadLine());
                    Console.Write("Enter right triangle side C: ");
                    sideC = float.Parse(Console.ReadLine());
                    return new RightTriangle(height, baseWidth, sideB, sideC);
                case 5:
                    Console.Write("Enter equilateral triangle side: ");
                    baseWidth = float.Parse(Console.ReadLine());
                    return new EquilateralTriangle(baseWidth, baseWidth, baseWidth, baseWidth);

                default:
                    Console.Write("Incorrect shape type. Choose again: ");
                    goto Read_Input;
            }
        }
    }
}