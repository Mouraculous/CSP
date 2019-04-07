using System.Collections.Generic;
using CSP.Interfaces;

namespace CSP.Problems
{
    public abstract class Board
    {
        public int Dimension { get; set; }
        public List<Variable> Tiles { get; set; }
        public List<int> Domain { get; set; }
        public List<IConstraint> Constraints { get; set; }
    }
}
