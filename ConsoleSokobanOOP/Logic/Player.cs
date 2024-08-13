namespace ConsoleSokobanOOP
{
    public class Player : StageObject
    {
        private string renderString;
        public override string RenderString => renderString;

        public override ConsoleColor Color => ConsoleColor.Yellow;

        public Player()
        {
            renderString = DataContainer.GetRenderString("Player");
        }

        public override void AwayFrom(Tile tile)
        {
        }

        public override void EntryTo(Tile tile)
        {
        }
    }
}
