using System;

#pragma warning disable CS0162 // Unreachable code detected
namespace the_amazing_race
{
    class DebugConsole
    {
        private const bool isEnabled = false;

        public static void WriteLine(string line)
        {
            if (isEnabled)
            {
                Console.WriteLine(line);
            }
        }
    }
}
#pragma warning restore CS0162 // Unreachable code detected
