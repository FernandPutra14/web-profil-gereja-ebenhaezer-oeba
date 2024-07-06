using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PKMGerejaEbenhaezer.DataAccess
{
    public static class AssemblyReference
    {
        public static Assembly Assembly { get => typeof(AssemblyReference).Assembly; }
    }
}
