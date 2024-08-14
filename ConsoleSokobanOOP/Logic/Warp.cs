using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Warp : TileAttribute
    {

        class WarpExit : TileAttribute
        {
            private Warp entrance;

            public WarpExit(Warp entrance)
            {
                this.entrance = entrance;
            }

            public override void RemoveFrom(Tile tile)
            {
                Debug.Assert(false, "제거가 고려되지 않은 속성이 제거됨");
                tile.RenderString = "  ";
            }

            public override void SetAttribute(Tile tile)
            {
                entrance.exitTile = tile;
                tile.RenderString = entrance.renderString[1];
                tile.Color = ConsoleColor.Magenta;
            }
        }
        public TileAttribute Exit { get; private set; }

        private string[] renderString = new string[2];
        private Tile? entrnceTile = null;
        private Tile? exitTile = null;

        public Warp()
        {
            Exit = new WarpExit(this);

            renderString[0] = DataContainer.GetRenderString("WarpIn");
            renderString[1] = DataContainer.GetRenderString("WarpOut");
        }

        public override void RemoveFrom(Tile tile)
        {
            Debug.Assert(false, "제거가 고려되지 않은 속성이 제거됨");
            tile.RenderString = "  ";
        }

        public override void SetAttribute(Tile tile)
        {
            entrnceTile = tile;
            tile.RenderString = renderString[0];
            tile.OnEntry += TryWarp;
            tile.Color = ConsoleColor.Cyan;
        }

        private void TryWarp(StageObject obj)
        {
            if (entrnceTile is null || exitTile is null)
                throw new Exception("정의되지 않은 동작: 출입구가 설정되지 않음");

            // 출구가 막혀있는 경우
            if (exitTile.Holding is not null)
                return;

            entrnceTile.Away();
            exitTile.Entry(obj);
        }
    }
}
