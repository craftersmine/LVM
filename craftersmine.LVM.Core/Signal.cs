using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents a machine signal with name and containing data. This class cannot be inherited
    /// </summary>
    public sealed class Signal
    {
        /// <summary>
        /// Gets or sets name of signal
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets signal data
        /// </summary>
        public object[] Data { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
