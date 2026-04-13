using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Serilog;
using XIVLauncher.Settings;

namespace XIVLauncher.Theme
{
    public static class LauncherThemeManager
    {
        public sealed class ThemeColorOption
        {
            public ThemeColorOption(string name, string hexColor)
            {
                Name = name;
                HexColor = hexColor;
            }

            public string Name { get; }
            public string HexColor { get; }
        }

        private const string DefaultPrimaryHex = "#2196F3";
        private const string DefaultSecondaryHex = "#03A9F4";

        private static readonly ReadOnlyCollection<ThemeColorOption> ThemeColorOptions = new(new[]
        {
            new ThemeColorOption("Blue", "#2196F3"),
            new ThemeColorOption("Light Blue", "#03A9F4"),
            new ThemeColorOption("Red", "#F44336"),
            new ThemeColorOption("Green", "#4CAF50"),
            new ThemeColorOption("Purple", "#9C27B0"),
            new ThemeColorOption("Orange", "#FF9800"),
            new ThemeColorOption("Pink", "#E91E63"),
            new ThemeColorOption("Teal", "#009688"),
            new ThemeColorOption("Indigo", "#3F51B5"),
            new ThemeColorOption("Cyan", "#00BCD4"),
            new ThemeColorOption("Amber", "#FFC107"),
        });

        public static ReadOnlyCollection<ThemeColorOption> GetThemeColorOptions() => ThemeColorOptions;

        public static void ApplyFromSettings(ILauncherSettingsV3 settings)
        {
            Apply(settings.LauncherThemePrimaryColor, settings.LauncherThemeSecondaryColor);
        }

        public static void Apply(string? primaryHex, string? secondaryHex)
        {
            try
            {
                var paletteHelper = new PaletteHelper();
                var theme = paletteHelper.GetTheme();

                theme.SetPrimaryColor(ParseColorOrDefault(primaryHex, DefaultPrimaryHex));
                theme.SetSecondaryColor(ParseColorOrDefault(secondaryHex, DefaultSecondaryHex));

                paletteHelper.SetTheme(theme);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Could not apply launcher theme");
            }
        }

        public static string GetDefaultPrimaryHex() => DefaultPrimaryHex;
        public static string GetDefaultSecondaryHex() => DefaultSecondaryHex;

        private static Color ParseColorOrDefault(string? rawColor, string fallbackColor)
        {
            if (!string.IsNullOrWhiteSpace(rawColor) &&
                ColorConverter.ConvertFromString(rawColor) is Color parsedColor)
            {
                return parsedColor;
            }

            return (Color)ColorConverter.ConvertFromString(fallbackColor);
        }
    }
}
