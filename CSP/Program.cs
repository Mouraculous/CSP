using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_1
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var stop = new Stopwatch();
            stop.Start();
            var futo = Loader.LoadFutoshiki(Config.FutoshikiPath);
            Console.WriteLine("BACKTRACKING:\n");
            if (futo.BacktrackingSolve())
                futo.Print();
            else
                Console.WriteLine("No solution");
            Console.WriteLine(stop.ElapsedMilliseconds);
            stop.Restart();
            futo = Loader.LoadFutoshiki(Config.FutoshikiPath);
            Console.WriteLine("\n\nFORWARD CHECKING:\n");
            if (futo.ForwardcheckingSolve())
                futo.Print();
            else
                Console.WriteLine("No solution");
            Console.WriteLine(stop.ElapsedMilliseconds);

            //var sky = Loader.LoadSkyscrapers(Config.SkyscraperPath);
            //Console.WriteLine("BACKTRACKING:\n");
            //if (sky.BacktrackingSolve())
            //    sky.Print();
            //else
            //    Console.WriteLine("No solution");

            //Console.WriteLine("\n\nFORWARD CHECKING:\n");
            //if (sky.BacktrackingSolve())
            //    sky.Print();
            //else
            //    Console.WriteLine("No solution");
            Console.Read();
        }
    }
}
