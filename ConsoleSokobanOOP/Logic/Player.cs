namespace ConsoleSokobanOOP
{
    public class Player : StageObject
    {
        public override string RenderString => "ⓟ";

        public override ConsoleColor Color => ConsoleColor.Yellow;

        public override void AwayFrom(Tile tile)
        {
        }

        public override void EntryTo(Tile tile)
        {
        }
    }
}
