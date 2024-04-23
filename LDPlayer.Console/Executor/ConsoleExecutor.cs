using System.Diagnostics;

namespace LDPlayer.Console.Executor
{
    internal class ConsoleExecutor
    {
        public string ConsolePath { get; }
        public IConsoleExecutorOption Option { get; }

        public ConsoleExecutor(string procPath)
        {
            ConsolePath = procPath;
            Option = new ExeExcutorOption();
        }

        public ConsoleExecutor(string procPath, IConsoleExecutorOption option)
        {
            ConsolePath = procPath;
            Option = option;
        }

        public async Task<string> Execute(string args)
        {
            int retryCount = 0;

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ConsolePath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            while (retryCount < Option.GetRetryCount())
            {
                CancellationTokenSource cts = new(Option.GetTimeout());
                try
                {
                    process.Start();
                    //await process.WaitForExitAsync(cts.Token);

                    string output = await process.StandardOutput.ReadToEndAsync(cancellationToken: cts.Token);

                    return output;
                }
                catch
                {
                    retryCount++;
                }
            }
            throw new Exception("Failed to execute the process.");
        }

        public class ExeExcutorOption : IConsoleExecutorOption
        {
            private static readonly int DefaultTimeout = 10000;
            private static readonly int DefaultRetryCount = 3;

            private readonly int _timeout;
            private readonly int _retryCount;

            public ExeExcutorOption()
            {
                _timeout = DefaultTimeout;
                _retryCount = DefaultRetryCount;
            }

            public ExeExcutorOption(int timeout, int retryCount)
            {
                _timeout = timeout;
                _retryCount = retryCount;
            }

            public int GetTimeout()
            {
                return _timeout;
            }

            public int GetRetryCount()
            {
                return _retryCount;
            }
        }
    }
}
