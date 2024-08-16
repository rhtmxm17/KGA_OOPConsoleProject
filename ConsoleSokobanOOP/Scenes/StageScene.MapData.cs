namespace ConsoleSokobanOOP
{

    public partial class StageScene
    {
        private class MapData : IRenderGroup
        {
            #region IRenderGroup

            public Point Anchor { get; set; } = (0, 0);

            IEnumerable<IConsoleRender> IRenderGroup.Items
            {
                get
                {
                    foreach (IConsoleRender item in map)
                    {
                        yield return item;
                    }
                }
            }

            #endregion IRenderGroup

            public int SizeX => map.GetLength(0);
            public int SizeY => map.GetLength(1);


            private Tile[,] map;
            public Tile this[Point pt] => map[pt.x, pt.y];

            public MapData(int sizeX, int sizeY)
            {
                map = new Tile[sizeX, sizeY];
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        map[i, j] = new Tile(new Point() { x = i, y = j });
                    }
                }
            }
        }
    }

}
