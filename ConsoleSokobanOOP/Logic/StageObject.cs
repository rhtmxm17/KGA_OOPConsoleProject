namespace ConsoleSokobanOOP
{
    public abstract class StageObject
    {
        public abstract string RenderString { get; }

        public abstract void EntryTo(Tile tile);
        public abstract void AwayFrom(Tile tile);
    }
}
