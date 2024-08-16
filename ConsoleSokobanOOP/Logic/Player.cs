using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public class Player : StageObject
    {
        private string renderString;
        public override string RenderString => renderString;

        public override ConsoleColor Color
        {
            get
            {
                if(IsGamingColor)
                {
                    return (ConsoleColor)(((int)stopwatch.Elapsed.TotalMilliseconds >> 5) % 16);
                }
                else
                {
                    return ConsoleColor.DarkRed;
                }
            }
        }

        private Stopwatch stopwatch = new();

        private bool isGamingColor;

        public bool IsGamingColor 
        {
            get => isGamingColor;
            set
            {
                if (value == isGamingColor)
                    return;

                isGamingColor = value;

                if (value)
                {
                    stopwatch.Start();
                }
                else
                {
                    stopwatch.Stop();
                }
            }
        }

        public Player()
        {
            renderString = DataContainer.GetRenderString("Player");
        }
    }
}
