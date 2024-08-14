namespace ConsoleSokobanOOP
{
    public class Ball : StageObject
    {
        public bool IsOnGole { get; set; } = false;

        private string renderString;

        public override string RenderString => renderString;

        public override ConsoleColor Color
        {
            get
            {
                if (IsOnGole)
                    return ConsoleColor.DarkBlue;
                else
                    return ConsoleColor.Blue;
            }
        }

        public Ball()
        {
            renderString = DataContainer.GetRenderString("Ball");
        }

        public override void AwayFrom(Tile tile)
        {
            base.EntryTo(tile);
            tile.state = TileState.Empty;
        }

        public override void EntryTo(Tile tile)
        {
            base.EntryTo(tile);
            tile.state = TileState.Moveable;
        }
    }
}
