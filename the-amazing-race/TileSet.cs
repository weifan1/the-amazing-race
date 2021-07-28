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

                case TileSetType.Square:
                    return 4;

                case TileSetType.Hexagonal:
                    return 6;

                default:
                    /*
                     * Sure, this may look hacky.
                     * And sure, it's only here because C# insists.
                     * But as it turns out, diangles are a real thing in curved space!
                     * 
                     * Hack gracefully explained away.
                    */
                    return 2;
            }

        }
    }
}
