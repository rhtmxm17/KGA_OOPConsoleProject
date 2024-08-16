namespace ConsoleSokobanOOP
{
    public class TextUI : IConsoleRender
    {
        public Point Point { get; set; }

        public string RenderString { get; set; } = string.Empty;
    }
}
