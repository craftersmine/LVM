using craftersmine.LVM.GUI.Display.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.GUI.Display
{
    public class DisplayControl : Control
    {
        private Graphics renderer { get; set; }
        private Bitmap buffer { get; set; }
        private RectangleF charBounds;
        private SolidBrush charBgBrush;
        private SolidBrush charFgBrush;

        public event EventHandler DisplayInitialized;

        public Font DisplayFont { get; private set; }
        public Size CharSize { get; private set; }
        public int DisplayWidth { get; private set; } = 80;
        public int DisplayHeight { get; private set; } = 24;

        public Color ClearColor { get; set; } = Color.Black;

        public DisplayControl()
        {
            var fontCollection = ResourceFontLoader.AddFontFromMemory(Resources.UnsciiFont);
            DisplayFont = new Font(fontCollection.Families[0], 16, FontStyle.Regular, GraphicsUnit.Pixel);

            //CharSize = TextRenderer.MeasureText(" ", DisplayFont);
            CharSize = new Size(8, 16);
            charBounds = new Rectangle(0, 0, CharSize.Width, CharSize.Height);
            charBgBrush = new SolidBrush(ClearColor);
            charFgBrush = new SolidBrush(Color.White);

            Initialize();
        }

        public void Initialize()
        {
            Size = new Size(CharSize.Width * DisplayWidth, CharSize.Height * DisplayHeight);

            buffer = new Bitmap(Size.Width, Size.Height);
            renderer = Graphics.FromImage(buffer);
            renderer.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            renderer.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            renderer.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

            Clear();

            Invalidate();

            DisplayInitialized?.Invoke(this, EventArgs.Empty);
        }

        public void Clear()
        {
            for (int y = 0; y < DisplayHeight; y++)
                for (int x = 0; x < DisplayWidth; x++)
                    DrawChar(x, y, ' ', Color.White, ClearColor);
        }

        public void DrawChar(int x, int y, char chr, Color fgColor, Color bgColor)
        {

            if (InvokeRequired)
                Invoke(new Action(() => { drawChar(x, y, chr, fgColor, bgColor); }));
            else { drawChar(x, y, chr, fgColor, bgColor); }
        }

        private void drawChar(int x, int y, char chr, Color fg, Color bg)
        {
            charBounds.X = x * CharSize.Width;
            charBounds.Y = y * CharSize.Height;
            charBgBrush.Color = bg;

            renderer.FillRectangle(charBgBrush, charBounds);
            charFgBrush.Color = fg;
            charBounds.X -= 3;
            renderer.DrawString(chr.ToString(), DisplayFont, charFgBrush, charBounds, new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(buffer, Point.Empty);
        }
    }
}
