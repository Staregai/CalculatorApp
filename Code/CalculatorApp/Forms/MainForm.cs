using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CalculatorApp.Services;
using CalculatorApp.Data;
using CalculatorApp.Models;

namespace CalculatorApp.Forms
{
    public partial class MainForm : Form
    {
        private string _expr = "";
        private bool _scientific = false;
        private bool _resultJustShown = false;
        private string _lastResult = "";

        private readonly Dictionary<Keys, string> _keyMap = new()
        {
            [Keys.D0] = "0", [Keys.D1] = "1", [Keys.D2] = "2", [Keys.D3] = "3", [Keys.D4] = "4",
            [Keys.D5] = "5", [Keys.D6] = "6", [Keys.D7] = "7", [Keys.D8] = "8", [Keys.D9] = "9",
            [Keys.NumPad0] = "0", [Keys.NumPad1] = "1", [Keys.NumPad2] = "2", [Keys.NumPad3] = "3", [Keys.NumPad4] = "4",
            [Keys.NumPad5] = "5", [Keys.NumPad6] = "6", [Keys.NumPad7] = "7", [Keys.NumPad8] = "8", [Keys.NumPad9] = "9",
            [Keys.OemPeriod] = ".", [Keys.Decimal] = ".",
            [Keys.Add] = "+", [Keys.Oemplus] = "+",
            [Keys.Subtract] = "-", [Keys.OemMinus] = "-",
            [Keys.Multiply] = "*",
            [Keys.Divide] = "/", [Keys.OemQuestion] = "/",
            [Keys.Oem5] = "^",
            [Keys.Oem6] = ")",
            [Keys.OemOpenBrackets] = "(",
            [Keys.Back] = "⌫",
            [Keys.Escape] = "C",
            [Keys.Enter] = "=", [Keys.Return] = "="
        };

        private readonly Dictionary<(Keys, bool), string> _funcMap = new()
        {
            [(Keys.S, true)] = "sin(",
            [(Keys.C, true)] = "cos(",
            [(Keys.T, true)] = "tan(",
            [(Keys.L, true)] = "ln(",
            [(Keys.G, true)] = "log10(",
            [(Keys.P, true)] = "π",
            [(Keys.E, true)] = "e"
        };

        private readonly Dictionary<Keys, string> _shiftMap = new()
        {
            [Keys.D1] = "!",
            [Keys.D6] = "^",
            [Keys.D8] = "*",
            [Keys.D9] = "(",
            [Keys.D0] = ")"
        };

        private readonly Dictionary<Keys, string> _ctrlMap = new()
        {
            [Keys.D1] = "!",
            [Keys.D6] = "^",
            [Keys.D8] = "*",
            [Keys.D9] = "(",
            [Keys.D0] = ")"
        };

        public MainForm()
        {
            InitializeComponent();
            KeyPreview = true;
            KeyDown += MainForm_KeyDown;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            ApplyTabControlTheme();
            BuildGrid();
            UpdateThemeButtonText();

            txtDisplay.TabStop = false;
            if (cmbCode != null) cmbCode.SelectedIndex = 0;
            if (dtpFrom != null) dtpFrom.Value = DateTime.Today.AddDays(-14);
            if (dtpTo != null) dtpTo.Value = DateTime.Today;
            RefreshHistory();
        }

        private void BuildGrid()
        {
            panelGrid.Controls.Clear();
            var grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 5,
                Margin = new Padding(5),
                Padding = new Padding(5)
            };
            for (int i = 0; i < 4; i++)
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            for (int r = 0; r < 5; r++)
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            panelGrid.Controls.Add(grid);

            void Add(string text, Action onClick)
            {
                var b = new Button
                {
                    Text = text,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(1),
                    Font = new Font("Segoe UI", 8F),
                    MinimumSize = new Size(0, 0),
                    FlatStyle = FlatStyle.Flat
                };
                b.FlatAppearance.BorderSize = 1;

                b.FlatAppearance.BorderColor = ThemeManager.Current == AppTheme.Dark ? Color.WhiteSmoke : Color.DimGray;

                b.Click += (_, __) =>
                {
                    onClick();
                    txtDisplay.Focus();
                };
                grid.Controls.Add(b);
            }

            if (!_scientific)
            {
                string[] order = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", ".", "0", "=", "+", "(", ")", "C", "⌫" };
                foreach (var t in order) Add(t, () => HandleBasic(t));
            }
            else
            {
                Add("sin", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Sin); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"sin({input})", result); });
                Add("cos", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Cos); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"cos({input})", result); });
                Add("tan", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Tan); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"tan({input})", result); });
                Add("√", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Sqrt); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"sqrt({input})", result); });
                Add("^2", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, v => v * v); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"sqr({input})", result); });
                Add("1/x", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, v => 1.0 / v); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"1/({input})", result); });
                Add("ln", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Log); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"ln({input})", result); });
                Add("log10", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Log10); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"log10({input})", result); });
                Add("|x|", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Abs); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"abs({input})", result); });
                Add("π", () => Append(Math.PI.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Add("e", () => Append(Math.E.ToString(System.Globalization.CultureInfo.InvariantCulture)));
                Add("!", () => { var input = GetVal(); var result = Factorial(); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"{input}!", result); });
                Add("2^x", () => { var input = GetVal(); var result = Math.Pow(2, ToDouble(input)).ToString(System.Globalization.CultureInfo.InvariantCulture); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"2^{input}", result); });
                Add("x^y", () => Append("^"));
                Add("sin^-1", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Asin); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"asin({input})", result); });
                Add("cos^-1", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Acos); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"acos({input})", result); });
                Add("tan^-1", () => { var input = GetVal(); var result = CalculatorEngine.Unary(input, Math.Atan); txtDisplay.Text = result; _expr = result; _lastResult = result; SaveToHistory($"atan({input})", result); });
                Add("=", () => Eval());
                Add("C", () => { _expr = ""; txtDisplay.Text = ""; });
                Add("⌫", () => { if (_expr.Length > 0) _expr = _expr[..^1]; txtDisplay.Text = _expr; });
            }
        }

        private double ToDouble(string s) => double.TryParse(s.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var v) ? v : double.NaN;

        private string GetVal() => string.IsNullOrWhiteSpace(txtDisplay.Text) ? "0" : txtDisplay.Text;

        private void Append(string s)
        {
            _expr += s;
            txtDisplay.Text = _expr;
        }

        private void HandleBasic(string t)
        {
            switch (t)
            {
                case "C":
                    _expr = "";
                    txtDisplay.Text = "";
                    _resultJustShown = false;
                    break;
                case "⌫":
                    if (_expr.Length > 0) _expr = _expr[..^1];
                    txtDisplay.Text = _expr;
                    break;
                case "=":
                    Eval();
                    break;
                default:
                    if (_resultJustShown && "+-*/^".Contains(t))
                    {
                        _expr = _lastResult + t;
                        txtDisplay.Text = _expr;
                        _resultJustShown = false;
                    }
                    else
                    {
                        _expr += t;
                        txtDisplay.Text = _expr;
                    }
                    break;
            }
        }

        private void Eval()
        {
            string exprToEval = _expr
                .Replace("π", Math.PI.ToString(System.Globalization.CultureInfo.InvariantCulture))
                .Replace("e", Math.E.ToString(System.Globalization.CultureInfo.InvariantCulture));

            var res = CalculatorEngine.Evaluate(exprToEval);
            txtDisplay.Text = res;
            using (var db = new AppDbContext())
            {
                db.CalculationHistories.Add(new CalculationHistory { Expression = _expr, Result = res });
                db.SaveChanges();
            }
            RefreshHistory();
            _expr = res == "BŁĄD" ? "" : res;
            _resultJustShown = true;
        }

        private string Factorial()
        {
            if (!int.TryParse(GetVal(), out int n) || n < 0) return "BŁĄD";
            try
            {
                long f = 1;
                for (int i = 2; i <= n; i++) f *= i;
                return f.ToString();
            }
            catch { return "BŁĄD"; }
        }

        private void btnTheme_Click(object sender, EventArgs e)
        {
            var next = ThemeManager.Current == AppTheme.Light ? AppTheme.Dark : AppTheme.Light;
            ThemeManager.SaveTheme(next);
            ThemeManager.ApplyTheme(this);
            ApplyTabControlTheme();
            BuildGrid();
            UpdateThemeButtonText();
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            _scientific = !_scientific;
            btnMode.Text = _scientific ? "Tryb: Naukowy" : "Tryb: Prosty";
            BuildGrid();
        }

        private async void btnFetch_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            var svc = new CurrencyService(db);
            var code = (cmbCode.SelectedItem?.ToString() ?? "USD").ToUpperInvariant();
            try
            {
                btnFetch.Enabled = false;
                await svc.FetchAndStoreRatesAsync(code, dtpFrom.Value, dtpTo.Value);

                var rates = db.CurrencyRates
                    .Where(x => x.Code == code && x.EffectiveDate >= dtpFrom.Value.Date && x.EffectiveDate <= dtpTo.Value.Date)
                    .ToList();

                var min = rates.OrderBy(x => x.Mid).FirstOrDefault();
                var max = rates.OrderByDescending(x => x.Mid).FirstOrDefault();

                lblBest.Text =
                    (min == null && max == null)
                    ? "Brak danych"
                    : $"Optymalna cena zakupu waluty: {min?.EffectiveDate:dd.MM.yyyy}, kurs: {min?.Mid:F2}\noptymalna cena sprzedaży waluty: {max?.EffectiveDate:dd.MM.yyyy}, kurs: {max?.Mid:F2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd pobierania kursów: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFetch.Enabled = true;
            }
        }

        private async void btnConvertPln_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            var svc = new CurrencyService(db);
            var code = (cmbCode.SelectedItem?.ToString() ?? "USD").ToUpperInvariant();

            var rates = db.CurrencyRates
                .Where(x => x.Code == code && x.EffectiveDate >= dtpFrom.Value.Date && x.EffectiveDate <= dtpTo.Value.Date)
                .ToList();

            var min = rates.OrderBy(x => x.Mid).FirstOrDefault();

            if (min == null) { MessageBox.Show("Najpierw pobierz kursy dla zakresu dat.", "Info"); return; }
            var pln = numAmount.Value;
            var foreign = pln / min.Mid;
            lblBest.Text = $"Optymalna cena zakupu waluty: {min.EffectiveDate:dd.MM.yyyy}, kurs: {min.Mid:F2}\n{pln:F2} PLN => {foreign:F2} {code}";
            db.CurrencyConversionHistories.Add(new CurrencyConversionHistory { Operation = $"PLN->{code}", Currency = code, Rate = min.Mid, AmountInput = pln, AmountOutput = foreign });
            db.SaveChanges();
            RefreshHistory();
        }

        private async void btnConvertToPln_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            var svc = new CurrencyService(db);
            var code = (cmbCode.SelectedItem?.ToString() ?? "USD").ToUpperInvariant();

            var rates = db.CurrencyRates
                .Where(x => x.Code == code && x.EffectiveDate >= dtpFrom.Value.Date && x.EffectiveDate <= dtpTo.Value.Date)
                .ToList();

            var max = rates.OrderByDescending(x => x.Mid).FirstOrDefault();

            if (max == null) { MessageBox.Show("Najpierw pobierz kursy dla zakresu dat.", "Info"); return; }
            var foreign = numAmount.Value;
            var pln = foreign * max.Mid;
            lblBest.Text = $"Optymalna cena sprzedaży waluty: {max.EffectiveDate:dd.MM.yyyy}, kurs: {max.Mid:F2}\n{foreign:F2} {code} => {pln:F2} PLN";
            db.CurrencyConversionHistories.Add(new CurrencyConversionHistory { Operation = $"{code}->PLN", Currency = code, Rate = max.Mid, AmountInput = foreign, AmountOutput = pln });
            db.SaveChanges();
            RefreshHistory();
        }

        private void RefreshHistory()
        {
            using var db = new AppDbContext();
            var calcList = db.CalculationHistories.OrderByDescending(h => h.TimestampUtc).Take(20).ToList();
            lstHistory.Items.Clear();
            foreach (var h in calcList)
                lstHistory.Items.Add($"{h.TimestampUtc:u}  {h.Expression} = {h.Result}");

            var currList = db.CurrencyConversionHistories.OrderByDescending(h => h.TimestampUtc).Take(20).ToList();
            lstCurrencyHistory.Items.Clear();
            foreach (var h in currList)
                lstCurrencyHistory.Items.Add($"{h.TimestampUtc:u}  {h.Operation} {h.AmountInput:F2} @ {h.Rate:F2} = {h.AmountOutput:F2}");
        }

        private void SaveToHistory(string expr, string result)
        {
            using var db = new AppDbContext();
            db.CalculationHistories.Add(new CalculationHistory { Expression = expr, Result = result });
            db.SaveChanges();
            RefreshHistory();
        }

        private void ApplyTabControlTheme()
        {
            if (ThemeManager.Current == AppTheme.Dark)
            {
                tabControlMain.BackColor = Color.FromArgb(30, 30, 30);
                tabControlMain.ForeColor = Color.White;
                foreach (TabPage tab in tabControlMain.TabPages)
                {
                    tab.BackColor = Color.FromArgb(30, 30, 30);
                    tab.ForeColor = Color.White;
                }
            }
            else
            {
                tabControlMain.BackColor = SystemColors.Control;
                tabControlMain.ForeColor = SystemColors.ControlText;
                foreach (TabPage tab in tabControlMain.TabPages)
                {
                    tab.BackColor = SystemColors.Control;
                    tab.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            using var db = new AppDbContext();
            db.CalculationHistories.RemoveRange(db.CalculationHistories);
            db.CurrencyConversionHistories.RemoveRange(db.CurrencyConversionHistories);
            db.SaveChanges();
            RefreshHistory();
        }

        private void UpdateThemeButtonText()
        {
            btnTheme.Text = ThemeManager.Current == AppTheme.Dark
                ? "Motyw: 🌙 Ciemny"
                : "Motyw: ☀️ Jasny";
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && _ctrlMap.TryGetValue(e.KeyCode, out var ctrlInput))
            {
                HandleInput(ctrlInput);
                e.Handled = true;
                return;
            }

            if (e.Shift && _shiftMap.TryGetValue(e.KeyCode, out var shiftInput))
            {
                HandleInput(shiftInput);
                e.Handled = true;
                return;
            }

            if (_funcMap.TryGetValue((e.KeyCode, e.Control), out var funcInput))
            {
                HandleInput(funcInput);
                e.Handled = true;
                return;
            }

            if (_keyMap.TryGetValue(e.KeyCode, out var input))
            {
                HandleInput(input);
                e.Handled = true;
                return;
            }
        }

        private void HandleInput(string input)
        {
            if (_resultJustShown && input.Length == 1 && "0123456789.eπ".Contains(input))
            {
                _expr = "";
                txtDisplay.Text = "";
                _resultJustShown = false;
            }

            if (input == "sin(" || input == "cos(" || input == "tan(" || input == "ln(")
            {
                _expr += input;
                txtDisplay.Text = _expr;
                _resultJustShown = false;
                return;
            }

            HandleBasic(input);
        }
    }
}
