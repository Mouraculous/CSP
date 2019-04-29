using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSP_1
{
    public class Futoshiki : ICspProblemo
    {
        public int returns = 0;
        public int visited = 0;

        public int Size { get; set; }

        public int[][] Matrix { get; set; }

        public string[] Relations { get; set; }

        private int[] UniversalDomain;

        public Dictionary<string, int[]> Domains = new Dictionary<string, int[]>();

        public Futoshiki(int size)
        {
            Size = size;
            var domain = new int[Size];

            for (var i = 0; i < Size; i++)
            {
                domain[i] = i + 1;
            }

            UniversalDomain = (int[])domain.Clone();

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    Domains.Add($"{Config.ReverseMap[i]}{j}", (int[])domain.Clone());
                }
            }
        }

        public void Print()
        {
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    Console.Write(Matrix[i][j] + "\t");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("Returns: " + returns);
            Console.WriteLine("Nodes visited: " + visited);
        }

        private int Get(string coord)
        {
            var x = Config.IndexMap[coord[0]];
            var y = int.Parse((coord[1] - '1').ToString());
            return Matrix[x][y];
        }

        private int[] NumberUnassigned(int row, int col)
        {
            var numUnassigned = 0;
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (Matrix[i][j] == 0)
                    {
                        row = i;
                        col = j;

                        numUnassigned = 1;
                        var a = new []{ numUnassigned, row, col };
                        return a;
                    }
                }
            }
            var array = new [] { numUnassigned, -1, -1 };
            return array;
        }

        private int[] NumberUnassignedMinDomain(int row, int col)
        {
            var numUnassigned = 0;
            var list = new List<int[]>();
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (Matrix[i][j] == 0)
                    {
                        row = i;
                        col = j;

                        numUnassigned = 1;
                        var a = new[] { numUnassigned, row, col };
                        list.Add(a);
                    }
                }
            }

            if (list.Any())
            {
                return list.OrderBy(o => Domains[$"{Config.ReverseMap[o[1]]}{o[2]}"].Length).First();
            }
            var array = new[] { numUnassigned, -1, -1 };
            return array;
        }

        private int[] NumberUnassignedMinDependence(int row, int col)
        {
            var numUnassigned = 0;
            var list = new List<int[]>();
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    if (Matrix[i][j] == 0)
                    {
                        row = i;
                        col = j;
                        var regex = new Regex($"{Config.ReverseMap[row]}{col + 1}");
                        var rels = Relations.Where(s => regex.IsMatch(s)).ToList();

                        numUnassigned = 1;
                        var a = new[] { numUnassigned, row, col, rels.Count };
                        list.Add(a);
                    }
                }
            }

            if (list.Any())
            {
                return list
                    .OrderBy(o => o[3])
                    .First()
                    .Take(3)
                    .ToArray();
            }
            var array = new[] { numUnassigned, -1, -1 };
            return array;
        }

        private bool IsSafe(int value, int rowIndex, int columnIndex)
        {
            for (var i = 0; i < Size; i++)
            {
                if (Matrix[rowIndex][i] == value)
                    return false;
            }

            for (var i = 0; i < Size; i++)
            {
                if (Matrix[i][columnIndex] == value)
                    return false;
            }

            var coord = $"{Config.ReverseMap[rowIndex]}{columnIndex + 1}";
            var xd = new Regex(coord);
            var rels = Relations.Where(s => xd.IsMatch(s)).ToList();
            foreach (var rel in rels)
            {
                var splittedRel = rel.Split(';').ToArray();
                var get1 = coord.Equals(splittedRel[0]) ? value : Get(splittedRel[0]);
                var get2 = coord.Equals(splittedRel[1]) ? value : Get(splittedRel[1]);
                if (get1 > get2 && get1 != 0 && get2 != 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void UpdateDomains()
        {
            var keys = Domains.Keys.ToArray();
            for (var x = 0; x < keys.Length; x++)
            {
                var list = UniversalDomain.Where(i => IsSafe(i, Config.IndexMap[keys[x][0]], int.Parse((keys[x][1] - '0').ToString()))).ToList();
                Domains[keys[x]] = list.ToArray();
            }
        }
        
        public bool ForwardcheckingSolve()
        {
            var row = 0;
            var col = 0;
            var a = NumberUnassigned(row, col);
            //var a = NumberUnassignedMinDomain(row, col);
            //var a = NumberUnassignedMinDependence(row, col);

            if (a[0] == 0)
                return true;

            row = a[1];
            col = a[2];
            for (var i = 1; i <= Size; i++)
            {
                visited++;
                if (IsSafe(i, row, col))
                {
                    Matrix[row][col] = i;

                    if (ForwardcheckingSolve())
                        return true;

                    Matrix[row][col] = 0;
                }
            }

            returns++;
            return false;
        }

        public bool BacktrackingSolve()
        {
            var row = 0;
            var col = 0;
            var a = NumberUnassigned(row, col);
            //var a = NumberUnassignedMinDomain(row, col);
            //var a = NumberUnassignedMinDependence(row, col);

            if (a[0] == 0)
                return true;

            row = a[1];
            col = a[2];
            UpdateDomains();
            foreach (var i in Domains[$"{Config.ReverseMap[row]}{col}"])
            {
                visited++;
                Matrix[row][col] = i;

                if (BacktrackingSolve())
                    return true;

                Matrix[row][col] = 0;
                returns++;
            }

            return false;
        }
    }
}
