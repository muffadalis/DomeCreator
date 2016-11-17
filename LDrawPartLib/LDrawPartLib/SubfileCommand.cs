using System;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;

namespace LDrawPartLib
{
    public class SubfileCommand : CommandLine
    {
        public Matrix3D Matrix { get; set; }
        public string FileName { get; set; }

        private SubfileCommand() { }

        public SubfileCommand(string line) : base(line) { }

        protected override void SetValues() {
            base.SetValues();

            MatchCollection values = WordsEx.Matches(Line);

            FileName =  values[values.Count - 1].Value;

            Matrix = new Matrix3D(Convert.ToDouble(values[5].Value), Convert.ToDouble(values[8].Value), Convert.ToDouble(values[11].Value), 0,
                                   Convert.ToDouble(values[6].Value), Convert.ToDouble(values[9].Value), Convert.ToDouble(values[12].Value), 0,
                                   Convert.ToDouble(values[7].Value), Convert.ToDouble(values[10].Value), Convert.ToDouble(values[13].Value), 0,
                                   Convert.ToDouble(values[2].Value), Convert.ToDouble(values[3].Value), Convert.ToDouble(values[4].Value), 1);
        }
    }
}
