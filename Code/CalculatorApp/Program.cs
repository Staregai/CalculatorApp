using System;
using System.IO;
using System.Windows.Forms;
using CalculatorApp.Services;
using CalculatorApp.Data;

namespace CalculatorApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var dbDir = Path.Combine(AppContext.BaseDirectory, "AppData");
            Directory.CreateDirectory(dbDir);

            using (var db = new AppDbContext()) db.Database.EnsureCreated();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ThemeManager.LoadTheme();
            Application.Run(new Forms.MainForm());
        }
    }
}
