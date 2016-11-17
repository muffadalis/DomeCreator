using System;
using System.Text.RegularExpressions;

namespace DomeCreator.Core
{
    public class MetaCommand : CommandLine
    {
        string[] _commands = new string[4];

        public MetaCommand(string line) : base(line) { }

        public MetaCommandType Type { get; set; }
        public string[] Commands {
            get {
                return _commands;
            }
        }

        protected override void SetValues() {
            base.SetValues();

            MatchCollection commands = WordsEx.Matches(Line);

            if (commands.Count > 2) {
                if (commands[1].Value == "BFC") {
                    this.Type = MetaCommandType.BFC;
                    ParseBFC(commands);
                }
            }
        }

        private void ParseBFC(MatchCollection commands) {
            for (int i = 0; i < commands.Count; i++) {
                string command = commands[i].Value;

                switch (command) {
                    case "CERTIFY":
                    case "NOCERTIFY":
                        _commands[0] = command;
                        break;
                    case "CW":
                        _commands[1] = Winding.CW.ToString();
                        break;
                    case "CCW":
                        _commands[1] = Winding.CCW.ToString();
                        break;
                    case "INVERTNEXT":
                        _commands[3] = Boolean.TrueString;
                        break;
                }
            }
        }
    }
}