using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.Core
{
    public interface IDeviceConfigurator
    {
        public bool AutoWidth { get; set; }
        public bool AutoHeight { get; set; }

        public void ApplyConfig(IDevice device);

        public void LoadConfig(IDevice device);
    }
}
