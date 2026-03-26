using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pr4;

namespace UnitTestProject {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void Formula1Positive() {
            Assert.AreEqual(4.0426, Calc.Formula1(2, 1, 0), 0.001);
            Assert.AreEqual(3.0237, Calc.Formula1(2, 1, 1), 0.001);
            Assert.AreEqual(2.8702, Calc.Formula1(1, 2, 1), 0.001);
        }

        [TestMethod]
        public void Formula1Negative() {
            Assert.ThrowsException<DivideByZeroException>(() => Calc.Formula1(1, 1, -1));
            Assert.ThrowsException<DivideByZeroException>(() => Calc.Formula1(0, 1, 0));
            Assert.ThrowsException<DivideByZeroException>(() => Calc.Formula1(5, 1, -5));
        }

        [TestMethod]
        public void Formula2Positive() {
            Assert.AreEqual(25.0, Calc.Formula2(2, 2, 3), 0.001);
            Assert.AreEqual(0.0, Calc.Formula2(5, 0, 0), 0.001);
            Assert.AreEqual(1.0, Calc.Formula2(-5, -2, 0), 0.001);
        }

        [TestMethod]
        public void Formula2Negative() {
            Assert.ThrowsException<ArgumentException>(() => Calc.Formula2(2, 2, double.NaN));
            Assert.ThrowsException<ArgumentException>(() => Calc.Formula2(5, 0, double.NaN));
            Assert.ThrowsException<ArgumentException>(() => Calc.Formula2(-5, -2, double.PositiveInfinity));
        }

        [TestMethod]
        public void Formula3Positive() {
            Assert.AreEqual(0.8405, Calc.Formula3(0), 0.001);
            Assert.AreEqual(9.8498, Calc.Formula3(1), 0.001);
            Assert.AreEqual(144.8589, Calc.Formula3(2), 0.001);
        }

        [TestMethod]
        public void Formula3Negative() {
            Assert.AreEqual(double.NaN, Calc.Formula3(double.NaN));
            Assert.AreEqual(double.NaN, Calc.Formula3(double.PositiveInfinity));
            Assert.AreEqual(double.NaN, Calc.Formula3(double.NegativeInfinity));
        }
    }
}