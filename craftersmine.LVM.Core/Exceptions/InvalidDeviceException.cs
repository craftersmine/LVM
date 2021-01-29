using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{
    /// <summary>
    /// Thrown when specified <see cref="IDevice"/> object doesn't have <see cref="craftersmine.LVM.Core.Attributes.DeviceComponentAttribute"/>
    /// </summary>
    [Serializable]
    public class InvalidDeviceException : Exception
    {
#pragma warning disable CS1591
        public InvalidDeviceException() { }
        public InvalidDeviceException(string message) : base(message) { }
        public InvalidDeviceException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDeviceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
#pragma warning restore CS1591
    }
}
