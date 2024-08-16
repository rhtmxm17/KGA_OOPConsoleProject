using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class SelectScene : Scene
    {
        private const int CursorBegin = 2;
        private const int CursorEnd = CursorBegin + Constant.Stages + 1;
        private int selected = CursorBegin;

        private bool animationToggle = false;
        private Stopwatch stopwatch = new();

        public SelectScene(Game game) : base(game)
        {
        }

        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("스테이지 선택:");
            Console.WriteLine();
            for (int i = 0; i < Constant.Stages; i++)
            {
                Console.Write($"  |스테이지 {i + 1}");
                if (DataContainer.isLocked[i])
                {
                    Console.Write(" (잠김)");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("  |종료");
            Console.WriteLine();
            Console.WriteLine("방향키: 선택      엔터: 확인");

            OnKeyInput += this.KeyCheck;
            stopwatch.Start();
        }

        public override void Exit()
        {
            OnKeyInput -= this.KeyCheck;
            stopwatch.Stop();
        }

        public override void Render()
        {
            Console.SetCursorPosition(0, CursorBegin);
            for (int i = CursorBegin; i < CursorEnd + 1; i++)
            {
                Console.WriteLine("  ");
            }

            Console.SetCursorPosition(0, selected);
            if (animationToggle)
            {
                Console.Write(" ");
            }
            Console.Write(">");
        }

        public override void Update()
        {
            // 약 0.5초마다 변경
            animationToggle = (stopwatch.ElapsedMilliseconds >> 9) % 2 == 0;
        }

        private void KeyCheck(ConsoleKey key)
        {
            switch (InputKey)
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
            // 빈 칸
            if (selected == CursorEnd - 1)
                return;

            // 게임 종료
            if (selected == CursorEnd)
            {
                Game.IsRunning = false;
                return;
            }

            // 잠겨있는 스테이지
            if (DataContainer.isLocked[selected - CursorBegin])
                return;

            ChangeScene(SceneFactory(SceneType.Stage, selected - CursorBegin));
        }
    }
}
