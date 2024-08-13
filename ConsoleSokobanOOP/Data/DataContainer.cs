using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public static class DataContainer
    {
        private static Dictionary<string, string> strings = new();
        private static StageSetup[] stageData = new StageSetup[Constant.Stages];

        static DataContainer()
        {
            stageData[0] = new StageSetup
            {
                sizeX = 8,
                sizeY = 8,
                player = { x = 4, y = 4 },
                balls =
                [
                    new Point { x = 3, y = 3 },
                    new Point { x = 3, y = 5 },
                    new Point { x = 4, y = 3 },
                    new Point { x = 5, y = 4 },
                ],
                goal =
                [
                    new Point { x = 1, y = 3 },
                    new Point { x = 3, y = 6 },
                    new Point { x = 4, y = 1 },
                    new Point { x = 6, y = 4 },
                ],
                wall = new Point[28],
            };

            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                stageData[0].wall[index++] = new Point { x = 0, y = i };
                stageData[0].wall[index++] = new Point { x = 7, y = i };
            }

            for (int i = 1; i < 7; i++)
            {
                stageData[0].wall[index++] = new Point { x = i, y = 0 };
                stageData[0].wall[index++] = new Point { x = i, y = 7 };
            }
        }

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

        public static ref readonly StageSetup GetStageData(int arg) => ref stageData[arg];
    }
}
