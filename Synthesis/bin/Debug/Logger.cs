﻿namespace Playground
{
    using System;
    using static System.Console;

    public static class Logger
    {
        public static double Add(double a, double b)
        {
            return a + b;
        }

        public static double Sub(double a, double b)
        {
            return a - b;
        }

        public static double Mul(double a, double b)
        {
            return a * b;
        }

        public static double Div(double a, double b)
        {
            return a / b;
        }

        public static double AreaOfTriangle(double a, double h)
        {
            return a * h / 2;
        }

        public static double AreaOfEquilateralTriangle(double a)
        {
            return a * a * Math.Sqrt(3) / 2;
        }

        public static double SquareFuncion (double a, double b, double c, double x)
        {
            return a * x * x  + b * x + c;
        }

        public static double Delta(double a, double b, double c, double x)
        {
            return b * b - 4 * a * c;
        }

        public static void Main()
        {

            Console.WriteLine(Add(6, 3));
            Console.WriteLine(Sub(6, 3));
            Console.WriteLine(Mul(6, 3));
            Console.WriteLine(Div(6, 3));
            Console.WriteLine(AreaOfTriangle(6,3));
            Console.WriteLine(AreaOfEquilateralTriangle(6));
            Console.WriteLine(SquareFuncion(3,6,7,8.5));
            Console.WriteLine(Delta(3, 6, 7, 8.5));
            Console.ReadKey();
        }

        private static void Log(string str)
        {
            ConsoleColor aux = ForegroundColor;
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine(str);
            ForegroundColor = aux;
        }
            
        private static void Log(string str, object o)
        {
            ConsoleColor aux = ForegroundColor;
            ForegroundColor = ConsoleColor.DarkGreen;
            WriteLine(str+" " + o);
            ForegroundColor = aux;
        }
    }
}

