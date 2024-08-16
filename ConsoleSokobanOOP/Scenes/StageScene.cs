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

            public Point Anchor { get; set; } = (0, 0);

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
        private Player player;
        private Goal goal;
        private TextUI scoreUI;

        private PlaneRenderGroup UILayer;
        private PlaneRenderGroup DynamicLayer;

        private Direction pushDirection = default;
        private int pushCount = 0;

        public StageScene(Game game, in StageSetup data) : base(game)
        {
            map = new MapData(data.sizeX, data.sizeY);
            player = new();
            goal = new();
            scoreUI = new();
            UILayer = new();
            DynamicLayer = new PlaneRenderGroup(data.balls.Length + 1);
            Setup(in data);
        }

        private void Setup(in StageSetup data)
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

            // UI
            TextUI ui;

            ui = new TextUI();
            ui.Point = (2, 0);
            ui.RenderString = "돌아가기: Esc";
            UILayer.Items.Add(ui);

            ui = new TextUI();
            ui.Point = (0, 0);
            ui.RenderString = "남은 공:";
            UILayer.Items.Add(ui);

            scoreUI.Point = (0, 5);
            scoreUI.RenderString = goal.Score.ToString();
            UILayer.Items.Add(scoreUI);

            UILayer.Anchor = (map.SizeX + 1, 0);
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
            UILayer.GroupRender();
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
                    PushGame(inputDirection);
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
            scoreUI.RenderString = goal.Score.ToString();

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

        private void PushGame(Direction direction)
        {
            if (direction != pushDirection)
            {
                pushDirection = direction;
                pushCount = 0;
                return;
            }

            pushCount++;
            if (pushCount < 5)
                return;

            Point moveTo = direction.Next(map.Anchor);
            if (moveTo.x < 0 || moveTo.y < 0)
                return;

            map.Anchor = moveTo;
            UILayer.Anchor = direction.Next(UILayer.Anchor);
            DynamicLayer.Anchor = direction.Next(DynamicLayer.Anchor);
            Console.Clear();
        }
    }

}
