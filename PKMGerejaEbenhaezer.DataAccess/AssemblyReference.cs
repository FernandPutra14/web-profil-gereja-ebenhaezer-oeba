using System.Reflection;

namespace PKMGerejaEbenhaezer.DataAccess
{
    public static class AssemblyReference
    {
        public static Assembly Assembly { get => typeof(AssemblyReference).Assembly; }
    }
}
