﻿using System;
using System.Collections.Generic;

namespace the_amazing_race
{
    class Tile
    {
        public static readonly double Radius = 1;
        public static readonly double FudgeFactorToCompensateForGradualLossInFloatingPointPrecision = 1.002;
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

            return "tile at " + roundedCoordinates;
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
            List<Tile> tilesAlreadyChecked = new List<Tile>();

            Tile spotlightTile = this;

            while (spotlightTile != someOtherTile)
            {
                tilesAlreadyChecked.Add(spotlightTile);

                try
                {
                    int fewestHops = spotlightTile.NumberOfHopsToOtherTiles[someOtherTile];

                    foreach (Tile immediateNeighbor in spotlightTile.ImmediateNeighbors)
                    {
                        if (immediateNeighbor.AllowsMovement
                            && immediateNeighbor.NumberOfHopsToOtherTiles[someOtherTile] < fewestHops
                            && !tilesAlreadyChecked.Contains(immediateNeighbor))
                        {
                            fewestHops = immediateNeighbor.NumberOfHopsToOtherTiles[someOtherTile];
                            spotlightTile = immediateNeighbor;
                            path.Add(immediateNeighbor);
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.Error.WriteLine("The " + spotlightTile + " encountered an error.", exception);
                }
            }

            return path.ToArray();
        }
    }
}
