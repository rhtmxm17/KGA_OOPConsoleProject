namespace ConsoleSokobanOOP
{
    public abstract class StageObject
    {
        public abstract void EntryTo(Tile tile);
        public abstract void AwayFrom(Tile tile);

        public abstract string GetRenderString();
    }
}
