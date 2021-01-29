using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using craftersmine.LVM.Core.Exceptions;

using NLua;
using NLua.Exceptions;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents LVM Lua processor, allows to execute Lua scripts. This class cannot be inherited
    /// </summary>
    public sealed class LuaProcessor
    {
        private Lua proc;
        private bool abort = false;
        private Thread thread;
        private LuaFunction machineFnc;

        /// <summary>
        /// Gets or sets Lua processor debug mode
        /// </summary>
        public bool IsDebugMode { get; set; }

        /// <summary>
        /// Occurs when processor halts
        /// </summary>
        public event EventHandler<ProcessorHaltEventArgs> ProcessorHalted;

        /// <summary>
        /// Creates a new Lua processor instance
        /// </summary>
        public LuaProcessor()
        {
            IsDebugMode = true;
            proc = new Lua();
            proc.LoadCLRPackage();

            proc.RegisterFunction("erroredFunction", typeof(LuaProcessor).GetMethod("ErroredFunction"));
            proc.RegisterFunction("log", typeof(LuaProcessor).GetMethod("Log"));
            proc.RegisterFunction("debugNetVal", typeof(LuaProcessor).GetMethod("DebugNetValue"));

            proc.SetDebugHook(KeraLua.LuaHookMask.Line, 0);

            proc.DebugHook += Proc_DebugHook;
        }

        private void Proc_DebugHook(object sender, NLua.Event.DebugHookEventArgs e)
        {
            if (abort) // if abort is true
            {
                Lua lState = (Lua)sender;
                // Put error on Lua stack with MachineHalted error message
                lState.State.Error(MachineErrorMessages.MachineHalted);
            }
        }

        /// <summary>
        /// Runs a processor with "Common\machine.lua" script
        /// </summary>
        public void Run()
        {
            string machineCode = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Common\\machine.lua"));

            machineFnc = proc.LoadString(machineCode, "machine");

            thread = new Thread(new ThreadStart(runMachine));

            thread.Start();
        }

        private void runMachine()
        {
            try
            {
                machineFnc.Call("", IsDebugMode, Settings.LoggerInstance);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(MachineErrorMessages.MachineHalted))
                {
                    ProcessorHalted?.Invoke(this, new ProcessorHaltEventArgs() { Reason = ProcessorHaltReason.Halt });
                }
                else ProcessorHalted?.Invoke(this, new ProcessorHaltEventArgs() { Reason = ProcessorHaltReason.Crash, CrashException = ex });
            }
        }

        /// <summary>
        /// Interrupts Lua execution and halts processor
        /// </summary>
        public void Halt()
        {
            abort = true;
        }

        /// <summary>
        /// Creates new <see cref="LuaTable"/>
        /// </summary>
        /// <returns>Empty <see cref="LuaTable"/></returns>
        public LuaTable CreateTable()
        {
            proc.NewTable("tempTable");
            LuaTable table = proc["tempTable"] as LuaTable;
            proc["tempTable"] = null;
            return table;
        }

        /// <summary>
        /// Just error function for debugging. Will be removed in future
        /// </summary>
        public static void ErroredFunction()
        {

            throw new LuaMachineException("", "an error has occured");
        }

        /// <summary>
        /// Logs a specific line in log file
        /// </summary>
        /// <param name="prefix">Log line prefix</param>
        /// <param name="contents">Log line contents</param>
        public static void Log(string prefix, string contents)
        {
            Settings.LoggerInstance.Log(prefix, contents);
        }

        /// <summary>
        /// Used for debugging unmanaged Lua values in IDE. Will be removed in future
        /// </summary>
        /// <param name="obj"></param>
        public static void DebugNetValue(object obj)
        {
            return;
        }
    }

    /// <summary>
    /// Contains processor halt event arguments
    /// </summary>
    public class ProcessorHaltEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets processor halt reason
        /// </summary>
        public ProcessorHaltReason Reason { get; set; }
        /// <summary>
        /// Gets or sets processor crash exception data
        /// </summary>
        public Exception CrashException { get; set; }
    }

    /// <summary>
    /// Defines processor halt reason
    /// </summary>
    public enum ProcessorHaltReason
    {
        /// <summary>
        /// Processor halted without error
        /// </summary>
        Halt,
        /// <summary>
        /// Processor halted with crash
        /// </summary>
        Crash,
        /// <summary>
        /// Processor halted for restart
        /// </summary>
        Restart
    }
}
