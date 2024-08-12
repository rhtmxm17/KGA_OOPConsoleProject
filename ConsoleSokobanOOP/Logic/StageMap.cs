namespace ConsoleSokobanOOP
{

    public struct Point
    {
        public int x;
        public int y;
    }

    public class StageMap
    {
        private Tile[,] map;

        public Tile this[Point pt]
        {
            get => map[pt.x, pt.y];
            private set => map[pt.x, pt.y] = value;
        }

        public StageMap(Point pt)
        {
            map = new Tile[pt.x, pt.y];
        }


    }
}
