namespace ConsoleSokobanOOP
{
    public interface IConsoleRenader
    {
        public abstract Point Point { get; }

        public abstract string RenderString { get; }

        public virtual ConsoleColor Color => ConsoleColor.White;

        // 어째서인지 인터페이스를 구현중인 클래스 타입 변수로 사용하려면 형변환 필요...
        public virtual void Render()
        {
            Console.SetCursorPosition(Point.y * 2, Point.x);
            Console.ForegroundColor = Color;
            Console.Write(RenderString);
        }
    }

    public static partial class Extention
    {
        // 확장시키니 또 되네?
        public static void Render(this IConsoleRenader obj) => obj.Render();
    }
}