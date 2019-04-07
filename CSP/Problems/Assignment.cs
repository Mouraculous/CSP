using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Problems
{
    public class Assignment
    {
        public Dictionary<Variable, int> Assignments { get; set; }
        public Dictionary<Variable, List<int>> Domain { get; set; }

        static Assignment Blank()
        {
            var blank = new Assignment
            {
                Assignments = new Dictionary<Variable, int>(),
                Domain = new Dictionary<Variable, List<int>>()
            };
            return blank;
        }


        public Assignment Assign(Variable v, int val)
        {
            var n = new Assignment
            {
                Assignments = new Dictionary<Variable, int>(Assignments) {{v, val}},
                Domain = new Dictionary<Variable, List<int>>(Domain)
            };

            // Restrict the domain to only a single value
            var varDomain = new List<int> {val};
            n.RestrictDomain(v, varDomain);

            return n;
        }

        public int GetValue(Variable v) => Assignments[v];
        
        public void RestrictDomain(Variable v, List<int> dom) 
            => Domain.Add(v, dom);
        
        public List<int> GetDomain(Variable v) => Domain[v];
        
        public int Size => Assignments.Count;
    }
}
