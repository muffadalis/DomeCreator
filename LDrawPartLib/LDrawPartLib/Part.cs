using System;
using System.Windows.Media.Media3D;

namespace LDrawPartLib
{
	[Serializable]
	public class Part
	{
		public Guid Id { get; set; }
		public Guid ParentId { get; set; }
		public bool IsGroup { get; set; }
		public string GroupName { get; set; }
		public string Number { get; set; }
		public string Description { get; set; }
		public PartColors PartColor { get; set; }
		public Point3D Position { get; set; }

		public Part(string number, string description) {
			Number = number.Trim();
			Description = description.Trim();
		}
	}
}