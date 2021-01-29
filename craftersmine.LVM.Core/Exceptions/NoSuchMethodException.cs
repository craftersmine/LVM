using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Exceptions
{
    /// <summary>
    /// Thrown when no such property found
    /// </summary>
    [Serializable]
    public class NoSuchMethodException : Exception
    {
        /// <summary>
        /// Gets or sets name of not found method
        /// </summary>
        public string Method { get; set; } = "no method name specified";
#pragma warning disable CS1591
        public NoSuchMethodException() { }
        public NoSuchMethodException(string message) : base(message) { }
        public NoSuchMethodException(string method, string message) : base(message) { Method = method; }
        public NoSuchMethodException(string method, string message, Exception inner) : base(message, inner) { Method = method; }
        public NoSuchMethodException(string message, Exception inner) : base(message, inner) { }
        protected NoSuchMethodException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + " : " + Method;
        }
#pragma warning restore CS1591
    }
}
