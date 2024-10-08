﻿namespace ConsoleSokobanOOP
{
    public class Tile : IConsoleRender
    {
        #region IConsoleRender
        public string RenderString { get; set; } = "  ";
        public Point Point { get; private set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        #endregion IConsoleRenader

        public TileState state = TileState.Empty;

        // 타일에 속성을 부여하는 물체(Goal, Wall)가 설정하기 위한 이벤트
        // 다음엔 이벤트를 발생시킨 주체도 던져주자...  Action<Tile, StageObject>
        public event Action<Tile, StageObject>? OnEntry;
        public event Action<StageObject>? OnAway;

        public StageObject? Holding { get; private set; } = null;

        public Tile(Point point)
        {
            Point = point;
        }

        public void Entry(StageObject obj)
        {
            Holding = obj;
            obj.EntryTo(this);
            OnEntry?.Invoke(this, obj);
        }

        public StageObject? Away()
        {
            if (Holding is null)
                return null;

            StageObject away = Holding;
            Holding = null;
            away.AwayFrom(this);
            OnAway?.Invoke(away);

            return away;
        }
    }
}
