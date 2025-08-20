using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CalculatorApp.Services
{
    public static class CalculatorEngine
    {
        private static readonly Dictionary<string, (int precedence, bool rightAssoc)> Operators = new()
        {
            { "+", (1, false) },
            { "-", (1, false) },
            { "*", (2, false) },
            { "/", (2, false) },
            { "^", (3, true) },
            { "!", (4, false) }
        };

        private static readonly HashSet<string> Functions = new()
        {
            "sin", "cos", "tan", "ln", "log10", "abs"
        };

        public static string Evaluate(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression)) return "0";
                expression = expression.Replace(',', '.').Trim();

                if (!Regex.IsMatch(expression, @"^[0-9\.\+\-\*/\^\(\)!a-zA-Z]+$"))
                    return "BŁĄD";

                expression = InsertImplicitMultiplication(expression);
                expression = Regex.Replace(expression, @"(?<=\()\-", "0-");
                expression = Regex.Replace(expression, @"^\-", "0-");

                var tokens = Tokenize(expression);
                tokens = CleanFunctionMultiplication(tokens);
                var rpn = ToRpn(tokens);
                var result = EvalRpn(rpn);
                if (double.IsInfinity(result) || double.IsNaN(result))
                    return "BŁĄD";
                return result.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
                return "BŁĄD";
            }
        }

        private static string InsertImplicitMultiplication(string expr)
        {
            string pattern = @"(?<=[0-9\)])\s*\((?=(?![a-zA-Z]+\())";
            return Regex.Replace(expr, pattern, "*(");
        }

        private static List<string> Tokenize(string expr)
        {
            var pattern = @"(\d+(\.\d+)?)|([a-zA-Z_][a-zA-Z0-9_]*)|[\+\-\*/\^!(),]";
            var matches = Regex.Matches(expr, pattern);
            var tokens = new List<string>();
            foreach (Match m in matches)
                tokens.Add(m.Value);
            return tokens;
        }

        private static List<string> ToRpn(List<string> tokens)
        {
            var output = new List<string>();
            var stack = new Stack<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                string token = tokens[i];
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    output.Add(token);
                }
                else if (Functions.Contains(token))
                {
                    stack.Push(token);
                }
                else if (Operators.ContainsKey(token))
                {
                    while (stack.Count > 0 && Operators.ContainsKey(stack.Peek()))
                    {
                        var op1 = Operators[token];
                        var op2 = Operators[stack.Peek()];
                        if ((op1.rightAssoc && op1.precedence < op2.precedence) ||
                            (!op1.rightAssoc && op1.precedence <= op2.precedence))
                        {
                            output.Add(stack.Pop());
                        }
                        else break;
                    }
                    stack.Push(token);
                }
                else if (token == "(")
                {
                    stack.Push(token);
                }
                else if (token == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                        output.Add(stack.Pop());
                    if (stack.Count == 0) throw new Exception("Mismatched parentheses");
                    stack.Pop();
                    while (stack.Count > 0 && Functions.Contains(stack.Peek()))
                        output.Add(stack.Pop());
                }
                else
                {
                    throw new Exception("Invalid token");
                }
            }
            while (stack.Count > 0)
            {
                var t = stack.Pop();
                if (t == "(" || t == ")") throw new Exception("Mismatched parentheses");
                output.Add(t);
            }
            return output;
        }

        private static double EvalRpn(List<string> rpn)
        {
            var stack = new Stack<double>();
            foreach (var token in rpn)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out double num))
                {
                    stack.Push(num);
                }
                else if (Functions.Contains(token))
                {
                    if (stack.Count < 1) throw new Exception("Insufficient operands");
                    double arg = stack.Pop();
                    double res = token switch
                    {
                        "sin" => Math.Sin(arg),
                        "cos" => Math.Cos(arg),
                        "tan" => Math.Tan(arg),
                        "ln" => Math.Log(arg),
                        "log10" => Math.Log10(arg),
                        "abs" => Math.Abs(arg),
                        _ => throw new Exception("Unknown function")
                    };
                    stack.Push(res);
                }
                else if (Operators.ContainsKey(token))
                {
                    if (token == "!")
                    {
                        if (stack.Count < 1) throw new Exception("Insufficient operands");
                        double arg = stack.Pop();
                        if (arg < 0 || arg % 1 != 0)
                            throw new Exception("Invalid factorial");
                        long n = (long)arg;
                        long f = 1;
                        for (long i = 2; i <= n; i++) f *= i;
                        stack.Push(f);
                    }
                    else
                    {
                        if (stack.Count < 2) throw new Exception("Insufficient operands");
                        double b = stack.Pop();
                        double a = stack.Pop();
                        double res = token switch
                        {
                            "+" => a + b,
                            "-" => a - b,
                            "*" => a * b,
                            "/" => b == 0 ? throw new Exception("Div by zero") : a / b,
                            "^" => Math.Pow(a, b),
                            _ => throw new Exception("Unknown operator")
                        };
                        stack.Push(res);
                    }
                }
                else
                {
                    throw new Exception("Invalid token in RPN");
                }
            }
            if (stack.Count != 1) throw new Exception("Invalid expression");
            return stack.Pop();
        }

        public static string Unary(string current, Func<double, double> op)
        {
            if (!double.TryParse(current.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out double v))
                return "BŁĄD";
            double r = op(v);
            return r.ToString(CultureInfo.InvariantCulture);
        }

        private static List<string> CleanFunctionMultiplication(List<string> tokens)
        {
            var cleaned = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                cleaned.Add(tokens[i]);
                if (Functions.Contains(tokens[i]) &&
                    i + 2 < tokens.Count &&
                    tokens[i + 1] == "*" &&
                    tokens[i + 2] == "(")
                {
                    i++;
                }
            }
            return cleaned;
        }
    }
}
