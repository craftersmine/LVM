using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Contains vendor specified device parameter data
    /// </summary>
    public struct DeviceVendorParameter
    {
        /// <summary>
        /// Gets or sets parameter key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets parameter value
        /// </summary>
        public string Value { get; set; }
    }
}
