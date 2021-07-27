using System;
using System.Collections.Generic;
using System.IO;

namespace the_amazing_race_wei_fan
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                ReadInput(args[0]);
            }
            else
            {
                Console.WriteLine("Error: Expected a command line argument, for the input file.");
                Console.WriteLine("Remedy: Specify the input file from the command line,");
                Console.WriteLine("\tor just modify the Debug tab of the Project Properties page.");
            }

            Console.WriteLine("\nThe Amazing Race will now exit.");
            Console.WriteLine("Press any key to close the console.");
            Console.ReadKey();
        }

        static void ReadInput(string theFileName)
        {
            List<string> theInputLines = new List<string>();

            try
            {
                using (StreamReader theStreamReader = new StreamReader(theFileName))
                {
                    while (!theStreamReader.EndOfStream)
                    {
                        string Line = theStreamReader.ReadLine();
                        theInputLines.Add(Line);
                        Console.WriteLine(Line);
                    }
                }
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Remedy: verify the path, name, and existence of the file.");
            }

            ProcessInput(theInputLines);
        }

        static void ProcessInput(List<string> theInputLines)
        {
            bool isBrandonStyleInput = true;

            foreach (string inputLine in theInputLines)
            {
                if (inputLine.Contains(","))
                {
                    isBrandonStyleInput = false;
                }
            }

            if (isBrandonStyleInput)
            {
                ConvertBrandonStyleInput(theInputLines);
            }
            else
            {
                ProcessWeiStyleInput(theInputLines);
            }
        }

        static void ConvertBrandonStyleInput(List<string> theInputLines)
        {

        }

        static void ProcessWeiStyleInput(List<string> theInputLines)
        {

        }
    }
}
