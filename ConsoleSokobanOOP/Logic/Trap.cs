namespace ConsoleSokobanOOP
{
    public class Trap : TileAttribute
    {
        // Trap이 스테이지 씬을 들고있는게 영 좋은 방법인 것 같지는 않다
        // 차후엔 게임의 흐름을 관리하는 클래스와 물체의 생성, 제거, 출력등을 관리할 중재자 클래스를 따로 둘듯
        private StageScene stage;

        public Trap(StageScene stage)
        {
            this.stage = stage;
        }

        public override void RemoveFrom(Tile tile)
        {
            tile.RenderString = "  ";
            tile.Color = ConsoleColor.White;
            tile.OnEntry -= OnTrapped;
        }

        public override void SetAttribute(Tile tile)
        {
            tile.RenderString = "XX";
            tile.Color = ConsoleColor.Red;
            tile.OnEntry += OnTrapped;
        }

        private void OnTrapped(Tile tile, StageObject obj)
        {
            if (obj is Player)
            {
                stage.GameOver("실패!!");
            }
            else
            {
                stage.Remove(obj);
                RemoveFrom(tile);
            }
        }
    }
}
