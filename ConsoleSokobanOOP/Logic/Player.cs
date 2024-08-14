namespace ConsoleSokobanOOP
{
    public class Player : StageObject
    {
        private string renderString;
        public override string RenderString => renderString;

        public override ConsoleColor Color => ConsoleColor.DarkRed;

        public Player()
        {
            renderString = DataContainer.GetRenderString("Player");
        }
    }
}
