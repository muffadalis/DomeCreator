using System.Windows.Media;

namespace DomeCreator.Core
{
    public class PartColor
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public Brush Color { get; set; }
        public Color EdgeColor { get; set; }
        public byte Alpha { get; set; }
        public byte Luminance { get; set; }
        public PartTextures Texture { get; set; }
        public string Tooltip { get; set; }

        public override string ToString() {
            return string.Format("0 !COLOUR {0} CODE {1} VALUE {2} EDGE {3} ALPHA {4} LUMINANCE {5}",
                Color, Code, Name, EdgeColor, Alpha, Luminance);
        }
    }
}