using LDPlayer.Console.Dto;
using LDPlayer.Console.Executor;

namespace LDPlayer.Console
{
    public class LDConsole
    {
        private readonly ConsoleExecutor _executor;

        public LDConsole(string consolePath)
        {
            _executor = new(consolePath);
        }

        public LDConsole(string consolePath, IConsoleExecutorOption option)
        {
            _executor = new(consolePath, option);
        }

        public async Task<List<LDPlayerInfo>> GetList()
        {
            var resultString = await _executor.Execute("list2");
            var lines = resultString.Split("\n");

            var result = new List<LDPlayerInfo>();

            foreach (var line in lines)
            {
                var info = ParseInfoString(line);

                if (info == null)
                {
                    continue;
                }
                result.Add(info);
            }
            return result;
        }

        private static LDPlayerInfo? ParseInfoString(string infoString)
        {
            var parts = infoString.Split(",");

            if (parts.Length < 10)
            {
                return null;
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

            return player;
        }

        public async Task<string> ExecuteAdbCommand(int id, string command)
        {
            string result = await _executor.Execute($"adb --index {id} --command \"{command}\"");

            return result.Trim();
        }
    }
}
