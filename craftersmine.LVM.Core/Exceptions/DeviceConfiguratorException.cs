using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{

    [Serializable]
    public class DeviceConfiguratorException : Exception
    {
        public DeviceConfiguratorException() { }
        public DeviceConfiguratorException(string message) : base(message) { }
        public DeviceConfiguratorException(string message, Exception inner) : base(message, inner) { }
        protected DeviceConfiguratorException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
