using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Wall : StageObject
    {
        public override void AwayFrom(Tile tile)
        {
            Debug.Assert(false, "제거 불가능한 물체가 제거됨");
            tile.State = TileState.Empty;
            tile.OnEntry -= IncorrectEntry;
        }

        public override void EntryTo(Tile tile)
        {
            tile.State = TileState.Blocked;
            tile.OnEntry += IncorrectEntry;
        }

        public override string GetRenderString()
        {
            return "▦";
        }

        private void IncorrectEntry(StageObject obj)
        {
            Debug.Assert(obj == this, "벽에 진입 명령됨");
        }
    }
}
