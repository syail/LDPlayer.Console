using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDPlayer.Console.Executor
{
    public interface IExeExecutorOption
    {
        public int Timeout { get; set; }
        public int RetryCount { get; set; }
    }
}
