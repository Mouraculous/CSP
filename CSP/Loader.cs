using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_1
{
    class Loader
    {
        public static Futoshiki LoadFutoshiki(string path)
        {
            var problem = File.ReadAllLines(path);
            var size = int.Parse(problem.First());
            var fields = problem.Skip(2).TakeWhile(w => !w.Contains("REL:"));
            var tiles = fields.Select(s => s.Split(';').Select(int.Parse).ToArray()).ToArray();
            var rels = problem.SkipWhile(s => !s.Contains("REL:")).Skip(1);
            return new Futoshiki (size)
            {
                Matrix = (int[][])tiles.Clone(),
                Relations = rels.ToArray()
            };
        }

        public static Skyscraper LoadSkyscrapers(string path)
        {
            var problem = File.ReadAllLines(path);
            var size = int.Parse(problem.First());
            var fields = problem.Skip(1);
            var vis = fields.Select(s => s.Split(';')).ToDictionary(k => k.First(), v => v.Skip(1).Select(int.Parse).ToArray());
            return new Skyscraper(size) {Visibility = vis};
        }
    }
}
