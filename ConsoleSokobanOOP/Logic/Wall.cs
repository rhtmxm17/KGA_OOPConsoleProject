using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Wall : TileAttribute
    {
        private string renderString;

        public Wall()
        {
            renderString = DataContainer.GetRenderString("Wall");
        }

        public override void RemoveFrom(Tile tile)
        {
            Debug.Assert(false, "제거가 고려되지 않은 속성이 제거됨");
            tile.state = TileState.Empty;
            tile.OnEntry -= IncorrectEntry;
        }

        public override void SetAttribute(Tile tile)
        {
            tile.RenderString = renderString;
            tile.state = TileState.Blocked;
            tile.OnEntry += IncorrectEntry;
            tile.Color = ConsoleColor.Yellow;
        }

        private void IncorrectEntry(Tile tile, StageObject obj)
        {
            Debug.Assert(false, "벽에 진입 명령됨");
        }
    }
}
