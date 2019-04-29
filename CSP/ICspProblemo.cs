using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_1
{
    interface ICspProblemo
    {
        bool BacktrackingSolve();
        bool ForwardcheckingSolve();
        void Print();
    }
}
