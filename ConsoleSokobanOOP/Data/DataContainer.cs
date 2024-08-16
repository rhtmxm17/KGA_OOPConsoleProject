using System.Diagnostics;

namespace ConsoleSokobanOOP
{
    public static class DataContainer
    {
        private static Dictionary<string, string> strings = new();
        private static StageSetup[] stageData = new StageSetup[Constant.Stages];
        private static bool[] isDataLoaded = new bool[Constant.Stages];
        public static bool[] isLocked = new bool[Constant.Stages + 1];

        static DataContainer()
        {
            isLocked = [false, true, true, false, false, false];
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
                strings.Add("WarpIn", "IN");
                strings.Add("WarpOut", "OU");

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

        public static ref readonly StageSetup GetStageData(int stage)
        {
            if (isDataLoaded[stage] == false)
            {
                stageData[stage] = new StageSetup();
                stageData[stage].stage = stage;

                using (StreamReader stream = new StreamReader($"../../../Data/stage{stage + 1}.CSV"))
                {
                    List<Point> ballTemp = new();
                    List<Point> wallTemp = new();
                    List<Point> goalTemp = new();
                    List<Point> trapTemp = new();
                    List<(char key, Point pt)> warpInTemp = new();
                    List<(char key, Point pt)> warpOutTemp = new();

                    int line = 0;
                    int cell = 0;
                    for (line = 0; false == stream.EndOfStream; line++)
                    {
                        var symbols = stream.ReadLine()?.Split(',') ?? Array.Empty<string>();
                        for (cell = 0; cell < symbols.Length; cell++)
                        {
                            for (int i = 0; i < symbols[cell].Length; i++)
                            {
                                switch (symbols[cell][i])
                                {
                                    case 'P': // player
                                        stageData[stage].player = (line, cell);
                                        break;
                                    case 'B': // ball
                                        ballTemp.Add((line, cell));
                                        break;
                                    case 'W': // wall
                                        wallTemp.Add((line, cell));
                                        break;
                                    case 'G': // goal
                                        goalTemp.Add((line, cell));
                                        break;
                                    case 'T': // trap
                                        trapTemp.Add((line, cell));
                                        break;
                                    case 'I': // warp 입구, 반드시 I1, Ia 와 같이 동일한 한글자 키값 사용
                                        i++;
                                        warpInTemp.Add((symbols[cell][i], (line, cell)));
                                        break;
                                    case 'O': // warp 출구, 반드시 입구와 짝을 이루고 키값 사용할것
                                        i++;
                                        warpOutTemp.Add((symbols[cell][i], (line, cell)));
                                        break;
                                }
                            }
                        }
                    }

                    stageData[stage].sizeX = line;
                    stageData[stage].sizeY = cell;
                    stageData[stage].balls = ballTemp.ToArray();
                    stageData[stage].wall = wallTemp.ToArray();
                    stageData[stage].goal = goalTemp.ToArray();
                    stageData[stage].trap = trapTemp.ToArray();

                    stageData[stage].warpIn = new Point[warpInTemp.Count];
                    stageData[stage].warpOut = new Point[warpOutTemp.Count];
                    for (int i = 0; i < warpInTemp.Count; i++)
                    {
                        stageData[stage].warpIn[i] = warpInTemp[i].pt;
                        stageData[stage].warpOut[i] = warpOutTemp.Find(pair => pair.key == warpInTemp[i].key).pt;
                    }
                }

                isDataLoaded[stage] = true;
            }

            return ref stageData[stage];
        }
    }
}
