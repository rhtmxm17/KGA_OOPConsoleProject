namespace ConsoleSokobanOOP
{
    public static class Constant
    {
        public const int Stages = 4;
    }

    public struct Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static implicit operator Point((int x, int y) value)
        {
            return new Point(value.x, value.y);
        }
    }

    public static partial class Extention
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