using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace CalculatorApp.Services
{
    public enum AppTheme { Light, Dark }

    public static class ThemeManager
    {
        private static readonly string ThemeFile = Path.Combine(AppContext.BaseDirectory, "AppData", "theme.json");
        public static AppTheme Current { get; private set; } = AppTheme.Light;

        public static void LoadTheme()
        {
            try
            {
                if (File.Exists(ThemeFile))
                {
                    var json = File.ReadAllText(ThemeFile);
                    var t = JsonSerializer.Deserialize<AppTheme>(json);
                    if (t != null) Current = t;
                }
            }
            catch { Current = AppTheme.Light; }
        }

        public static void SaveTheme(AppTheme theme)
        {
            Current = theme;
            Directory.CreateDirectory(Path.GetDirectoryName(ThemeFile)!);
            File.WriteAllText(ThemeFile, JsonSerializer.Serialize(Current));
        }

        public static void ApplyTheme(Control root)
        {
            Color back = Current == AppTheme.Dark ? Color.FromArgb(30, 30, 30) : Color.White;
            Color fore = Current == AppTheme.Dark ? Color.WhiteSmoke : Color.Black;
            if (!(root is Form))
            {
                root.BackColor = back;
                root.ForeColor = fore;
            }
            foreach (Control c in root.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Current == AppTheme.Dark ? Color.FromArgb(50, 50, 50) : Color.FromArgb(240, 240, 240);
                    btn.ForeColor = fore;
                }
                else
                {
                    c.BackColor = back;
                    c.ForeColor = fore;
                }
                ApplyTheme(c);
            }
            if (root is Form f)
            {
                f.BackColor = back;
                f.ForeColor = fore;
            }
        }
    }
}
