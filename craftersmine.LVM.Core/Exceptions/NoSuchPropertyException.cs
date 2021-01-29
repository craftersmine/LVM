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
    public class NoSuchPropertyException : Exception
    {
        /// <summary>
        /// Gets or sets name of not found property
        /// </summary>
        public string Property { get; set; } = "no property name specified";
#pragma warning disable CS1591
        public NoSuchPropertyException() { }
        public NoSuchPropertyException(string message) : base(message) { }
        public NoSuchPropertyException(string propertyName, string message) : base(message) { Property = propertyName; }
        public NoSuchPropertyException(string propertyName, string message, Exception inner) : base(message, inner) { Property = propertyName; }
        public NoSuchPropertyException(string message, Exception inner) : base(message, inner) { }
        protected NoSuchPropertyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

        public override string ToString()
        {
            return Message + " : " + Property;
        }
#pragma warning restore CS1591
    }
}
