namespace ConsoleSokobanOOP
{
    public abstract class StageObject : IConsoleRenader
    {
        public virtual Point Point { get; set; }
        public abstract string RenderString { get; }
        public virtual ConsoleColor Color => ConsoleColor.White;

        public virtual void EntryTo(Tile tile)
        {
            this.Point = tile.Point;
        }

        public virtual void AwayFrom(Tile tile) { }
    }
}
