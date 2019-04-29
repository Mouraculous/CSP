using System;
using System.Collections.Generic;
using System.Linq;


namespace CSP_1
{
    public class Skyscraper : ICspProblemo
    {
        public int Size { get; set; }

        public int[][] Matrix { get; set; }

        public Dictionary<string, int[]> Visibility { get; set; }

        private readonly int[] _universalDomain;

        public Dictionary<string, int[]> Domains= new Dictionary<string, int[]>();

        public Skyscraper(int size)
        {
            Size = size;
            var domain = new int[Size];

            for (var i = 0; i < Size; i++)
            {
                domain[i] = i + 1;
            }

            _universalDomain = (int[])domain.Clone();
            Matrix = new int[Size][];

            for (var i = 0; i < Size; i++)
            {
                Matrix[i] = new int[Size];
                for (var j = 0; j < Size; j++)
                {
                    Domains.Add($"{Config.ReverseMap[i]}{j}", (int[])domain.Clone());
                    Matrix[i][j] = 0;
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
                        var a = new[] { numUnassigned, row, col };
                        return a;
                    }
                }
            }
            var array = new[] { numUnassigned, -1, -1 };
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
                        var temp = 0;
                        foreach (var pair in Visibility)
                        {
                            if (pair.Key.Equals("P") || pair.Key.Equals("L") && pair.Value[col] != 0) temp++;
                            else if (pair.Key.Equals("G") || pair.Key.Equals("D") && pair.Value[row] != 0) temp++;
                        }
                        numUnassigned = 1;
                        var a = new[] { numUnassigned, row, col, temp};
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

            return CheckVisibility(value, rowIndex, columnIndex);
        }

        private bool CheckVisibility(int value, int rowIndex, int columnIndex)
        {
            var visible = new List<int>();
            var array = Visibility["L"];
            if (array[rowIndex] != 0)
            {
                for (var i = 0; i < Size; i++)
                {
                    //if (Matrix[rowIndex][i] == 0) return true;

                    if (Matrix[rowIndex][i] != 0 && (!visible.Any() || Matrix[rowIndex][i] > visible.Max()))
                        visible.Add(value);
                }

                if (visible.Count > array[rowIndex])
                {
                    return false;
                }
            }

            var visible2 = new List<int>();
            array = Visibility["P"];
            if (array[rowIndex] != 0)
            {
                for (var i = Size - 1; i > 0; i--)
                {
                    //if (Matrix[rowIndex][i] == 0) return true;

                    if (Matrix[rowIndex][i] != 0 && (!visible2.Any() || Matrix[rowIndex][i] > visible2.Max()))
                        visible2.Add(value);
                }

                if (visible2.Count > array[rowIndex])
                {
                    return false;
                }
            }

            var visible3 = new List<int>();
            array = Visibility["G"];
            if (array[columnIndex] != 0)
            {
                for (var i = 0; i < Size; i++)
                {
                    //if (Matrix[rowIndex][i] == 0) return true;

                    if (Matrix[i][columnIndex] != 0 && (!visible3.Any() || Matrix[i][columnIndex] > visible3.Max()))
                        visible3.Add(value);
                }

                if (visible3.Count > array[columnIndex])
                {
                    return false;
                }
            }

            var visible4 = new List<int>();
            array = Visibility["D"];
            if (array[columnIndex] != 0)
            {
                for (var i = Size - 1; i > 0; i--)
                {
                    //if (Matrix[rowIndex][i] == 0) return true;

                    if (Matrix[i][columnIndex] != 0 && (!visible4.Any() || Matrix[i][columnIndex] > visible4.Max()))
                        visible4.Add(value);
                }

                if (visible4.Count > array[columnIndex])
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
                var list = _universalDomain.Where(i => IsSafe(i, Config.IndexMap[keys[x][0]], int.Parse((keys[x][1] - '0').ToString()))).ToList();
                Domains[keys[x]] = list.ToArray();
            }
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
            for (var i = 1; i <= Size; i++)
            {
                if (IsSafe(i, row, col))
                {
                    Matrix[row][col] = i;

                    if (BacktrackingSolve())
                        return true;

                    Matrix[row][col] = 0;
                }
            }
            return false;
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
            UpdateDomains();
            for (var i = 1; i <= Size; i++)
            {
                Matrix[row][col] = i;

                if (ForwardcheckingSolve())
                    return true;

                Matrix[row][col] = 0;
            }

            return false;
        }
    }
}
