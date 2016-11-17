using System;
using System.Text.RegularExpressions;

namespace LDrawPartLib
{
    public abstract class CommandLine
    {
        protected static Regex WordsEx = new Regex(
                            @"\S+",
                            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public PartColors Color { get; set; }
        public string Line { get; private set; }

        protected CommandLine() { }

        protected CommandLine(string line) {
            line = line.Trim();
            if (line.Length == 0)
                return;

            Line = line;

            SetValues();
        }

        protected virtual void SetValues() {
            MatchCollection values = WordsEx.Matches(Line);

            if (Line[0] != '0') {
                // Set the line color
                Color = (PartColors)Enum.Parse(typeof(PartColors), values[1].Value);
            }
        }
    }
}