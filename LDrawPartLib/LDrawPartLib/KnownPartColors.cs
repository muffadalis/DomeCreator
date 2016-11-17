using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.IO;

namespace LDrawPartLib
{
    public static class KnownPartColors
    {
        public static PartColor GetColor(PartColors color) {
            PartColor c;
            try {
                c = Colors[(int) color];
            }
            catch (Exception) {
                c = GetColor(PartColors.Black);
            }

            return c;
        }

        public static Dictionary<int, PartColor> Colors { get; set; }

        private static Regex _wordsEx = new Regex(
                            @"\S+",
                            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        static KnownPartColors() {

            if (Colors != null) return;

            string[] lines = File.ReadAllLines(LDrawHelper.LDrawConfigLocation);

            Colors = new Dictionary<int, PartColor>();

            foreach (string line in lines) {
                if (line.StartsWith("0 !COLOUR")) {
                    PartColor c = ParseColor(line);
                    Colors.Add(c.Code, c);
                }
            }
        }

        private static PartColor ParseColor(string line) {
            PartColor c = new PartColor();
            MatchCollection words = _wordsEx.Matches(line);

            for (int i = 1; i < words.Count; i += 2) {
                string keyword = words[i].Value.Trim();
                string value;

                switch (keyword) {
                    case "!COLOUR":
                        value = words[i + 1].Value.Trim().Replace("Trans", "Transparent");
                        value = value.Replace("_", " ");
                        c.Tooltip = value;
                        c.Name = value.Replace(" ", "");
                        break;
                    case "CODE":
                        value = words[i + 1].Value.Trim();
                        c.Code = Convert.ToInt32(value);
                        break;
                    case "VALUE":
                        value = words[i + 1].Value.Trim();
                        System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(value);
                        c.Color = new SolidColorBrush(Color.FromRgb(color.R, color.G, color.B));
                        break;
                    case "EDGE":
                        value = words[i + 1].Value.Trim();
                        System.Drawing.Color edgeColor = System.Drawing.ColorTranslator.FromHtml(value);
                        c.EdgeColor = Color.FromRgb(edgeColor.R, edgeColor.G, edgeColor.B);
                        break;
                    case "ALPHA":
                        value = words[i + 1].Value.Trim();
                        c.Alpha = Convert.ToByte(value);
                        c.Color.Opacity = c.Alpha / 255.0;
                        break;
                    case "LUMINANCE":
                        value = words[i + 1].Value.Trim();
                        c.Luminance = Convert.ToByte(value);
                        break;
                }
            }

            return c;
        }
    }
}