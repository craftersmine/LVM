using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Contains info about Lua callback method or property
    /// </summary>
    public struct LuaCallbackInfo
    {
        /// <summary>
        /// Gets or sets name of Lua callback method or property
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets documentation for Lua callback method or property
        /// </summary>
        public string Doc { get; set; }
        /// <summary>
        /// Gets or sets is Lua callback method or property is getter
        /// </summary>
        public bool IsGetter { get; set; }
        /// <summary>
        /// Gets or sets is Lua callback method or property is setter
        /// </summary>
        public bool IsSetter { get; set; }
        /// <summary>
        /// Gets or sets is Lua callback method or property is method
        /// </summary>
        public bool IsDirect { get; set; }
    }
}
