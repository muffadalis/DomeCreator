using System;
using System.Text.RegularExpressions;
using System.Windows.Media.Media3D;

namespace DomeCreator.Core
{
    public class QuadrilateralCommand : CommandLine
    {
        public Point3D [] Points { get; set; }

        public QuadrilateralCommand(string line) : base(line) { }

        protected override void SetValues() {
            base.SetValues();
            Points = new Point3D [4];

            MatchCollection values = WordsEx.Matches(Line);

            Points[0] = new Point3D(
                    Convert.ToDouble(values[2].Value),
                    Convert.ToDouble(values[3].Value),
                    Convert.ToDouble(values[4].Value));

            Points[1] = new Point3D(
                    Convert.ToDouble(values[5].Value),
                    Convert.ToDouble(values[6].Value),
                    Convert.ToDouble(values[7].Value));

            Points[2] = new Point3D(
                    Convert.ToDouble(values[8].Value),
                    Convert.ToDouble(values[9].Value),
                    Convert.ToDouble(values[10].Value));
            Points[3] = new Point3D(
                    Convert.ToDouble(values[11].Value),
                    Convert.ToDouble(values[12].Value),
                    Convert.ToDouble(values[13].Value));
        }
    }
}