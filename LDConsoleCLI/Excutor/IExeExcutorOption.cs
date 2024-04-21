using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDConsoleCLI.Excutor
{
    public interface IExeExcutorOption
    {
        public int Timeout { get; set; }
        public int RetryCount { get; set; }
    }
}
