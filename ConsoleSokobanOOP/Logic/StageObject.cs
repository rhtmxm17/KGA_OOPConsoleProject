namespace ConsoleSokobanOOP
{
    public abstract class StageObject
    {
        public Point Point { get; set; }

        public abstract string RenderString { get; }
        public virtual ConsoleColor Color => ConsoleColor.White;

        public abstract void EntryTo(Tile tile);
        public abstract void AwayFrom(Tile tile);
    }
}
