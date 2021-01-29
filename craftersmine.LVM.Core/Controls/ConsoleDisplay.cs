using RazorGDI;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.LVM.Core.Controls
{
    public sealed class ConsoleDisplay : RazorPainterControl
    {
        private Size charSize;
        private SolidBrush foregroundColorBrush;
        private SolidBrush backgroundColorBrush;
        private RectangleF charRect;

        public new Font Font { get; private set; }
        public int DisplayWidth { get; private set; }
        public int DisplayHeight { get; private set; }

        public ConsoleDisplay()
        {
            RazorPaint();
            Initialize();
        }

        public void Initialize()
        {
            SetDisplaySize(80, 25);
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            foregroundColorBrush = new SolidBrush(Color.White);
            backgroundColorBrush = new SolidBrush(Color.Black);

            RazorGFX.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            RazorGFX.Clear(backgroundColorBrush.Color);

            RazorPaint();
        }

        public void SetFont(Font font)
        {
            Font = font;
            SetDisplaySize(DisplayWidth, DisplayHeight);
        }

        public void SetDisplaySize(int width, int height)
        {
            DisplayWidth = width;
            DisplayHeight = height;

            charSize = new Size(8, 16);

            Size = new Size(charSize.Width * DisplayWidth, charSize.Height * DisplayHeight);

            charRect = new RectangleF(0, 0, charSize.Width, charSize.Height);
        }

        public void SetColors(Color foreground, Color background)
        {
            SetBackgroundColor(background);
            SetForegroundColor(foreground);
        }

        public void SetBackgroundColor(Color color)
        {
            backgroundColorBrush.Color = color;
        }

        public void SetForegroundColor(Color color)
        {
            foregroundColorBrush.Color = color;
        }

        public void DrawChar(char character, int x, int y)
        {
            lock (RazorLock)
            {
                charRect.X = x * charSize.Width;
                charRect.Y = y * charSize.Height;

                RazorGFX.FillRectangle(backgroundColorBrush, charRect);
                RazorGFX.DrawRectangle(Pens.Red, charRect.X, charRect.Y, charRect.Width, charRect.Height);

                //charRect.X -= 3;
                RazorGFX.DrawString(character.ToString(), Font, foregroundColorBrush, charRect, StringFormat.GenericTypographic);
            }
            RazorPaint();
        }
    }
}
