using craftersmine.LVM.Core.Extensions;

using MoonSharp.Interpreter;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.LuaCore
{
    public sealed class LuaExecutionController
    {
        private readonly Script state;
        private readonly Random rnd;
        private DynValue codeCoroutine;

        public bool IsAbortRequested { get; private set; }

        public LuaExecutionController()
        {
            rnd = new Random();
            state = new Script(CoreModules.Basic);
        }

        //private void DebugHook(object sender, NLua.Event.DebugHookEventArgs e)
        //{
            //if (IsAbortRequested)
            //{
            //    Logger.Instance.Log(LogEntryType.Info, "Aborting Lua execution...");
            //    state.Error(Errors.MachineHalted);
            //}
        //}

        public void RequestAbort()
        {
            IsAbortRequested = true;
            Logger.Instance.Log(LogEntryType.Info, "Requested Lua execution abort...");
        }

        public void ExecuteString(string code, string chunkName = "cpu")
        {
            try
            {
                var fnc = state.LoadFunction(code, null, chunkName);
                codeCoroutine = state.CreateCoroutine(fnc);
                codeCoroutine.
                var data = state.DoString(code, null, chunkName);
            }
            catch
            {
                //if (ex.Message.Contains(Errors.MachineHalted))
                //    Logger.Instance.Log(LogEntryType.Info, "Machine halted signal received");
            }
        }

        //public LuaTable CreateTable()
        //{
        //    string tmpTableName = "tempTable_" + rnd.Next(int.MinValue, int.MaxValue).ToString("X");
        //    state.NewTable(tmpTableName);
        //    LuaTable table = state[tmpTableName] as LuaTable;
        //    state[tmpTableName] = null;
        //    return table;
        //}
    }
}
