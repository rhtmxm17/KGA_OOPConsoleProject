using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Wall : TileAttribute
    {
        public override void RemoveFrom(Tile tile)
        {
            Debug.Assert(false, "제거가 고려되지 않은 속성이 제거됨");
            tile.State = TileState.Empty;
            tile.OnEntry -= IncorrectEntry;
        }

        public override void SetAttribute(Tile tile)
        {
            tile.RenderString = "▦";
            tile.State = TileState.Blocked;
            tile.OnEntry += IncorrectEntry;
        }

        private void IncorrectEntry(StageObject obj)
        {
            Debug.Assert(false, "벽에 진입 명령됨");
        }
    }
}
