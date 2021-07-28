namespace the_amazing_race
{
    class Tile
    {
        public readonly Position MyPosition;
        public readonly bool AllowsMovement;

        public Tile(Position position, bool allowsMovement)
        {
            MyPosition = position;
            AllowsMovement = allowsMovement;
        }

        public void FindImmediateNeighbors(Board board)
        {

        }

        public void CalculatePathDistanceToAllOtherTiles(Board board)
        {

        }
    }
}
