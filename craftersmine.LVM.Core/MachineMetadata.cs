using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    [Serializable]
    public struct MachineMetadata
    {
        public Guid MachineAddress { get; set; }
        public string MachineName { get; set; }
        public Version MachineVersion { get; set; }
    }
}
