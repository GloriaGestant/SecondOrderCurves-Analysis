using System;
using System.Collections.Generic;

// Абстрактный класс линии второго порядка
public abstract class SecondOrderCurve
{
    protected double A, B, C, D, E, F;
    
    public SecondOrderCurve(double a, double b, double c, double d, double e, double f)
    {
        A = a;
        B = b;
        C = c;
        D = d;
        E = e;
        F = f;
    }
    
    // Абстрактные методы
    public abstract List<Tuple<double, double>> FindCentersOrVertices();
    public abstract void PrintData();
}

// Класс окружности
public class Circle : SecondOrderCurve
{
    public Circle(double a, double b, double c, double d, double e, double f) 
        : base(a, b, c, d, e, f)
    {
        // Проверка на условие окружности: A = C, B = 0
        if (A != C || B != 0)
        {
            throw new ArgumentException("Уравнение не соответствует окружности");
        }
    }
    
    public override List<Tuple<double, double>> FindCentersOrVertices()
    {
        double x0 = -D / (2 * A);
        double y0 = -E / (2 * A);
        return new List<Tuple<double, double>> { Tuple.Create(x0, y0) };
    }
    
    public override void PrintData()
    {
        var center = FindCentersOrVertices()[0];
        double radius = Math.Sqrt((D*D + E*E) / (4*A*A) - F/A;
        Console.WriteLine("Окружность:");
        Console.WriteLine($"Центр: ({center.Item1}, {center.Item2})");
        Console.WriteLine($"Радиус: {radius}");
    }
}

// Класс параболы
public class Parabola : SecondOrderCurve
{
    public Parabola(double a, double b, double c, double d, double e, double f) 
        : base(a, b, c, d, e, f)
    {
        // Проверка на условие параболы: B² - 4AC = 0
        if (B*B - 4*A*C != 0)
        {
            throw new ArgumentException("Уравнение не соответствует параболе");
        }
    }
    
    public override List<Tuple<double, double>> FindCentersOrVertices()
    {
        // Вершина параболы
        double x0 = (2*C*D - B*E) / (B*B - 4*A*C);
        double y0 = (2*A*E - B*D) / (B*B - 4*A*C);
        return new List<Tuple<double, double>> { Tuple.Create(x0, y0) };
    }
    
    public double FindDirectrix()
    {
        // Для вертикальной параболы (x - x0)² = 4p(y - y0)
        var vertex = FindCentersOrVertices()[0];
        double p = 1 / (4 * A);
        return vertex.Item2 - p;
    }
    
    public override void PrintData()
    {
        var vertex = FindCentersOrVertices()[0];
        Console.WriteLine("Парабола:");
        Console.WriteLine($"Вершина: ({vertex.Item1}, {vertex.Item2})");
        Console.WriteLine($"Директриса: y = {FindDirectrix()}");
    }
}

// Класс эллипса
public class Ellipse : SecondOrderCurve
{
    public Ellipse(double a, double b, double c, double d, double e, double f) 
        : base(a, b, c, d, e, f)
    {
        // Проверка на условие эллипса: B² - 4AC < 0
        if (B*B - 4*A*C >= 0)
        {
            throw new ArgumentException("Уравнение не соответствует эллипсу");
        }
    }
    
    public override List<Tuple<double, double>> FindCentersOrVertices()
    {
        // Центр эллипса
        double x0 = (2*C*D - B*E) / (B*B - 4*A*C);
        double y0 = (2*A*E - B*D) / (B*B - 4*A*C);
        return new List<Tuple<double, double>> { Tuple.Create(x0, y0) };
    }
    
    public double FindEccentricity()
    {
        // Приведение к каноническому виду
        var center = FindCentersOrVertices()[0];
        // Упрощенный расчет (для примера)
        return Math.Sqrt(1 - (C)/(A));
    }
    
    public override void PrintData()
    {
        var center = FindCentersOrVertices()[0];
        Console.WriteLine("Эллипс:");
        Console.WriteLine($"Центр: ({center.Item1}, {center.Item2})");
        Console.WriteLine($"Эксцентриситет: {FindEccentricity()}");
    }
}

// Класс гиперболы
public class Hyperbola : SecondOrderCurve
{
    public Hyperbola(double a, double b, double c, double d, double e, double f) 
        : base(a, b, c, d, e, f)
    {
        // Проверка на условие гиперболы: B² - 4AC > 0
        if (B*B - 4*A*C <= 0)
        {
            throw new ArgumentException("Уравнение не соответствует гиперболе");
        }
    }
    
    public override List<Tuple<double, double>> FindCentersOrVertices()
    {
        // Центр гиперболы
        double x0 = (2*C*D - B*E) / (B*B - 4*A*C);
        double y0 = (2*A*E - B*D) / (B*B - 4*A*C);
        return new List<Tuple<double, double>> { Tuple.Create(x0, y0) };
    }
    
    public override void PrintData()
    {
        var center = FindCentersOrVertices()[0];
        Console.WriteLine("Гипербола:");
        Console.WriteLine($"Центр: ({center.Item1}, {center.Item2})");
    }
}

// Пример использования
public class Program
{
    public static void Main()
    {
        try
        {
            // Создаем окружность: x² + y² - 4x - 6y + 9 = 0
            var circle = new Circle(1, 0, 1, -4, -6, 9);
            circle.PrintData();
            
            // Создаем параболу: y² - 4x = 0
            var parabola = new Parabola(0, 0, 1, -4, 0, 0);
            parabola.PrintData();
            
            // Создаем эллипс: 4x² + 9y² - 16x - 18y - 11 = 0
            var ellipse = new Ellipse(4, 0, 9, -16, -18, -11);
            ellipse.PrintData();
            
            // Создаем гиперболу: x² - y² - 2x + 4y - 4 = 0
            var hyperbola = new Hyperbola(1, 0, -1, -2, 4, -4);
            hyperbola.PrintData();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
