using craftersmine.LVM.Core.LuaCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.VM
{
    public sealed class VM
    {
        private Thread executionThread;

        public LuaExecutionController LuaExecutionController { get; private set; }
        public static VM Instance { get; private set; }

        public VM()
        {
            LuaExecutionController = new LuaExecutionController();


            Instance = this;
        }

        public void Run()
        {
            executionThread = new Thread(new ThreadStart(ExecutionThreadMethod));
            executionThread.Start();
        }

        private void ExecutionThreadMethod()
        {
            LuaExecutionController = new LuaExecutionController();
            string code = "while true do print('test') end";
            LuaExecutionController.ExecuteString(code);
        }

        public void Stop()
        {
            LuaExecutionController.RequestAbort();
        }
    }
}
