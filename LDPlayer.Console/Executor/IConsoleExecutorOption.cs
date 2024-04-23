namespace LDPlayer.Console.Executor
{
    public interface IConsoleExecutorOption
    {
        public int GetTimeout();
        public int GetRetryCount();
    }
}
