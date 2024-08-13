namespace ConsoleSokobanOOP
{
    public abstract class Scene
    {
        private ConsoleKey inputKey;
        public ConsoleKey InputKey
        {
            get => inputKey;
            private set
            {
                inputKey = value;
                if (InputKey != ConsoleKey.None)
                    OnKeyInput?.Invoke(inputKey);
            }
        }

        public event Action<ConsoleKey> OnKeyInput;

        private Game game;

        public Scene(Game game)
        {
            this.game = game;
        }


        public virtual void Input()
        {
            if (Console.KeyAvailable)
            {
                InputKey = Console.ReadKey(true).Key;

                // 키 길게 누르기로 입력이 쌓여있으면 제거
                while (Console.KeyAvailable)
                    _ = Console.ReadKey(true);
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

        protected void ChangeScene(Scene scene) => game.ChangeScene(scene);

        public Scene SceneFactory(SceneType type, int arg = 0) => SceneFactory(game, type, arg);


        private static Scene s_selectScene;
        public static Scene SceneFactory(Game game, SceneType type, int arg = 0)
        {
            switch (type)
            {
                case SceneType.Title:
                    return new TitleScene(game);
                case SceneType.Setting:
                    return new SettingScene(game);
                case SceneType.Select:
                    if(s_selectScene is null)
                        s_selectScene = new SelectScene(game);
                    return s_selectScene;
                case SceneType.Stage:
                    return new StageScene(game, DataContainer.GetStageData(arg));
                default:
                    throw new ArgumentException("잘못된 SceneType");
            }
        }
    }
}
