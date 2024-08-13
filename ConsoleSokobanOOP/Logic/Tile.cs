namespace ConsoleSokobanOOP
{
    public class Tile
    {
        public string RenderString { get; set; } = "  ";

        public TileState state = TileState.Empty;

        // 타일에 속성을 부여하는 물체(Goal, Wall)가 설정하기 위한 이벤트
        public event Action<StageObject>? OnEntry;
        public event Action<StageObject>? OnAway;

        public StageObject? FluidObj { get; private set; } = null;

        public Tile() { }

        public void Entry(StageObject obj)
        {
            OnEntry?.Invoke(obj);
            FluidObj = obj;
            obj.EntryTo(this);
        }

        public StageObject? Away()
        {
            if (FluidObj is null)
                return null;

            OnAway?.Invoke(FluidObj);

            StageObject away = FluidObj;
            FluidObj = null;
            away.AwayFrom(this);
            return away;
        }
    }
}
