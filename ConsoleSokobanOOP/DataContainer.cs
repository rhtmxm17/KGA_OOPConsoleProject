using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public static class DataContainer
    {
        private static Dictionary<string, string> strings = new();

        public static void Setup(RenderMode mode)
        {
            if (mode == RenderMode.Default)
            {
                strings.Add("Wall", "▦");
                strings.Add("Ball", "●");
                strings.Add("Player", "ⓟ");
                strings.Add("Goal", "○");

            }
            else if (mode == RenderMode.Safe)
            {
                strings.Add("Wall", "##");
                strings.Add("Ball", "＠");
                strings.Add("Player", "ⓟ");
                strings.Add("Goal", "ㅇ");

            }
            else
                throw new ArgumentException();
        }

        public static string GetRenderString(string key)
        {
            bool result = strings.TryGetValue(key, out var value);
            Debug.Assert(result);
            return value!;
        }

    }
}
