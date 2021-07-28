using System;

namespace the_amazing_race
{
    class DebugConsole
    {
        private static bool isEnabled = true;

        public static void WriteLine(string line)
        {
            if (isEnabled)
            {
                Console.WriteLine(line);
            }
        }
    }
}
