using System;
using System.Collections.Generic;
using System.IO;

namespace the_amazing_race
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nThe Amazing Race has begun.");

            if (args.Length == 0)
            {
                Console.Error.WriteLine("\nError: Expected a command line argument, for the input file.");
                Console.Error.WriteLine("Remedy: Specify the input file from the command line,");
                Console.Error.WriteLine("\tor just modify the Debug tab of the Project Properties page, if that's available.");
            }
            else
            {
/*
                try
                {
*/
                    string[] inputStrings = ReadInput(args[0]);
                    
                    Board board = CreateBoardFromInputOfUnknownFormat(inputStrings);

                    board.AcquaintEachTileWithItsImmediateNeighbors();
                    board.HaveEachTileCalculateItsPathDistanceToAllOtherTiles();
/*
                }
                catch (Exception exception)
                {
                    Console.Error.WriteLine("\nError: " + exception.Message);
                    throw;
                }
*/
            }

            Console.WriteLine("\nThe Amazing Race has ended.");
            Console.WriteLine("Press any key to close the console.\n");
            Console.ReadKey();
        }

        static string[] ReadInput(string fileName)
        {
            List<string> inputStringList = new List<string>();

            using (StreamReader theStreamReader = new StreamReader(fileName))
            {
                while (!theStreamReader.EndOfStream)
                {
                    string Line = theStreamReader.ReadLine();
                    inputStringList.Add(Line);
                }
            }

            return inputStringList.ToArray();
        }

        static Board CreateBoardFromInputOfUnknownFormat(string[] inputStrings)
        {
            string theFirstInputString = inputStrings[0].ToLower();
            bool isRequiredStyleInput = theFirstInputString.Contains("1");

            if (isRequiredStyleInput)
            {
                Console.WriteLine("\nAt first glance, the input file appears to be of the original format.");

                return CreateBoardFromInputOfOriginalFormat(inputStrings);
            }
            else
            {
                Console.WriteLine("\nAt first glance, the input file appears to be of the custom format.");

                return CreateBoardFromInputOfCustomFormat(inputStrings);
            }
        }

        static Board CreateBoardFromInputOfOriginalFormat(string[] inputStrings)
        {
            TileSet tileSet = new TileSet(TileSetType.Square);

            Board board = new Board(tileSet);

            for (int y = 1; y < inputStrings.Length; y++)
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

            return board;
        }

        static Board CreateBoardFromInputOfCustomFormat(string[] inputStrings)
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

            return board;
        }
    }
}
