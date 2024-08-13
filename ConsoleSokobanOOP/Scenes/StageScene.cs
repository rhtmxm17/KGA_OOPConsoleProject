using System.Text;

namespace ConsoleSokobanOOP
{
    internal class StageScene : Scene
    {
        private Tile[,] map;
        private Goal goal;
        private Player player;

        private string fixedLayer;
        private List<StageObject> DynamicLayer = new();

        public StageScene(Game game) : base(game)
        {
        }

        public override void Enter()
        {
            Console.Clear();

            SampleStageScene();

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
            sb.Append("남은 공: ");

            fixedLayer = sb.ToString();
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(fixedLayer);
            Console.Write(goal.Count);

            foreach (var obj in DynamicLayer)
            {
                Console.SetCursorPosition(obj.Y * 2, obj.X);
                Console.ForegroundColor = obj.Color;
                Console.Write(obj.RenderString);
            }

            Console.ResetColor();
        }

        public override void Update()
        {
            switch (InputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    player.X--;
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    player.Y--;
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    player.X++;
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    player.Y++;
                    break;
            }
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
            map[3, 4].Entry(player);
            player.X = 3;
            player.Y = 4;

            Ball ball;

            ball = new Ball();
            DynamicLayer.Add(ball);
            map[4, 3].Entry(ball);
            ball.X = 4;
            ball.Y = 3;

            ball = new Ball();
            DynamicLayer.Add(ball);
            map[3, 2].Entry(ball);
            ball.X = 3;
            ball.Y = 2;
        }

    }
}
