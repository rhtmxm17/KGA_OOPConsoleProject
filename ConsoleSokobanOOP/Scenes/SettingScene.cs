namespace ConsoleSokobanOOP
{
    public class SettingScene : Scene
    {
        public SettingScene(Game game) : base(game)
        {
        }

        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("[▦ ● ○]");
            Console.WriteLine("위 괄호 안의 문자가 물음표로 변환되지 않고 모두 정상적으로 출력됩니까?(Y/N)");
            Console.WriteLine();
            OnKeyInput += this.KeyCheck;
        }

        public override void Exit() { }

        public override void Render() { }

        public override void Update() { }

        private void KeyCheck(ConsoleKey key)
        {
            if (InputKey == ConsoleKey.Y)
            {
                Console.WriteLine("일반 모드로 게임을 시작합니다.");
                DataContainer.Setup(RenderMode.Default);
                Thread.Sleep(1000);
                ChangeScene(SceneFactory(SceneType.Select));
            }
            else if (InputKey == ConsoleKey.N)
            {
                Console.WriteLine("호환 모드로 게임을 시작합니다.");
                DataContainer.Setup(RenderMode.Safe);
                Thread.Sleep(1000);
                ChangeScene(SceneFactory(SceneType.Select));
            }
        }
    }
}
