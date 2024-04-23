using System.Diagnostics;

namespace LDPlayer.Console.Executor
{
    internal class ExeExecutor
    {
        public string ProcPath { get; }
        public IExeExecutorOption Option { get; }

        public ExeExecutor(string procPath)
        {
            ProcPath = procPath;
            Option = new ExeExcutorOption();
        }

        public ExeExecutor(string procPath, IExeExecutorOption option)
        {
            ProcPath = procPath;
            Option = option;
        }

        public async Task<string> Execute(string args)
        {
            int retryCount = 0;

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ProcPath,
                    Arguments = args,
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            while (retryCount < Option.RetryCount)
            {
                CancellationTokenSource cts = new(Option.Timeout);
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

        internal class ExeExcutorOption : IExeExecutorOption
        {
            public int Timeout { get; set; } = 5000;
            public int RetryCount { get; set; } = 3;
        }
    }
}
