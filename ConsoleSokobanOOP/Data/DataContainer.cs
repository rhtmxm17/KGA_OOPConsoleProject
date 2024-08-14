using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public static class DataContainer
    {
        private static Dictionary<string, string> strings = new();
        private static StageSetup[] stageData = new StageSetup[Constant.Stages];

        static DataContainer()
        {
            for (int stage = 0; stage < Constant.Stages; stage++)
            {
                using (StreamReader stream = new StreamReader($"../../../Data/stage{stage + 1}.CSV"))
                {
                    List<Point> ballTemp = new();
                    List<Point> wallTemp = new();
                    List<Point> goalTemp = new();

                    int line = 0;
                    int cell = 0;
                    for (line = 0; false == stream.EndOfStream; line++)
                    {
                        var symbols = stream.ReadLine()?.Split(',') ?? Array.Empty<string>();
                        for (cell = 0; cell < symbols.Length; cell++)
                        {
                            switch (symbols[cell])
                            {
                                case "W":
                                    wallTemp.Add(new Point { x = line, y = cell });
                                    break;
                                case "G":
                                    goalTemp.Add(new Point { x = line, y = cell });
                                    break;
                                case "B":
                                    ballTemp.Add(new Point { x = line, y = cell });
                                    break;
                                case "GB":
                                    goalTemp.Add(new Point { x = line, y = cell });
                                    ballTemp.Add(new Point { x = line, y = cell });
                                    break;
                                case "P":
                                    stageData[stage].player = new Point { x = line, y = cell };
                                    break;
                            }
                        }
                    }

                    stageData[stage].sizeX = line;
                    stageData[stage].sizeY = cell;
                    stageData[stage].balls = ballTemp.ToArray();
                    stageData[stage].wall = wallTemp.ToArray();
                    stageData[stage].goal = goalTemp.ToArray();
                }
            }

            return;
        }

        public static void Setup(RenderMode mode)
        {
            if (mode == RenderMode.Default)
            {
                strings.Add("Wall", "▦");
                strings.Add("Ball", "●");
                strings.Add("Player", "ⓟ");
                strings.Add("Goal", "○");
                strings.Add("WarpIn", "●");
                strings.Add("WarpOut", "●");

            }
            else if (mode == RenderMode.Safe)
            {
                strings.Add("Wall", "##");
                strings.Add("Ball", "ㅇ");
                strings.Add("Player", "ⓟ");
                strings.Add("Goal", "ㅁ");
                strings.Add("WarpIn", "＠");
                strings.Add("WarpOut", "＠");

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
