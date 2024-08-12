namespace ConsoleSokobanOOP
{
    public class Ball : StageObject
    {
        public bool IsOnGole { get; set; } = false;

        public override void AwayFrom(Tile tile)
        {
            tile.State = TileState.Empty;
        }

        public override void EntryTo(Tile tile)
        {
            tile.State = TileState.Moveable;
        }

        public override string GetRenderString()
        {
            if (IsOnGole)
                return "ⓞ";
            else
                return "●";
        }
    }
}
