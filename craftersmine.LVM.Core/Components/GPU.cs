using craftersmine.LVM.Core.Attributes;
using craftersmine.LVM.Core.Exceptions;
using craftersmine.LVM.Core.Properties;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core.Components
{
    [DeviceComponent(ComponentType = DeviceTypes.Gpu, DefaultTooltip = "Provides output to screen", UserFriendlyName = "GPU")]
    public sealed class GPU : BaseDevice
    {
        private PrivateFontCollection privateFontCollection;
        private Font font;
        private Bitmap screenBuffer;
        private Graphics renderer;
        private RectangleF charBounds;
        private SolidBrush charBgBrush;
        private SolidBrush charFgBrush;
        private Dictionary<int, ScreenBuffer> buffers = new Dictionary<int, ScreenBuffer>();

        private Color fgColor = Color.White;
        private Color bgColor = Color.Black;

        public Color ClearColor { get; set; }
        public int ViewportWidth { get; set; } = 80;
        public int ViewportHeight { get; set; } = 24;

        public GPU()
        {
            privateFontCollection = ResourceFontLoader.AddFontFromMemory(Resources.Unscii);
            font = new Font(privateFontCollection.Families[0], 16, FontStyle.Regular, GraphicsUnit.Pixel);
            charBounds = new Rectangle(0, 0, 8, 16);
            charBgBrush = new SolidBrush(ClearColor);
            charFgBrush = new SolidBrush(Color.White);

            screenBuffer = new Bitmap(8 * ViewportWidth, 16 * ViewportHeight);
            renderer = Graphics.FromImage(screenBuffer);
            renderer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            renderer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            renderer.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

            DeviceIcon = DeviceDefaultIcons.GPU;
        }

        [LuaCallback(Doc = "set(x: number, y: number, chr: string) -- Sets a character at specified position on screen")]
        public void set(long x, long y, string chr)
        {
            if (chr.Length < 1)
            {
                DrawChar((int)x, (int)y, ' ', fgColor, bgColor);
                return;
            }
            DrawChar((int)x, (int)y, chr[0], fgColor, bgColor);
        }

        [LuaCallback(Doc = "bind(address: string) -- Binds screen with specified address to this GPU")]
        public void bind(string address)
        {
            var screen = Machine.RunningInstance.DeviceBus.GetDevice<Screen>(address);
            if (screen == null)
                throw new LuaMachineException("Unable to bind screen to GPU, no such device", MachineErrorMessages.NoSuchComponent);
            if (screen.GetComponentAttribute().ComponentType != DeviceTypes.Screen)
                throw new LuaMachineException("Unable to bind screen to GPU, device is not a screen", MachineErrorMessages.NotAScreen);

            screen.BindGpu(this);
        }

        private void DrawChar(int x, int y, char chr, Color fg, Color bg)
        {
            charBounds.X = x * 8;
            charBounds.Y = y * 16;
            charBgBrush.Color = bg;

            renderer.FillRectangle(charBgBrush, charBounds);
            charFgBrush.Color = fg;
            charBounds.X -= 3;
            renderer.DrawString(chr.ToString(), font, charFgBrush, charBounds, new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox));
        }

        public Bitmap GetScreenBuffer()
        {
            return screenBuffer;
        }
    }

    public sealed class ScreenBuffer
    {
        private ScreenChar[] chars;

        public int Width { get; private set; }
        public int Height { get; private set; }

        private ScreenBuffer() { }
        public ScreenBuffer(int w, int h)
        {
            Width = w;
            Height = h;

            chars = new ScreenChar[w * h];
        }
    }

    public struct ScreenChar
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color ForegroundColor { get; set; }
        public Color BackgroundColor { get; set; }
    }
}
