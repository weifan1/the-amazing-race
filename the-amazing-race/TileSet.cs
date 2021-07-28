namespace the_amazing_race
{
    enum TileSetType
    {
        Triangular,
        Square,
        Hexagonal,
    }

    class TileSet
    {
        private TileSetType MyTileSetType;

        public TileSet(TileSetType tileSetType)
        {
            MyTileSetType = tileSetType;
        }

        public int GetNumberOfSides()
        {
            switch (MyTileSetType)
            {
                case TileSetType.Triangular:
                    return 3;

                default:
                case TileSetType.Square:
                    return 4;

                case TileSetType.Hexagonal:
                    return 6;
            }
        }
    }
}
