namespace ConsoleSokobanOOP
{
    public class TitleScene : Scene
    {
        public TitleScene(Game game) : base(game) { }

        public override void Enter()
        {
            Console.Clear();
            Console.WriteLine("===================================");
            Console.WriteLine("=                                 =");
            Console.WriteLine("=            퍼즐 게임            =");
            Console.WriteLine("=                                 =");
            Console.WriteLine("===================================");
            Console.WriteLine();
            Console.WriteLine("아무 키나 눌러서 시작");

            OnKeyInput += key =>
            {
                ChangeScene(SceneFactory(SceneType.Setting));
            };
        }

        public override void Exit() { }

        public override void Render() { }

        public override void Update() { }
    }
}
