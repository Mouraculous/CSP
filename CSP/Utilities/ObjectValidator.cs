using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP.Utilities
{
    public class ObjectValidator
    {
        public static void IfNullThrowException(object obj, string name)
        {
            if(obj is null) throw new ArgumentNullException($"Teh argument for {name} you provided was null");
        }
    }
}
