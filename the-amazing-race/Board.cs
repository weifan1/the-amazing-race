using System;
using System.Collections.Generic;

namespace the_amazing_race
{
    class Board
    {
        public readonly TileSet MyTileSet;
        public readonly List<Tile> MyTiles = new List<Tile>();

        public Tile EntranceTile;
        public Tile ExitTile;

        public Board(TileSet tileSet)
        {
            MyTileSet = tileSet;
        }

        public void AddTile(Tile tile)
        {
            MyTiles.Add(tile);

            DebugConsole.WriteLine("\tAdding the " + tile + ".");
        }

        public void IntroduceEachTileToImmediateNeighborsWhoAreNotItself()
        {
            foreach (Tile tile in MyTiles)
            {
                tile.MeetImmediateNeighborsWhoAreNotMyself(this);
            }
        }

        public void HaveEachTileCalculateItsNumberOfHopsToReachablyDistantNeighborsStartingWithItself()
        {
            foreach (Tile tile in MyTiles)
            {
                tile.CalculateMyNumberOfHopsToReachablyDistantNeighborsStartingWithMyself(this);
            }
        }
    }
}
