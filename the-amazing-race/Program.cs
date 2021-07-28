using System;
using System.Collections.Generic;
using System.IO;

namespace the_amazing_race
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("\nThe Amazing Race has begun.");

            try
            {
                if (args.Length == 0)
                {
                    throw new Exception("Expected a single command line argument for the input file and if applicable, its path.");
                }

                string[] inputStrings = ReadInput(args[0]);

                Board board = CreateBoardFromInputOfUnknownFormat(inputStrings);
                board.IntroduceEachTileToImmediateNeighborsWhoAreNotItself();
                board.HaveEachTileCalculateItsNumberOfHopsToReachablyDistantNeighborsStartingWithItself();
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine("\n" + exception.Message);
            }

            Console.WriteLine("\nThe Amazing Race has ended.");
            Console.WriteLine("Press any key to close the console.");
            Console.ReadKey();
        }

        private static string[] ReadInput(string fileName)
        {
            List<string> inputStringList = new List<string>();

            try
            {
                using (StreamReader theStreamReader = new StreamReader(fileName))
                {
                    while (!theStreamReader.EndOfStream)
                    {
                        string Line = theStreamReader.ReadLine();
                        inputStringList.Add(Line);
                    }
                }
            }
            catch (FileNotFoundException exception)
            {
                throw new Exception("The specified input file, " + fileName + ", could not be found.", exception);
            }

            return inputStringList.ToArray();
        }

        private static Board CreateBoardFromInputOfUnknownFormat(string[] inputStrings)
        {
            string theFirstInputString = inputStrings[0].ToLower();
            bool isInputOfOriginalFormat = theFirstInputString.Contains("1");

            if (isInputOfOriginalFormat)
            {
                DebugConsole.WriteLine("\nAt first glance, the input file appears to be of the original format.");

                return CreateBoardFromInputOfOriginalFormat(inputStrings);
            }
            else
            {
                DebugConsole.WriteLine("\nAt first glance, the input file appears to be of the custom format.");

                return CreateBoardFromInputOfCustomFormat(inputStrings);
            }
        }

        private static Board CreateBoardFromInputOfOriginalFormat(string[] inputStrings)
        {
            TileSet tileSet = new TileSet(TileSetType.Square);

            Board board = new Board(tileSet);

            try
            {
                for (int y = 0; y < inputStrings.Length; y++)
                {
                    for (int x = 0; x < inputStrings[y].Length; x++)
                    {
                        char c = inputStrings[y][x];
                        bool allowsMovement = (c == '1');

                        Position position = new Position(x, y);
                        Tile tile = new Tile(position, allowsMovement);

                        board.AddTile(tile);
                    }
                }
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new Exception("The input data doesn't conform to the original format as expected.", exception);
            }

            return board;
        }

        private static Board CreateBoardFromInputOfCustomFormat(string[] inputStrings)
        {
            TileSetType tileSetType;

            switch (inputStrings[0].ToLower())
            {
                case "triangular":
                    tileSetType = TileSetType.Triangular;
                    break;

                case "square":
                    tileSetType = TileSetType.Square;
                    break;

                case "hexagonal":
                    tileSetType = TileSetType.Hexagonal;
                    break;

                default:
                    throw new Exception("The custom format requires the first input line to be one of these exact words: triangular, square, hexagonal.");
            }

            TileSet tileSet = new TileSet(tileSetType);

            Board board = new Board(tileSet);

            try
            {
                for (int i = 1; i < inputStrings.Length; i++)
                {
                    string[] inputParameters = inputStrings[i].Split(',');
                    double x = Double.Parse(inputParameters[0]);
                    double y = Double.Parse(inputParameters[1]);
                    bool allowsMovement = Boolean.Parse(inputParameters[2]);

                    Position position = new Position(x, y);
                    Tile tile = new Tile(position, allowsMovement);

                    board.AddTile(tile);
                }
            }
            catch (FormatException exception)
            {
                throw new Exception("The input data doesn't conform to the original format as expected.", exception);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new Exception("The input data doesn't conform to the original format as expected.", exception);
            }

            return board;
        }
    }
}
