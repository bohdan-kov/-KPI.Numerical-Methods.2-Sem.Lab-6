using System;
using System.Linq;

namespace Lab_6
{
    internal class LagrangeMethod
    {
        public double[] _x_i { get; private set; }
        public double[] _y_i { get; private set; }
        public LagrangeMethod(double[] x_i, double[] y_i)
        {
            _x_i = x_i;
            _y_i = y_i;
        }
        public string LagrangeView()
        {
            int n = _x_i.Length;
            string resLagrange = "Отриманий палiном лагранджа: ";


            for (int i = 0; i < n; i++)
            {
                string numeratorLagrange = ""; //Записа чисельника
                string denomLagrange = ""; //Записати знаменник 
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        string sign = _x_i[j] <= 0 ? " - " : " - ";
                        numeratorLagrange += $"(x{sign}{_x_i[j]})";

                        sign = _x_i[j] <= 0 ? " - " : " - ";
                        denomLagrange += $"({_x_i[i]}{sign}{_x_i[j]})";

                    }
                }
                resLagrange += $"\n\t\t\t{numeratorLagrange}\n{_y_i[i]}   *\t" +
                    $"{string.Concat(Enumerable.Repeat("-", 25))} +" +
                    $"\n\t\t\t{denomLagrange}\n\n";
            }
            return resLagrange;
        }
    }
}