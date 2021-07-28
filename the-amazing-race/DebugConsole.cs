using System;
using System.Diagnostics;

namespace the_amazing_race
{
    class DebugConsole
    {
        [Conditional("DEBUG")]
        public static void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
