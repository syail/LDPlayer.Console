using LDPlayer.Console.Dto;
using LDPlayer.Console.Executor;

namespace LDPlayer.Console
{
    public class LDConsole
    {
        private readonly ExeExecutor _excutor;

        public LDConsole(string consolePath)
        {
            _excutor = new(consolePath);
        }

        public LDConsole(string consolePath, IExeExecutorOption option)
        {
            _excutor = new(consolePath, option);
        }

        public async Task<List<LDPlayerInfo>> GetList()
        {
            var resultString = await _excutor.Execute("list2");
            var lines = resultString.Split("\n");

            var result = new List<LDPlayerInfo>();

            foreach (var line in lines)
            {
                var parts = line.Split(",");

                if (parts.Length < 10)
                {
                    continue;
                }

                var player = new LDPlayerInfo
                {
                    Index = int.Parse(parts[0]),
                    Name = parts[1],
                    BaseProcessHandle = int.Parse(parts[2]),
                    RendererProcessHandle = int.Parse(parts[3]),
                    State = (LDPlayerInfo.LDPlayerState)int.Parse(parts[4]),
                    BoxerPID = int.Parse(parts[5]),
                    VirtualBoxPID = int.Parse(parts[6]),
                    ScreenWidth = int.Parse(parts[7]),
                    ScreenHeight = int.Parse(parts[8]),
                    DPI = int.Parse(parts[9])
                };

                result.Add(player);
            }
            return result;
        }

        public async Task<string> RunAdbCommand(int id, string command)
        {
            string result = await _excutor.Execute($"adb --index {id} --command \"{command}\"");

            return result.Trim();
        }
    }
}
