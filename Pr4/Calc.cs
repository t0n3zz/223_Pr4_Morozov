using SharpDX.Direct3D9;
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
        public static double Formula2(double x, double m, double w) {
            double result;
            double epsilon = 1e-6;

            if (Math.Abs(x - m) < epsilon) {
                result = Math.Pow(w + m, 2);
            } else if (-1 < m && m < x) {
                result = Math.Sin(5 * w + 3 * m * Math.Abs(w));
            } else {
                result = Math.Cos(3 * w + 5 * m * Math.Abs(w));
            }

            if (double.IsInfinity(result) || double.IsNaN(result))
                throw new ArgumentException("The calculation resulted in an undefined value.");
            return result;
        }
        public static double Formula3(double x) {
            double term1 = 9 * Math.Pow(x, 4);
            double angleInDegrees = 57.2 + x;
            double angleInRadians = angleInDegrees * Math.PI / 180.0;
            double term2 = Math.Sin(angleInRadians);

            return term1 + term2;
        }
    }
}
