using System;
using System.Collections.Generic;

namespace the_amazing_race
{
    class Board
    {
        public readonly TileSet MyTileSet;
        private readonly List<Tile> MyTiles = new List<Tile>();

        public Board(TileSet tileSet)
        {
            MyTileSet = tileSet;
        }

        public void AddTile(Tile tile)
        {
            MyTiles.Add(tile);

            Console.WriteLine("\tAdded a tile at " + tile.MyPosition.X + ", " + tile.MyPosition.Y + " which " + (tile.AllowsMovement ? "allows" : "does not allow") + " movement.");
        }

        public void AcquaintEachTileWithItsImmediateNeighbors()
        {
            foreach (Tile tile in MyTiles)
            {
                tile.FindImmediateNeighbors(this);
            }
        }

        public void HaveEachTileCalculateItsPathDistanceToAllOtherTiles()
        {
            foreach (Tile tile in MyTiles)
            {
                tile.CalculatePathDistanceToAllOtherTiles(this);
            }
        }
    }
}
