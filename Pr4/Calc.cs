using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr4 {
    public class Calc {
        public static double Formula1(double x, double y, double z) {
            double numerator = Math.Exp(Math.Abs(x - y)) *
                               Math.Pow(Math.Abs(x - y), x + y);

            double denominator = Math.Atan(x) + Math.Atan(z);
            if (denominator == 0) {
                throw new DivideByZeroException("Denominator cannot be zero.");
            }

            double firstPart = numerator / denominator;

            double secondPart = Math.Pow(
                x * x + Math.Pow(Math.Log(y), 2),
                1.0 / 3.0);

            double phi = firstPart + secondPart;
            return phi;
        }
    }
}
