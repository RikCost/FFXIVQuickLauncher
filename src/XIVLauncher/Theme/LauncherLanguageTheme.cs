using System;
using System.Collections.Generic;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using Serilog;
using XIVLauncher.Common;

namespace XIVLauncher.Theme
{
    public static class LauncherLanguageTheme
    {
        private sealed class ThemePalette
        {
            public ThemePalette(Color primary, Color secondary)
            {
                Primary = primary;
                Secondary = secondary;
            }

            public Color Primary { get; }
            public Color Secondary { get; }
        }

        private static readonly Dictionary<LauncherLanguage, ThemePalette> LanguagePalettes = new()
        {
            { LauncherLanguage.Japanese, new ThemePalette(Hex(0xFFFFFF), Hex(0xBC002D)) },          // Japan
            { LauncherLanguage.English, new ThemePalette(Hex(0x3C3B6E), Hex(0xB22234)) },           // USA
            { LauncherLanguage.German, new ThemePalette(Hex(0x000000), Hex(0xDD0000)) },            // Germany
            { LauncherLanguage.French, new ThemePalette(Hex(0x0055A4), Hex(0xEF4135)) },            // France
            { LauncherLanguage.Italian, new ThemePalette(Hex(0x009246), Hex(0xCE2B37)) },           // Italy
            { LauncherLanguage.Spanish, new ThemePalette(Hex(0xAA151B), Hex(0xF1BF00)) },           // Spain
            { LauncherLanguage.Portuguese, new ThemePalette(Hex(0x046A38), Hex(0xDA291C)) },        // Portugal
            { LauncherLanguage.Korean, new ThemePalette(Hex(0x003478), Hex(0xC60C30)) },            // South Korea
            { LauncherLanguage.Norwegian, new ThemePalette(Hex(0xBA0C2F), Hex(0x00205B)) },         // Norway
            { LauncherLanguage.Russian, new ThemePalette(Hex(0x0039A6), Hex(0xD52B1E)) },           // Russia
            { LauncherLanguage.SimplifiedChinese, new ThemePalette(Hex(0xDE2910), Hex(0xFFDE00)) }, // China
            { LauncherLanguage.TraditionalChinese, new ThemePalette(Hex(0xFE0000), Hex(0x000095)) },// Taiwan
            { LauncherLanguage.Swedish, new ThemePalette(Hex(0x006AA7), Hex(0xFECC00)) },           // Sweden
        };

        public static void Apply(LauncherLanguage? language)
        {
            var selectedLanguage = language ?? LauncherLanguage.English;
            var palette = LanguagePalettes.GetValueOrDefault(selectedLanguage, LanguagePalettes[LauncherLanguage.English]);

            try
            {
                var paletteHelper = new PaletteHelper();
                var theme = paletteHelper.GetTheme();

                theme.SetPrimaryColor(palette.Primary);
                theme.SetSecondaryColor(palette.Secondary);

                paletteHelper.SetTheme(theme);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Could not apply launcher language theme for {Language}", selectedLanguage);
            }
        }

        private static Color Hex(int value)
        {
            return Color.FromRgb(
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF));
        }
    }
}
