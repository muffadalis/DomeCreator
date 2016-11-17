using System.Collections.Generic;

namespace DomeCreator.Core
{
    public class Category
    {
        public string Name { get; set; }
        public string Expression { get; private set; }
        public List<Part> Parts { get; set; }
        public string Status { get; set; }

        public Category(string name, string expression) {
            Name = name;
            Expression = expression.Trim();
            Status = string.Empty;
        }
    }
}
