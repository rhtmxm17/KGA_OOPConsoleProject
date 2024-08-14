using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Game
    {
        public bool IsRunning { get; set; } = false;

        public int FrameMilliSec { get; set; } = 100;
        public Scene currentScene { get; private set; }

        public Game()
        {
            currentScene = Scene.SceneFactory(this, SceneType.Title);
        }

        public void Run()
        {
            Start();
            Stopwatch stopwatch = new Stopwatch();

            while (IsRunning)
            {
                stopwatch.Restart();

                currentScene.Input();
                currentScene.Update();
                currentScene.Render();

                while (stopwatch.ElapsedMilliseconds < FrameMilliSec) ;
            }
            End();
        }

        public void Start()
        {
            IsRunning = true;
            currentScene.Enter();
            Console.CursorVisible = false;
        }

        public void End()
        {

        }

        public void ChangeScene(Scene scene)
        {
            currentScene.Exit();
            currentScene = scene;
            currentScene.Enter();
        }
    }
}
