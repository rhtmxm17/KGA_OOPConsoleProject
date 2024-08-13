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
    }

    internal class StageScene : Scene
    {
        private Tile[,] map;
        private Goal goal;
        private Player player;

        private string fixedLayer = string.Empty;
        private List<StageObject> DynamicLayer;

        public StageScene(Game game, in StageSetup data) : base(game)
        {
            map = new Tile[data.sizeX, data.sizeY];
            goal = new();
            player = new();
            DynamicLayer = new(data.balls.Length + 1);
            SetupMap(in data);
        }

        private void SetupMap(in StageSetup data)
        {
            // 타일
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = new Tile();
                }
            }

            // 플레이어
            DynamicLayer.Add(player);
            player.Point = data.player;
            Map(data.player).Entry(player);

            // 공
            foreach (var pt in data.balls)
            {
                Ball ball = new();
                DynamicLayer.Add(ball);
                ball.Point = pt;
                Map(pt).Entry(ball);
            }

            // 벽
            Wall wall = new Wall();
            foreach (var pt in data.wall)
            {
                wall.SetAttribute(Map(pt));
            }

            // 골
            foreach (var pt in data.goal)
            {
                goal.SetAttribute(Map(pt));
            }

            // 고정 출력
            StringBuilder sb = new();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    sb.Append(map[i, j].RenderString);
                }
                sb.AppendLine();
            }

            sb.AppendLine();
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
            Console.SetCursorPosition(0, 0);
            Console.Write(fixedLayer);
            Console.Write(goal.Score);

            foreach (var obj in DynamicLayer)
            {
                Console.SetCursorPosition(obj.Point.y * 2, obj.Point.x);
                Console.ForegroundColor = obj.Color;
                Console.Write(obj.RenderString);
            }

            Console.ResetColor();
        }

        public override void Update()
        {
        }

        private Tile Map(Point pt) => map[pt.x, pt.y];

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
            switch (Map(next).state)
            {
                case TileState.Blocked:
                    return;

                case TileState.Moveable:
                    {
                        Point pushTo = inputDirection.Next(next);
                        if (Map(pushTo).state == TileState.Blocked)
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
            StageObject moved = Map(from).Away() ?? throw new Exception("논리오류: 정상일 경우 not null");
            moved.Point = to;
            Map(to).Entry(moved);
        }

        private void ScoreCheck()
        {
            if (goal.Score > 0)
                return;

            Render();

            int y = map.GetLength(1) - 2;
            if (y < 0)
                y = 0;
            Console.SetCursorPosition(y, map.GetLength(0) >> 1);
            Console.Write("클리어!");
            Thread.Sleep(2000);
            ChangeScene(SceneFactory(SceneType.Select));
        }

        private void SampleStageScene()
        {
            map = new Tile[8, 8];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = new Tile();
                }
            }

            goal = new Goal();
            TileAttribute wall = new Wall();

            for (int i = 1; i < 7; i++)
            {
                wall.SetAttribute(map[1, i]);
                wall.SetAttribute(map[6, i]);
            }

            for (int i = 2; i < 6; i++)
            {
                wall.SetAttribute(map[i, 1]);
                wall.SetAttribute(map[i, 6]);
            }

            goal.SetAttribute(map[2, 2]);
            goal.SetAttribute(map[4, 3]);

            player = new Player();
            DynamicLayer.Add(player);
            player.Point = new Point { x = 3, y = 4 };
            map[player.Point.x, player.Point.y].Entry(player);


            Ball ball;

            ball = new Ball();
            DynamicLayer.Add(ball);
            ball.Point = new Point { x = 4, y = 3 };
            map[ball.Point.x, ball.Point.y].Entry(ball);

            ball = new Ball();
            DynamicLayer.Add(ball);
            ball.Point = new Point { x = 3, y = 2 };
            map[ball.Point.x, ball.Point.y].Entry(ball);
        }

    }
}
