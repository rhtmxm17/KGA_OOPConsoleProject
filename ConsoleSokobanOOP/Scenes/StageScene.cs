using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleSokobanOOP
{
    public struct StageSetup
    {
        public int sizeX;
        public int sizeY;
        public Point player;
        public Point[] balls;
        public Point[] wall;
        public Point[] goal;
    }

    internal class StageScene : Scene
    {
        class MapData : IConsoleRenader
        {
            // 출력 기준점(Left Top)
            public Point Point { get; set; } = new Point() { x = 0, y = 0 };

            public int SizeX => map.GetLength(0);
            public int SizeY => map.GetLength(1);

            private Tile[,] map;
            public Tile this[Point pt] => map[pt.x, pt.y];
            
            public MapData(int sizeX, int sizeY)
            {
                map = new Tile[sizeX, sizeY];
                for (int i = 0; i < sizeX; i++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        map[i, j] = new Tile();
                    }
                }
            }

            public string RenderString { get; private set; } = "주의: 맵 출력이 준비되지 않음";

            public void UpdateRenderString()
            {
                StringBuilder sb = new();
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        sb.Append(map[i, j].RenderString);
                    }
                    sb.AppendLine();
                }
                RenderString = sb.ToString();
            }
        }

        private MapData map;
        private Goal goal;
        private Player player;

        private string fixedLayer = string.Empty;
        private List<StageObject> DynamicLayer;

        public StageScene(Game game, in StageSetup data) : base(game)
        {
            map = new MapData(data.sizeX, data.sizeY);
            goal = new();
            player = new();
            DynamicLayer = new(data.balls.Length + 1);
            SetupMap(in data);
        }

        private void SetupMap(in StageSetup data)
        {
            // 벽
            Wall wall = new Wall();
            foreach (var pt in data.wall)
            {
                wall.SetAttribute(map[pt]);
            }

            // 골
            foreach (var pt in data.goal)
            {
                goal.SetAttribute(map[pt]);
            }

            // 플레이어
            DynamicLayer.Add(player);
            player.Point = data.player;
            map[data.player].Entry(player);

            // 공
            foreach (var pt in data.balls)
            {
                Ball ball = new();
                DynamicLayer.Add(ball);
                ball.Point = pt;
                map[pt].Entry(ball);
            }

            // 고정 출력
            map.UpdateRenderString();

            StringBuilder sb = new();
            
            sb.AppendLine("돌아가기: Esc");
            sb.Append("남은 공: ");

            fixedLayer = sb.ToString();
        }

        public override void Enter()
        {
            Console.Clear();

            OnKeyInput += KeyCheck;
            goal.OnScoreChanged += ScoreCheck;
        }

        public override void Exit()
        {
            OnKeyInput -= KeyCheck;
            goal.OnScoreChanged -= ScoreCheck;
        }

        public override void Render()
        {
            map.Render();

            Console.Write(fixedLayer);
            Console.Write(goal.Score);

            foreach (var obj in DynamicLayer)
            {
                obj.Render();
            }

            Console.ResetColor();
        }

        public override void Update()
        {
        }

        private void KeyCheck(ConsoleKey key)
        {
            Direction inputDirection = Direction.NONE;
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    inputDirection = Direction.Up;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    inputDirection = Direction.Down;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    inputDirection = Direction.Left;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    inputDirection = Direction.Right;
                    break;
                // 스테이지 선택: Esc
                case ConsoleKey.Escape:
                    ChangeScene(SceneFactory(SceneType.Select));
                    return;
                default:
                    return;
            }

            // 방향 입력이 있었을 경우 이동 함수 수행
            if (inputDirection == Direction.NONE)
                Debug.Assert(false);

            Point next = inputDirection.Next(player.Point);
            switch (map[next].state)
            {
                case TileState.Blocked:
                    return;

                case TileState.Moveable:
                    {
                        Point pushTo = inputDirection.Next(next);
                        if (map[pushTo].state == TileState.Blocked)
                            return;

                        MoveStageObject(next, pushTo);
                        MoveStageObject(player.Point, next);
                    }
                    return;

                case TileState.Empty:
                    MoveStageObject(player.Point, next);
                    return;

                default:
                    throw new ArgumentException();
            }
        }

        // 완전히 조건 확인 후 실제 이동에 사용
        private void MoveStageObject(Point from, Point to)
        {
            StageObject moved = map[from].Away() ?? throw new Exception("논리오류: 정상일 경우 not null");
            moved.Point = to;
            map[to].Entry(moved);
        }

        private void ScoreCheck()
        {
            if (goal.Score > 0)
                return;

            Render();

            int y = map.SizeY - 2;
            if (y < 0)
                y = 0;
            Console.SetCursorPosition(y, map.SizeX >> 1);
            Console.Write("클리어!");
            Thread.Sleep(2000);
            ChangeScene(SceneFactory(SceneType.Select));
        }
    }
}
