using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_1
{
    static class Config
    {
        public const string FutoshikiPath =
            @"C:\Users\Daniel\Desktop\Polibuda\Sem6\SI\CSP - lab2\Implementation\CSP\CSP\test\test_futo_6_0.txt";

        public const string SkyscraperPath =
            @"C:\Users\Daniel\Desktop\Polibuda\Sem6\SI\CSP - lab2\Implementation\CSP\CSP\test\test_sky_6_4.txt";


        public static Dictionary<char, int> IndexMap = new Dictionary<char, int>
        {
            {'A', 0 },
            {'B', 1 },
            {'C', 2 },
            {'D', 3 },
            {'E', 4 },
            {'F', 5 },
            {'G', 6 },
            {'H', 7 },
            {'I', 8 },
        };

        public static Dictionary<int, char> ReverseMap = new Dictionary<int, char>
        {
            {0, 'A'},
            {1, 'B'},
            {2, 'C'},
            {3, 'D'},
            {4, 'E'},
            {5, 'F'},
            {6, 'G'},
            {7, 'H'},
            {8, 'I'},
        };
    }
}
