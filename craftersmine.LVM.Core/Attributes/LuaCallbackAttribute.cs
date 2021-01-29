using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Attributes
{
    /// <summary>
    /// Specifies a method or property as a valid LuaCallback or LuaField to be used in invoke, methods functions
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class LuaCallbackAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LuaCallbackAttribute"/> with the specified parameters
        /// </summary>
        public LuaCallbackAttribute()
        {
        }

        /// <summary>
        /// Is LuaCallback a direct call method
        /// </summary>
        public bool IsDirect { get; set; } = false;
        /// <summary>
        /// Is LuaField can get a value
        /// </summary>
        public bool IsGetter { get; set; } = false;
        /// <summary>
        /// Is LuaField can set a value
        /// </summary>
        public bool IsSetter { get; set; } = false;
        /// <summary>
        /// Gets or sets LuaCallback short documentation of usage
        /// </summary>
        public string Doc { get; set; } = "";
    }
}
