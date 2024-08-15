using System.Collections;
using System.Drawing;

namespace ConsoleSokobanOOP
{
    public interface IConsoleRenader
    {
        public abstract Point Point { get; }

        public abstract string RenderString { get; }

        public virtual ConsoleColor Color => ConsoleColor.White;

        public virtual IEnumerable<IConsoleRenader>? Childs => null;

    }

    public static partial class Extention
    {
        public static void Render(this IConsoleRenader obj)
        {
            Console.SetCursorPosition(obj.Point.y * 2, obj.Point.x);
            Console.ForegroundColor = obj.Color;
            Console.Write(obj.RenderString);
            if (obj.Childs is not null)
            {
                foreach (var child in obj.Childs)
                {
                    RenderChild(obj.Point, child);
                }
            }
        }


        private static void RenderChild(Point basePoint, IConsoleRenader obj)
        {
            Point renderPoint = (basePoint.x + obj.Point.x, basePoint.y + obj.Point.y);
            Console.SetCursorPosition(renderPoint.y * 2, renderPoint.x);
            Console.ForegroundColor = obj.Color;
            Console.Write(obj.RenderString);
            if (obj.Childs is not null)
            {
                foreach (var child in obj.Childs)
                    RenderChild(renderPoint, child);
            }
        }
    }
}