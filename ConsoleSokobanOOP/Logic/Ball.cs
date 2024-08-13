namespace ConsoleSokobanOOP
{
    public class Ball : StageObject
    {
        public bool IsOnGole { get; set; } = false;

        public override string RenderString => "●";

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

        public override void AwayFrom(Tile tile)
        {
            tile.state = TileState.Empty;
        }

        public override void EntryTo(Tile tile)
        {
            tile.state = TileState.Moveable;
        }
    }
}
