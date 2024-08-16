using System.Diagnostics;
using System.Text;

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
        public Point[] warpIn;
        public Point[] warpOut;
    }

    internal class StageScene : Scene
    {
        private class MapData : IRenderGroup
        {
            #region IRenderGroup

            Point IRenderGroup.Anchor { get; set; } = new Point() { x = 3, y = 3 };

            IEnumerable<IConsoleRender> IRenderGroup.Items
            {
                get
                {
                    foreach (IConsoleRender item in map)
                    {
                        yield return item;
                    }
                }
            }

            #endregion IRenderGroup

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
                        map[i, j] = new Tile(new Point() { x = i, y = j });
                    }
                }
            }
        }

        private MapData map;
        private Goal goal;
        private Player player;

        private string textUI = string.Empty;
        private List<IConsoleRender> BackGroundLayer;
        private PlaneRenderGroup DynamicLayer;

        public StageScene(Game game, in StageSetup data) : base(game)
        {
            map = new MapData(data.sizeX, data.sizeY);
            goal = new();
            player = new();
            BackGroundLayer = new();
            DynamicLayer = new PlaneRenderGroup(data.balls.Length + 1);
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

            // 워프
            for (int i = 0; i < data.warpIn.Length; i++)
            {
                Warp warp = new();

                warp.SetAttribute(map[data.warpIn[i]]);
                warp.Exit.SetAttribute(map[data.warpOut[i]]);
            }

            // 플레이어
            DynamicLayer.Items.Add(player);
            map[data.player].Entry(player);

            // 공
            foreach (var pt in data.balls)
            {
                Ball ball = new();
                DynamicLayer.Items.Add(ball);
                ball.Point = pt;
                map[pt].Entry(ball);
            }

            // 고정 출력
            StringBuilder sb = new();

            sb.AppendLine("돌아가기: Esc");
            sb.Append("남은 공: ");

            textUI = sb.ToString();

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
            map.GroupRender();

            foreach (var obj in BackGroundLayer)
            {
                obj.Render();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, map.SizeX + 1);
            Console.Write(textUI);
            Console.Write(goal.Score);

            DynamicLayer.GroupRender();

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
                        if (map[pushTo].state != TileState.Empty)
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
