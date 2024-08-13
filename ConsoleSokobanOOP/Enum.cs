namespace ConsoleSokobanOOP
{
    public enum SceneType { Title, Setting, Select, Stage };
    public enum TileState { Blocked, Moveable, Empty };
    public enum RenderMode { Default, Safe };

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        NONE,
    }

    public struct Point
    {
        public int x;
        public int y;
    }

    public static class Constant
    {
        public const int Stages = 3;
    }

    public static class Extention
    {
        public static Point Next(this Direction dir, Point pt)
        {
            switch (dir)
            {
                case Direction.Up:
                    pt.x--;
                    break;
                case Direction.Down:
                    pt.x++;
                    break;
                case Direction.Left:
                    pt.y--;
                    break;
                case Direction.Right:
                    pt.y++;
                    break;

                case Direction.NONE:
                default:
                    break;
            }
            return pt;
        }
    }
}