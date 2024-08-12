namespace ConsoleSokobanOOP
{
    public class SelectScene : Scene
    {
        private const int CursorBegin = 2;
        private const int CursorEnd = CursorBegin + 4;
        private int selected = CursorBegin;

        public SelectScene(Game game) : base(game)
        {
        }

        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("스테이지 선택:");
            Console.WriteLine();
            Console.WriteLine("  |스테이지 1");
            Console.WriteLine("  |스테이지 2");
            Console.WriteLine("  |스테이지 3");
            Console.WriteLine();
            Console.WriteLine("  |종료");
            Console.WriteLine();
            Console.WriteLine("방향키: 선택      엔터: 확인");
        }

        public override void Exit() { }

        public override void Render()
        {
            Console.SetCursorPosition(0, CursorBegin);
            for (int i = CursorBegin; i < CursorEnd + 1; i++)
            {
                Console.WriteLine("  ");
            }

            Console.SetCursorPosition(0, selected);
            Console.Write(">");
        }

        public override void Update()
        {
            switch(InputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    selected--;
                    if (selected < CursorBegin)
                        selected = CursorBegin;
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    selected++;
                    if (selected > CursorEnd)
                        selected = CursorEnd;
                    break;

                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    EnterStage();
                    break;
            }
        }

        private void EnterStage()
        {
            if (selected == CursorEnd - 1)
                return;

            if (selected == CursorEnd)
            {
                throw new NotImplementedException("게임 종료 요청됨");
            }

            ChangeScene(SceneFactory(SceneType.Stage, selected - CursorBegin));
        }
    }
}
