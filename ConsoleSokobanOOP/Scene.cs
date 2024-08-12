namespace ConsoleSokobanOOP
{
    public abstract class Scene
    {
        public ConsoleKey InputKey { get; set; }

        private Scene() { }

        public virtual void Input()
        {
            if (Console.KeyAvailable)
            {
                InputKey = Console.ReadKey(true).Key;
                Console.In.Dispose(); // 버퍼에 남은 입력이 쌓여있다면 제거
            }
            else
            {
                // 입력중이 아니라면 None
                InputKey = ConsoleKey.None;
            }
        }

        public abstract void Update();

        public abstract void Render();

        public abstract void Enter();

        public abstract void Exit();

        public static Scene SceneFactory(SceneType type, int arg = 0)
        {
            switch (type)
            {
                case SceneType.Title:
                    throw new NotImplementedException();
                case SceneType.Setting:
                    throw new NotImplementedException();
                case SceneType.Select:
                    throw new NotImplementedException();
                case SceneType.Stage:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException("잘못된 SceneType");
            }
        }
    }
}
