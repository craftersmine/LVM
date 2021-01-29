using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Represents interface between backend of machine and graphical part of LVM. This class cannot be inherited
    /// </summary>
    [Obsolete("Must be removed in future")]
    public sealed class DisplayInterface
    {
        private DrawCharEventArgs drawCharEventArgs = new DrawCharEventArgs();

        /// <summary>
        /// Occurs when character being drawn on display
        /// </summary>
        public event EventHandler<DrawCharEventArgs> OnDrawChar;
        public event EventHandler<DrawBufferEventArgs> OnRedraw;

        /// <summary>
        /// Calls DrawChar method for display to draw char with specified parameters
        /// </summary>
        /// <param name="x">X position on display</param>
        /// <param name="y">Y position on dislay</param>
        /// <param name="character">Character to draw</param>
        /// <param name="fgColor">Foreground color</param>
        /// <param name="bgColor">Background color</param>
        public void CallDrawChar(int x, int y, char character, Color fgColor, Color bgColor)
        {
            drawCharEventArgs.X = x;
            drawCharEventArgs.Y = y;
            drawCharEventArgs.Character = character;
            drawCharEventArgs.ForegroundColor = fgColor;
            drawCharEventArgs.BackgroundColor = bgColor;
            OnDrawChar?.Invoke(null, drawCharEventArgs);
        }

        public void CallRedraw(Bitmap bitmap)
        {
            OnRedraw?.Invoke(null, new DrawBufferEventArgs() { Bitmap = bitmap });
        }
    }

    /// <summary>
    /// Contains OnDrawChar event arguments
    /// </summary>
    public sealed class DrawCharEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets character that being drawn
        /// </summary>
        public char Character { get; set; }
        /// <summary>
        /// Gets or sets X position of character on display
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets Y position of character on display
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Gets or sets foreground character color
        /// </summary>
        public Color ForegroundColor { get; set; }
        /// <summary>
        /// Gets or sets background characted color
        /// </summary>
        public Color BackgroundColor { get; set; }
    }

    public sealed class DrawBufferEventArgs:EventArgs
    {
        public Bitmap Bitmap { get; set; }
    }
}
