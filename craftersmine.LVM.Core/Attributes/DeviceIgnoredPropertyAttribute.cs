using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace craftersmine.LVM.Core.Attributes
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class DeviceIgnoredPropertyAttribute : XmlIgnoreAttribute
    {
        public DeviceIgnoredPropertyAttribute()
        {
        }
    }
}
