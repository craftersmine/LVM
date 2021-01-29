using craftersmine.LVM.Core.Attributes;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Components
{
    [DeviceComponent(ComponentType = DeviceTypes.Screen, DefaultTooltip = "Screen is not binded to any GPU", UserFriendlyName = "Screen")]
    public sealed class Screen : BaseDevice
    {
        Bitmap placeholderBitmap;
        private GPU bindedGpu { get; set; }

        [DeviceIgnoredProperty]
        public Bitmap GpuBuffer { get { if (bindedGpu == null) return placeholderBitmap; else return bindedGpu.GetScreenBuffer(); } }

        public Screen()
        {
            DeviceIcon = DeviceDefaultIcons.Screen;
            placeholderBitmap = new Bitmap(8 * 80, 16 * 24);
        }

        public void BindGpu(GPU gpu)
        {
            bindedGpu = gpu;
            DeviceStatusIconManager.SetStatusIconTooltipForDevice(getAddress(), DeviceTypes.Screen, "Screen is bound to GPU: " + gpu.getAddress());
        }
    }
}
