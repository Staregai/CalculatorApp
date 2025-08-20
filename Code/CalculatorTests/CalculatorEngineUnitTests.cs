using CalculatorApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator_tests
{
    [TestClass]
    public class CalculatorEngineUnitTests
    {
        [TestMethod]
        [DataRow("2+2", "4")]
        [DataRow("2-2", "0")]
        [DataRow("2*3", "6")]
        [DataRow("6/2", "3")]
        [DataRow("2^3", "8")]
        [DataRow("2^3!", "64")]
        [DataRow("3!", "6")]
        [DataRow("0!", "1")]
        [DataRow("1+2*3", "7")]
        [DataRow("2*(3+4)", "14")]
        [DataRow("sin(0)", "0")]
        [DataRow("cos(0)", "1")]
        [DataRow("tan(0)", "0")]
        [DataRow("ln(1)", "0")]
        [DataRow("log10(100)", "2")]
        [DataRow("abs(-5)", "5")]
        [DataRow("2^0", "1")]
        [DataRow("2^3!+1", "65")]
        [DataRow("3!^2", "36")]
        [DataRow("2^3^2", "512")] 
        public void Evaluate_ValidExpressions_ReturnsExpected(string expr, string expected)
        {
            var result = CalculatorEngine.Evaluate(expr);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void Evaluate_EmptyOrNull_ReturnsZero(string expr)
        {
            var result = CalculatorEngine.Evaluate(expr);
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        [DataRow("2/0")]
        [DataRow("sqrt(-1)")]
        [DataRow("abc")]
        [DataRow("3.5!")]
        [DataRow("(-1)!")]
        public void Evaluate_InvalidExpressions_ReturnsError(string expr)
        {
            var result = CalculatorEngine.Evaluate(expr);
            Assert.AreEqual("BŁĄD", result);
        }

        [TestMethod]
        public void Unary_Sin_ReturnsCorrect()
        {
            var result = CalculatorEngine.Unary("0", Math.Sin);
            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void Unary_InvalidInput_ReturnsError()
        {
            var result = CalculatorEngine.Unary("abc", Math.Sin);
            Assert.AreEqual("BŁĄD", result);
        }
    }
}