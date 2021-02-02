using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class DeviceConfiguratorAttribute : Attribute
    {
        public DeviceConfiguratorAttribute()
        {
        }
    }
}
