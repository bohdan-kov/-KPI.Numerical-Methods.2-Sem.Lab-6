using System;
using System.Linq;

namespace Lab_6
{
    internal class Program
    {
        static void ViewInfo(double[] x_i, double[] y_i)
        {
            Console.WriteLine("Значення функцiї в узлах iнтерполяцiї");

            for (int i = 0; i < x_i.Length; i++)
            {
                Console.WriteLine($"X[{i}]: {x_i[i]}\nY[{i}]: {y_i[i]}");
                PrintSep();
            }
        }
        static double calc_fun(double argument)
        {
            double result_fun = 0.5 * argument * Math.Cos(argument);
            return result_fun;
        }
        static void PrintSep()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));

        }

        static void Main(string[] args)
        {
            PrintSep();
            Console.WriteLine("\tЛабораторна робота #6");
            Console.WriteLine("Виконав студент групи IC-31 Коваль Богдан");
            PrintSep();


            double[] x_i = new double[] {-6, -4, -2, 0, 2};

            int x_len = x_i.Length;

            double[] y_i = new double[x_len];

            //Заповнюємо значення функції в точках x_i
            for (int i = 0; i < x_len; i++)
                {
                    y_i[i] = calc_fun(x_i[i]);
                }

            //Перегляд значень у вузлах iнтерполяцiї
            ViewInfo(x_i, y_i);

            //Алгоритм Лагранджа
            LagrangeMethod lagra = new LagrangeMethod(x_i, y_i);

            Console.WriteLine(lagra.LagrangeView());


            //Кубічна сплайн-інтерполяція
            SplineInterpolation spline = new SplineInterpolation(x_i, y_i);


            Console.WriteLine(spline.processingSplineCoef());
        }
    }
}