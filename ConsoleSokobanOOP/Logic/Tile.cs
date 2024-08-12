using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Tile
    {
        public TileState State { get; set; }

        // 타일에 속성을 부여하는 물체(Goal, Wall)가 설정하기 위한 이벤트
        public event Action<StageObject>? OnEntry;
        public event Action<StageObject>? OnAway;

        private List<StageObject> objects = new List<StageObject>();

        public void Entry(StageObject obj)
        {
            OnEntry?.Invoke(obj);
            objects.Add(obj);
        }

        public void Away(StageObject obj)
        {
            // 게임 로직상 objects.Count는 대부분 0~1개이므로 단순 탐색 수행
            bool result = objects.Remove(obj);
            Debug.Assert(result);

            OnAway?.Invoke(obj);
        }
    }
}
