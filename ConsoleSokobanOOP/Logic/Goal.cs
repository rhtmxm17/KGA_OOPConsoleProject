using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Goal : TileAttribute
    {
        public event Action? OnScoreChanged;

        private int count = 0;
        public int Score
        {
            get => count;
            set 
            {
                count = value;
                OnScoreChanged?.Invoke();
            }
        }

        private string renderString;

        public Goal()
        {
            renderString = DataContainer.GetRenderString("Goal");
        }

        public override void RemoveFrom(Tile tile)
        {
            Debug.Assert(false, "제거가 고려되지 않은 속성이 제거됨");
            Score--;
        }

        public override void SetAttribute(Tile tile)
        {
            tile.RenderString = renderString;
            Score++;
            tile.OnEntry += obj =>
            {
                if (obj is Ball ball)
                {
                    Score--;
                    ball.IsOnGole = true;
                }
            };
            tile.OnAway += obj =>
            {
                if (obj is Ball ball)
                {
                    Score++;
                    ball.IsOnGole = false;
                }
            };
        }
    }
}
