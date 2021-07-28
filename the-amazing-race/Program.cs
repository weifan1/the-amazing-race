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

            if (args.Length == 0)
            {
                Console.Error.WriteLine("\nExpected a single command line argument for the input file and if applicable, its path.");
            }
            else
            {
                try
                {
                    string[] inputStrings = ReadInput(args[0]);

                    Board board = CreateBoardFromInputOfUnknownFormat(inputStrings);
                    board.IntroduceEachTileToImmediateNeighborsWhoAreNotItself();
                    board.HaveEachTileCalculateItsNumberOfHopsToReachablyDistantNeighborsStartingWithItself();

                    Tile[] path = board.EntranceTile.FindAnyShortestPathTo(board.ExitTile);

                    WriteOutput(path);
                }
                catch (Exception exception)
                {
                    Console.Error.WriteLine("\n" + exception.Message);
                }
            }

            Console.WriteLine("\nThe Amazing Race has ended.");
            Console.WriteLine("Press any key to close the console.");

            Console.ReadKey();
        }

        private static void WriteOutput(Tile[] path)
        {
            Console.WriteLine();

            for (int i = 0; i < path.Length; i++)
            {
                Console.WriteLine("\tStep " + (i + 1) + ": the " + path[i]);
            }
        }

        private static string[] ReadInput(string fileName)
        {
            List<string> inputStrings = new List<string>();

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string inputString = streamReader.ReadLine();
                        inputStrings.Add(inputString);
                    }
                }
            }
            catch (FileNotFoundException exception)
            {
                throw new FileNotFoundException("The specified input file does not exist in the working directory or if applicable, its relative path.", exception);
            }

            return inputStrings.ToArray();
        }

        private static Board CreateBoardFromInputOfUnknownFormat(string[] inputStrings)
        {
            bool isInputOfOriginalFormat = true;

            foreach (char c in inputStrings[0])
            {
                bool is0 = (c == '0');
                bool is1 = (c == '1');

                if (!(is0 || is1))
                {
                    isInputOfOriginalFormat = false;
                    break;
                }
            }

            if (isInputOfOriginalFormat)
            {
                Console.WriteLine("\nAt first glance, the input file looks like it's in the original format.");

                return CreateBoardFromInputOfOriginalFormat(inputStrings);
            }
            else
            {
                Console.WriteLine("\nAt first glance, the input file looks like it's in the custom format.");

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

                        if (allowsMovement && y == 0)
                        {
                            board.EntranceTile = tile;
                        }

                        if (allowsMovement && y == inputStrings.Length - 1)
                        {
                            board.ExitTile = tile;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new InvalidDataException("The input file doesn't conform to the original format as expected.", exception);
            }

            return board;
        }

        private static Board CreateBoardFromInputOfCustomFormat(string[] inputStrings)
        {
            TileSetType tileSetType;

            string firstInputString = inputStrings[0].ToLower();

            switch (firstInputString)
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
                    throw new InvalidDataException("The custom format is insensitive to case, but requires the first input line to be one of these exact words: triangular, square, hexagonal.");
            }

            TileSet tileSet = new TileSet(tileSetType);

            Board board = new Board(tileSet);

            try
            {
                for (int i = 1; i < inputStrings.Length; i++)
                {
                    string[] tileParameters = inputStrings[i].Split(',');
                    double x = Double.Parse(tileParameters[0]);
                    double y = Double.Parse(tileParameters[1]);
                    bool allowsMovement = Boolean.Parse(tileParameters[2]);

                    Position position = new Position(x, y);
                    Tile tile = new Tile(position, allowsMovement);

                    board.AddTile(tile);

                    if (i == 0)
                    {
                        board.EntranceTile = tile;
                    }

                    if (i == board.MyTiles.Count - 1)
                    {
                        board.ExitTile = tile;
                    }
                }
            }
            catch (FormatException exception)
            {
                throw new InvalidDataException("The input file doesn't conform to the custom format as expected.", exception);
            }
            catch (IndexOutOfRangeException exception)
            {
                throw new InvalidDataException("The input file doesn't conform to the custom format as expected.", exception);
            }

            return board;
        }
    }
}
