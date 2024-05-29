using System;

namespace Lab_6
{
    internal class SplineInterpolation
    {
        public double[] _x_i { get; private set; }
        public double[] _y_i { get; private set; }
        public SplineInterpolation(double[] x_i, double[] y_i)
        {
            _x_i = x_i;
            _y_i = y_i;
        }

        // Метод для обчислення коефiцiєнтiв c за допомогою методу прогону
        private double[] tridiagonaAlgo(double[,] matrix, double[] y)
        {
            int n = y.Length;

            // Масиви для коефiцiєнтiв прогону
            double[] p = new double[n];
            double[] q = new double[n];

            // Пряма прогонка
            p[1] = matrix[1, 2] / matrix[1, 1];
            q[1] = y[1] / matrix[1, 1];

            for (int i = 2; i < n - 1; i++)
            {
                double denom = matrix[i, i] - matrix[i, i - 1] * p[i - 1];
                p[i] = matrix[i, i + 1] / denom;
                q[i] = (y[i] - matrix[i, i - 1] * q[i - 1]) / denom;
            }

            // Знаходимо зворотнім ходом
            double[] c = new double[n];
            c[n - 1] = 0;
            for (int i = n - 2; i >= 0; i--)
            {
                c[i] = q[i] - p[i] * c[i + 1];
            }

            return c;
        }

        public string processingSplineCoef()
        {
            //Задаєм кiлькiсть наборiв сплайнiв
            int n = _x_i.Length;

            double[] a_i = new double[n];

            //Масив для зберігання довжини частинного відрізка сплайна. (Формула кін - початок)
            double[] h_i = new double[n - 1];


            //Допомiжний масив для зберiг. правої частини формули (8) 
            double[] tmpRight_i = new double[n - 1];

            double[] b_i = new double[n - 1];
            double[] d_i = new double[n - 1];


            //Коефiцiєнтиi a визначають з рiвностi (7)
            for (int i = 0; i < n; i++)
            {
                a_i[i] = _y_i[i];
            }

            //Визначемо довжину частинного вiдрузка h = x(кiнець вiд.) - x(початок вiдрiзка) 
            for (int i = 1; i < n; i++)
            {
                h_i[i - 1] = _x_i[i] - _x_i[i - 1];
            }

            //Заповнюєму змiну. Формула (8) Права частина.
            for (int i = 1; i < n - 1; i++)
            {
                tmpRight_i[i - 1] = (3 / h_i[i] * (a_i[i + 1] - a_i[i]) - 3 / h_i[i - 1] * (a_i[i] - a_i[i - 1]));
            }


            //Створюємо матрицю для зберiгання даних потрiбних для знаходження c_i
            double[,] matrix = new double[n, n];
            double[] y = new double[n];

            for (int i = 1; i < n - 1; i++)
            {
                //Обрахунок доданкiв лiвої частини формули (3)
                matrix[i, i - 1] = h_i[i - 1]; //Найперший iз доданкiв
                matrix[i, i] = 2 * (h_i[i - 1] + h_i[i]); //Другий iз доданкiв
                matrix[i, i + 1] = h_i[i]; //Третiй iз доданкiв
                y[i] = tmpRight_i[i - 1];
            }

            double[] c_i = tridiagonaAlgo(matrix, y);


            for (int i = 0; i < n - 1; i++)
            {
                //Знаходимо b_i за формулою (9)
                b_i[i] = (a_i[i + 1] - a_i[i]) / h_i[i] - h_i[i] * (c_i[i + 1] + 2 * c_i[i]) / 3;

                //Знаходимо c_i за формулою (10)
                d_i[i] = (c_i[i + 1] - c_i[i]) / (3 * h_i[i]);
            }

            string result = "";

            // Виводимо результати для кожного сплайну
            for (int i = 0; i < n - 1; i++)
            {
                result += $"Коефiцiєнти для сплайна на iнтервалi |{_x_i[i]}/{_x_i[i+1]}|\n" + 
                    $"a - {a_i[i]}\n" + 
                    $"b - {b_i[i]}\n" +
                    $"c - {c_i[i]}\n" +
                    $"d - {d_i[i]}\n";
            }

            //Вивід в одну строку масив 
            //var str_a = string.Join(" ", a_i);
            //var str_b = string.Join(" ", b_i);
            //var str_c = string.Join(" ", c_i);
            //var str_d = string.Join(" ", d_i);

            //Console.WriteLine($"\na = {str_a}\n\nb = {str_b}\n\nc = {str_c}\n\nd = {str_d}\n");

            return result;
        }
    }
}
