namespace ConsoleSokobanOOP
{
    public interface IConsoleRender
    {
        public abstract Point Point { get; }

        public abstract string RenderString { get; }

        public virtual ConsoleColor Color => ConsoleColor.White;

        public virtual void Render()
        {
            Console.SetCursorPosition(Point.y * 2, Point.x);
            Console.ForegroundColor = Color;
            Console.Write(RenderString);
        }
    }

    public static partial class Extention
    {
        public static void Render(this IConsoleRender obj) => obj.Render();

        public static void Render(this IConsoleRender obj, Point anchor)
        {
            Console.SetCursorPosition((anchor.y + obj.Point.y) * 2, (anchor.x + obj.Point.x));
            Console.ForegroundColor = obj.Color;
            Console.Write(obj.RenderString);
        }
    }
}