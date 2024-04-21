using System.Diagnostics;

namespace LDConsoleCLI.Excutor
{
    internal class ExeExcutor
    {
        public string ProcPath { get; }
        public IExeExcutorOption Option { get; }

        public ExeExcutor(string procPath)
        {
            ProcPath = procPath;
            Option = new ExeExcutorOption();
        }

        public ExeExcutor(string procPath, IExeExcutorOption option)
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
            while(retryCount < Option.RetryCount)
            {
                CancellationTokenSource cts = new(Option.Timeout);
                try
                {
                    process.Start();
                    //await process.WaitForExitAsync(cts.Token);

                    string output = await process.StandardOutput.ReadToEndAsync();

                    return output;
                }
                catch
                {
                    retryCount++;
                }
            }
            throw new Exception("Failed to execute the process.");
        }

        internal class ExeExcutorOption : IExeExcutorOption
        {
            public int Timeout { get; set; } = 5000;
            public int RetryCount { get; set; } = 3;
        }
    }
}
