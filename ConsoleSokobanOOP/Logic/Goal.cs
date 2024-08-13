using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Goal : TileAttribute
    {
        public int Count { get; set; } = 0;
        private string renderString;

        public Goal()
        {
            renderString = DataContainer.GetRenderString("Goal");
        }

        public override void RemoveFrom(Tile tile)
        {
            Debug.Assert(false, "제거가 고려되지 않은 속성이 제거됨");
            Count--;
        }

        public override void SetAttribute(Tile tile)
        {
            tile.RenderString = renderString;
            Count++;
            tile.OnEntry += obj =>
            {
                if (obj is Ball ball)
                {
                    Count--;
                    ball.IsOnGole = true;
                }
            };
            tile.OnAway += obj =>
            {
                if (obj is Ball ball)
                {
                    Count++;
                    ball.IsOnGole = false;
                }
            };
        }
    }
}
