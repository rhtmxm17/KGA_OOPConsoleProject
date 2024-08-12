using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Goal : StageObject
    {
        public int Count { get; set; } = 0;

        public override void AwayFrom(Tile tile)
        {
            Debug.Assert(false, "제거 불가능한 물체가 제거됨");
            Count--;
        }

        public override void EntryTo(Tile tile)
        {
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

        public override string GetRenderString()
        {
            return "○";
        }
    }
}
