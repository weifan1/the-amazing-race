using System;
using System.Collections.Generic;

namespace the_amazing_race
{
    class Tile
    {
        public static readonly double Radius = 1;
        public static readonly double FudgeFactorToCompensateForGradualLossInFloatingPointPrecision = (Math.Sqrt(2) + 1) / 2;
        public static readonly double MaximumDistanceToQualifyAsImmediateNeighbor = Radius * FudgeFactorToCompensateForGradualLossInFloatingPointPrecision;

        public readonly Position MyPosition;
        public readonly bool AllowsMovement;

        public readonly List<Tile> ImmediateNeighbors = new List<Tile>();
        public readonly Dictionary<Tile, int> NumberOfHopsToOtherTiles = new Dictionary<Tile, int>();

        public Tile(Position position, bool allowsMovement)
        {
            MyPosition = position;
            AllowsMovement = allowsMovement;
        }

        public override string ToString()
        {
            string roundedCoordinates = "("
                + String.Format("{0:N4}", MyPosition.X) + ", "
                + String.Format("{0:N4}", MyPosition.Y) + ")";
            string allowsOrDoesNotAllow = AllowsMovement
                ? "    allows    "
                : "does not allow";

            return "tile at " + roundedCoordinates + " which " + allowsOrDoesNotAllow + " movement";
        }

        public void MeetImmediateNeighborsWhoAreNotMyself(Board board)
        {
            ImmediateNeighbors.Clear();

            DebugConsole.WriteLine("\n\tThe " + this);

            foreach (Tile tile in board.MyTiles)
            {
                double distance = MyPosition.GetDistanceTo(tile.MyPosition);

                if (distance < MaximumDistanceToQualifyAsImmediateNeighbor && tile != this)
                {
                    ImmediateNeighbors.Add(tile);

                    DebugConsole.WriteLine("\t\t- met an immediate neighbor; the " + tile + ".");
                }
            }
        }

        public void CalculateMyNumberOfHopsToReachablyDistantNeighborsStartingWithMyself(Board board)
        {
            if (!AllowsMovement)
            {
                return;
            }

            NumberOfHopsToOtherTiles.Add(this, 0);

            List<Tile> reachablyDistantNeighbors = new List<Tile>();
            reachablyDistantNeighbors.Add(this);
            DebugConsole.WriteLine("\n\tThe " + this);

            while (reachablyDistantNeighbors.Count > 0)
            {
                Tile[] copyOfReachablyDistantNeighbors = reachablyDistantNeighbors.ToArray();

                foreach (Tile reachablyDistantNeighbor in copyOfReachablyDistantNeighbors)
                {
                    foreach (Tile distantNeighbor in reachablyDistantNeighbor.ImmediateNeighbors)
                    {
                        if (distantNeighbor.AllowsMovement && !distantNeighbor.NumberOfHopsToOtherTiles.ContainsKey(this))
                        {
                            distantNeighbor.NumberOfHopsToOtherTiles.Add(this, reachablyDistantNeighbor.NumberOfHopsToOtherTiles[this] + 1);
                            reachablyDistantNeighbors.Add(distantNeighbor);
                            DebugConsole.WriteLine("\t\t- discovered a reachably distant neighbor; the " + distantNeighbor + ".");
                        }
                    }

                    reachablyDistantNeighbors.Remove(reachablyDistantNeighbor);
                }
            }
        }

        public Tile[] FindAnyShortestPathTo(Tile someOtherTile)
        {
            if (!this.NumberOfHopsToOtherTiles.ContainsKey(someOtherTile))
            {
                throw new Exception("The " + someOtherTile + " is not reachable from " + this);
            }

            List<Tile> path = new List<Tile>();

            Tile spotlightTile = this;

            foreach (Tile immediateNeighbor in spotlightTile.ImmediateNeighbors)
            {

            }

            return path.ToArray();
        }
    }
}
