using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create the individual shapes using positive dimensions.
        Square square = new Square("Red", 5);
        Rectangle rectangle = new Rectangle("Blue", 4, 6);
        Circle circle = new Circle("Green", 3);

        // Store different types of shapes in one list.
        List<Shape> shapes = new List<Shape>();
        shapes.Add(square);
        shapes.Add(rectangle);
        shapes.Add(circle);

        // Polymorphism calls the appropriate GetArea() override.
        foreach (Shape shape in shapes)
        {
            string color = shape.GetColor();
            double area = shape.GetArea();

            Console.WriteLine($"Color: {color}, Area: {area:F2}");
        }
    }
}