using System;
using System.Collections.Generic;
using System.Text;

namespace the_amazing_race_wei_fan
{
    class Tile
    {
        private Position MyPosition;
        private bool AllowsMovement;

        public Tile(Position position, bool allowsMovement)
        {
            MyPosition = position;
            AllowsMovement = allowsMovement;
        }
    }
}
